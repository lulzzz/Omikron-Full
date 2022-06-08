using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Data.Model;
using Omikron.SharedKernel.Security;
using Omikron.SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Omikron.IdentityService.UnitTest
{
    [CollectionDefinition(name: "Database collection")]
    public class UsersTests : IClassFixture<FixtureTest>
    {
        private readonly FixtureTest _fixture;

        public UsersTests(FixtureTest fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("fox")]
        [InlineData("john")]
        [InlineData("Selena")]
        [InlineData("tesla")]
        [InlineData("TeSla")]
        public async Task GetUsersQuery_Should_Return_Users_By_Filter_Criteria(string keyword)
        {
            // Arrange
            const int page = 1;
            var query = new GetUserAccounts.Query(page: page, keyword: keyword);

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: query);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            result.Records.All(predicate: r =>
                    r.FirstName.StartsWith(value: query.Keyword)
                    || r.LastName.StartsWith(value: query.Keyword)
                    || r.Username.StartsWith(value: query.Keyword))
                .Should().BeTrue();
        }

        [Theory]
        [InlineData(data: null)]
        public async Task GetUsersQuery_Should_Return_Users_If_Keyword_Is_Null_Or_White_Space(string keyword)
        {
            // Arrange
            const int page = 1;
            var query = new GetUserAccounts.Query(page: page, keyword: keyword);

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: query);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            result.Records.Any().Should().BeTrue();
        }

        [Fact]
        public async Task UpdateUserBasicInformation_Should_Update_User_Basic_Information()
        {
            // Arrange
            var command = new UpdateUserAccountBasicInformationByUsername.Command
            {
                Username = "marie@mail.com",
                LastName = "X-Doe",
                FirstName = "XPace",
                PhoneNumber = "387 61 764 891"
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);
            var expectedUser = await _fixture.UserManager.FindByNameAsync(userName: command.Username);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            expectedUser.Should().NotBeNull();
            expectedUser.FirstName.Should().Be(expected: command.FirstName);
            expectedUser.LastName.Should().Be(expected: command.LastName);
            expectedUser.PhoneNumber.Should().Be(expected: command.PhoneNumber);
        }

        [Fact]
        public async Task DeleteUser_Should_Delete_User_Account()
        {
            // Arrange
            var id = new Guid(g: "43FE198B-1435-483B-80E8-3B0A2BBDAEDF");
            var command = new DeleteUserAccount.Command(id: id, 999999, true);

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);
            var user = await _fixture.UserManager.Users.SingleOrDefaultAsync(predicate: x => x.ExternalId == command.Id);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            user.Should().BeNull();
        }

        [Fact]
        public async Task DeleteUser_Should_Return_NotFound_If_User_Does_Not_Exist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteUserAccount.Command(id: id, 999999, true);

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangeUserPasswordRequest_Should_Create_Reset_Password_Token()
        {
            // Arrange
            var command = new ChangeUserAccountPasswordRequest.Command
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Email = "selena@mail.com"
            };

            // Act
            var actualUser = await _fixture.UserManager.Users.Include(navigationPropertyPath: x => x.ConfirmationTokens)
                .FirstOrDefaultAsync(predicate: x => x.Email == command.Email);

            var result = await _fixture.Dispatcher.DispatchAsync(command: command);
            var expectedUser = await _fixture.UserManager.Users.Include(navigationPropertyPath: x => x.ConfirmationTokens)
                .FirstOrDefaultAsync(predicate: x => x.Email == command.Email);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            expectedUser.ConfirmationTokens.Any(predicate: x => x.Type == ConfirmationTokenType.ResetPassword).Should().BeTrue();
        }

        [Fact]
        public async Task ChangeUserPasswordRequest_Should_Create_Reset_Password_Token_If_Confirmation_Email_Is_Bypassed()
        {
            // Arrange
            var command = new ChangeUserAccountPasswordRequest.Command
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Email = "kenny@mail.com",
                BypassEmailConfirmation = true
            };

            // Act
            var actualUser = await _fixture.UserManager.Users.Include(navigationPropertyPath: x => x.ConfirmationTokens)
                .FirstOrDefaultAsync(predicate: x => x.Email == command.Email);

            actualUser.ConfirmationTokens.Any(predicate: x => x.Type == ConfirmationTokenType.ResetPassword).Should().BeFalse();

            var result = await _fixture.Dispatcher.DispatchAsync(command: command);
            var expectedUser = await _fixture.UserManager.Users.Include(navigationPropertyPath: x => x.ConfirmationTokens)
                .FirstOrDefaultAsync(predicate: x => x.Email == command.Email);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            expectedUser.ConfirmationTokens.Any(predicate: x => x.Type == ConfirmationTokenType.ResetPassword).Should().BeTrue();
        }

        [Fact]
        public async Task ChangeUserPasswordRequest_Should_Return_Bad_Request_If_User_Does_Not_Confirmed_Email()
        {
            // Arrange
            var command = new ChangeUserAccountPasswordRequest.Command
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Email = "kenny@mail.com"
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task VerifyUserResetPasswordToken_Should_Return_Success_OK_If_Token_Valid()
        {
            // Arrange
            var command = new VerifyUserAccountResetPasswordToken.Query
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Token = "A4625DF3-8CBF-4D3C-9016-DA6947C20A6B"
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
        }

        [Fact]
        public async Task VerifyUserResetPasswordToken_Should_Return_Bad_Request_If_Token_Is_Not_Valid()
        {
            // Arrange
            var command = new VerifyUserAccountResetPasswordToken.Query
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Token = "A4625DF3-8CBF-4D3C-9016"
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.BadRequest);
            result.Errors.Any().Should().BeTrue();
        }

        [Fact]
        public async Task ChangeUserPasswordByToken_Should_Reset_Password_By_Token_If_Token_Valid()
        {
            // Arrange
            var changeUserPasswordRequestCommand = new ChangeUserAccountPasswordRequest.Command
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Email = "selena@mail.com"
            };

            var changeUserPasswordByTokenCommand = new ChangeUserAccountPasswordByToken.Command
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Password = "!A463Plavi123"
            };

            // Act
            await _fixture.Dispatcher.DispatchAsync(command: changeUserPasswordRequestCommand);
            var user = await _fixture.UserManager.Users.Include(navigationPropertyPath: x => x.ConfirmationTokens)
                .FirstOrDefaultAsync(predicate: x => x.Email == changeUserPasswordRequestCommand.Email);

            changeUserPasswordByTokenCommand.Token = user.ConfirmationTokens
                .Where(predicate: x => x.Type == ConfirmationTokenType.ResetPassword).Select(selector: x => x.Value).FirstOrDefault();

            var result = await _fixture.Dispatcher.DispatchAsync(command: changeUserPasswordByTokenCommand);

            user = await _fixture.UserManager.Users.Include(navigationPropertyPath: x => x.ConfirmationTokens)
                .FirstOrDefaultAsync(predicate: x => x.Email == changeUserPasswordRequestCommand.Email);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            user.PasswordHash.Should().NotBeNullOrWhiteSpace();
            _fixture.UserManager.PasswordHasher.VerifyHashedPassword(user: user, hashedPassword: user.PasswordHash,
                providedPassword: changeUserPasswordByTokenCommand.Password).Should().Be(expected: PasswordVerificationResult.Success);
        }

        [Fact]
        public async Task ChangeUserPasswordByToken_Should_Return_Bad_Request_If_Token_Is_Not_Valid()
        {
            // Arrange
            var changeUserPasswordByTokenCommand = new ChangeUserAccountPasswordByToken.Command
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Password = "!A463Plavi123",
                Token = "A67179E3-DC1B-484E-8D2E-AB2AD1F52687"
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: changeUserPasswordByTokenCommand);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task VerifyUserConfirmationEmailToken_Should_Return_Success_OK_If_Token_Valid()
        {
            // Arrange
            var query = new VerifyUserAccountConfirmationEmailToken.Query
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Token = "0AFFBC9C-8363-42C8-91C2-9D93DA06FA7B"
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: query);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
        }

        [Fact]
        public async Task VerifyUserConfirmationEmailToken_Should_Return_Bad_Request_If_Token_Is_Not_Valid()
        {
            // Arrange
            var query = new VerifyUserAccountResetPasswordToken.Query
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Token = "A4625DF3-8CBF-4D3C-9016"
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: query);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.BadRequest);
            result.Errors.Any().Should().BeTrue();
        }

        [Fact]
        public async Task ConfirmUserEmailByToken_Should_Return_Bad_Request_If_Token_Is_Not_Valid()
        {
            // Arrange
            var command = new ConfirmUserAccountEmailByToken.Command
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Token = "A4625DF3-8CBF-4D3C-9016"
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ConfirmUserEmailByToken_Should_Return_OK_If_Token_Is_Valid_And_User_Should_Be_Confirmed()
        {
            // Arrange
            const string email = "felix@mail.com";
            var user = await _fixture.UserManager.FindByEmailAsync(email: email);
            var token = await _fixture.UserManager.GenerateEmailConfirmationTokenAsync(user: user);

            user.ConfirmationTokens.Add(item: new ConfirmationToken(type: ConfirmationTokenType.ConfirmationEmail, value: token));
            await _fixture.UserManager.UpdateAsync(user: user);

            var command = new ConfirmUserAccountEmailByToken.Command
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Token = token
            };

            // Act
            var actualEmailConfirmed = await _fixture.UserManager.Users.AsNoTracking()
                .Where(predicate: x => x.Email == email)
                .Select(selector: x => x.EmailConfirmed)
                .FirstOrDefaultAsync();

            var result = await _fixture.Dispatcher.DispatchAsync(command: command);

            var expectedEmailAndStatus = await _fixture.UserManager.Users.AsNoTracking()
                .Where(predicate: x => x.Email == email)
                .Select(selector: x => new
                {
                    x.EmailConfirmed,
                    x.AccountStatus
                })
                .FirstOrDefaultAsync();

            var numberOfTokens = await _fixture.UserManager.Users.Include(navigationPropertyPath: x => x.ConfirmationTokens)
                .AsNoTracking()
                .Where(predicate: x => x.Email == email)
                .Select(selector: x => x.ConfirmationTokens.Count(t => t.Type == ConfirmationTokenType.ConfirmationEmail))
                .FirstOrDefaultAsync();

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            actualEmailConfirmed.Should().BeFalse();
            expectedEmailAndStatus.EmailConfirmed.Should().BeTrue();
            expectedEmailAndStatus.AccountStatus.Should().Be(expected: AccountStatus.OnBoarding);
        }

        [Fact]
        public async Task UpdateUserBasicInformationByUserId_Should_Update_User_Basic_Information_By_User_Id()
        {
            // Arrange
            var command = new UpdateUserAccountBasicInformationByUserAccountId.Command
            {
                FirstName = "Fox",
                LastName = "Conor",
                Id = new Guid(g: "8F89A16F-CDC3-4675-A2C2-DF4AF0DAA329"),
                PhoneNumber = "387 61 764 891"
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);
            var expectedUser = await _fixture.UserManager.Users.SingleOrDefaultAsync(predicate: u => u.ExternalId == command.Id);

            // Assert
            result.Should().NotBeNull();
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            expectedUser.Should().NotBeNull();
            expectedUser.FirstName.Should().Be(expected: command.FirstName);
            expectedUser.LastName.Should().Be(expected: command.LastName);
            expectedUser.PhoneNumber.Should().Be(expected: command.PhoneNumber);
        }

        [Theory]
        [InlineData(AccountStatus.Active)]
        [InlineData(AccountStatus.Disabled)]
        [InlineData(AccountStatus.New)]
        [InlineData(AccountStatus.OnBoarding)]
        public async Task UpdateUserAccountStatus_Should_Should_Update_User_Account_Status(AccountStatus accountStatus)
        {
            // Arrange
            var id = new Guid(g: "D390F43B-2F70-422B-BC91-09BE7726FB91");
            var command = new UpdateUserAccountStatus.Command(userId: id, status: accountStatus);

            // Act
            var response = await _fixture.Dispatcher.DispatchAsync(command: command);
            var expectedAccountStatus = await _fixture.UserManager.Users.Where(predicate: u => u.ExternalId == id)
                .Select(selector: u => u.AccountStatus).FirstOrDefaultAsync();

            // Assert
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            expectedAccountStatus.Should().Be(expected: accountStatus);
        }

        [Fact]
        public async Task CreateEmailConfirmationToken_Should_Create_Token_For_Email_Confirmation()
        {
            // Arrange
            var id = new Guid(g: "D390F43B-2F70-422B-BC91-09BE7726FB91");
            var command = new CreateEmailConfirmationToken.Command
            {
                TenantIdentifier = Tenant.SystemTenant.Identifier,
                Id = id
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);
            var expectedUser = await _fixture.UserManager.Users.Include(navigationPropertyPath: x => x.ConfirmationTokens)
                .FirstOrDefaultAsync(predicate: x => x.ExternalId == command.Id);

            // Assert
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            expectedUser.ConfirmationTokens.Count(predicate: x => x.Type == ConfirmationTokenType.ConfirmationEmail).Should()
                .Be(expected: 1);
        }

        [Theory]
        [InlineData("+440000000", "not.existing@mail.com")]
        public async Task SendVerificationCode_Should_Add_New_Phone_Number_To_Database_If_It_Does_Not_Exist(string number, string email)
        {
            //Arrange
            var command = new SendVerificationCode.Command(number, email);

            //Act
            var beforeRequest = await _fixture.PhoneNumberRepository.GetPhoneNumberByNumberAsync(command.PhoneNumber);
            var response = await _fixture.Dispatcher.DispatchAsync(command);
            var afterRequestPhoneNumber = await _fixture.PhoneNumberRepository.GetPhoneNumberByNumberAsync(command.PhoneNumber);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            beforeRequest.Should().BeNull();
            afterRequestPhoneNumber.Should().NotBeNull();
            afterRequestPhoneNumber.Number.Should().Be(command.PhoneNumber);
            afterRequestPhoneNumber.VerificationAttempts.Should().Be(0);
        }

        [Theory]
        [InlineData("+440000001", 100000, "not.existing@mail.com")]
        public async Task SendVerificationCode_Should_Generate_New_Token(string number, int token, string email)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new SendVerificationCode.Command(phoneNumber.Number, email);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);
            var afterRequest = await _fixture.PhoneNumberRepository.GetPhoneNumberByNumberAsync(command.PhoneNumber);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            afterRequest.Token.Should().NotBe(token);
        }

        [Theory]
        [InlineData("+440000002")]
        public async Task SendVerificationCode_Should_Make_User_Wait_15_Seconds_Before_Generating_New_Token(string number)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = "+440000002",
            };
            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);

            var user = new User
            {
                FirstName = "New",
                LastName = "User",
                PhoneNumber = "+440000002",
                UserName = "new.user@mail.com",
                Email = "new.user@mail.com",
                ExternalId = new Guid(g: "56AE198B-1435-483B-80E8-3B0A3CCDAEDF"),
                PhoneNumberId = phoneNumber.Id
            };
            await _fixture.UserManager.CreateAsync(user);
            var command = new SendVerificationCode.Command(number, user.UserName);

            //Act
            var firstResponse = await _fixture.Dispatcher.DispatchAsync(command);
            var secondResponse = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            firstResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            secondResponse.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+441000012", 888888, "Wolverine", "logan.wolf@avenger.com", "Avengers.123!")]
        public async Task GenerateTokenForNewNumber_Should_Make_User_Wait_15_Seconds_Before_Generating_New_Token(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);
            var user = await _fixture.UserManager.FindByEmailAsync(email);

            var command = new GenerateTokenForNewNumber.Command(number, user.ExternalId);

            //Act
            var firstResponse = await _fixture.Dispatcher.DispatchAsync(command);
            var secondResponse = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            firstResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            secondResponse.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+442000012", 888888, "Daredevil", "matt.murdock@avenger.com", "Avengers.123!")]
        public async Task GenerateTokenForNewNumber_Should_Return_BadRequest_If_Phone_Number_Has_Been_Locked_Out(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);


            phoneNumber.LockedOut = true;
            phoneNumber.LockoutTime = Clock.GetTime();

            _fixture.PhoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var user = await _fixture.UserManager.FindByEmailAsync(email);

            var command = new GenerateTokenForNewNumber.Command(number, user.ExternalId);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+447000012", 888888, "Phoenix", "jean.grey@avenger.com", "Avengers.123!")]
        public async Task GenerateTokenForNewNumber_Should_Generate_Token(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            var user = await _fixture.UserManager.FindByEmailAsync(email);

            var command = new GenerateTokenForNewNumber.Command(number, user.ExternalId);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task EditProfileDetails_Should_Return_NotFound_If_User_Does_Not_Exist()
        {
            //Arrange
            var command = new EditProfileDetails.Command
            {
                UserId = Guid.NewGuid()
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("+443000012", 888888, "Magneto", "eric.lensherr@avenger.com", "Avengers.123!")]
        public async Task EditProfileDetails_Should_Return_Bad_Request_If_Verification_Fails(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            var user = await _fixture.UserManager.FindByEmailAsync(email);

            phoneNumber.Verified = false;

            _fixture.PhoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new EditProfileDetails.Command
            {
                UserId = user.ExternalId,
                VerificationToken = 999999
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+444000012", 888888, "Falcon", "sam.wilson@avenger.com", "Avengers.123!", "+445000012", "Captain America", "sam.wils@avenger.com")]
        public async Task EditProfileDetails_Should_EditProfileDetails(string number, int token, string nickname, string email, string password, string newNumber, string newNickname, string newEmail)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            var user = await _fixture.UserManager.FindByEmailAsync(email);

            phoneNumber.Verified = false;
            phoneNumber.TokenExpired = false;

            _fixture.PhoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new EditProfileDetails.Command
            {
                UserId = user.ExternalId,
                VerificationToken = token,
                Email = newEmail,
                Nickname = newNickname,
                PhoneNumber = newNumber
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            var updatedUser = await _fixture.UserManager.FindByEmailAsync(newEmail);
            var updatedPhoneNumber = await _fixture.PhoneNumberRepository.GetPhoneNumberByUserIdAsync(user.ExternalId);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            updatedUser.Nickname.Should().Be(newNickname);
            updatedUser.Email.Should().Be(newEmail);
            updatedUser.UserName.Should().Be(newEmail);
            updatedPhoneNumber.Number.Should().Be(newNumber);
        }

        [Theory]
        [InlineData("+440000003", "marie@mail.com")]
        public async Task SendVerificationCode_Should_Return_BadRequest_If_Phone_Number_Is_LockedOut(string number, string email)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                LockedOut = true,
                LockoutTime = Clock.GetTime()
            };
            var user = _fixture.UserManager.Users.FirstOrDefault(x => x.Email == email);
            user.PhoneNumberId = phoneNumber.Id;

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new SendVerificationCode.Command(phoneNumber.Number, email);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000004", 888888, "Black Widow", "natasha.romanoff@avenger.com", "Avengers.123!")]
        public async Task CreateUser_Should_Return_NotFound_If_Phone_Number_Does_Not_Exist(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var command = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("+440000005", 888888)]
        public async Task VerifyPhoneNumber_Should_Return_BadRequest_If_Phone_Number_Has_Been_Locked_Out(string number, int token)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                LockedOut = true
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new VerifyPhoneNumber.Command(phoneNumber, token);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000006", 888888)]
        public async Task VerifyPhoneNumber_Should_Return_BadRequest_If_Token_Has_Expired(string number, int token)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                TokenExpired = true
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new VerifyPhoneNumber.Command(phoneNumber, token);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000007", 777777)]
        public async Task VerifyPhoneNumber_Should_Return_BadRequest_If_Token_Has_Already_Been_Verified(string number, int token)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Verified = true
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new VerifyPhoneNumber.Command(phoneNumber, token);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000008", 888888, 999999)]
        public async Task VerifyPhoneNumber_Should_Return_BadRequest_If_Token_Is_Invalid(string number, int validToken, int invalidToken)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = validToken
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new VerifyPhoneNumber.Command(phoneNumber, invalidToken);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);
            var phoneNumberAfterRequest = await _fixture.PhoneNumberRepository.GetPhoneNumberByNumberAsync(number);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            phoneNumberAfterRequest.VerificationAttempts.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("+440000009", 888888, 999999)]
        public async Task VerifyPhoneNumber_Should_Lockout_Phone_Number_After_Multiple_Invalid_Attempts(string number, int validToken, int invalidToken)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = validToken
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var invalidCommand = new VerifyPhoneNumber.Command(phoneNumber, invalidToken);

            //Act

            for (int i = 0; i < Constants.MaximumAllowedVerificationAttempts; i++)
            {
                await _fixture.Dispatcher.DispatchAsync(invalidCommand);
            }

            var validCommand = new VerifyPhoneNumber.Command(phoneNumber, validToken);

            var response = await _fixture.Dispatcher.DispatchAsync(validCommand);
            var phoneNumberAfterRequest = await _fixture.PhoneNumberRepository.GetPhoneNumberByNumberAsync(number);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            phoneNumberAfterRequest.LockedOut.Should().BeTrue();
        }

        [Theory]
        [InlineData("+440000010", 888888)]
        public async Task VerifyPhoneNumber_Should_Unlock_Phone_Number_After_Certain_Amount_Of_Time(string number, int validToken)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = validToken,
                LockedOut = true,
                LockoutTime = Clock.GetTime() - Constants.PhoneNumberTokenLockoutDuration,
                TokenExpired = true
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new VerifyPhoneNumber.Command(phoneNumber, validToken);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);
            var phoneNumberAfterRequest = await _fixture.PhoneNumberRepository.GetPhoneNumberByNumberAsync(number);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            phoneNumberAfterRequest.LockedOut.Should().BeFalse();
            phoneNumberAfterRequest.TokenExpired.Should().BeTrue();
        }

        [Theory]
        [InlineData("+440000011", 888888)]
        public async Task VerifyPhoneNumber_Should_Return_OK_If_Token_Is_Valid(string number, int validToken)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = validToken,
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new VerifyPhoneNumber.Command(phoneNumber, validToken);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);
            var phoneNumberAfterRequest = await _fixture.PhoneNumberRepository.GetPhoneNumberByNumberAsync(number);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            phoneNumberAfterRequest.Verified.Should().BeTrue();
        }

        [Theory]
        [InlineData("+440000012", 888888, "Iron Man", "tony.stark@avenger.com", "Avengers.123!")]
        public async Task CreateUser_Should_Create_New_User(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);
            var user = await _fixture.UserManager.FindByNameAsync(command.Email);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            user.Should().NotBeNull();
            user.Nickname.Should().Be(command.Nickname);
            user.Email.Should().Be(command.Email);
        }

        [Theory]
        [InlineData("+440000013", "+440000014", 888888, "Captain America", "steve.rogers@avenger.com", "Avengers.123!")]
        public async Task CreateUser_Should_Not_Create_User_If_It_Already_Exists(string number1, string number2, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber1 = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number1,
                Token = token
            };

            var phoneNumber2 = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number2,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber1);
            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber2);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command1 = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number1
            };

            var command2 = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number2
            };

            //Act
            var firstAttempt = await _fixture.Dispatcher.DispatchAsync(command1);
            var secondAttempt = await _fixture.Dispatcher.DispatchAsync(command2);

            //Assert
            firstAttempt.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            secondAttempt.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000015", 888888, "Spider-Man", "peter.parker@avenger.com", "Avengers.123!")]
        public async Task CreateBudCustomer_Should_Update_User(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Number = number,
                Token = token
            };

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            var user = await _fixture.UserManager.FindByNameAsync(email);
            var createBudCustomerCommand = new CreateBudCustomer.Command(user);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(createBudCustomerCommand);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            user.BudCustomerId.Should().Be("64590D21-47D9-41C7-920D-3B597A09FE73");     //TODO: How to not hard code this ?
            user.BudCustomerSecret.Should().Be("58F7D134-CF06-4826-A9D1-183A49D7592E");
        }

        [Fact]
        public async Task PerformKyc_Should_Return_NotFound_If_User_Does_Not_Exist()
        {
            //Arrange
            var kycCommand = new PerformKyc.Command()
            {
                UserName = "natasha.romanoff@avenger.com"
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(kycCommand);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("+440000016", 888888, "Dr.Strange", "steven.strange@avenger.com", "Avengers.123!")]
        public async Task PerformKyc_Should_Return_BadRequest_If_User_Is_Already_Verified(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Number = number,
                Token = token
            };

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            var kycCommand = new PerformKyc.Command()
            {
                Title = UserTitle.Dr,
                FirstName = "Steven",
                LastName = "Strange",
                DateOfBirth = Clock.GetTime(),
                Postcode = "11101",
                Address = "Queens, New York City, New York",
                UserName = email
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            var user = await _fixture.UserManager.FindByNameAsync(email);
            var createBudCustomerCommand = new CreateBudCustomer.Command(user);
            await _fixture.Dispatcher.DispatchAsync(createBudCustomerCommand);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(kycCommand);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000017", 888888, "Hulk", "bruce.banner@avenger.com", "Avengers.123!", "https://dummy-url.com", "Natwest")]
        public async Task ObLogin_Should_Return_BadRequest_If_User_Has_Not_Completed_KYC(string number, int token, string nickname, string email, string password, string dummyUrl, string providerName)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Number = number,
                Token = token
            };

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            var command = new OpenBankingLogin.Command()
            {
                UserName = email,
                RedirectUrl = dummyUrl,
                ProviderName = providerName
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("natwest")]
        public async Task GetListOfObProviders_Search_Should_Filter_Results(string searchTerm)
        {
            //Arrange
            var searchQuery = new GetListOfObProviders.Query(searchTerm);
            var query = new GetListOfObProviders.Query();

            //Act
            var resultWithSearch = await _fixture.Dispatcher.DispatchAsync(searchQuery);
            var resultWithoutSearch = await _fixture.Dispatcher.DispatchAsync(query);

            //Assert
            resultWithSearch.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            resultWithoutSearch.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            resultWithoutSearch.Records.Values.Count.Should().BeGreaterThan(resultWithSearch.Records.Values.Count);
        }

        [Theory]
        [InlineData("tony@stark.com")]
        public async Task GenerateTokenForUser_Should_Return_NotFound_If_PhoneNumber_Does_Not_Exist(string email)
        {
            //Arrange
            var command = new GenerateTokenForUser.Command()
            {
                Email = email
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("+440000018", 888888, "nick.fury@avenger.com")]
        public async Task GenerateTokenForUser_Should_Return_NotFound_If_User_Does_Not_Exist(string number, int token, string email)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Number = number,
                Token = token
            };

            var command = new GenerateTokenForUser.Command()
            {
                Email = email
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();


            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("dead@pool.com", "Deadpool123", 555555)]
        public async Task PasswordChange_Should_Return_NotFound_If_PhoneNumber_Does_Not_Exist(string email, string password, int verificationToken)
        {
            //Arrange
            var command = new ResetPassword.Command()
            {
                Email = email,
                Password = password,
                VerificationToken = verificationToken
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("+440000019", 888888, "black.panther@avenger.com", "Avengers.123!")]
        public async Task PasswordChange_Should_Return_NotFound_If_User_Does_Not_Exist(string number, int token, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Number = number,
                Token = token
            };

            var command = new ResetPassword.Command()
            {
                Email = email,
                Password = password,
                VerificationToken = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();


            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("+440000020", 888888, "Wanda Maximoff", "scarlet.witch@avenger.com", "Avengers.123!", "Witch.123!")]
        public async Task PasswordChange_Should_Return_OK(string number, int token, string nickname, string email, string password, string newPassword)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Number = number,
                Token = token,
                Verified = false
            };

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            phoneNumber.Verified = false;
            phoneNumber.TokenExpired = false;
            _fixture.PhoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var changePasswordCommand = new ResetPassword.Command()
            {
                Email = email,
                Password = newPassword,
                VerificationToken = token
            };

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(changePasswordCommand);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("+440000021", 888888, "Winter Soldier", "bucky.barnes@avenger.com", "Avengers.123!")]
        public async Task GenerateToken_Should_Return_OK(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Number = number,
                Token = token,
                Verified = false
            };

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            var generatePasswordCommand = new GenerateTokenForUser.Command()
            {
                Email = email
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();
            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(generatePasswordCommand);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("+440000022", 888888, "Thor Odinson", "thor.odinson@avenger.com", "Avengers.123!")]
        public async Task AddPhoneNumber_Should_Return_Success_If_User_With_The_Same_Phone_Number_Already_Exists(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token,
                Verified = false
            };

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };
            var secondUser = "thor2.odinson@avenger.com";
            var addPhoneNumberCommand = new AddPhoneNumber.Command(number, secondUser);

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();
            var succeded = await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(addPhoneNumberCommand);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("+440000023", "not.existing@mail.com")]
        public async Task AddPhoneNumber_Should_Return_OK_If_Phone_Number_Does_Not_Exist(string number, string email)
        {
            //Arrange
            var addPhoneNumberCommand = new AddPhoneNumber.Command(number, email);

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(addPhoneNumberCommand);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ChangePassword_Should_Return_BadRequest_If_User_Does_Not_Exist()
        {
            //Arrange
            var command = new ChangePassword.Command()
            {
                Username = "",
            };

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000024", 888888, "Profesor X", "charles.xavier@avenger.com", "Avengers.123!")]
        public async Task ChangePassword_Should_Return_BadRequest_If_Current_Password_Is_Incorect(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            var command = new ChangePassword.Command()
            {
                Username = email,
                CurrentPassword = ""
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000125", 888888, "Pyro", "alex.burton@avenger.com", "Avengers.123!")]
        public async Task ChangePassword_Should_Return_BadRequest_If_Current_Password_Is_Weak(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token,
                TokenExpired = false,
                Verified = false
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            phoneNumber.TokenExpired = false;
            phoneNumber.Verified = false;
            _fixture.PhoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new ChangePassword.Command()
            {
                Username = email,
                CurrentPassword = password,
                NewPassword = "weakpassowrd",
                VerificationToken = token
            };

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000025", 888888, "Cyclops", "scott.summers@avenger.com", "Avengers.123!")]
        public async Task ChangePassword_Should_Change_Password(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token,
                TokenExpired = false,
                Verified = false
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            phoneNumber.TokenExpired = false;
            phoneNumber.Verified = false;
            _fixture.PhoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new ChangePassword.Command()
            {
                Username = email,
                CurrentPassword = password,
                NewPassword = "StrongPassword.123!",
                VerificationToken = token
            };

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);
            var user = await _fixture.UserManager.FindByEmailAsync(email);
            var passwordChanged = await _fixture.UserManager.CheckPasswordAsync(user, command.NewPassword);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            passwordChanged.Should().BeTrue();
        }

        [Theory]
        [InlineData("+440000026", 999999)]
        public async Task GenerateTokenForExistingPhoneNumber_Should_Make_User_Wait_15_Seconds_Before_Generating_New_Token(string number, int token)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token,
                TokenExpired = false,
                Verified = false
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new GenerateTokenForExistingPhoneNumber.Command(phoneNumber);

            //Act
            var firstResponse = await _fixture.Dispatcher.DispatchAsync(command);
            var secondResponse = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            firstResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            secondResponse.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000027")]
        public async Task GenerateTokenForExistingPhoneNumber_Should_Return_BadRequest_If_Phone_Number_Is_LockedOut(string number)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                LockedOut = true,
                LockoutTime = Clock.GetTime()
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new GenerateTokenForExistingPhoneNumber.Command(phoneNumber);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000028", 100000)]
        public async Task GenerateTokenForExistingPhoneNumber_Should_Generate_New_Token(string number, int token)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var command = new GenerateTokenForExistingPhoneNumber.Command(phoneNumber);

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);
            var afterRequest = await _fixture.PhoneNumberRepository.GetPhoneNumberByNumberAsync(command.PhoneNumber.Number);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            afterRequest.Token.Should().NotBe(token);
        }

        [Fact]
        public async Task GenerateTokenForNewPassword_Should_Return_Bad_Request_If_User_Does_Not_Exist()
        {
            //Arrange
            var command = new GenerateTokenForNewPassword.Command()
            {
                Username = string.Empty
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000029", 888888, "Storm", "ororo.munroe@avenger.com", "Avengers.123!")]
        public async Task GenerateTokenForNewPassword_Should_Return_Bad_Request_If_Password_Is_Incorrect(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token,
                TokenExpired = false,
                Verified = false
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            var command = new GenerateTokenForNewPassword.Command()
            {
                Username = email,
                CurrentPassword = "incorrect-password"
            };

            //Act
            var result = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("+440000030", 888888, "Rogue", "anna.marie@avenger.com", "Avengers.123!")]
        public async Task GenerateTokenForNewPassword_Should_Generate_New_Token(string number, int token, string nickname, string email, string password)
        {
            //Arrange
            var phoneNumber = new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                Token = token,
                TokenExpired = false,
                Verified = false
            };

            await _fixture.PhoneNumberRepository.AddPhoneNumberAsync(phoneNumber);
            await _fixture.PhoneNumberRepository.SaveChangesAsync();

            var createUserCommand = new CreateUser.Command()
            {
                Nickname = nickname,
                Email = email,
                Password = password,
                VerificationToken = token,
                PhoneNumber = number
            };

            await _fixture.Dispatcher.DispatchAsync(createUserCommand);

            var command = new GenerateTokenForNewPassword.Command()
            {
                Username = email,
                CurrentPassword = password
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);
            var afterRequest = await _fixture.PhoneNumberRepository.GetPhoneNumberByNumberAsync(number);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            afterRequest.Token.Should().NotBe(token);
        }

        [Fact]
        public async Task GetProfileDetails_Should_Return_NotFound_If_User_Does_Not_Exist()
        {
            //Arrange
            var command = new GetProfileDetails.Query(Guid.NewGuid());

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateMarketingCommunications_Should_Return_NotFound_If_User_Does_Not_Exist()
        {
            //Arrange
            var command = new UpdateMarketingCommunications.Command()
            {
                Username = string.Empty
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateNotifications_Should_Return_NotFound_If_User_Does_Not_Exist()
        {
            //Arrange
            var command = new UpdateNotifications.Command()
            {
                Username = string.Empty
            };

            //Act
            var response = await _fixture.Dispatcher.DispatchAsync(command);

            //Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ActivateRememberMe_Should_Update_RememberMe_By_User_Name()
        {
            // Arrange
            var command = new ActivateRememberMe.Command
            {
                UserName = "kenan@mail.com",
                RememberMeAt = new DateTime(2021, 09, 15, 11, 00, 00)
            };

            // Act
            var result = await _fixture.Dispatcher.DispatchAsync(command: command);
            var expectedUser = await _fixture.UserManager.Users.SingleOrDefaultAsync(predicate: u => u.UserName == command.UserName);

            // Assert
            result.Should().NotBeNull();
            result.HttpStatusCode.Should().Be(expected: HttpStatusCode.OK);
            expectedUser.Should().NotBeNull();
            expectedUser.RememberMeAt.Should().Be(expected: command.RememberMeAt);
        }

    }
}
using FluentAssertions;
using Moq;
using Omikron.IdentityService.Domain.Email.Factories;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Email;
using Xunit;

namespace Omikron.IdentityService.UnitTest.Email
{
    public class EmailFactories : IClassFixture<EmailContentFactoryFixture>
    {
        private readonly EmailContentFactoryFixture _fixture;
        private readonly Mock<ITenantAccessor> _mockTenantAccessor;

        public EmailFactories(EmailContentFactoryFixture _fixture)
        {
            this._fixture = _fixture;
            _mockTenantAccessor = new Mock<ITenantAccessor>();
        }

        [Fact]
        public void NewUserSendGridFactory_Should_Return_Filled_Message()
        {
            var model = new NewUserEmailModel(new User() { FirstName = "User", LastName = "Test", Email = "user@email.com" });

            var result = new NewUserSendGridMessageFactory(new EmailSettings() { NewUserTemplateId = "TemplateId", SenderName = "Sender", Sender = "Sender@email.com" }).Factory(model);

            result.Personalizations.Should().HaveCount(1);
            result.Personalizations[0].Tos.Should().HaveCount(1);
            result.Personalizations[0].Tos[0].Email.Should().Be(model.User.Email);
            result.Personalizations[0].Tos[0].Name.Should().Be(model.User.FirstName);

            result.TemplateId.Should().Be("TemplateId");
            result.From.Email.Should().Be("Sender@email.com");
            result.From.Name.Should().Be("Sender");
        }
    }
}
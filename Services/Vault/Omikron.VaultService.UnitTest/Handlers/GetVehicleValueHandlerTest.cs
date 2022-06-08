using FluentAssertions;
using Omikron.VaultService.Domain.Handlers;
using Omikron.VaultService.Domain.Queries;
using Omikron.VaultService.Infrastructure.UkVehicleData;
using Omikron.VaultService.UnitTest.Helpers;
using Omikron.VaultService.UnitTest.Properties;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Omikron.VaultService.UnitTest.Handlers
{
    public class GetVehicleValueHandlerTest
    {
        private readonly UkVehicleSettings _settings = new()
        {
            ApiKey = "123",
            BaseUrl = "https://base",
            ValuationEndpoint = "ValuationData?v=2&api_nullitems=1&auth_apikey={0}&key_VRM={1}&key_mileage={2}"
        };

        [Fact]
        public async Task ExternalApiReturns_Not_200_Return_BadRequest_With_Reason()
        {
            //Arrange
            var expectedPhrase = "Bad Request";
            var mileage = 10;
            var registration = "abc";

            var (clientFactory, mockClient) = HttpClientFactoryBuilder.CreateClient(new StringContent(""), HttpStatusCode.BadRequest, expectedPhrase, nameof(GetVehicleValueQueryHandler), _settings.BaseUrl);

            var handler = new GetVehicleValueQueryHandler(null, _settings, clientFactory);

            //Act
            var result = await handler.Handle(new GetVehicleValue.Query() { Mileage = mileage, Registration = registration }, CancellationToken.None);

            //Assert
            mockClient.BaseAddress.OriginalString.Should().Be(_settings.BaseUrl);
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Errors.Should().Contain(expectedPhrase);
        }

        [Fact]
        public async Task ExternalApi_Returns_200_Content_Response_Not_Success_Returns_BadRequest_StatusMessage()
        {
            //Arrange
            var expectedPhrase = "KeyInvalid: The VRM key value is not recognised as a valid vehicle registration mark.";
            var mileage = 10;
            var registration = "abc";

            var (clientFactory, mockClient) = HttpClientFactoryBuilder.CreateClient(new StringContent(""), HttpStatusCode.BadRequest, expectedPhrase, nameof(GetVehicleValueQueryHandler), _settings.BaseUrl);

            var handler = new GetVehicleValueQueryHandler(null, _settings, clientFactory);

            //Act
            var result = await handler.Handle(new GetVehicleValue.Query() { Mileage = mileage, Registration = registration }, CancellationToken.None);


            //Assert
            mockClient.BaseAddress.OriginalString.Should().Be(_settings.BaseUrl);
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Errors.Should().Contain(expectedPhrase);
        }

        [Fact]
        public async Task ExternalApi_Returns_200_Content_Response_Returns_Success_PrivateClean_Value_Used()
        {
            //Arrange
            var mileage = 10;
            var registration = "abc";

            var (clientFactory, mockClient) = HttpClientFactoryBuilder.CreateClient(new StringContent(Resources.UkVehicleDataSuccesJSON), HttpStatusCode.OK, "", nameof(GetVehicleValueQueryHandler), _settings.BaseUrl);
            var handler = new GetVehicleValueQueryHandler(null, _settings, clientFactory);

            //Act
            var result = await handler.Handle(new GetVehicleValue.Query() { Mileage = mileage, Registration = registration }, CancellationToken.None);

            //Assert
            mockClient.BaseAddress.OriginalString.Should().Be(_settings.BaseUrl);
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Value.Should().Be(1605m);
        }
    }
}
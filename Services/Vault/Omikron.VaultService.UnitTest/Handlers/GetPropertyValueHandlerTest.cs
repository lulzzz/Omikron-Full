using FluentAssertions;
using Omikron.VaultService.Domain.Handlers;
using Omikron.VaultService.Domain.Queries;
using Omikron.VaultService.Infrastructure.PropertyData;
using Omikron.VaultService.UnitTest.Helpers;
using Omikron.VaultService.UnitTest.Properties;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Omikron.VaultService.UnitTest.Handlers
{
    public class GetPropertyValueHandlerTest
    {
        private readonly PropertyDataSettings _settings = new PropertyDataSettings() { ApiKey = "123", BaseUrl = "https://base", PricesEndpoint = "prices?key={0}&postcode={1}&bedrooms={2}" };

        [Fact]
        public async Task ExternalApi_Returns_Not_Success_Returns_BadRequest_ReasonPhrase()
        {
            //Arrange
            var expectedPhrase = "Expected Phrase";

            var (clientFactory, mockClient) = HttpClientFactoryBuilder.CreateClient(new StringContent(""), HttpStatusCode.BadRequest, expectedPhrase, nameof(GetPropertyValueQueryHandler), _settings.BaseUrl);
            var handler = new GetPropertyValueQueryHandler(null, _settings, clientFactory);

            //Act
            var result = await handler.Handle(new GetPropertyValue.Query() { NumberOfBedrooms = 2, PostCode = "abc" }, CancellationToken.None);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Errors.Should().Contain(expectedPhrase);
        }

        [Fact]
        public async Task ExternalApi_Returns_Success_Content_Set_Average_Price_Taken()
        {
            //Arrange
            var expectedValue = 602345m;

            var (clientFactory, mockClient) = HttpClientFactoryBuilder.CreateClient(new StringContent(Resources.ResourceManager.GetString("PropertyDataSuccessJSON")), HttpStatusCode.OK, "", nameof(GetPropertyValueQueryHandler), _settings.BaseUrl);
            var handler = new GetPropertyValueQueryHandler(null, _settings, clientFactory);

            //Act
            var result = await handler.Handle(new GetPropertyValue.Query() { NumberOfBedrooms = 2, PostCode = "abc" }, CancellationToken.None);

            //Assert
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.Records.Value.Should().Be(expectedValue);
        }
    }
}
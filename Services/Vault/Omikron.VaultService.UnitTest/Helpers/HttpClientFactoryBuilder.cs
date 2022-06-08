using System;
using System.Net;
using System.Net.Http;
using Moq;

namespace Omikron.VaultService.UnitTest.Helpers
{
    public class HttpClientFactoryBuilder
    {
        public static (IHttpClientFactory, HttpClient) CreateClient(StringContent content, HttpStatusCode statusCode, string reasonPhrase, string serviceName, string baseUrl)
        {
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            var mockMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
                { StatusCode = statusCode, Content = content, ReasonPhrase = reasonPhrase });
            var mockClient = new HttpClient(mockMessageHandler);
            mockClient.BaseAddress = new Uri(baseUrl);

            mockHttpClientFactory.Setup(x => x.CreateClient(serviceName)).Returns(mockClient);

            return (mockHttpClientFactory.Object, mockClient);
        }
    }
}
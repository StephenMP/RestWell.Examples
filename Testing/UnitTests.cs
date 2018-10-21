using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Client.Testing;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTests
    {
        #region Return a Response From a Specific ProxyRequest

        [Fact]
        public async Task ReturnAResponseFromASpecificProxyRequest()
        {

            // Setup your mocked stuff
            var mockedProxyRequest = ProxyRequestBuilder<string[]>
                                    .CreateBuilder("https://www.this.is/fake", HttpRequestMethod.Get)
                                    .AppendToRoute("api/example")
                                    .Accept("application/json")
                                    .Build();

            var mockedProxyResponse = new ProxyResponse<string[]>
            {
                IsSuccessfulStatusCode = true,
                StatusCode = HttpStatusCode.OK,
                ResponseDto = new[] { "Mocked Value From A Specific Request 1", "Mocked Value From A Specific Request 2" }
            };

            // Create and configure TestProxy
            var testProxy = new TestProxy();
            testProxy.WhenIReceiveAnyRequest<Missing, string[]>().ThenIShouldReturnThisResponse(mockedProxyResponse);

            // Use the TetProxy in the class that needs it
            var classThatUsesProxy = new ClassThatUsesProxy(testProxy);
            var methodThatUsesProxyResponse = await classThatUsesProxy.MethodThatUsesProxyAsync(mockedProxyRequest).ConfigureAwait(false);

            Assert.Equal("Mocked Value From A Specific Request 1", methodThatUsesProxyResponse[0]);
            Assert.Equal("Mocked Value From A Specific Request 2", methodThatUsesProxyResponse[1]);
        }

        #endregion

        #region Return a Response From a Any ProxyRequest

        [Fact]
        public async Task ReturnAResponseFromAnyProxyRequest()
        {
            // Setup your mocked stuff
            var mockedProxyRequest = ProxyRequestBuilder<string[]>
                                    .CreateBuilder("https://www.this.is/fake", HttpRequestMethod.Get)
                                    .AppendToRoute("api/example")
                                    .Accept("application/json")
                                    .Build();

            var mockedProxyResponse = new ProxyResponse<string[]>
            {
                IsSuccessfulStatusCode = true,
                StatusCode = HttpStatusCode.OK,
                ResponseDto = new[] { "Mocked Value From Any Request 1", "Mocked Value From Any Request 2" }
            };

            // Create and configure TestProxy
            var testProxy = new TestProxy();
            testProxy.WhenIReceiveAnyRequest<Missing, string[]>().ThenIShouldReturnThisResponse(mockedProxyResponse);

            // Use the TetProxy in the class that needs it
            var classThatUsesProxy = new ClassThatUsesProxy(testProxy);
            var methodThatUsesProxyResponse = await classThatUsesProxy.MethodThatUsesProxyAsync(mockedProxyRequest).ConfigureAwait(false);

            Assert.Equal("Mocked Value From Any Request 1", methodThatUsesProxyResponse[0]);
            Assert.Equal("Mocked Value From Any Request 2", methodThatUsesProxyResponse[1]);
        }

        #endregion

    }
}

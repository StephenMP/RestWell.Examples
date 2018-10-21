using System.Reflection;
using System.Threading.Tasks;
using RestWell.Client;
using RestWell.Client.Request;

namespace XUnitTestProject1
{
    public class ClassThatUsesProxy
    {
        private readonly IProxy proxy;

        public ClassThatUsesProxy(IProxy proxy)
        {
            this.proxy = proxy;
        }

        public async Task<string[]> MethodThatUsesProxyAsync(IProxyRequest<Missing, string[]> proxyRequest)
        {
            var proxyResponse = await this.proxy.InvokeAsync(proxyRequest).ConfigureAwait(false);

            if (proxyResponse.IsSuccessfulStatusCode)
            {
                return proxyResponse.ResponseDto;
            }

            return null;
        }
    }
}

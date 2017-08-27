using RestWell.Client;
using RestWell.Client.Request;
using System.Reflection;

namespace XUnitTestProject1
{
    public class ClassThatUsesProxy
    {
        private readonly IProxy proxy;

        public ClassThatUsesProxy(IProxy proxy)
        {
            this.proxy = proxy;
        }

        public string[] MethodThatUsesProxy(IProxyRequest<Missing, string[]> proxyRequest)
        {
            var proxyResponse = this.proxy.Invoke(proxyRequest);

            if (proxyResponse.IsSuccessfulStatusCode)
            {
                return proxyResponse.ResponseDto;
            }

            return null;
        }
    }
}

using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;
using RestWell.Client;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Examples.Resource.Shared;
using System.Threading.Tasks;

namespace DependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProxyConfiguration injectedProxyConfiguration;
        private readonly IProxy injectedProxy;

        // The ProxyConfiguration and Proxy will be injected the the DI framework
        public HomeController(IProxyConfiguration proxyConfiguration, IProxy proxy)
        {
            this.injectedProxyConfiguration = proxyConfiguration;
            this.injectedProxy = proxy;
        }

        public async Task<IActionResult> Index()
        {
            HomeModel model = new HomeModel();

            using (var environment = ExampleEnvironment.Start())
            {
                var baseUri = environment.GetResourceWebService<RestWell.Examples.Resource.Api.Startup>().BaseUri;

                var proxyRequest = ProxyRequestBuilder<string[]>
                                    .CreateBuilder(baseUri, HttpRequestMethod.Get)
                                    .AppendToRoute("api/example")
                                    .Accept("application/json")
                                    .Build();

                // Using the inject ProxyConfiguration
                using (var proxy = new Proxy(this.injectedProxyConfiguration))
                {
                    var proxyResponseForInjectedProxyConfiguration = await this.injectedProxy.InvokeAsync(proxyRequest);

                    if (proxyResponseForInjectedProxyConfiguration.IsSuccessfulStatusCode)
                    {
                        model.ValuesFromInjectedProxyConfiguration = proxyResponseForInjectedProxyConfiguration.ResponseDto;
                    }
                }

                // Using the injected Proxy
                var proxyResponseForInjectedProxy = await this.injectedProxy.InvokeAsync(proxyRequest);

                if (proxyResponseForInjectedProxy.IsSuccessfulStatusCode)
                {
                    model.ValuesFromInjectedProxy = proxyResponseForInjectedProxy.ResponseDto;
                }
            }

            return View(model);
        }
    }
}

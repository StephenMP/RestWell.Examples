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
        private readonly IProxy proxy;

        // The proxy will be injected the the DI framework
        public HomeController(IProxy proxy)
        {
            this.proxy = proxy;
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

                var proxyResponse = await this.proxy.InvokeAsync(proxyRequest);

                if (proxyResponse.IsSuccessfulStatusCode)
                {
                    model.Values = proxyResponse.ResponseDto;
                }
            }

            return View(model);
        }
    }
}

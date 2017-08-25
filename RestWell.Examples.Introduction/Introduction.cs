using RestWell.Client;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Examples.Resource.Shared;
using System;

namespace RestWell.Examples.Introduction
{
    public class Introduction
    {
        static void Main(string[] args)
        {
            using (var environment = ExampleEnvironment.Start())
            {

                /* 
                 * The using statment and this line only serve the purpose of spinning up
                 * a demo API (found in RestWell.Examples.Resource.Api) that we can use for 
                 * demo purposes and gett it's base URI. These steps are not required for
                 * the usage of RestWell.
                 */
                var baseUri = environment.GetResourceWebService<Resource.Api.Startup>().BaseUri;

                #region Creating a ProxyRequest using the ProxyRequestBuilder

                /* 
                 * First, we create a ProxyRequest using the ProxyRequestBuilder to setup a RESTful request to our demo API.
                 * Now, the request we are going to create will be a GET request with no request body. The request body
                 * returned from the API is a string array of values which should be { "value1", "value2" }.
                 */

                /*
                 * In order to have a valid request, we must specify a base URI and request type
                 * We also specify the TResposneDto as string[] since the response body is
                 * a string array.
                 */
                var proxyRequestBuilder = ProxyRequestBuilder<string[]>.CreateBuilder(baseUri, HttpRequestMethod.Get);

                // Now we add /api/example to the URI to point to the API's ExampleController
                proxyRequestBuilder.AppendToRoute("api/example");

                // Now we set the Accept header to application/json
                proxyRequestBuilder.Accept("application/json");

                /*
                 * Now that we have a ProxyRequestBuilder configured for our request
                 * we can use the Build() method to build a ProxyRequest
                 */
                var proxyRequest = proxyRequestBuilder.Build();

                #endregion

                #region Creating a Proxy, invoking the ProxyRequest, and obtaining a ProxyResult

                /*
                 * Now with a proxy request, we can create a proxy which we
                 * use to invoke the request. The Proxy class is disposable
                 * so we wrap it in a using statement here; however, we
                 * will cover Dependency Injection in a more advanced example.
                 */
                using (var proxy = new Proxy())
                {
                    #region Issue the ProxyRequest

                    // Use the proxy to invoke the request and obtain a response
                    var proxyResponse = proxy.Invoke(proxyRequest);

                    #endregion

                    #region Use the ProxyResponse

                    // Now we see if the request was successful
                    if (proxyResponse.IsSuccessfulStatusCode)
                    {
                        // If so, we can get the response body and do something with it
                        var valuesArray = proxyResponse.ResponseDto;

                        Console.WriteLine("Values from the API:");

                        foreach (var value in valuesArray)
                        {
                            Console.WriteLine($"\t{value}");
                        }
                    }

                    // Otherwise, we should probably handle a failed request
                    else
                    {
                        Console.WriteLine("The request failed!!! :(");
                    }

                    #endregion

                }

                #endregion

            }

            Console.ReadKey();
        }
    }
}

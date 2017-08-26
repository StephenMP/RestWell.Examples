using RestWell.Client;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Examples.RequestAndResponseBodies.Dtos;
using RestWell.Examples.Resource.Shared;
using System;

namespace RestWell.Examples.RequestAndResponseBodies
{
    class RequestAndResponseBodies
    {
        static void Main(string[] args)
        {
            using (var environment = ExampleEnvironment.Start())
            {
                var baseUri = environment.GetResourceWebService<Resource.Api.Startup>().BaseUri;

                /*
                 * RequestDto and ResponseDto refer to the Request Body and Response Body.
                 * The reason in which it is specified as a DTO is in reference to
                 * Data Transfer Object. This is a POCO (Plain Old C Object) where the
                 * class consists of ONLY properties with a get and set. For example:
                 * 
                 *  public class MyDto
                 *  {
                 *       public string Message { get; set; }
                 *       public int Id { get; set; }
                 *  }
                 *  
                 *  DTOs are a fantastic way to transfer data across layers of an application.
                 *  That's why in RestWell, we refer to the objects that represent the request 
                 *  and response bodies as DTOs.
                 *  
                 *  You can create your DTOs to match one-for-one with the body and allow the 
                 *  framework to auto-magically know how to serialize and deserialize your DTO.
                 *  
                 *  If complete utter control is your thing, no problem. You can mark up your 
                 *  request with Newtonsoft.Json annotations to better describe how to 
                 *  serialize/deserialize the request/response body into it's corresponding DTO.
                 */

                using (var proxy = new Proxy())
                {
                    // Set the Request DTO and Response DTO types for the ProxyRequestBuilder
                    var proxyRequest = ProxyRequestBuilder<ExampleRequestDto, ExampleResponseDto>
                                        .CreateBuilder(baseUri, HttpRequestMethod.Get)
                                        .AppendToRoute("api/example/body")
                                        .Accept("application/json")
                                        // Set the Request DTO
                                        .SetRequestDto(new ExampleRequestDto { Message = "Hello World" })
                                        .Build();

                    var proxyResponse = proxy.Invoke(proxyRequest);

                    if (proxyResponse.IsSuccessfulStatusCode)
                    {
                        Console.WriteLine("Proxy Response Body:");
                        Console.WriteLine($"\tDTO Type: {proxyResponse.ResponseDto.GetType().Name}");
                        Console.WriteLine($"\tResponse Message: {proxyResponse.ResponseDto.Message}");
                    }
                }
            }

            Console.ReadKey();
        }
    }
}

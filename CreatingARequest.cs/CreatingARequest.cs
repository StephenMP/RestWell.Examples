using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Examples.CreatingARequest.cs.Requests;
using RestWell.Examples.CreatingARequest.Dtos;
using RestWell.Examples.Resource.Shared;
using System;

namespace RestWell.Examples.CreatingARequest.cs
{
    class CreatingARequest
    {
        static void Main(string[] args)
        {
            /*
             * There are two ways in which you can create a request.
             *  1. Using the ProxyRequestBuilder class (easiest)
             *  2. Implementing the IProxyRequest<TRequestDto, TResponseDto> yourself (most configurable)
             *  3. Why not both?
             */

            #region Using the Proxy Request Builder

            var proxyRequest = ProxyRequestBuilder<ExampleRequestDto, ExampleResponseDto>
                                .CreateBuilder("https://www.this.is/a/base/url/api", HttpRequestMethod.Get)
                                .AppendToRoute($"controller")
                                .Authorization("Basic", "Username:Password")
                                .Accept("application/json")
                                .AddHeader("x-customHeader", "value1", "value2", "value3")
                                .AddPathArguments("arg1", "arg2")
                                .AddQueryParameter("queryParam1", "value")
                                .SetRequestDto(new ExampleRequestDto { Message = "Hello World" })
                                .Build();

            Writer.WriteRequest(proxyRequest);

            #endregion

            Console.WriteLine("\n==========\n");

            #region Implmenting your own IProxyRequest<TRequestDto, TResponseDto>

            /*
             * Implementing the IProxyRequest<TRequestDto, TResponseDto> gives you complete control over
             * structure of the request. It also allows you to perform logic to build up your request
             * in a class of it's own rather than using the fluent ProxyRequestBuilder.
             */

            // MyProxyRequest is a custom class which implement IProxyRequest<ExampleRequestDto, ExampleResponseDto>
            // It's implementation matches the ProxyRequestBuilder code above.
            var myProxyRequest = new MyProxyRequest("https://www.this.is/a/base/url/api", new ExampleRequestDto { Message = "Hello World" });
            Writer.WriteRequest(myProxyRequest);

            #endregion

            Console.WriteLine("\n==========\n");

            #region Why Not Both?

            /*
             * There's nothing that says we can't use both methods of creaing
             * a ProxyRequest... :)
             */

            var myMixedProxyRequest = new WhyNotBothProxyRequest("https://www.this.is/a/base/url/api", new ExampleRequestDto { Message = "Hello World" });
            Writer.WriteRequest(myMixedProxyRequest);

            #endregion

            Console.ReadKey();
        }
    }
}

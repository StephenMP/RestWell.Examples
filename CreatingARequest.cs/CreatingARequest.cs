using RestWell.Client.Enums;
using RestWell.Client.Request;
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

            Console.ReadKey();

            #endregion
        }
    }
}

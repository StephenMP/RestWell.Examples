using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Examples.CreatingARequest.Dtos;
using System;
using System.Collections.Generic;

namespace RestWell.Examples.CreatingARequest.cs.Requests
{
    public class WhyNotBothProxyRequest : IProxyRequest<ExampleRequestDto, ExampleResponseDto>
    {
        public IDictionary<string, IEnumerable<string>> Headers { get; set; }
        public HttpRequestMethod HttpRequestMethod { get; set; }
        public ExampleRequestDto RequestDto { get; set; }
        public Uri RequestUri { get; set; }

        public WhyNotBothProxyRequest(string baseUri, ExampleRequestDto requestDto)
        {
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

            this.Headers = proxyRequest.Headers;
            this.HttpRequestMethod = proxyRequest.HttpRequestMethod;
            this.RequestDto = proxyRequest.RequestDto;
            this.RequestUri = proxyRequest.RequestUri;
        }
    }
}
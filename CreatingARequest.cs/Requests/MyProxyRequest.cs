using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Examples.CreatingARequest.Dtos;
using System;
using System.Collections.Generic;

namespace RestWell.Examples.CreatingARequest.cs.Requests
{
    public class MyProxyRequest : IProxyRequest<ExampleRequestDto, ExampleResponseDto>
    {
        public IDictionary<string, IEnumerable<string>> Headers { get; set; }
        public HttpRequestMethod HttpRequestMethod { get; set; }
        public ExampleRequestDto RequestDto { get; set; }
        public Uri RequestUri { get; set; }

        public MyProxyRequest(string baseUri, ExampleRequestDto requestDto)
        {
            this.RequestUri = new Uri($"{baseUri.TrimEnd('/')}/controller/arg1/arg2?queryParam1=value");
            this.HttpRequestMethod = HttpRequestMethod.Get;
            this.RequestDto = requestDto;
            this.Headers = new Dictionary<string, IEnumerable<string>>
            {
                { "Authorization", new[] { "Basic Username:Password" } },
                { "Accept", new[] { "application/json" } },
                { "x-customHeader", new[] { "value1", "value2", "value3" } }
            };
        }
    }
}
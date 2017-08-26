using RestWell.Client.Request;
using System;

namespace RestWell.Examples.Resource.Shared
{
    public static class Writer
    {
        public static void WriteValues(string[] values)
        {
            Console.WriteLine("Values from the API:");

            foreach (var value in values)
            {
                Console.WriteLine($"\t{value}");
            }
        }

        public static void WriteRequest<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> proxyRequest)
        {
            Console.WriteLine("Proxy Request:");

            Console.WriteLine($"\tHeaders:");

            foreach (var header in proxyRequest.Headers)
            {
                Console.WriteLine($"\t  {header.Key}:");

                foreach (var value in header.Value)
                {
                    Console.WriteLine($"\t    {value}");
                }
            }

            Console.WriteLine();

            Console.WriteLine($"\tRequest Method: {proxyRequest.HttpRequestMethod}");

            Console.WriteLine();

            Console.WriteLine($"\tRequest DTO:");
            Console.WriteLine($"\t  DTO Type: {proxyRequest.RequestDto.GetType().Name}");
            Console.WriteLine($"\t  Message: {proxyRequest.RequestDto.ToString()}");

            Console.WriteLine();

            Console.WriteLine($"\tResponse DTO Type: {typeof(TResponseDto).Name}");

            Console.WriteLine();

            Console.WriteLine($"\tRequest URI: {proxyRequest.RequestUri}");
        }
    }
}

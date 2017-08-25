﻿using RestWell.Client;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Examples.ConfiguringTheProxy.DelegatingHandlers;
using RestWell.Examples.Resource.Shared;
using System;
using System.Net.Http.Headers;

namespace RestWell.Examples.ConfiguringTheProxy
{
    public class ConfiguringTheProxy
    {
        static void Main(string[] args)
        {
            using (var environment = ExampleEnvironment.Start())
            {
                var baseUri = environment.GetResourceWebService<Resource.Api.Startup>().BaseUri;


                /*
                 * Configuring the proxy allows you to customize the proxy's behavior.
                 * For example, you can inject your own delegating handler into the 
                 * request pipeline or set default request headers for every request!
                 */

                #region Setting a Default Request Header


                // In order to configure the proxy, you need to create a ProxyConfiguration using the ProxyConfigurationBuilder
                var proxyConfigurationBuilder = ProxyConfigurationBuilder.CreateBuilder();

                /*
                 * The ProxyConfigurationBuilder exposes a few things to use that we can take
                 * advantage of for every request. For example, if we always want
                 * to Accept application/json, we can set the default Accept header to
                 * always be application/json
                 */
                proxyConfigurationBuilder.UseDefaultAcceptHeader(new MediaTypeWithQualityHeaderValue("application/json"));

                // Now, we use the ProxyConfigurationBuilder.Build() to create a ProxyConfiguration
                var proxyConfiguration = proxyConfigurationBuilder.Build();

                // Now, you use the ProxyConfiguration when creating the Proxy to tell the Proxy how to be configured
                using (var proxy = new Proxy(proxyConfiguration))
                {
                    // Notice on this request we did not use the Accept() method to add an accept header
                    var proxyRequest = ProxyRequestBuilder<string[]>
                                        .CreateBuilder(baseUri, HttpRequestMethod.Get)
                                        .AppendToRoute("api/example")
                                        .Build();

                    var proxyResponse = proxy.Invoke(proxyRequest);

                    if (proxyResponse.IsSuccessfulStatusCode)
                    {
                        // Let's see what the accept header looks like
                        var requestHeaders = proxyResponse.RequestHeaders;
                        var acceptHeader = requestHeaders.Accept;

                        Console.WriteLine("The Accept Header Value:");
                        Console.WriteLine($"\t{acceptHeader}");

                        Console.WriteLine();

                        // Show the API response
                        var valuesArray = proxyResponse.ResponseDto;
                        WriteValues(valuesArray);
                    }
                }

                #endregion

                Console.WriteLine("\n==========\n");

                #region Overriding a Default Request Header

                /*
                 * But what if we want application/xml as the default, but for a certain request we want application/json?
                 * Great question, you can use the ProxyRequest to override the ProxyConfiguration's default header values.
                 */
                proxyConfiguration = ProxyConfigurationBuilder.CreateBuilder()
                                                              // Setting the default Accept header to "application/xml"
                                                              .UseDefaultAcceptHeader(new MediaTypeWithQualityHeaderValue("application/xml"))
                                                              .Build();

                using (var proxy = new Proxy(proxyConfiguration))
                {
                    var proxyRequest = ProxyRequestBuilder<string[]>
                                        .CreateBuilder(baseUri, HttpRequestMethod.Get)
                                        .AppendToRoute("api/example")
                                        // Override the default Accept header value
                                        .Accept("application/json")
                                        .Build();

                    var proxyResponse = proxy.Invoke(proxyRequest);

                    if (proxyResponse.IsSuccessfulStatusCode)
                    {
                        // Let's see what the accept header looks like
                        var requestHeaders = proxyResponse.RequestHeaders;
                        var acceptHeader = requestHeaders.Accept;

                        Console.WriteLine("The Overriden Accept Header Value:");
                        Console.WriteLine($"\t{acceptHeader}");

                        Console.WriteLine();

                        // Show the API response
                        var valuesArray = proxyResponse.ResponseDto;
                        WriteValues(valuesArray);
                    }
                }

                #endregion

                Console.WriteLine("\n==========\n");

                #region Injecting Delegating Handlers

                /*
                 * Another incredibly awesome thing we can do is inject a DelegatingHandler into the
                 * request pipeline of the Proxy. A DelegatingHandler sits between your request and
                 * the actual request out to the RESTful service. Think of it like a translator where
                 * you can give the translator a message, they can manipulate the message and send it
                 * to someone else. One really awesome thing you can do with Delegating Handlers is
                 * insert logging into your request pipeline!
                 */

                proxyConfiguration = ProxyConfigurationBuilder
                                        .CreateBuilder()
                                        // Inject our LoggingDelegatingHandler
                                        .AddDelegatingHandlers(new LoggingDelegatingHandler())
                                        .UseDefaultAcceptHeader(new MediaTypeWithQualityHeaderValue("application/json"))
                                        .Build();

                using (var proxy = new Proxy(proxyConfiguration))
                {
                    var proxyRequest = ProxyRequestBuilder<string[]>
                                        .CreateBuilder(baseUri, HttpRequestMethod.Get)
                                        .AppendToRoute("api/example")
                                        .Build();

                    var proxyResponse = proxy.Invoke(proxyRequest);

                    if (proxyResponse.IsSuccessfulStatusCode)
                    {
                        var valuesArray = proxyResponse.ResponseDto;
                        WriteValues(valuesArray);
                    }
                }

                #endregion
            }

            Console.ReadKey();
        }

        private static void WriteValues(string[] values)
        {
            Console.WriteLine("Values from the API:");

            foreach (var value in values)
            {
                Console.WriteLine($"\t{value}");
            }
        }
    }
}

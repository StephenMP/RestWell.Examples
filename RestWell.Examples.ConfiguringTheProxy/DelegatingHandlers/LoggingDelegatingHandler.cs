using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestWell.Examples.ConfiguringTheProxy.DelegatingHandlers
{
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        #region Protected Methods

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(LoggingDelegatingHandler)} Picked Up Request:");
            Console.WriteLine($"\tRequest Method: {request.Method.Method}");
            Console.WriteLine($"\tAccept Header: {request.Headers.Accept}");
            Console.WriteLine($"\tRequest URI: {request.RequestUri}");
            Console.WriteLine();

            return await base.SendAsync(request, cancellationToken);
        }

        #endregion Protected Methods
    }
}

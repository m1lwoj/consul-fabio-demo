using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common
{
    public class HttpMessageHandler : DelegatingHandler
    {
        private readonly ILogger _logger = ApplicationLogging.CreateLogger<HttpMessageHandler>();
        private readonly IOptions<FabioOptions> _options;
        private readonly string _servicePath;

        public HttpMessageHandler(IOptions<FabioOptions> options, string serviceName)
        {
            _options = options;
            _servicePath = serviceName;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.RequestUri = new Uri($"{_options.Value.Address}/{_servicePath}/{request.RequestUri.Host}{request.RequestUri.PathAndQuery}");
           
            _logger.LogDebug($"Modified uri: {request.RequestUri.AbsoluteUri}");
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
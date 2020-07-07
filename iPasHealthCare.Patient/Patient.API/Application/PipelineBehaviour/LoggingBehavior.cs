using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Patient.API.Application.PipelineBehaviour
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {            
           
            TResponse response;

            try
            {
                _logger.LogInformation($"[REQUEST-PROPS] {JsonSerializer.Serialize(request)}");

                response = await next();

                _logger.LogInformation($"[RESPONSE-PROPS] {JsonSerializer.Serialize(response)}");
            }
            finally
            {
                
            }
            return response;
        }
    }
}

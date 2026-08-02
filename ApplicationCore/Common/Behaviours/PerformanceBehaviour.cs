using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ApplicationCore.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                //var userId = _user.Id ?? string.Empty;
                //var userName = string.Empty;

                _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", requestName, elapsedMilliseconds, request);

            }

            return response;

        }

    }
}

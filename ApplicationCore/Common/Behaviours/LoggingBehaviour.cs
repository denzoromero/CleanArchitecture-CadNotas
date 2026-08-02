using ApplicationCore.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;
        private readonly IUser _user;

        public LoggingBehaviour(ILogger<TRequest> logger, IUser user)
        {
            _logger = logger;
            _user = user;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _user.Id ?? string.Empty;

            _logger.LogInformation("Request: {Name} - {@Request}", requestName, request);
        }
    }
}

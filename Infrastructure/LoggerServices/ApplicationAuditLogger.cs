using ApplicationCore.Common.DTO;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.LoggerServices
{
    public class ApplicationAuditLogger(ILogger<ApplicationAuditLogger> logger, IUser user) : IAuditLogger
    {
        private readonly ILogger<ApplicationAuditLogger> _logger = logger;
        private readonly IUser _user = user;

        public Task LogAsync(AuditEntry entry)
        {

            _logger.LogInformation("Entity: {Entity}, Action: {Action} User> {UserId}",entry.EntityName,entry.Action, _user.Id);
            return Task.CompletedTask;
        }

    }
}

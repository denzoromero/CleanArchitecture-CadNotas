using ApplicationCore.Interfaces;
using ApplicationCore.Common.DTO;
using Infrastructure.LoggerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interceptors
{
    public class AuditInterceptor(IAuditLogger logger) : SaveChangesInterceptor
    {
        private readonly IAuditLogger _logger = logger;

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,InterceptionResult<int> result,CancellationToken cancellationToken = default)
        {
            //Console.WriteLine($"SavingChangesAsync - {DateTime.Now:HH:mm:ss.fff}");
            //Console.WriteLine($"Context: {eventData.Context?.GetHashCode()}");
            await LogAction(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        private async Task LogAction(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        await _logger.LogAsync(new AuditEntry(entry.Entity.GetType().Name, "Added"));
                        break;

                    case EntityState.Modified:
                        await _logger.LogAsync(new AuditEntry(entry.Entity.GetType().Name, "Modified"));
                        break;

                    case EntityState.Deleted:
                        await _logger.LogAsync(new AuditEntry(entry.Entity.GetType().Name, "Deleted"));
                        break;
                }
            }

        }


    }
}

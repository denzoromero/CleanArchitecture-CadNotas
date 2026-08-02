using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IIdempotencyRepository
    {
        Task CreateProcessingAsync(Guid key, string requestName, CancellationToken ct);
        Task MarkCompletedAsync(Guid key, int entityId, CancellationToken ct);
        Task MarkFailedAsync(Guid key, CancellationToken ct);
        Task<bool> ExistsAsync(Guid key, CancellationToken ct);
    }
}

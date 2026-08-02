using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.DataCad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.IdempotencyServices
{
    public class IdempotencyRepository : IIdempotencyRepository
    {
        private readonly ContextCad _context;
        private readonly IUser _user;

        public IdempotencyRepository(ContextCad context, IUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task CreateProcessingAsync(Guid key, string requestName, CancellationToken ct)
        {
            if (await ExistsAsync(key, ct))
            {
                throw new ValidationException([new ValidationFailure(nameof(key), "This request has already been processed.")]);
            }


            var request = new IdempotencyRequest
            {
                IdempotencyKey = key,
                RequestName = requestName,
                UserId = _user.UserId,
                Status = IdempotencyStatus.Processing,
                CreatedAt = DateTime.UtcNow
            };

            _context.IdempotencyRequests.Add(request);

    

            await _context.SaveChangesAsync(ct);

        }

        public async Task MarkCompletedAsync(Guid key, int entityId, CancellationToken ct)
        {
            var request = await _context.IdempotencyRequests.FirstOrDefaultAsync(x => x.IdempotencyKey == key, ct);

            if (request == null)
                return;

            request.EntityId = entityId;
            request.Status = IdempotencyStatus.Completed;
            request.CompletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        public async Task MarkFailedAsync(Guid key, CancellationToken ct)
        {
            var request = await _context.IdempotencyRequests.FirstOrDefaultAsync(x => x.IdempotencyKey == key, ct);

            if (request == null)
                return;

            request.Status = IdempotencyStatus.Failed;

            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> ExistsAsync(Guid key, CancellationToken ct)
        {
            return await _context.IdempotencyRequests.AnyAsync(x => x.IdempotencyKey == key, ct);
        }
    }
}

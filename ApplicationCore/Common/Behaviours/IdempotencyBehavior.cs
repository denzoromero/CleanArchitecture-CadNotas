using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Common.Behaviours
{
    public class IdempotencyBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IIdempotencyRepository _repository;

        public IdempotencyBehavior(IIdempotencyRepository repository)
        {
            _repository = repository;
        }

        public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next,CancellationToken cancellationToken)
        {
            if (request is not IIdempotentRequest idempotentRequest)
            {
                return await next();
            }

            var requestName = typeof(TRequest).Name;

            try
            {
                await _repository.CreateProcessingAsync(idempotentRequest.IdempotencyKey, requestName,cancellationToken);

                var response = await next();

                if (response is Result<int> result)
                {
                    await _repository.MarkCompletedAsync(idempotentRequest.IdempotencyKey, result.Value, cancellationToken);
                }

                return response;
            }
            catch (DbUpdateException)
            {
                throw new ValidationException([new ValidationFailure(nameof(idempotentRequest.IdempotencyKey),"This request has already been processed.")]);
            }
            catch
            {
                await _repository.MarkFailedAsync(idempotentRequest.IdempotencyKey, cancellationToken);
                throw;
            }
        }
    }
}

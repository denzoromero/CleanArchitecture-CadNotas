using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using Domain.Enums;
using FluentAssertions;
using FluentValidation;
using Infrastructure.DataCad;
using Infrastructure.IdempotencyServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureIntegrationTests.IdempotencyTests
{
    public class IdempotencyRepositoryTests : IClassFixture<TestCadNotasFixture>
    {
        private readonly ContextCad _context;
        private readonly Mock<IUser> _userMock;

        public IdempotencyRepositoryTests(TestCadNotasFixture fixture)
        {
            _context = fixture.ContextCad;
            _userMock = new Mock<IUser>();
            _userMock.Setup(x => x.UserId).Returns(1);
        }

        [Fact]
        public async Task CreateProcessingAsync_Should_Create_Request()
        {
            // Arrange
            var repository = new IdempotencyRepository(_context, _userMock.Object);

            var key = Guid.NewGuid();

            // Act
            await repository.CreateProcessingAsync(key,"CreateFornecedor",CancellationToken.None);

            // Assert
            var request = await _context.IdempotencyRequests.FirstOrDefaultAsync(x => x.IdempotencyKey == key);

            request.Should().NotBeNull();

            request!.RequestName.Should().Be("CreateFornecedor");

            request.Status.Should().Be(IdempotencyStatus.Processing);

            request.UserId.Should().Be(1);
        }

        [Fact]
        public async Task ExistsAsync_Should_Return_True_When_Request_Exists()
        {
            // Arrange
            var repository = new IdempotencyRepository(_context,_userMock.Object);

            var key = Guid.NewGuid();

            _context.IdempotencyRequests.Add(new IdempotencyRequest
                {
                    IdempotencyKey = key
                });

            await _context.SaveChangesAsync();

            // Act
            var exists = await repository.ExistsAsync( key,CancellationToken.None);

            // Assert
            exists.Should().BeTrue();
        }

        [Fact]
        public async Task CreateProcessingAsync_Should_Throw_When_Key_Already_Exists()
        {
            // Arrange
            var repository = new IdempotencyRepository(
                _context,
                _userMock.Object);

            var key = Guid.NewGuid();

            await repository.CreateProcessingAsync(
                key,
                "CreateFornecedor",
                CancellationToken.None);

            // Act
            Func<Task> act = () => repository.CreateProcessingAsync(
                key,
                "CreateFornecedor",
                CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task MarkCompletedAsync_Should_Update_Request()
        {
            // Arrange
            var repository = new IdempotencyRepository(
                _context,
                _userMock.Object);

            var key = Guid.NewGuid();

            await repository.CreateProcessingAsync(
                key,
                "CreateFornecedor",
                CancellationToken.None);

            // Act
            await repository.MarkCompletedAsync(
                key,
                123,
                CancellationToken.None);

            // Assert
            var request = await _context.IdempotencyRequests
                .FirstAsync(x => x.IdempotencyKey == key);

            request.Status.Should()
                .Be(IdempotencyStatus.Completed);

            request.EntityId.Should()
                .Be(123);

            request.CompletedAt.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task MarkFailedAsync_Should_Update_Status()
        {
            // Arrange
            var repository = new IdempotencyRepository(
                _context,
                _userMock.Object);

            var key = Guid.NewGuid();

            await repository.CreateProcessingAsync(
                key,
                "CreateFornecedor",
                CancellationToken.None);

            // Act
            await repository.MarkFailedAsync(
                key,
                CancellationToken.None);

            // Assert
            var request = await _context.IdempotencyRequests
                .FirstAsync(x => x.IdempotencyKey == key);

            request.Status.Should()
                .Be(IdempotencyStatus.Failed);
        }

    }
}

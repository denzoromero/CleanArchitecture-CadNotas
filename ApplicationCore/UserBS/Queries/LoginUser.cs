using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using ApplicationCore.UserBS.Specifications;
using Domain.Entities.EntitiesBS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ApplicationCore.UserBS.Queries
{
    public record class LoginUser : IRequest<Result>
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUser, Result>
    {

        private readonly IRepositoryBS<UsuarioBS> _readRepository;
        private readonly IAuthService _auth;

        public LoginUserQueryHandler(IRepositoryBS<UsuarioBS> repos, IAuthService auth)
        {
            _readRepository = repos;
            _auth = auth;
        }

        public async Task<Result> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await _readRepository.FirstOrDefaultAsync(new UserBSSpecification(request.Username), cancellationToken);
            if (user == null) return Result.Failure(new Error("AUTH001", "User Not Found"));
            if (user.Ativo == 0) return Result.Failure(new Error("AUTH002", "User Not Active"));

            await _auth.SignInAsync(user);

            return Result.Success();
        }
    }


}

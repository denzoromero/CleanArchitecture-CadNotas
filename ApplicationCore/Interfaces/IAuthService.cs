using Domain.Entities.EntitiesBS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IAuthService
    {
        Task SignInAsync(UsuarioBS user);
    }
}

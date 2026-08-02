using Domain.Entities.EntitiesBS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IUser
    {
        string? Id { get; }

        int UserId { get; }
    }
}

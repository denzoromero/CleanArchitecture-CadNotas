using ApplicationCore.UserBS.Queries;
using Ardalis.Specification;
using Domain.Entities.EntitiesBS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.UserBS.Specifications
{
    public class UserBSSpecification : Specification<UsuarioBS>
    {
        public UserBSSpecification(string username)
        {
            Query.AsNoTracking().Where(x => x.Email == username);
        }
    }

}

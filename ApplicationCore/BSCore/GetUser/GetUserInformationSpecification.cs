using Ardalis.Specification;
using Domain.Entities.EntitiesBS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.BSCore.GetUser
{
    public class GetUserInformationSpecification : Specification<UsuarioBS, string>
    {
        public GetUserInformationSpecification(int IdUser) 
        {
            Query.AsNoTracking().Where(i => i.Id == IdUser).Select(i => i.Chapa);
        }
    }
}

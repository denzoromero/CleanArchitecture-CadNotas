using Domain.Entities.EntitiesBS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IContextBS
    {
        DbSet<Estado> Estados { get; }
        DbSet<Cidade> Cidades { get; }
    }
}

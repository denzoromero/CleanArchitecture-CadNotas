using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Domain;
using Infrastructure.DataCad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class EfRepositoryCad<T> : RepositoryBase<T>, IRepositoryCad<T> where T : class, IAggregateRoot
    {
        public EfRepositoryCad(ContextCad contextCad) : base(contextCad)
        {

        }
    }
}

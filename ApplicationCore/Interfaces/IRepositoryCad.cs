using Ardalis.Specification;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IRepositoryCad<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}

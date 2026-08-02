using Ardalis.Specification;
using Domain;

namespace ApplicationCore.Interfaces
{
    public interface IRepositoryBS<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    { }
}

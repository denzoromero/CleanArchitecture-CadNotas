using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Domain;
using Infrastructure.DataBS;

namespace Infrastructure
{
    public class EfRepository<T> : RepositoryBase<T>, IRepositoryBS<T> where T : class, IAggregateRoot
    {
        public EfRepository(ContextBS dbContext) : base(dbContext)
        {
        }
    }
}
    
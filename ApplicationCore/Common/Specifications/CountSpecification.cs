using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Common.Specifications
{
    public class CountSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        public CountSpecification(Expression<Func<TEntity, bool>> predicate)
        {
            Query.Where(predicate);
        }
    }
}

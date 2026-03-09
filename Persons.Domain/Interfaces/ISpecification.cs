using System;
using System.Linq.Expressions;

namespace Persons.Domain.Interfaces
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}

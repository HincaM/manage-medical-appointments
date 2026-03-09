using System;
using System.Linq.Expressions;

namespace Prescriptions.Domain.Interfaces
{
    public abstract class Specification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
    }
}

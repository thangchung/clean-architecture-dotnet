using System;
using System.Linq.Expressions;

namespace N8T.Core.Specification
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        private Func<T, bool> _compiledExpression;
 
        private Func<T, bool> CompiledExpression
        {
            get { return _compiledExpression ??= SpecExpression.Compile(); }
        }
 
        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }
    }
}

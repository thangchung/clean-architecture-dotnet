using System;
using System.Linq.Expressions;

namespace N8T.Core.Specification
{
    public class Or<T> : SpecificationBase<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public Or(
            ISpecification<T> left,
            ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        // OrSpecification
        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(
                        Expression.Invoke(_left.SpecExpression, objParam),
                        Expression.Invoke(_right.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}

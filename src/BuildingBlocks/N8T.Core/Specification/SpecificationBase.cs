using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace N8T.Core.Specification
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public List<string> IncludeStrings { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression) =>
            OrderBy = orderByExpression;

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) =>
            OrderByDescending = orderByDescendingExpression;

        protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression) =>
            GroupBy = groupByExpression;

        protected void ApplySorting(string sort)
        {
            if (string.IsNullOrEmpty(sort)) return;

            const string descendingSuffix = "Desc";

            var descending = sort.EndsWith(descendingSuffix, StringComparison.Ordinal);
            var propertyName = sort.Substring(0, 1).ToUpperInvariant() +
                               sort.Substring(1, sort.Length - 1 - (descending ? descendingSuffix.Length : 0));

            var specificationType = GetType().BaseType;
            var targetType = specificationType?.GenericTypeArguments[0];
            var property = targetType!.GetRuntimeProperty(propertyName) ??
                           throw new InvalidOperationException($"Because the property {propertyName} does not exist it cannot be sorted.");

            var lambdaParamX = Expression.Parameter(targetType, "x");

            var propertyReturningExpression = Expression.Lambda(
                Expression.Convert(
                    Expression.Property(lambdaParamX, property),
                    typeof(object)),
                lambdaParamX);

            if (descending)
            {
                specificationType?.GetMethod(
                        nameof(ApplyOrderByDescending),
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    ?.Invoke(this, new object[]{propertyReturningExpression});
            }
            else
            {
                specificationType?.GetMethod(
                        nameof(ApplyOrderBy),
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    ?.Invoke(this, new object[]{propertyReturningExpression});
            }
        }

    }
}

namespace N8T.Core.Specification
{
    public static class Extensions
    {
        public static ISpecification<T> And<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            return new And<T>(left, right);
        }

        public static ISpecification<T> Or<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            return new Or<T>(left, right);
        }

        public static ISpecification<T> Negate<T>(this ISpecification<T> inner)
        {
            return new Negated<T>(inner);
        }
    }
}

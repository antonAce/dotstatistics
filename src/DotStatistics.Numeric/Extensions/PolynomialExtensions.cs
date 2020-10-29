using System;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Extensions
{
    public static class PolynomialExtensions
    {
        public static IPolynomial Add(this IPolynomial source, IPolynomial operand)
        {
            for (var i = Math.Min(source.Min, operand.Min); i < Math.Max(source.Max, operand.Max); i++)
                source[i] += operand[i];

            return source;
        }

        public static IPolynomial Subtract(this IPolynomial source, IPolynomial operand)
        {
            for (var i = Math.Min(source.Min, operand.Min); i < Math.Max(source.Max, operand.Max); i++)
                source[i] -= operand[i];

            return source;
        }
    }
}

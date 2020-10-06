using System;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Extensions
{
    public static class MatrixExtensions
    {
        public static IMatrix Add(this IMatrix source, IMatrix operand)
        {
            if (source.Height != operand.Height || source.Width != operand.Width)
                throw new InvalidOperationException("The operands' size doesn't match!");
            
            var result = source.Copy();

            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    result[i, j] += operand[i, j];
                }
            }

            return result;
        }
        
        public static IMatrix Subtract(this IMatrix source, IMatrix operand)
        {
            if (source.Height != operand.Height || source.Width != operand.Width)
                throw new InvalidOperationException("The operands' size doesn't match!");
            
            var result = source.Copy();

            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    result[i, j] -= operand[i, j];
                }
            }
            
            return result;
        }
        
        public static IMatrix Multiply(this IMatrix source, IMatrix operand)
        {
            if (source.Height != operand.Height || source.Width != operand.Width)
                throw new InvalidOperationException("The operands' size doesn't match!");
            
            var result = source.Copy();

            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    result[i, j] *= operand[i, j];
                }
            }

            return result;
        }
        
        public static IMatrix Multiply(this IMatrix source, double scalar)
        {
            var result = source.Copy();
            
            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    result[i, j] *= scalar;
                }
            }

            return result;
        }
        
        public static IMatrix Dot(this IMatrix source, IMatrix operand)
        {
            if (source.Width != operand.Height)
                throw new InvalidOperationException(
                    "Rows amount of first operand must match columns amount of second!");

            var result = source.Copy();

            for (int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    double elementByproduct = 0;

                    for (int k = 0; k < source.Width; k++)
                        elementByproduct += source[i, k] * operand[k, j];

                    result[i, j] = elementByproduct;
                }
            }

            return result;
        }
    }
}
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Extensions
{
    public static class ConversionExtensions
    {
        public static double[,] ToArray(this IMatrix matrix)
        {
            var result = new double[matrix.Height, matrix.Width];

            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }

            return result;
        }
    }
}

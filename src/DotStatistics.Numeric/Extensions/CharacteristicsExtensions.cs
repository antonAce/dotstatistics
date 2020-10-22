using System.Linq;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Extensions
{
    public static class CharacteristicsExtensions
    {
        public static IMatrix Covariance(this IMatrix source)
        {
            var covariance = Matrix.Zeros(source.Height, source.Width);

            for (var i = 0; i < covariance.Height; i++)
            {
                for (var j = 0; j < covariance.Width; j++)
                {
                    var iMean = Enumerable.Range(0, source.Height)
                        .Sum(k => source[k, i]) / covariance.Height;

                    var jMean = Enumerable.Range(0, source.Height)
                        .Sum(k => source[k, j]) / covariance.Height;

                    covariance[i, j] = Enumerable.Range(0, source.Height)
                        .Sum(k => (source[k, i] - iMean) * (source[k, j] - jMean)) / covariance.Height;
                }
            }

            return covariance;
        }
    }
}

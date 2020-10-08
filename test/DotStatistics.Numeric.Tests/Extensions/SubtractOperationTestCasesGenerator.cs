using System.Collections.Generic;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Tests.Extensions
{
    public class SubtractOperationTestCasesGenerator
    {
        public static IEnumerable<object[]> GetSubtractOperationPositiveTestCases()
        {
            yield return new object[]
            {
                new Matrix(new double[,] {{0, 0}, {0, 0}}),
                new Matrix(new double[,] {{1, 1}, {1, 1}}),
                new Matrix(new double[,] {{-1, -1}, {-1, -1}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 2}, {2, 3}, {3, 4}}),
                new Matrix(new double[,] {{1, 1}, {1, 1}, {1, 1}}),
                new Matrix(new double[,] {{0, 1}, {1, 2}, {2, 3}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{-1, -2, 0}, {2, -3, 2}, {3, 4, 1}}),
                new Matrix(new double[,] {{2, 5, 12}, {0, 5, 1}, {1, 1, 4}}),
                new Matrix(new double[,] {{-3, -7, -12}, {2, -8, 1}, {2, 3, -3}})
            };
        }

        public static IEnumerable<object[]> GetSubtractOperationNegativeTestCases()
        {
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 0}}),
                new Matrix(new double[,] {{1, 1}, {1, 1}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 0}, {0, 1}}),
                new Matrix(new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 0}})
            };
        }
    }
}
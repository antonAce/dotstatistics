using System.Collections.Generic;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Tests.Extensions
{
    public class DotOperationTestCasesGenerator
    {
        public static IEnumerable<object[]> GetDotOperationPositiveTestCases()
        {
            yield return new object[]
            {
                new Matrix(new double[,] {{2, 2, -1}, {0, -2, -1}, {0, 0, 5}}),
                new Matrix(new double[,] {{2}, {2}, {3}}),
                new Matrix(new double[,] {{5}, {-7}, {15}})
            };

            yield return new object[]
            {
                new Matrix(new double[,] {{5, 8, -4}, {6, 9, -5}, {4, 7, -3}}),
                new Matrix(new double[,] {{3, 2, 5}, {4, -1, 3}, {9, 6, 5}}),
                new Matrix(new double[,] {{11, -22, 29}, {9, -27, 32}, {13, -17, 26}})
            };
        }
        
        public static IEnumerable<object[]> GetDotOperationNegativeTestCases()
        {
            yield return new object[]
            {
                new Matrix(new double[,] {{3}, {-1}}),
                new Matrix(new double[,] {{-2, 1}, {5, 4}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{3, -1, 0}, {5, 4, 0}}),
                new Matrix(new double[,] {{-2, 1}, {5, 4}})
            };
        }
    }
}
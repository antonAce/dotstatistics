using System.Collections.Generic;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Tests.Extensions
{
    public class AddOperationTestCasesGenerator
    {
        public static IEnumerable<object[]> GetAddOperationPositiveTestCases()
        {
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 0}, {0, 1}}),
                new Matrix(new double[,] {{1, 1}, {1, 1}}),
                new Matrix(new double[,] {{2, 1}, {1, 2}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 2}, {2, 3}, {3, 4}}),
                new Matrix(new double[,] {{1, 1}, {1, 1}, {1, 1}}),
                new Matrix(new double[,] {{2, 3}, {3, 4}, {4, 5}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{-1, -2, 0}, {2, -3, 2}, {3, 4, 1}}),
                new Matrix(new double[,] {{2, 5, 12}, {0, 5, 1}, {1, 1, 4}}),
                new Matrix(new double[,] {{1, 3, 12}, {2, 2, 3}, {4, 5, 5}})
            };
        }
    }
}
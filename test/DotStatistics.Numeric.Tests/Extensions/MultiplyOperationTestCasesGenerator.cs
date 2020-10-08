using System.Collections.Generic;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Tests.Extensions
{
    public class MultiplyOperationTestCasesGenerator
    {
        public static IEnumerable<object[]> GetMultiplyByMatrixOperationPositiveTestCases()
        {
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 5}, {3, 4}}),
                new Matrix(new double[,] {{2, 2}, {2, 2}}),
                new Matrix(new double[,] {{2, 10}, {6, 8}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 2}, {2, 3}, {3, 4}}),
                new Matrix(new double[,] {{1, 1}, {1, 1}, {1, 1}}),
                new Matrix(new double[,] {{1, 2}, {2, 3}, {3, 4}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{-1, -2, 0}, {2, -3, 2}, {3, 4, 1}}),
                new Matrix(new double[,] {{2, -5, 12}, {0, 5, 1}, {1, 1, 4}}),
                new Matrix(new double[,] {{-2, 10, 0}, {0, -15, 2}, {3, 4, 4}})
            };
        }
        
        public static IEnumerable<object[]> GetMultiplyByMatrixOperationNegativeTestCases()
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
        
        public static IEnumerable<object[]> GetMultiplyByScalarOperationTestCases()
        {
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 2, 3}, {3, 4, 5}, {5, 6, 7}}),
                3,
                new Matrix(new double[,] {{3, 6, 9}, {9, 12, 15}, {15, 18, 21}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 2, 3}, {3, 4, 5}, {5, 6, 7}}),
                -1,
                new Matrix(new double[,] {{-1, -2, -3}, {-3, -4, -5}, {-5, -6, -7}})
            };
            
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 2, 3}, {3, 4, 5}, {5, 6, 7}}),
                0,
                new Matrix(new double[,] {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}})
            };
        }
    }
}
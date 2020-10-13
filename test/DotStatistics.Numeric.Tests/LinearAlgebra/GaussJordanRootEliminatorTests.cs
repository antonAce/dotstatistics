using System;
using System.Collections.Generic;
using Xunit;
using DotStatistics.Numeric.LinearAlgebra;
using DotStatistics.Numeric.Primitives;
using FluentAssertions;

namespace DotStatistics.Numeric.Tests.LinearAlgebra
{
    public class GaussJordanRootEliminatorTests
    {
        private readonly double _epsilon = Math.Pow(10, -6);
        private readonly IRootEliminator _eliminator;

        public GaussJordanRootEliminatorTests() =>
            _eliminator = new GaussJordanRootEliminator();

        public static IEnumerable<object[]> GaussJordanRootEliminatorTestCases()
        {
            yield return new object[]
            {
                new Matrix(new double[,] {{2, 1, -1}, {-3, -1, 2}, {-2, 1, 2}}),
                new Matrix(new double[,] {{8}, {-11}, {-3}}),
                new Matrix(new double[,] {{2}, {3}, {-1}})
            };
            yield return new object[]
            {
                new Matrix(new double[,] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}}),
                new Matrix(new double[,] {{1}, {2}, {3}}),
                new Matrix(new double[,] {{1}, {2}, {3}})
            };
            yield return new object[]
            {
                new Matrix(new[,] {{1.0, 2.0, -3.0, -1.0}, {0.0, -3.0, 2.0, 6.0}, {0.0, 5.0, -6.0, -2.0}, {0.0, -1.0, 8.0, 1.0}}),
                new Matrix(new[,] {{0.0}, {-8.0}, {0.0}, {-8.0}}),
                new Matrix(new[,] {{-1.0}, {-2.0}, {-1.0}, {-2.0}})
            };
        }

        [Theory]
        [MemberData(
            nameof(GaussJordanRootEliminatorTestCases),
            MemberType = typeof(GaussJordanRootEliminatorTests))]
        public void AddOperation_PositiveTestCases(IMatrix coefficients, IMatrix outputs, IMatrix roots) =>
            ColumnVectorApproximatelyEqual(_eliminator.Eliminate(coefficients, outputs), roots);

        private void ColumnVectorApproximatelyEqual(IMatrix expected, IMatrix actual)
        {
            expected.Height.Should().Be(actual.Height);
            
            for (int i = 0; i < expected.Height; i++)
            {
                expected[i, 0].Should().BeApproximately(actual[i, 0], _epsilon);
            }
        }
    }
}
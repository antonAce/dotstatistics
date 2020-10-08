using System;
using System.Linq;
using Xunit;
using DotStatistics.Numeric.Extensions;
using DotStatistics.Numeric.Primitives;

namespace DotStatistics.Numeric.Tests.Extensions
{
    public class MatrixExtensionsTests
    {
        [Theory]
        [MemberData(
            nameof(AddOperationTestCasesGenerator.GetAddOperationPositiveTestCases),
            MemberType = typeof(AddOperationTestCasesGenerator))]
        public void AddOperation_PositiveTestCases(IMatrix leftOperand, IMatrix rightOperand, IMatrix result) =>
            Assert.True(MatricesAreEqual(leftOperand.Add(rightOperand), result));
        
        [Theory]
        [MemberData(
            nameof(AddOperationTestCasesGenerator.GetAddOperationNegativeTestCases),
            MemberType = typeof(AddOperationTestCasesGenerator))]
        public void AddOperation_NegativeTestCases(IMatrix leftOperand, IMatrix rightOperand) =>
            Assert.Throws<InvalidOperationException>(() => leftOperand.Add(rightOperand));
        
        [Theory]
        [MemberData(
            nameof(SubtractOperationTestCasesGenerator.GetSubtractOperationPositiveTestCases),
            MemberType = typeof(SubtractOperationTestCasesGenerator))]
        public void SubtractOperation_PositiveTestCases(IMatrix leftOperand, IMatrix rightOperand, IMatrix result) =>
            Assert.True(MatricesAreEqual(leftOperand.Subtract(rightOperand), result));
        
        [Theory]
        [MemberData(
            nameof(SubtractOperationTestCasesGenerator.GetSubtractOperationNegativeTestCases),
            MemberType = typeof(SubtractOperationTestCasesGenerator))]
        public void SubtractOperation_NegativeTestCases(IMatrix leftOperand, IMatrix rightOperand) =>
            Assert.Throws<InvalidOperationException>(() => leftOperand.Subtract(rightOperand));

        private bool MatricesAreEqual(IMatrix expected, IMatrix actual) =>
            Enumerable.Range(0, expected.Height - 1)
                .Join(Enumerable.Range(0, expected.Width - 1), i => i, j => j, (i, j) => new {i, j})
                .All(_ => Math.Abs(expected[_.i, _.j] - actual[_.i, _.j]) <= Double.Epsilon);
    }
}
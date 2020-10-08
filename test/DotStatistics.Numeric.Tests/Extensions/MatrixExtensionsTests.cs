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
        
        [Theory]
        [MemberData(
            nameof(MultiplyOperationTestCasesGenerator.GetMultiplyByMatrixOperationPositiveTestCases),
            MemberType = typeof(MultiplyOperationTestCasesGenerator))]
        public void MultiplyByMatrixOperation_PositiveTestCases(IMatrix leftOperand, IMatrix rightOperand, IMatrix result) =>
            Assert.True(MatricesAreEqual(leftOperand.Multiply(rightOperand), result));
        
        [Theory]
        [MemberData(
            nameof(MultiplyOperationTestCasesGenerator.GetMultiplyByMatrixOperationNegativeTestCases),
            MemberType = typeof(MultiplyOperationTestCasesGenerator))]
        public void MultiplyByMatrixOperation_NegativeTestCases(IMatrix leftOperand, IMatrix rightOperand) =>
            Assert.Throws<InvalidOperationException>(() => leftOperand.Multiply(rightOperand));
        
        [Theory]
        [MemberData(
            nameof(MultiplyOperationTestCasesGenerator.GetMultiplyByScalarOperationTestCases),
            MemberType = typeof(MultiplyOperationTestCasesGenerator))]
        public void MultiplyByScalarOperation_PositiveTestCases(IMatrix operand, double scalar, IMatrix result) =>
            Assert.True(MatricesAreEqual(operand.Multiply(scalar), result));
        
        [Theory]
        [MemberData(
            nameof(DotOperationTestCasesGenerator.GetDotOperationPositiveTestCases),
            MemberType = typeof(DotOperationTestCasesGenerator))]
        public void DotOperation_PositiveTestCases(IMatrix leftOperand, IMatrix rightOperand, IMatrix result) =>
            Assert.True(MatricesAreEqual(leftOperand.Dot(rightOperand), result));
        
        [Theory]
        [MemberData(
            nameof(DotOperationTestCasesGenerator.GetDotOperationNegativeTestCases),
            MemberType = typeof(DotOperationTestCasesGenerator))]
        public void DotOperation_NegativeTestCases(IMatrix leftOperand, IMatrix rightOperand) =>
            Assert.Throws<InvalidOperationException>(() => leftOperand.Dot(rightOperand));

        private bool MatricesAreEqual(IMatrix expected, IMatrix actual) =>
            Enumerable.Range(0, expected.Height - 1)
                .Join(Enumerable.Range(0, expected.Width - 1), i => i, j => j, (i, j) => new {i, j})
                .All(_ => Math.Abs(expected[_.i, _.j] - actual[_.i, _.j]) <= Double.Epsilon);
    }
}
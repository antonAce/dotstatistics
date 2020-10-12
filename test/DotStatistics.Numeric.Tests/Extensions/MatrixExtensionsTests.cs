using System;
using System.Linq;
using Xunit;
using FluentAssertions;
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
            MatricesAreEqual(leftOperand.Add(rightOperand), result).Should().BeTrue();

        [Theory]
        [MemberData(
            nameof(AddOperationTestCasesGenerator.GetAddOperationNegativeTestCases),
            MemberType = typeof(AddOperationTestCasesGenerator))]
        public void AddOperation_NegativeTestCases(IMatrix leftOperand, IMatrix rightOperand) =>
            new Action(() => leftOperand.Add(rightOperand)).Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage("The operands' size doesn't match.");

        [Theory]
        [MemberData(
            nameof(SubtractOperationTestCasesGenerator.GetSubtractOperationPositiveTestCases),
            MemberType = typeof(SubtractOperationTestCasesGenerator))]
        public void SubtractOperation_PositiveTestCases(IMatrix leftOperand, IMatrix rightOperand, IMatrix result) =>
            MatricesAreEqual(leftOperand.Subtract(rightOperand), result).Should().BeTrue();
        
        [Theory]
        [MemberData(
            nameof(SubtractOperationTestCasesGenerator.GetSubtractOperationNegativeTestCases),
            MemberType = typeof(SubtractOperationTestCasesGenerator))]
        public void SubtractOperation_NegativeTestCases(IMatrix leftOperand, IMatrix rightOperand) =>
            new Action(() => leftOperand.Subtract(rightOperand)).Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage("The operands' size doesn't match.");
        
        [Theory]
        [MemberData(
            nameof(MultiplyOperationTestCasesGenerator.GetMultiplyByMatrixOperationPositiveTestCases),
            MemberType = typeof(MultiplyOperationTestCasesGenerator))]
        public void MultiplyByMatrixOperation_PositiveTestCases(IMatrix leftOperand, IMatrix rightOperand, IMatrix result) =>
            MatricesAreEqual(leftOperand.Multiply(rightOperand), result).Should().BeTrue();
        
        [Theory]
        [MemberData(
            nameof(MultiplyOperationTestCasesGenerator.GetMultiplyByMatrixOperationNegativeTestCases),
            MemberType = typeof(MultiplyOperationTestCasesGenerator))]
        public void MultiplyByMatrixOperation_NegativeTestCases(IMatrix leftOperand, IMatrix rightOperand) =>
            new Action(() => leftOperand.Multiply(rightOperand)).Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage("The operands' size doesn't match.");
        
        [Theory]
        [MemberData(
            nameof(MultiplyOperationTestCasesGenerator.GetMultiplyByScalarOperationTestCases),
            MemberType = typeof(MultiplyOperationTestCasesGenerator))]
        public void MultiplyByScalarOperation_PositiveTestCases(IMatrix operand, double scalar, IMatrix result) =>
            MatricesAreEqual(operand.Multiply(scalar), result).Should().BeTrue();
        
        [Theory]
        [MemberData(
            nameof(DotOperationTestCasesGenerator.GetDotOperationPositiveTestCases),
            MemberType = typeof(DotOperationTestCasesGenerator))]
        public void DotOperation_PositiveTestCases(IMatrix leftOperand, IMatrix rightOperand, IMatrix result) =>
            MatricesAreEqual(leftOperand.Dot(rightOperand), result).Should().BeTrue();
        
        [Theory]
        [MemberData(
            nameof(DotOperationTestCasesGenerator.GetDotOperationNegativeTestCases),
            MemberType = typeof(DotOperationTestCasesGenerator))]
        public void DotOperation_NegativeTestCases(IMatrix leftOperand, IMatrix rightOperand) =>
            new Action(() => leftOperand.Dot(rightOperand)).Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage("Rows amount of first operand must match columns amount of second.");

        private bool MatricesAreEqual(IMatrix expected, IMatrix actual) =>
            Enumerable.Range(0, expected.Height - 1)
                .Join(Enumerable.Range(0, expected.Width - 1), i => i, j => j, (i, j) => new {i, j})
                .All(_ => Math.Abs(expected[_.i, _.j] - actual[_.i, _.j]) <= Double.Epsilon);
    }
}
using hw1;
using Xunit;

namespace Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(5, CalculatorOperation.Plus, 7, 12)]
        [InlineData(-5, CalculatorOperation.Plus, 7, 2)]
        [InlineData(-5, CalculatorOperation.Plus, -7, -12)]
        [InlineData(5, CalculatorOperation.Plus, -7, -2)]
        public void Calculate_Plus_WillReturnCorrectResult(int val1, CalculatorOperation operation, int val2, int expected)
        {
            var result = Calculator.Calculate(val1, operation, val2);
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(5, CalculatorOperation.Minus, 7, -2)]
        [InlineData(-5, CalculatorOperation.Minus, 7, -12)]
        [InlineData(-5, CalculatorOperation.Minus, -7, 2)]
        [InlineData(5, CalculatorOperation.Minus, -7, 12)]
        public void Calculate_Minus_WillReturnCorrectResult(int val1, CalculatorOperation operation, int val2, int expected)
        {
            var result = Calculator.Calculate(val1, operation, val2);
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(5, CalculatorOperation.Multiply, 7, 35)]
        [InlineData(-5, CalculatorOperation.Multiply, 7, -35)]
        [InlineData(-5, CalculatorOperation.Multiply, -7, 35)]
        [InlineData(5, CalculatorOperation.Multiply, -7, -35)]
        public void Calculate_Multiply_WillReturnCorrectResult(int val1, CalculatorOperation operation, int val2, int expected)
        {
            var result = Calculator.Calculate(val1, operation, val2);
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(10, CalculatorOperation.Divide, 5, 2)]
        [InlineData(-10, CalculatorOperation.Divide, 5, -2)]
        [InlineData(-10, CalculatorOperation.Divide, -5, 2)]
        [InlineData(10, CalculatorOperation.Divide, -5, -2)]
        [InlineData(7, CalculatorOperation.Divide, 2, 3.5)]
        public void Calculate_Divide_WillReturnCorrectResult(int val1, CalculatorOperation operation, int val2, double expected)
        {
            var result = Calculator.Calculate(val1, operation, val2);
            Assert.Equal(expected, result);
        }
    }
}
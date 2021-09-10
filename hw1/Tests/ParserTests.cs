using hw1;
using Xunit;

namespace Tests
{
    public class ParserTests
    {
        private const int Correct = 0;
        private const int WrongArgLength = 1;
        private const int WrongArgFormatInt = 2;
        private const int WrongArgFormatOperation = 3;
        
        [Theory]
        [InlineData("+", CalculatorOperation.Plus)]
        [InlineData("-", CalculatorOperation.Minus)]
        [InlineData("*", CalculatorOperation.Multiply)]
        [InlineData("/", CalculatorOperation.Divide)]
        public void ParseCalcArguments_Operation_WillParse(string operation, CalculatorOperation operationExpected)
        {
            var args = new[] { "4", operation, "777" };
            var check = Parser.ParseCalcArguments(args, out var val1, out var operationResult, out var val2);
            Assert.Equal(Correct, check);
            Assert.Equal(4, val1);
            Assert.Equal(operationExpected, operationResult);
            Assert.Equal(777, val2);
        }

        [Fact]
        public void ParseCalcArguments_NotNumber_WillReturnWrongArgFormatInt()
        {
            var args = new[] { "4", "+", "turn around" };
            var check = Parser.ParseCalcArguments(args, out _, out _, out _);
            Assert.Equal(WrongArgFormatInt, check);
        }
        
        [Fact]
        public void ParseCalcArguments_NotOperation_WillReturnWrongArgFormatOperation()
        {
            var args = new[] { "4", "turn around", "1337" };
            var check = Parser.ParseCalcArguments(args, out _, out _, out _);
            Assert.Equal(WrongArgFormatOperation, check);
        }

        [Fact]
        public void ParseCalcArguments_WrongLengthArgs_WillReturnWrongArgLength()
        {
            var args = new[] { "4", "+", "1337", "Timur" };
            var check = Parser.ParseCalcArguments(args, out _, out _, out _);
            Assert.Equal(WrongArgLength, check);
        }
    }
}
namespace hw1
{
    public static class Calculator
    {
        public static double Calculate(int val1, CalculatorOperation operation, int val2)
        {
            var result = operation switch
            {
                CalculatorOperation.Divide => (double) val1 / val2,
                CalculatorOperation.Multiply => val1 * val2,
                CalculatorOperation.Minus => val1 - val2,
                _ => val1 + val2,
            };
            
            return result;
        }
    }
}
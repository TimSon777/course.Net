using System;

namespace hw1
{
    public static class Parser
    {
        private const int WrongArgLength = 1;
        private const int WrongArgFormatInt = 2;
        private const int WrongArgFormatOperation = 3;

        public static int ParseCalcArguments(string[] args, out int val1, out CalculatorOperation operation, out int val2)
        {
            val1 = val2 = (int) (operation = 0);
            if (!CheckArgLength(args.Length)) return WrongArgLength;
            if (!TryParseIntOrQuit(args[0], out val1) || !TryParseIntOrQuit(args[2], out val2)) return WrongArgFormatInt;
            return !TryParseOperationOrQuit(args[1], out operation) ? WrongArgFormatOperation : 0;
        }
        
        private static bool CheckArgLength(int length)
        {
            if (length == 3) return true;
            Console.WriteLine($"The program requires 3 arguments, but was {length}");
            return false;
        }
        
        private static bool TryParseOperationOrQuit(string arg, out CalculatorOperation operation)
        {
            operation = arg switch
            {
                "-" => CalculatorOperation.Minus,
                "*" => CalculatorOperation.Multiply,
                "/" => CalculatorOperation.Divide,
                _ => CalculatorOperation.Plus
            };

            return operation != CalculatorOperation.Plus || arg == "+";
        }

        private static bool TryParseIntOrQuit(string arg, out int val)
        {
            if (int.TryParse(arg, out val)) return true;
            Console.WriteLine($"Value is not int: {arg}");
            return false;
        }
    }
}
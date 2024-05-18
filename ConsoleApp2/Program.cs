using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = "2/3+56*8"; // Пример входного выражения
            string output = ConvertToPolishNotation(input);
            Console.WriteLine("Исходное выражение: ");
            Console.WriteLine(input);
            Console.WriteLine("Постфиксная форма: ");
            Console.WriteLine(output); // Вывод постфиксной нотации
        }

        public static string ConvertToPolishNotation(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentException("Выражение не должно быть пустым.");
            }

            Stack<char> operators = new Stack<char>();
            List<string> output = new List<string>();

            // Удаление пробелов из входного выражения
            expression = expression.Replace(" ", string.Empty);

            int i = 0;
            while (i < expression.Length)
            {
                char token = expression[i];

                if (char.IsDigit(token))
                {
                    string number = string.Empty;
                    while (i < expression.Length && char.IsDigit(expression[i]))
                    {
                        number += expression[i];
                        i++;
                    }
                    output.Add(number);
                    continue; // Пропустить увеличение i, так как оно уже увеличено в while
                }
                else if (token == '(')
                {
                    operators.Push(token);
                }
                else if (token == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        output.Add(operators.Pop().ToString());
                    }
                    if (operators.Count > 0 && operators.Peek() == '(')
                    {
                        operators.Pop(); // Удаление открывающей скобки из стека
                    }
                    else
                    {
                        throw new ArgumentException("Несогласованные скобки в выражении.");
                    }
                }
                else if (IsOperator(token))
                {
                    while (operators.Count > 0 && Priority(operators.Peek()) >= Priority(token))
                    {
                        output.Add(operators.Pop().ToString());
                    }
                    operators.Push(token);
                }
                else
                {
                    throw new ArgumentException($"Недопустимый символ в выражении: {token}");
                }

                i++;
            }

            // Добавить оставшиеся операторы из стека в выходную строку
            while (operators.Count > 0)
            {
                char op = operators.Pop();
                if (op == '(' || op == ')')
                {
                    throw new ArgumentException("Несогласованные скобки в выражении.");
                }
                output.Add(op.ToString());
            }

            return string.Join(" ", output);
        }

        static bool IsOperator(char token)
        {
            return token == '+' || token == '-' || token == '*' || token == '/';
        }

        static int Priority(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace ExpressionValidator
{
    public class ExpressionValidator
    {
        public static bool ValidateExpression(string expression)
        {
            Stack<char> brackets = new Stack<char>();

            foreach (char c in expression)
            {
                if (c == '(')
                {
                    brackets.Push(c);
                }
                else if (c == ')')
                {
                    if (brackets.Count == 0)
                    {
                        return false; // Нет открывающей скобки для закрывающей
                    }
                    brackets.Pop();
                }
            }

            return brackets.Count == 0; // Все скобки должны быть закрыты
        }
    }
} 
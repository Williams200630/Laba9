using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionValidator
{
    public class CubeSumFinder
    {
        public static void FindNumbersWithMultipleCubeSums(int maxNumber = 1000000)
        {
            // Словарь для хранения комбинаций кубов
            // Key - сумма кубов, Value - список пар чисел, кубы которых дают эту сумму
            var cubeSums = new Dictionary<long, List<(int a, int b)>>();

            // Находим все возможные суммы кубов
            for (int a = 1; a <= Math.Cbrt(maxNumber); a++)
            {
                for (int b = a; b <= Math.Cbrt(maxNumber); b++)
                {
                    long sum = (long)a * a * a + (long)b * b * b;
                    
                    if (sum <= maxNumber)
                    {
                        if (!cubeSums.ContainsKey(sum))
                        {
                            cubeSums[sum] = new List<(int a, int b)>();
                        }
                        cubeSums[sum].Add((a, b));
                    }
                }
            }

            // Выводим числа с двумя и более комбинациями
            Console.WriteLine("\nЧисла с двумя и более комбинациями суммы кубов:");
            Console.WriteLine("Число | Комбинации");
            Console.WriteLine("------------------");

            foreach (var pair in cubeSums.Where(x => x.Value.Count >= 2))
            {
                Console.Write($"{pair.Key,8} | ");
                foreach (var (a, b) in pair.Value)
                {
                    Console.Write($"{a}³ + {b}³ = {pair.Key} | ");
                }
                Console.WriteLine();
            }
        }
    }
} 
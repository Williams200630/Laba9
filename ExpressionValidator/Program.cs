using System;
using LinkedListImplementation;
using ListImplementation;

namespace ExpressionValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Тестирование двусвязного списка
            Console.WriteLine("Тестирование двусвязного списка:");
            var linkedList = new DoublyLinkedList<int>();
            linkedList.Add(1);
            linkedList.Add(2);
            linkedList.Add(3);
            Console.WriteLine($"Количество элементов: {linkedList.Count}");
            Console.WriteLine($"Содержит 2: {linkedList.Contains(2)}");
            linkedList.Remove(2);
            Console.WriteLine($"После удаления 2, содержит 2: {linkedList.Contains(2)}");
            Console.WriteLine();

            // Тестирование List<T>
            Console.WriteLine("Тестирование List<T>:");
            var list = new ListDataStructure<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Console.WriteLine($"Количество элементов: {list.Count}");
            Console.WriteLine($"Содержит 2: {list.Contains(2)}");
            list.Remove(2);
            Console.WriteLine($"После удаления 2, содержит 2: {list.Contains(2)}");
            Console.WriteLine();

            // Тестирование валидации выражений
            Console.WriteLine("Тестирование валидации выражений:");
            string[] testExpressions = {
                "(2+3)(1+6)(((2-3)(5+1)))",
                "2(3)(1+6(7+2))((2-3)(5+1))",
                "2(3+5(((6))))",
                "((2+3)(4-1)))",
                "2(3+5(((6))"
            };

            foreach (var expr in testExpressions)
            {
                bool isValid = ExpressionValidator.ValidateExpression(expr);
                Console.WriteLine($"Выражение: {expr}");
                Console.WriteLine($"Корректно: {isValid}");
                Console.WriteLine();
            }

            // --- Игра "Считалка" ---
            Console.WriteLine("\nИгра 'Считалка':");
            string[] players = { "Аня", "Борис", "Вика", "Глеб", "Даша", "Егор", "Женя" };
            var circle = new CircularLinkedList<string>();
            foreach (var name in players)
                circle.Add(name);

            Console.Write("Введите строку считалки: ");
            string rhyme = Console.ReadLine() ?? "На златом крыльце сидели";
            Console.Write("С кого начать (имя): ");
            string startName = Console.ReadLine() ?? players[0];

            var startNode = circle.Find(n => n == startName);
            if (startNode == null)
            {
                Console.WriteLine($"Участник с именем '{startName}' не найден. Начинаем с первого.");
                startNode = circle.Head!;
            }

            string[] rhymeWords = rhyme.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var current = startNode;
            for (int i = 1; i < rhymeWords.Length; i++)
            {
                current = current!.Next!;
            }
            Console.WriteLine($"\nПоследнее слово выпало на: {current!.Data}");

            // --- Поиск чисел с несколькими комбинациями суммы кубов ---
            Console.WriteLine("\nЗадача 5: Поиск чисел с несколькими комбинациями суммы кубов");
            CubeSumFinder.FindNumbersWithMultipleCubeSums();

            // --- Анализ частоты слов в файле ---
            Console.WriteLine("\nАнализ частоты слов в файле ConsoleApp55/ConsoleApp55/words.txt:");
            WordFrequencyAnalyzer.PrintTopWords("ConsoleApp55/ConsoleApp55/words.txt");

            // --- Турнирная сетка плей-офф ---
            Console.WriteLine("\nТурнирная сетка плей-офф:");
            var teams = new List<string> { "BRA", "ARG", "FRA", "COL", "CHI", "URU", "GER", "NIG", "CRC", "MEX", "NED", "GRE", "BEL", "SWI", "USA", "POR" };
            var tree = new TournamentTree(teams);
            tree.PlayMatches(tree.Root);
            tree.PrintMatches(tree.Root);

            // --- Удаление неуникальных элементов ---
            Console.WriteLine("\nУдаление неуникальных элементов:");
            var testData = new int[] { 1, 2, 2, 3, 4, 4, 5, 6, 6, 7 };

            var linkedList2 = new DoublyLinkedList<int>();
            foreach (var x in testData) linkedList2.Add(x);
            linkedList2.RemoveNonUnique();
            Console.WriteLine("Уникальные элементы в DoublyLinkedList:");
            Console.WriteLine(string.Join(", ", linkedList2.ToArray()));

            var list2 = new ListDataStructure<int>();
            foreach (var x in testData) list2.Add(x);
            list2.RemoveNonUnique();
            Console.WriteLine("Уникальные элементы в ListDataStructure:");
            Console.WriteLine(string.Join(", ", list2.ToArray()));
        }
    }
}

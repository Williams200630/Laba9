using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExpressionValidator
{
    public class WordFrequencyAnalyzer
    {
        public static void PrintTopWords(string filePath, int topCount = 10)
        {
            Console.WriteLine($"Текущая рабочая директория: {Directory.GetCurrentDirectory()}");

            // Абсолютный путь к файлу (замените на свой при необходимости)
            string absolutePath = @"C:\Users\User\source\repos\ConsoleApp55\ConsoleApp55\words.txt";
            if (!File.Exists(absolutePath))
            {
                Console.WriteLine($"Файл '{absolutePath}' не найден.");
                return;
            }

            string text = File.ReadAllText(absolutePath);
            // Используем регулярное выражение для выделения слов
            var words = Regex.Matches(text.ToLower(), "[а-яa-z0-9ё]+", RegexOptions.IgnoreCase)
                .Select(m => m.Value)
                .ToList();

            var frequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (frequency.ContainsKey(word))
                    frequency[word]++;
                else
                    frequency[word] = 1;
            }

            var topWords = frequency
                .OrderByDescending(pair => pair.Value)
                .ThenBy(pair => pair.Key)
                .Take(topCount);

            Console.WriteLine($"\nТоп {topCount} самых частых слов:");
            foreach (var pair in topWords)
            {
                Console.WriteLine($"{pair.Key} — {pair.Value}");
            }
        }
    }
} 
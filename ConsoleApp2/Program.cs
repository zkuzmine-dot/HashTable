using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int[] sizes = { 10000, 100000, 200000 };
        Random rand = new Random();

        foreach (int n in sizes)
        {
            Console.WriteLine($"\nTesting with n = {n}");

            // Генерация массива
            int[] numbers = new int[n];
            for (int i = 0; i < n; i++)
                numbers[i] = rand.Next();

            // Тестирование AVLTree
            var avlTree = new AVLTree<int, int>();
            var watch = Stopwatch.StartNew();

            // Вставка
            for (int i = 0; i < n; i++)
                avlTree.Insert(numbers[i], numbers[i]);

            // Удаление от n/2 до 3/4 n
            for (int i = n / 2; i < 3 * n / 4; i++)
                avlTree.Remove(numbers[i]);

            // Поиск всех элементов
            for (int i = 0; i < n; i++)
                avlTree.TryGetValue(numbers[i], out _);

            watch.Stop();
            Console.WriteLine($"AVLTree time: {watch.ElapsedMilliseconds} ms");

            // Тестирование SortedDictionary
            var sortedDict = new SortedDictionary<int, int>();
            watch = Stopwatch.StartNew();

            // Вставка
            for (int i = 0; i < n; i++)
                sortedDict[numbers[i]] = numbers[i];

            // Удаление от n/2 до 3/4 n
            for (int i = n / 2; i < 3 * n / 4; i++)
                sortedDict.Remove(numbers[i]);

            // Поиск всех элементов
            for (int i = 0; i < n; i++)
                sortedDict.TryGetValue(numbers[i], out _);

            watch.Stop();
            Console.WriteLine($"SortedDictionary time: {watch.ElapsedMilliseconds} ms");
        }
    }
}
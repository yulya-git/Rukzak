using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rukzak
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\nВведите вместимость рюкзака (W): ");
            int capacity = int.Parse(Console.ReadLine());

            Console.Write("Введите количество предметов (k): ");
            int itemCount = int.Parse(Console.ReadLine());

            // Создаём массивы с размером itemCount (НЕ +1)
            int[] weights = new int[itemCount];
            int[] values = new int[itemCount];

            Console.WriteLine("\nВведите данные о предметах (вес и стоимость):");
            for (int i = 0; i < itemCount; i++)  // индекс с 0
            {
                Console.Write($"Предмет {i + 1} вес: ");
                weights[i] = int.Parse(Console.ReadLine());

                Console.Write($"Предмет {i + 1} стоимость: ");
                values[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine($"\nВместимость рюкзака: {capacity}");
            Console.WriteLine("Предметы (вес, стоимость):");
            for (int i = 0; i < itemCount; i++)
                Console.WriteLine($"  {i + 1}: вес = {weights[i]}, стоимость = {values[i]}");

            KnapsackProblem knapsack = new KnapsackProblem(weights, values, capacity);
            knapsack.SolveAndPrint();
            Console.ReadKey();
        }
    }
}

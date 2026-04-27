using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rukzak
{

    //РЮКЗАК

    public class KnapsackProblem
    {
        private int[] weights;
        private int[] values;
        private int capacity;
        private int[,] dp;

        public KnapsackProblem(int[] weights, int[] values, int capacity)
        {
            this.weights = weights;
            this.values = values;
            this.capacity = capacity;
            this.dp = new int[weights.Length + 1, capacity + 1];
        }

        public void SolveAndPrint()
        {
            // Заполнение таблицы ДП
            for (int i = 1; i <= weights.Length; i++)
            {
                for (int w = 0; w <= capacity; w++)
                {
                    if (weights[i - 1] <= w)
                    {
                        dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - weights[i - 1]] + values[i - 1]);
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            // Восстановление выбранных предметов
            MyList<int> selectedItems = new MyList<int>();
            int remaining = capacity;

            for (int i = weights.Length; i > 0 && remaining > 0; i--)
            {
                if (dp[i, remaining] != dp[i - 1, remaining])
                {
                    selectedItems.Add(i - 1);
                    remaining -= weights[i - 1];
                }
            }
            selectedItems.Reverse();

            // Вывод таблицы ДП
            Console.WriteLine("\nТаблица динамического программирования A[s][n]:");
            Console.Write("s\\n |");
            for (int n = 0; n <= Math.Min(capacity, 10); n++)
                Console.Write($"{n,3} ");
            Console.WriteLine();
            Console.WriteLine(new string('-', (Math.Min(capacity, 10) + 2) * 4));

            for (int i = 0; i <= weights.Length; i++)
            {
                Console.Write($"{i,2} |");
                for (int n = 0; n <= Math.Min(capacity, 10); n++)
                    Console.Write($"{dp[i, n],3} ");
                Console.WriteLine();
            }

            // Вывод результата
            Console.WriteLine($"\nРЕЗУЛЬТАТ ЗАДАЧИ О РЮКЗАКЕ:");
            Console.WriteLine($"  Максимальная стоимость: {dp[weights.Length, capacity]}");

            int totalWeight = 0;
            Console.WriteLine("  Выбранные предметы:");
            for (int i = 0; i < selectedItems.Count; i++)
            {
                int idx = selectedItems[i];
                Console.WriteLine($"    Предмет {idx + 1}: вес={weights[idx]}, ценность={values[idx]}");
                totalWeight += weights[idx];
            }
            Console.WriteLine($"  Общий вес: {totalWeight}");
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Алгоритмы_поиска
{


    class Program
    {
        static void Main(string[] args)
        {






            MyList<(int from, int to, int weight)> directedEdges = new MyList<(int, int, int)>();
            directedEdges.Add((1, 2, 8));
            directedEdges.Add((1, 5, 11));
            directedEdges.Add((2, 3, 15));
            directedEdges.Add((2, 6, 9));
            directedEdges.Add((3, 4, 2));
            directedEdges.Add((3, 7, 1));
            directedEdges.Add((4, 7, 6));
            directedEdges.Add((5, 2, 3));
            directedEdges.Add((5, 3, 11));
            directedEdges.Add((5, 6, 10));
            directedEdges.Add((6, 4, 1));
            directedEdges.Add((6, 7, 3));

            int vertexCount = 7;

            Console.WriteLine("\nСписок рёбер ОРИЕНТИРОВАННОГО графа:");

            Console.WriteLine("  │  A  │  B  │ Вес  │");

            for (int i = 0; i < directedEdges.Count; i++)
            {
                var e = directedEdges[i];
                Console.WriteLine($"  │  {e.from}  │  {e.to}  │  {e.weight,2}  │");
            }


            // Три пары вершин
            MyList<(int from, int to)> pairs = new MyList<(int, int)>();
            pairs.Add((1, 6));
            pairs.Add((2, 7));
            pairs.Add((3, 7));

            Console.WriteLine("\nЗаданные пары вершин для поиска кратчайшего пути:");
            for (int i = 0; i < pairs.Count; i++)
            {
                var p = pairs[i];
                Console.WriteLine($"  {i + 1}. {p.from} → {p.to}");
            }

            Stopwatch stpwatch = new Stopwatch();
            // 3. ФЛОЙДА-УОРШЕЛЛА 
            stpwatch.Start();

            Console.WriteLine("3. АЛГОРИТМ ФЛОЙДА-УОРШЕЛЛА ");


            FloydWarshallAlgorithm floyd = new FloydWarshallAlgorithm(vertexCount);
            for (int i = 0; i < directedEdges.Count; i++)
            {
                var e = directedEdges[i];
                floyd.AddDirectedEdge(e.from, e.to, e.weight);
            }
            floyd.Run();
            stpwatch.Stop();
            string yorchela_time = stpwatch.Elapsed.TotalMilliseconds.ToString();
            floyd.PrintResultForPairs(pairs);
            Console.WriteLine($"Время выполнения: {yorchela_time} тик");
            // 2. ДЕЙКСТРЫ 
            stpwatch.Reset();
            stpwatch.Start();

            Console.WriteLine("2. АЛГОРИТМ ДЕЙКСТРЫ");


            DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(vertexCount);
            for (int i = 0; i < directedEdges.Count; i++)
            {
                var e = directedEdges[i];
                dijkstra.AddDirectedEdge(e.from, e.to, e.weight);
            }
            stpwatch.Stop();
            string deik_time = stpwatch.Elapsed.TotalMilliseconds.ToString();
            dijkstra.PrintResultForPairs(pairs);
            Console.WriteLine($"Время выполнения: {deik_time} тик");
            // 3. БЕЛЛМАНА-ФОРДА 
            stpwatch.Reset();
            stpwatch.Start();

            Console.WriteLine("1. АЛГОРИТМ БЕЛЛМАНА-ФОРДА");


            BellmanFordAlgorithm bellmanFord = new BellmanFordAlgorithm(vertexCount);
            for (int i = 0; i < directedEdges.Count; i++)
            {
                var e = directedEdges[i];
                bellmanFord.AddDirectedEdge(e.from, e.to, e.weight);
            }
            stpwatch.Stop();
            string ber_time = stpwatch.Elapsed.TotalMilliseconds.ToString();
            bellmanFord.PrintResultForPairs(pairs);
            Console.WriteLine($"Время выполнения: {ber_time} тик");

            // 4. ПРИМА И КРУСКАЛА

            stpwatch.Reset();
            stpwatch.Start();
            Console.WriteLine("4. МИНИМАЛЬНОЕ ОСТОВНОЕ ДЕРЕВО");


            PrimAlgorithm prim = new PrimAlgorithm(vertexCount);


            for (int i = 0; i < directedEdges.Count; i++)
            {
                var e = directedEdges[i];
                prim.AddUndirectedEdge(e.from, e.to, e.weight);

            }
            stpwatch.Stop();
            string prim_time = stpwatch.Elapsed.TotalMilliseconds.ToString();
            stpwatch.Reset();
            stpwatch.Start();
            KruskalAlgorithm kruskal = new KruskalAlgorithm(vertexCount);
            for (int i = 0; i < directedEdges.Count; i++)
            {
                var e = directedEdges[i];
                kruskal.AddUndirectedEdge(e.from, e.to, e.weight);

            }
            stpwatch.Stop();
            string kruskala_time = stpwatch.Elapsed.TotalMilliseconds.ToString();
            prim.RunAndPrint(1);
            Console.WriteLine($"Время выполнения: {prim_time} тик");
            kruskal.RunAndPrint();
            Console.WriteLine($"Время выполнения: {kruskala_time} тик");



            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
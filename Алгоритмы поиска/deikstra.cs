using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритмы_поиска
{

    public class DijkstraAlgorithm
    {
        private int vertexCount;
        private MyList<(int to, int weight)>[] adjacencyList;

        public DijkstraAlgorithm(int vertexCount)
        {
            this.vertexCount = vertexCount;
            adjacencyList = new MyList<(int, int)>[vertexCount];
            for (int i = 0; i < vertexCount; i++)
                adjacencyList[i] = new MyList<(int, int)>();
        }

        public void AddDirectedEdge(int from, int to, int weight)
        {
            int u = from - 1;
            int v = to - 1;
            adjacencyList[u].Add((v, weight));
        }

        public DijkstraResult Run(int source)
        {
            int src = source - 1;
            int[] distances = new int[vertexCount];
            int[] previous = new int[vertexCount];
            bool[] visited = new bool[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                distances[i] = int.MaxValue;
                previous[i] = -1;
            }

            distances[src] = 0;

            for (int count = 0; count < vertexCount - 1; count++)
            {
                int u = MinDistance(distances, visited);
                if (u == -1) break;

                visited[u] = true;

                for (int i = 0; i < adjacencyList[u].Count; i++)
                {
                    var edge = adjacencyList[u][i];
                    int v = edge.to;
                    int weight = edge.weight;

                    if (!visited[v] && distances[u] != int.MaxValue &&
                        distances[u] + weight < distances[v])
                    {
                        distances[v] = distances[u] + weight;
                        previous[v] = u;
                    }
                }
            }

            return new DijkstraResult(distances, previous, source);
        }
        public void PrintResultForPairs(MyList<(int from, int to)> pairs)
        {
            Console.WriteLine("\nАЛГОРИТМ ДЕЙКСТРЫ:");
            for (int i = 0; i < pairs.Count; i++)  // ЦИКЛ по всем трём парам
            {
                var p = pairs[i];
                var result = Run(p.from);           // Запускаем от источника из пары
                int dist = result.GetDistance(p.to);
                MyList<int> path = result.GetPath(p.to);

                Console.Write($"  {i + 1}. {p.from} → {p.to}: ");
                if (dist == int.MaxValue)
                    Console.WriteLine("путь НЕ СУЩЕСТВУЕТ (∞)");
                else
                {
                    Console.Write($"расстояние = {dist}, путь = ");
                    path.Print(" → ");
                    Console.WriteLine();
                }
            }
        }

        private int MinDistance(int[] distances, bool[] visited)
        {
            int min = int.MaxValue;
            int minIndex = -1;

            for (int v = 0; v < vertexCount; v++)
            {
                if (!visited[v] && distances[v] <= min)
                {
                    min = distances[v];
                    minIndex = v;
                }
            }
            return minIndex;
        }

        public class DijkstraResult
        {
            public int[] Distances { get; private set; }
            public int[] Previous { get; private set; }
            public int Source { get; private set; }

            public DijkstraResult(int[] distances, int[] previous, int source)
            {
                Distances = distances;
                Previous = previous;
                Source = source;
            }

            public int GetDistance(int to)
            {
                return Distances[to - 1];
            }

            public MyList<int> GetPath(int to)
            {
                int dest = to - 1;
                if (Distances[dest] == int.MaxValue) return null;

                MyList<int> path = new MyList<int>();
                int current = dest;

                while (current != -1)
                {
                    path.Add(current + 1);
                    if (current == Source - 1) break;
                    current = Previous[current];
                }

                path.Reverse();
                return path;
            }
        }
    }
}

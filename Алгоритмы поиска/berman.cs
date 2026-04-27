using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритмы_поиска
{


    public class BellmanFordAlgorithm
    {
        private int vertexCount;
        private MyList<Edge> edges;

        public BellmanFordAlgorithm(int vertexCount)
        {
            this.vertexCount = vertexCount;
            edges = new MyList<Edge>();
        }

        public void AddDirectedEdge(int from, int to, int weight)
        {
            edges.Add(new Edge(from, to, weight));
        }

        public BellmanFordResult Run(int source)
        {
            int[] distances = new int[vertexCount];
            int[] previous = new int[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                distances[i] = int.MaxValue;
                previous[i] = -1;
            }

            distances[source - 1] = 0;

            for (int i = 0; i < vertexCount - 1; i++)
            {
                for (int j = 0; j < edges.Count; j++)
                {
                    Edge edge = edges[j];
                    int u = edge.From - 1;
                    int v = edge.To - 1;
                    int w = edge.Weight;

                    if (distances[u] != int.MaxValue && distances[u] + w < distances[v])
                    {
                        distances[v] = distances[u] + w;
                        previous[v] = u;
                    }
                }
            }

            bool hasNegativeCycle = false;
            for (int j = 0; j < edges.Count; j++)
            {
                Edge edge = edges[j];
                int u = edge.From - 1;
                int v = edge.To - 1;
                int w = edge.Weight;

                if (distances[u] != int.MaxValue && distances[u] + w < distances[v])
                {
                    hasNegativeCycle = true;
                    break;
                }
            }

            return new BellmanFordResult(distances, previous, source, hasNegativeCycle);
        }
        public void PrintResultForPairs(MyList<(int from, int to)> pairs)
        {
            Console.WriteLine("\nАЛГОРИТМ БЕЛЛМАНА-ФОРДА:");

            for (int i = 0; i < pairs.Count; i++)  // ЦИКЛ по всем трём парам
            {
                var p = pairs[i];
                var result = Run(p.from);

                Console.Write($"  {i + 1}. {p.from} → {p.to}: ");

                if (result.HasNegativeCycle)
                {
                    Console.WriteLine("ГРАФ СОДЕРЖИТ ЦИКЛ ОТРИЦАТЕЛЬНОГО ВЕСА!");
                    continue;
                }

                int dist = result.GetDistance(p.to);
                MyList<int> path = result.GetPath(p.to);

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

        public class BellmanFordResult
        {
            public int[] Distances { get; private set; }
            public int[] Previous { get; private set; }
            public int Source { get; private set; }
            public bool HasNegativeCycle { get; private set; }

            public BellmanFordResult(int[] distances, int[] previous, int source, bool hasNegativeCycle)
            {
                Distances = distances;
                Previous = previous;
                Source = source;
                HasNegativeCycle = hasNegativeCycle;
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
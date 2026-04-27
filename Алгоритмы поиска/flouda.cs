using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритмы_поиска
{

    public class FloydWarshallAlgorithm
    {
        private const int INF = 999999;
        private int vertexCount;
        private int[,] dist;
        private int[,] next;

        public FloydWarshallAlgorithm(int vertexCount)
        {
            this.vertexCount = vertexCount;
            dist = new int[vertexCount, vertexCount];
            next = new int[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    dist[i, j] = (i == j) ? 0 : INF;
                    next[i, j] = -1;
                }
            }
        }

        public void AddDirectedEdge(int from, int to, int weight)
        {
            int u = from - 1;
            int v = to - 1;
            dist[u, v] = weight;
            next[u, v] = v;
        }

        public void Run()
        {
            for (int k = 0; k < vertexCount; k++)
            {
                for (int i = 0; i < vertexCount; i++)
                {
                    for (int j = 0; j < vertexCount; j++)
                    {
                        if (dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                            next[i, j] = next[i, k];
                        }
                    }
                }
            }
        }

        public int GetDistance(int from, int to)
        {
            return dist[from - 1, to - 1];
        }

        public MyList<int> GetPath(int from, int to)
        {
            int u = from - 1;
            int v = to - 1;

            if (dist[u, v] >= INF)
                return null;

            MyList<int> path = new MyList<int>();
            path.Add(from);

            while (u != v)
            {
                u = next[u, v];
                if (u == -1) return null;
                path.Add(u + 1);
            }

            return path;
        }

        public void PrintResultForPairs(MyList<(int from, int to)> pairs)
        {
            Console.WriteLine("\nКратчайшие пути для заданных пар:");
            for (int i = 0; i < pairs.Count; i++)
            {
                var p = pairs[i];
                int dist = GetDistance(p.from, p.to);
                MyList<int> path = GetPath(p.from, p.to);

                Console.Write($"  {i + 1}. {p.from} → {p.to}: ");
                if (dist >= INF)
                {
                    Console.WriteLine("путь НЕ СУЩЕСТВУЕТ (∞)");
                }
                else
                {
                    Console.Write($"расстояние = {dist}, путь = ");
                    path.Print(" → ");
                    Console.WriteLine();
                }
            }
        }
    }
}



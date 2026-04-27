using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритмы_поиска
{

    public class PrimAlgorithm
    {
        private int vertexCount;
        private int[,] adjacencyMatrix;
        private const int INF = 999999;

        public PrimAlgorithm(int vertexCount)
        {
            this.vertexCount = vertexCount;
            adjacencyMatrix = new int[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; i++)
                for (int j = 0; j < vertexCount; j++)
                    adjacencyMatrix[i, j] = (i == j) ? 0 : INF;
        }

        public void AddUndirectedEdge(int from, int to, int weight)
        {
            int u = from - 1;
            int v = to - 1;
            adjacencyMatrix[u, v] = weight;
            adjacencyMatrix[v, u] = weight;
        }

        public void RunAndPrint(int startVertex = 1)
        {
            int start = startVertex - 1;
            bool[] inMST = new bool[vertexCount];
            int[] key = new int[vertexCount];
            int[] parent = new int[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                key[i] = INF;
                parent[i] = -1;
            }

            key[start] = 0;

            for (int count = 0; count < vertexCount - 1; count++)
            {
                int u = MinKey(key, inMST);
                if (u == -1) break;

                inMST[u] = true;

                for (int v = 0; v < vertexCount; v++)
                {
                    if (adjacencyMatrix[u, v] != INF && !inMST[v] && adjacencyMatrix[u, v] < key[v])
                    {
                        key[v] = adjacencyMatrix[u, v];
                        parent[v] = u;
                    }
                }
            }

            Console.WriteLine("\nАЛГОРИТМ ПРИМА (минимальное остовное дерево):");
            Console.WriteLine("  Ребро | Вес");
            int totalWeight = 0;
            for (int i = 0; i < vertexCount; i++)
            {
                if (parent[i] != -1)
                {
                    Console.WriteLine($"   {parent[i] + 1} - {i + 1} |  {key[i]}");
                    totalWeight += key[i];
                }
            }
            Console.WriteLine($"  Общий вес: {totalWeight}");
        }

        private int MinKey(int[] key, bool[] inMST)
        {
            int min = INF;
            int minIndex = -1;

            for (int v = 0; v < vertexCount; v++)
                if (!inMST[v] && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }
            return minIndex;
        }
    }
}

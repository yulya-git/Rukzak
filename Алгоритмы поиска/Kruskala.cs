using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритмы_поиска
{


    public class KruskalAlgorithm
    {
        private int vertexCount;
        private MyList<Edge> edges;

        public KruskalAlgorithm(int vertexCount)
        {
            this.vertexCount = vertexCount;
            edges = new MyList<Edge>();
        }

        public void AddUndirectedEdge(int from, int to, int weight)
        {
            edges.Add(new Edge(from, to, weight));
        }

        public void RunAndPrint()
        {
            // Сортировка рёбер по весу пузырьком
            for (int i = 0; i < edges.Count - 1; i++)
            {
                for (int j = 0; j < edges.Count - 1 - i; j++)
                {
                    if (edges[j].Weight > edges[j + 1].Weight)
                    {
                        Edge temp = edges[j];
                        edges[j] = edges[j + 1];
                        edges[j + 1] = temp;
                    }
                }
            }

            int[] parent = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
                parent[i] = i;

            Console.WriteLine("\nАЛГОРИТМ КРУСКАЛА (минимальное остовное дерево):");
            Console.WriteLine("  Ребро | Вес");
            int totalWeight = 0;

            for (int i = 0; i < edges.Count; i++)
            {
                Edge edge = edges[i];
                int rootFrom = Find(parent, edge.From - 1);
                int rootTo = Find(parent, edge.To - 1);

                if (rootFrom != rootTo)
                {
                    Console.WriteLine($"   {edge.From} - {edge.To} |  {edge.Weight}");
                    totalWeight += edge.Weight;
                    parent[rootTo] = rootFrom;
                }
            }
            Console.WriteLine($"  Общий вес: {totalWeight}");
        }

        private int Find(int[] parent, int x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent, parent[x]);
            return parent[x];
        }
    }
}


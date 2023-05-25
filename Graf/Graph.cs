using System;
using System.Collections.Generic;
using System.Text;

namespace Graf
{
    class Graph
    {
        private int V; // Количество вершин
        private List<Edge>[] adjList; // Список смежности

        public Graph(int vertices)
        {
            V = vertices;
            adjList = new List<Edge>[V];
            for (int i = 0; i < V; i++)
            {
                adjList[i] = new List<Edge>();
            }
        }

        public void AddEdge(Vertex u, Vertex v, int weight)
        {
            adjList[u.Id].Add(new Edge(u, v, weight));
        }

        public void TopologicalSort()
        {
            Stack<Vertex> stack = new Stack<Vertex>();
            bool[] visited = new bool[V];

            for (int i = 0; i < V; i++)
            {
                if (!visited[i])
                {
                    TopologicalSortUtil(adjList[i][0].Source, visited, stack);
                }
            }

            Console.WriteLine("Топологическая сортировка:");
            while (stack.Count > 0)
            {
                Console.Write(stack.Pop().Id + " ");
            }
            Console.WriteLine();
        }

        private void TopologicalSortUtil(Vertex v, bool[] visited, Stack<Vertex> stack)
        {
            visited[v.Id] = true;

            foreach (Edge edge in adjList[v.Id])
            {
                if (!visited[edge.Destination.Id])
                {
                    TopologicalSortUtil(edge.Destination, visited, stack);
                }
            }

            stack.Push(v);
        }

        public void KruskalMST()
        {
            List<Edge> edges = new List<Edge>();

            for (int v = 0; v < V; v++)
            {
                edges.AddRange(adjList[v]);
            }

            edges.Sort(); // Сортировка ребер по весу

            int[] parent = new int[V];
            int[] rank = new int[V];

            for (int i = 0; i < V; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }

            List<Edge> mst = new List<Edge>();

            foreach (Edge edge in edges)
            {
                int x = Find(parent, edge.Source.Id);
                int y = Find(parent, edge.Destination.Id);

                if (x != y)
                {
                    mst.Add(edge);
                    Union(parent, rank, x, y);
                }
            }

            Console.WriteLine("Минимальное остовное дерево по алгоритму Краскала:");
            foreach (Edge edge in mst)
            {
                Console.WriteLine(edge.Source.Id + " -- " + edge.Destination.Id + " (вес: " + edge.Weight + ")");
            }
        }

        public void TransitiveClosure()
        {
            bool[,] closure = new bool[V, V];

            for (int v = 0; v < V; v++)
            {
                DFSUtil(v, v, closure);
            }

            Console.WriteLine("Транзитивное замыкание:");
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    Console.Write(closure[i, j] ? "1 " : "0 ");
                }
                Console.WriteLine();
            }
        }

        private void DFSUtil(int source, int v, bool[,] closure)
        {
            closure[source, v] = true;

            foreach (Edge edge in adjList[v])
            {
                if (!closure[source, edge.Destination.Id])
                {
                    DFSUtil(source, edge.Destination.Id, closure);
                }
            }
        }

        private int Find(int[] parent, int i)
        {
            if (parent[i] != i)
            {
                parent[i] = Find(parent, parent[i]);
            }
            return parent[i];
        }

        private void Union(int[] parent, int[] rank, int x, int y)
        {
            int xRoot = Find(parent, x);
            int yRoot = Find(parent, y);

            if (rank[xRoot] < rank[yRoot])
            {
                parent[xRoot] = yRoot;
            }
            else if (rank[xRoot] > rank[yRoot])
            {
                parent[yRoot] = xRoot;
            }
            else
            {
                parent[yRoot] = xRoot;
                rank[xRoot]++;
            }
        }

        public override string ToString()
        {
            StringBuilder graphString = new StringBuilder();

            int[] vertexWidths = new int[V]; // Массив для хранения ширины вершин

            // Определение ширины каждой вершины на основе максимальной длины ребер, исходящих из нее
            for (int v = 0; v < V; v++)
            {
                if (adjList[v].Count > 0)
                {
                    foreach (var edge in adjList[v])
                    {
                        int edgeLength = edge.Weight.ToString().Length + 2; // 2 для учета круглых скобок
                        vertexWidths[v] = Math.Max(vertexWidths[v], edgeLength);
                        vertexWidths[edge.Destination.Id] = Math.Max(vertexWidths[edge.Destination.Id], edgeLength);
                    }
                }
            }

            // Вывод ребер и вершин
            for (int v = 0; v < V; v++)
            {
                if (adjList[v].Count > 0)
                {
                    foreach (var edge in adjList[v])
                    {
                        int edgeLength = edge.Weight.ToString().Length + 2; // 2 для учета круглых скобок

                        // Выравнивание ребра относительно вершины
                        int padding = vertexWidths[v] - edgeLength;
                        graphString.Append(new string(' ', padding));
                        graphString.AppendLine(v + " ─" + new string('─', edgeLength - 2) + "→ " + edge.Destination.Id + " (" + edge.Weight + ")");
                    }
                }
            }

            return graphString.ToString();
        }
    }
}

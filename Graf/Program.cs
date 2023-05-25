using System;

namespace Graf
{
    class Program
    {
        static void Main(string[] args)
        {
            int vertices = 5;
            Graph graph = new Graph(vertices);

            Vertex v0 = new Vertex(0);
            Vertex v1 = new Vertex(1);
            Vertex v2 = new Vertex(2);
            Vertex v3 = new Vertex(3);
            Vertex v4 = new Vertex(4);

            graph.AddEdge(v0, v1, 2);
            graph.AddEdge(v0, v2, 4);
            graph.AddEdge(v1, v3, 3);
            graph.AddEdge(v2, v3, 1);
            graph.AddEdge(v2, v4, 5);
            graph.AddEdge(v3, v4, 2);

            Console.WriteLine("Граф:");
            Console.WriteLine(graph.ToString());

            graph.TopologicalSort();
            graph.KruskalMST();
            graph.TransitiveClosure();
        }
    }
}

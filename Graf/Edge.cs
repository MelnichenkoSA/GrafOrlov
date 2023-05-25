using System;

namespace Graf
{
    class Edge : IComparable<Edge>
    {
        public Vertex Source { get; }
        public Vertex Destination { get; }
        public int Weight { get; }

        public Edge(Vertex source, Vertex destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }

        public int CompareTo(Edge other)
        {
            return Weight.CompareTo(other.Weight);
        }
    }
}


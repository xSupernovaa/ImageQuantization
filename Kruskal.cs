using System;
using System.Collections.Generic;
using Lucene.Net.Util;
//import priority queue library

namespace ImageQuantization
{
    public class Edge : IComparable
    {
        public int from;
        public int to;
        public double weight;

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Edge otherEdge = obj as Edge;
            return this.weight.CompareTo(otherEdge.weight);
        }
    }
    internal class EdgePQ : PriorityQueue<Edge>
    {
        public EdgePQ(int size)
        {
            base.Initialize(size);
        }
        public override bool LessThan(Edge a, Edge b)
        {
            return a.weight < b.weight;
        }
    }

    internal class Kruskal
    {
        public static VertexSet RunKruskal(List<RGBPixel> vertices, List<Edge> EdgeList, int number_of_clusters)
        {
            int verticesCount = vertices.Count;
            VertexSet set = new VertexSet(verticesCount);
            //initalize the sets (each vertex is in its own set)
            //O(V)
            
            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
            {
                set.MakeSet(vertexIndex);
            }

            EdgePQ pq = new EdgePQ(EdgeList.Count);
            foreach (var e in EdgeList)
            {
                pq.Add(e);
            }
            double totalWeight = 0;
            while (set.number_of_clusters != number_of_clusters && pq.Size() > 0)
            //while (pq.Size() > 0)
            {
                Edge edge = pq.Pop();

                int firstColorSet = set.FindSet(edge.from);
                int secondColorSet = set.FindSet(edge.to);

                if (firstColorSet != secondColorSet)
                {
                    totalWeight += edge.weight;
                    set.UnionSet(firstColorSet, secondColorSet); //O(V * O(UnionSet))
                }
            }
            Console.WriteLine("total weight: " + totalWeight);

            return set;
        }


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            
            EdgeList.Sort(); //O(ElogE)
            for(int i = 0; i< EdgeList.Count; i++)
            {
                if (set.number_of_clusters == number_of_clusters) break;

                Edge edge = EdgeList[i];

                int firstColorSet = set.FindSet(edge.from);
                int secondColorSet = set.FindSet(edge.to);

                if (firstColorSet != secondColorSet)
                    set.UnionSet(firstColorSet, secondColorSet); //O(V * O(UnionSet))
            }

            return set;
        }

        
    }
}

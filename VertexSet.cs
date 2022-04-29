using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class VertexSet
    {
        private int[] members;
        public int number_of_clusters;
        public VertexSet(int size)
        {
            members = new int[size];
            number_of_clusters = size;
        }

        /// <summary>
        /// Creates new set and add u in it.
        /// Complexity: O(1)
        /// </summary>
        /// <param name="v">The vertex</param>
        public void MakeSet(int v)
        {
            members[v] = v;
        }

        /// <summary>
        /// Returns the set ID of v
        /// Complexity: O(1)
        /// </summary>
        /// <param name="v">The vertex</param>
        public int FindSet(int v)
        {
            return members[v];
        }

        /// <summary>
        /// Merges the sets of u and v.
        /// Complexity: O(v), where v is the number of vertices
        /// </summary>
        /// <param name="u">First vertex</param>
        /// <param name="v">Second vertex</param>
        public void UnionSet(int u, int v)
        {
            for(int i = 0; i < members.Length; i++)
            {
                if(members[i] == members[u])
                {
                    members[i] = v;
                }
            }
            number_of_clusters--;
        }

        public Dictionary<int, List<RGBPixel>> GetClusters(List<RGBPixel> colorsList)
        {
            Dictionary<int, List<RGBPixel>> clusters = new Dictionary<int, List<RGBPixel>>();
            for (int i = 0; i < members.Length; i++)
            {
                if (clusters[members[i]] == null)
                {
                    clusters[members[i]] = new List<RGBPixel>();
                }
                clusters[members[i]].Add(colorsList[i]);
            }
            return clusters;
        }
    }
}

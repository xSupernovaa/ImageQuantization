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
                if (!clusters.ContainsKey(members[i]))
                {
                    clusters[members[i]] = new List<RGBPixel>();
                }
                clusters[members[i]].Add(colorsList[i]);
            }
            return clusters;
        }
         public Dictionary<int,RGBPixel> representativcolor (Dictionary<int, List<RGBPixel>> cluster)
        {
            //for every member of cluster sum all values and get the mean for the sum
            Dictionary<int,RGBPixel> reprsent=new Dictionary<int, RGBPixel>();
            foreach (int key in cluster.Keys)
            {
                int sumRed=0;
                int sumGreen=0;
                int sumBlue=0;
               
                    foreach(RGBPixel pixel in cluster[key])
                    {
                    sumRed+=pixel.red;
                    sumBlue+=pixel.blue;
                    sumGreen+=pixel.green;

                    }
                 Byte Red=Convert.ToByte(sumRed/cluster[key].Count);
                Byte blue=Convert.ToByte(sumBlue/cluster[key].Count);
                byte green=Convert.ToByte(sumGreen/cluster[key].Count);
                RGBPixel Pixl=new RGBPixel();
                Pixl.red=Red;
                Pixl.green=green;
                Pixl.blue=blue;
                reprsent.Add(key,Pixl);
                
                  
            }
            return reprsent;
            
        }
      


    }
}

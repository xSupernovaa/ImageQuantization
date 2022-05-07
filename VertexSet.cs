using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    class VertexSet
    {
        private int[] members;
        public int number_of_clusters;
        public double sumMST = 0;
        public VertexSet(int size)
        {
            members = new int[size];
            number_of_clusters = size;
        }
        public int[] getMembers()
        {
            return members;
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
                if(members[i] == u)
                {
                    members[i] = v;
                }
            }
            number_of_clusters--;
        }

        public static Dictionary<int, List<RGBPixel>> GetClusters(List<RGBPixel> colorsList, int[] members)
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

            Console.WriteLine("finished GetClusters at " + (MainForm.stopWatch.Elapsed).ToString());

            return clusters;
        }

        public static Dictionary<int ,RGBPixel> GetColorPallette(Dictionary<int, List<RGBPixel>> clusters)
        {
            // for every member of cluster sum all values and get the mean for the sum
            Dictionary<int, RGBPixel> colorPallete = new Dictionary<int, RGBPixel>();
            foreach (int clusterIndex in clusters.Keys)
            {
                int sumRed = 0, sumGreen = 0, sumBlue = 0;
                int numberOfColorsInCluster = clusters[clusterIndex].Count;

                foreach (RGBPixel pixel in clusters[clusterIndex])
                {
                    sumRed += pixel.red;
                    sumBlue += pixel.blue;
                    sumGreen += pixel.green;

                }
                sumRed = (int)Math.Ceiling((double)sumRed / numberOfColorsInCluster);
                sumGreen = (int)Math.Ceiling((double)sumGreen / numberOfColorsInCluster);
                sumBlue = (int)Math.Ceiling((double)sumBlue / numberOfColorsInCluster);

                byte red = Convert.ToByte(sumRed);
                byte green = Convert.ToByte(sumGreen);
                byte blue = Convert.ToByte(sumBlue);

                RGBPixel representitaveColor = new RGBPixel(red, green, blue);
                colorPallete.Add(clusterIndex, representitaveColor);
            }

            Console.WriteLine("finished GetColorPallette at " + (MainForm.stopWatch.Elapsed).ToString());
            return colorPallete;

        }
    }
}

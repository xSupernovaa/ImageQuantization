using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class ColorQuantization
    {
        public static RGBPixel[,] ColorQuantize(RGBPixel[,] ImageMatrix, int number_of_clusters)
        {
            List<RGBPixel> colorsList = GetDistinctColorsList(ImageMatrix);
            double[,] colorsGraph = createDistinctColorsGraph(colorsList);
            
            VertexSet set = Kruskal.RunKruskal(colorsList ,colorsGraph, number_of_clusters);

            Dictionary<int, List<RGBPixel>> clusters = set.GetClusters(colorsList);

            ////DEBUG
            //foreach (var l in clusters)
            //{
            //    Console.WriteLine("Cluster: ");
            //    foreach (var p in l.Value)
            //    {
            //        Console.WriteLine(p.red + " " + p.green + " " + p.blue);
            //    }
            //}
            ////DEBUG

            return ImageMatrix;

        }

        
        private static Dictionary<RGBPixel, int> GetDistinctColorsFreqArray(RGBPixel[,] ImageMatrix)
        {
            Dictionary<RGBPixel, int> colorsFrequencyArray = new Dictionary<RGBPixel, int>();

            foreach (RGBPixel color in ImageMatrix)
            {
                if (!colorsFrequencyArray.ContainsKey(color))
                {
                    colorsFrequencyArray.Add(color, 1);
                }
                else
                {
                    colorsFrequencyArray[color]++;
                }
            }

            return colorsFrequencyArray;
        }

        private static List<RGBPixel> GetDistinctColorsList(RGBPixel[,] ImageMatrix)
        {
            HashSet<RGBPixel> distinctColors = new HashSet<RGBPixel>();

            foreach (RGBPixel pixel in ImageMatrix)
                distinctColors.Add(pixel);

            // ToList because HashSets does not support indexing
            return distinctColors.ToList();
        }


        private static double Distance(RGBPixel a, RGBPixel b)
        {
            double differenceRed = Math.Pow(a.red - b.red, 2);
            double differenceGreen = Math.Pow(a.green - b.green, 2);
            double differenceBlue = Math.Pow(a.blue - b.blue, 2);

            double distance = Math.Sqrt(differenceRed + differenceGreen + differenceBlue);


            return distance;
        }

        private static double[,] createDistinctColorsGraph(List<RGBPixel> distinctColors)
        {
            int V = distinctColors.Count;
            double[,] Graph = new double[V,V];

            int offset = 0;

            for (int i = 0; i < V; i++)
            {
                for (int j = offset; j < V; j++)
                {
                    double edgeCost = Distance(distinctColors[i], distinctColors[j]);

                    Graph[i, j] = edgeCost;
                    Graph[j, i] = edgeCost;
                }

                offset++;
            }

            return Graph;

        }



    }
}

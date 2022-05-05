using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class ColorQuantization
    {
        private static Dictionary<RGBPixel, int> colorIndices;

        public static RGBPixel[,] ColorQuantize(RGBPixel[,] ImageMatrix, int number_of_clusters)
        {
            // O(N^2)
            List<RGBPixel> distinctColorsList = GetDistinctColorsList(ImageMatrix);
            
            ///DEBUG
            Console.WriteLine("finished GetDistinctColorsList at " + (MainForm.stopWatch.Elapsed).ToString());
            ///END DEBUG
            
            // O((V * ((V - 1) / 2)) --> O(V^2) where V is the number of distinct colors
            List<Edge> distinctColorsGraph = createDistinctColorsGraphEdgeList(distinctColorsList);
            
            //DEBUG
            Console.WriteLine("finished createDistinctColorsGraphEdgeList at " + (MainForm.stopWatch.Elapsed).ToString());
            ///END DEBUG
            
            VertexSet set = Kruskal.RunKruskal(distinctColorsList ,distinctColorsGraph, number_of_clusters);

            ///DEBUG
            Console.WriteLine("finished RunKruskal at " + (MainForm.stopWatch.Elapsed).ToString());
            ///END DEBUG
            
            Dictionary<int, List<RGBPixel>> clusters = VertexSet.GetClusters(distinctColorsList, set.getMembers());

            ///DEBUG
            Console.WriteLine("finished GetClusters at " + (MainForm.stopWatch.Elapsed).ToString());
            ///END DEBUG

            // O(V) where V is the number of distinct colors
            Dictionary<int, RGBPixel> colorPallette = VertexSet.GetColorPallette(clusters);

            ///DEBUG
            Console.WriteLine("finished GetColorPallette at " + (MainForm.stopWatch.Elapsed).ToString());
            ///END DEBUG

            ReduceImageColors(ImageMatrix, colorPallette, set);

            ///DEBUG
            Console.WriteLine("finished ReduceImageColors at " + (MainForm.stopWatch.Elapsed).ToString());
            ///END DEBUG

            int countColorsBefore = distinctColorsList.Count;
            int countColorsAfter = colorPallette.Count;

            Console.WriteLine("Reduced number of colors in image from " + countColorsBefore + " to " + countColorsAfter);

            return ImageMatrix;

        }

        
        private static List<RGBPixel> GetDistinctColorsList(RGBPixel[,] ImageMatrix)
        {
            HashSet<RGBPixel> distinctColors = new HashSet<RGBPixel>();


            foreach (RGBPixel pixel in ImageMatrix)
                distinctColors.Add(pixel);

            List<RGBPixel> colorsList = distinctColors.ToList();

            colorIndices = new Dictionary<RGBPixel, int>();

            for (int i = 0; i < colorsList.Count; i++)
                colorIndices.Add(colorsList[i], i);
            

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

        private static List<Edge> createDistinctColorsGraphEdgeList(List<RGBPixel> distinctColors)
        {
            int V = distinctColors.Count;
            int E = V * ((V - 1) / 2);
            List<Edge> GraphEdgeList = new List<Edge>(E);

            for (int i = 0; i < V; i++)
            {
                for (int j = i + 1; j < V; j++)
                {
                    double weight = Distance(distinctColors[i], distinctColors[j]);
                    Edge edge = new Edge();
                    edge.from = i;
                    edge.to = j;
                    edge.weight = weight;

                    GraphEdgeList.Add(edge);
                }
            }

            return GraphEdgeList;

        }


        private static void ReduceImageColors(RGBPixel[,] ImageMatrix, Dictionary<int, RGBPixel> ColorPallette, VertexSet set)
        {
            int rows = ImageOperations.GetHeight(ImageMatrix);
            int columns = ImageOperations.GetWidth(ImageMatrix);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    RGBPixel currentColor = ImageMatrix[i, j];

                    int currentColorIndex = colorIndices[currentColor];
                    int currentColorClusterIndex = set.FindSet(currentColorIndex);

                    RGBPixel newColor = ColorPallette[currentColorClusterIndex];
                    ImageMatrix[i, j] = newColor;

                }
            }

        }
    }
}

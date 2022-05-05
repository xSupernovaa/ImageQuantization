using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class ColorQuantization
    {
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
            List<RGBPixel> colorPallette = VertexSet.GetColorPallette(clusters);

            ///DEBUG
            Console.WriteLine("finished GetColorPallette at " + (MainForm.stopWatch.Elapsed).ToString());
            ///END DEBUG

            ReduceImageColors(ImageMatrix, colorPallette);

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


        // Not in use, might need it later
        private static double[,] createDistinctColorsGraphMatrix(List<RGBPixel> distinctColors)
        {
            int V = distinctColors.Count;
            double[,] GraphMatrix = new double[V, V];

            int offset = 0;

            for (int i = 0; i < V; i++)
            {
                for (int j = offset; j < V; j++)
                {
                    double edgeCost = Distance(distinctColors[i], distinctColors[j]);

                    GraphMatrix[i, j] = edgeCost;
                    GraphMatrix[j, i] = edgeCost;
                }

                offset++;
            }

            return GraphMatrix;

        }

        // Not in use, might need it later
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

        private static void ReduceImageColors(RGBPixel[,] ImageMatrix, List<RGBPixel> ColorPallette)
        {
            int rows = ImageOperations.GetHeight(ImageMatrix);
            int columns = ImageOperations.GetWidth(ImageMatrix);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    RGBPixel currentColor = ImageMatrix[i, j];

                    // Can we do better?
                    RGBPixel newColor = Search(ColorPallette, currentColor);
                    ImageMatrix[i, j] = newColor;
                }
            }
            
        }

        private static RGBPixel Search(List<RGBPixel> colorPallette, RGBPixel currentColor)
        {
            int minIndex = 0;
            double minDistance = 999999;

            double distance = minDistance;
            int indx = 0;

            foreach (RGBPixel color in colorPallette)
            {
                distance = Distance(currentColor, color);
                
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    minIndex = indx;
                }

                indx++;
            }

            return colorPallette[minIndex];
        }
    }
}

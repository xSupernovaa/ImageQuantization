using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class ColorQuantization
    {
        // this is bad for memory
        private static Dictionary<int, int> colorIndices;

        public static RGBPixel[,] ColorQuantize(RGBPixel[,] ImageMatrix, int number_of_clusters)
        {

            List<RGBPixel> distinctColorsList = GetDistinctColorsList(ImageMatrix);

            List<Edge> distinctColorsGraph = createDistinctColorsGraphEdgeList(distinctColorsList);  

            VertexSet set = Kruskal.RunKruskal(distinctColorsList ,distinctColorsGraph, number_of_clusters);

            Dictionary<int, List<RGBPixel>> clusters = VertexSet.GetClusters(distinctColorsList, set.getMembers());

            Dictionary<int, RGBPixel> colorPallette = VertexSet.GetColorPallette(clusters);

            ReduceImageColors(ImageMatrix, colorPallette, set);

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

            colorIndices = new Dictionary<int, int>(distinctColors.Count);
         

            for (int i = 0; i < colorsList.Count; i++)
                colorIndices.Add(RGBPixel.Hash(colorsList[i]), i);

            Console.WriteLine("finished GetDistinctColorsList at " + (MainForm.stopWatch.Elapsed).ToString());

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
                    Edge edge = new Edge(i, j, weight);
                    GraphEdgeList.Add(edge);
                }
            }

            Console.WriteLine("finished createDistinctColorsGraphEdgeList at " + (MainForm.stopWatch.Elapsed).ToString());

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

                    int currentColorIndex = colorIndices[RGBPixel.Hash(currentColor)];
                    int currentColorClusterIndex = set.FindSet(currentColorIndex);

                    RGBPixel newColor = ColorPallette[currentColorClusterIndex];
                    ImageMatrix[i, j] = newColor;

                }
            }
            Console.WriteLine("finished ReduceImageColors at " + (MainForm.stopWatch.Elapsed).ToString());

        }


    }
}

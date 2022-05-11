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

            List<int> distinctColorsList = GetDistinctColorsList(ImageMatrix);

            //double[,] graph = createDistinctColorsGraphMatrix(distinctColorsList);

            PrimMST(distinctColorsList);
            //List<Edge> distinctColorsGraph = createDistinctColorsGraphEdgeList(distinctColorsList);

            //VertexSet set = Kruskal.RunKruskal(distinctColorsList.Count, distinctColorsGraph, number_of_clusters);

            //Dictionary<int, List<int>> clusters = VertexSet.GetClusters(distinctColorsList, set.getMembers());

            //Dictionary<int, RGBPixel> colorPallette = VertexSet.GetColorPallette(clusters);

            //ReduceImageColors(ImageMatrix, colorPallette, set);

            //int countColorsBefore = distinctColorsList.Count;
            //int countColorsAfter = colorPallette.Count;
            //Console.WriteLine("Reduced number of colors in image from " + countColorsBefore + " to " + countColorsAfter);

            return ImageMatrix;
        }

        private static void PrimMST(List<int> distinctColors)
        {
            int V = distinctColors.Count;
            int[] parent = new int[V];
            double[] key = new double[V];
            bool[] mstSet = new bool[V];
            //initialize all keys as infinite
            for (int i = 0; i < V; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }
            key[0] = 0;
            parent[0] = -1;
            for (int i = 0; i < V - 1; i++)
            {
                int u = minKey(key, mstSet);
                mstSet[u] = true;
                //relax all edges connected to u
                for (int v = 0; v < V; v++)
                {
                    if (graph(distinctColors, u, v) != 0 
                        && mstSet[v] == false 
                        && graph(distinctColors, u, v) < key[v]
                        )
                    {
                        parent[v] = u;
                        key[v] = graph(distinctColors, u, v);
                    }
                }
            }
            double totalWeight = GetSumMST(distinctColors, parent);
            Console.WriteLine("Total weight of MST is " + totalWeight);

        }
        private static double GetSumMST(List<int> distinctColors, int[] parent)
        {
            double sum = 0;
            int V = distinctColors.Count;
            for (int i = 1; i < V; i++)
            {
                sum += graph(distinctColors, i, parent[i]);
            }
            return sum;
        }

        private static int minKey(double[] key, bool[] mstSet)
        {
            double min = double.MaxValue;
            int min_index = -1;
            for (int v = 0; v < key.Length; v++)
            {
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }
            }
            return min_index;
        }
        private static double graph(List<int> distinctColors, int i, int j)
        {
            return Distance(
                        RGBPixel.UnHash(distinctColors[i]),
                        RGBPixel.UnHash(distinctColors[j])
                        );
        }
            private static double[,] createDistinctColorsGraphMatrix(List<int> distinctColors)
        {
            int V = distinctColors.Count;
            double[,] GraphMatrix = new double[V, V];

            //int offset = 0;

            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    double edgeCost = Distance(
                        RGBPixel.UnHash(distinctColors[i]),
                        RGBPixel.UnHash(distinctColors[j])
                        );

                    GraphMatrix[i, j] = edgeCost;
                    GraphMatrix[j, i] = edgeCost;
                }

                //offset++;
            }

            return GraphMatrix;

        }

        private static List<int> GetDistinctColorsList(RGBPixel[,] ImageMatrix)
        {
            HashSet<int> distinctColors = new HashSet<int>();


            foreach (RGBPixel pixel in ImageMatrix)
                distinctColors.Add(RGBPixel.Hash(pixel));

            List<int> colorsList = distinctColors.ToList();

            colorIndices = new Dictionary<int, int>(distinctColors.Count);


            for (int i = 0; i < colorsList.Count; i++)
                colorIndices.Add(colorsList[i], i);

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

        private static List<Edge> createDistinctColorsGraphEdgeList(List<int> distinctColors)
        {
            int V = distinctColors.Count;
            int E = V * ((V - 1) / 2);
            List<Edge> GraphEdgeList = new List<Edge>(E);

            for (int i = 0; i < V; i++)
            {
                for (int j = i + 1; j < V; j++)
                {
                    double weight = Distance(RGBPixel.UnHash(distinctColors[i]), RGBPixel.UnHash(distinctColors[j]));
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

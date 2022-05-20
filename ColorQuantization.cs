using System;
using System.Collections.Generic;
using System.Linq;
using static ImageQuantization.MST;

namespace ImageQuantization
{
    class ColorQuantization
    {
        // this is bad for memory
        private static Dictionary<int, int> colorIndices;
        public static List<int> distinctColorsList;
        public static uint noise = 0;

        public static RGBPixel[,] ColorQuantize(RGBPixel[,] ImageMatrix, int number_of_clusters)
        {
            

            distinctColorsList = GetDistinctColorsList(ImageMatrix);

            int[] parent = Prim(distinctColorsList);

            ConstructEdges(distinctColorsList, parent);


            List<Edge> edges = ClusterEdges(number_of_clusters);

            Forest forest = new Forest(MST.edges);

            Dictionary<int, List<RGBPixel>> clusters = Forest.BFS(distinctColorsList.Count);

            Dictionary<int, short> clusterIndices = PopulateClusterIndicies(clusters);

            Dictionary<int, RGBPixel> colorPallette = GetColorPallette(clusters);

            ReduceImageColors(ImageMatrix, colorPallette, clusterIndices);

            int countColorsBefore = distinctColorsList.Count;
            int countColorsAfter = colorPallette.Count;
            Console.WriteLine("number of CLUSTERS: " + clusters.Count);
            Console.WriteLine("Reduced number of colors in image from " + countColorsBefore + " to " + countColorsAfter);
            Console.WriteLine("Noise: " + noise.ToString());
            return ImageMatrix;
        }

        private static Dictionary<int, short> PopulateClusterIndicies(Dictionary<int, List<RGBPixel>> clusters)
        {
            Console.WriteLine("START PopulateClusterIndicies at: " + (MainForm.stopWatch.Elapsed).ToString());
            Dictionary<int, short> clusterIndices = new Dictionary<int, short>();
            foreach (var cluster in clusters)
            {
                short index = (short)cluster.Key;

                foreach (RGBPixel pixel in cluster.Value)
                {
                    int pixelIndex = distinctColorsList.IndexOf(RGBPixel.Hash(pixel));
                    if (!clusterIndices.ContainsKey(pixelIndex))
                    {
                        clusterIndices.Add(pixelIndex, index);
                    }
                    else
                    {
                        clusterIndices.Add(pixelIndex, index);
                    }
                }

            }

            Console.WriteLine("finished PopulateClusterIndicies at " + (MainForm.stopWatch.Elapsed).ToString());
            return clusterIndices;
        }

        public static Dictionary<int, RGBPixel> GetColorPallette(Dictionary<int, List<RGBPixel>> clusters)
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

            return colorPallete;

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
            Console.WriteLine("Number of distinct colors is " + distinctColors.Count);
            return colorsList;
        }

        public static int GetWeight(int a, int b)
        {
            RGBPixel aPixel = RGBPixel.UnHash(a);
            RGBPixel bPixel = RGBPixel.UnHash(b);
            int differenceRed = (aPixel.red - bPixel.red) * (aPixel.red - bPixel.red);
            int differenceGreen = (aPixel.green - bPixel.green) * (aPixel.green - bPixel.green);
            int differenceBlue = (aPixel.blue - bPixel.blue) * (aPixel.blue - bPixel.blue);
            return differenceRed + differenceGreen + differenceBlue;
        }

        public static double GetDistance(int weight)
        {
            return Math.Sqrt(weight);
        }

        private static void ReduceImageColors(RGBPixel[,] ImageMatrix, Dictionary<int, RGBPixel> ColorPallette, Dictionary<int, short> clusterIndices)
        {
            int rows = ImageOperations.GetHeight(ImageMatrix);
            int columns = ImageOperations.GetWidth(ImageMatrix);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    RGBPixel currentColor = ImageMatrix[i, j];

                    int currentColorIndex = colorIndices[RGBPixel.Hash(currentColor)];
                    int currentColorClusterIndex = clusterIndices[currentColorIndex];

                    RGBPixel newColor = ColorPallette[currentColorClusterIndex];
                    ImageMatrix[i, j] = newColor;

                }
            }
            Console.WriteLine("finished ReduceImageColors at " + (MainForm.stopWatch.Elapsed).ToString());

        }
        public static  Dictionary<int, List<RGBPixel>> Truecluster( Dictionary<int, List<RGBPixel>> cluster,int num_of_clusters)
        {
            if (cluster.Count < num_of_clusters)
            {
                Dictionary<int, RGBPixel>ColorPallette=GetColorPallette(cluster);
                List<RGBPixel> colorsrep=new List<RGBPixel>();
                foreach(var color in ColorPallette.Values)
                {
                    colorsrep.Add(color);
                }
                
                for(int i = 0; i < num_of_clusters - cluster.Count; i++)
                {
                    int lastkey=Forest.getlaskey();
                    lastkey++;
                      cluster.Add(lastkey,colorsrep);
                    
                   
                }

            }
            return cluster;
        }

       

    }
}

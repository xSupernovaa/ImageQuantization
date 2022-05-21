using System;
using System.Collections.Generic;
using System.Linq;
using static ImageQuantization.MST;


namespace ImageQuantization
{
    class ColorQuantization
    {
        public static List<int> distinctColorsList;
        public static uint noise = 0;
        public static int k;

        public static RGBPixel[,] ColorQuantize(RGBPixel[,] ImageMatrix, int number_of_clusters)
        {
            
            distinctColorsList = GetDistinctColorsList(ImageMatrix);

            int[] parent = Prim(distinctColorsList); //O(D^2)

            ConstructEdges(distinctColorsList, parent); //O(D)

            if(Config.BOUNS)
                EdgesOfclustersB(number_of_clusters);
            else
                ClusterEdges(number_of_clusters); //O(DlogD)


            Forest forest = new Forest(MST.edges); //O(D-K)

            List<List<RGBPixel>> clusters = Forest.GetClusters(distinctColorsList.Count);
            if(Config.BOUNS)
                clusters = Truecluster(clusters, number_of_clusters);

            k = clusters.Count;
            Dictionary<int, short> clusterIndices = PopulateClusterIndicies(clusters); //O(D)

            List<RGBPixel> colorPallette = GetColorPallette(clusters); //O(D)

            ReduceImageColors(ImageMatrix, colorPallette, clusterIndices);

            int countColorsBefore = distinctColorsList.Count;
            int countColorsAfter = colorPallette.Count;
            Console.WriteLine("number of CLUSTERS: " + clusters.Count);
            Console.WriteLine("Reduced number of colors in image from " + countColorsBefore + " to " + countColorsAfter);
            Console.WriteLine("Noise: " + noise.ToString());
            return ImageMatrix;
        }

        private static Dictionary<int, short> PopulateClusterIndicies(List<List<RGBPixel>> clusters) //O(D) 
        {
            Console.WriteLine("START PopulateClusterIndicies at: " + (MainForm.stopWatch.Elapsed).ToString());
            Dictionary<int, short> clusterIndices = new Dictionary<int, short>();
            for(int i = 0; i< clusters.Count; i++)
            //this will loop over all nodes in all clusters,
            //since number of nodes is D then this whole loop takes
            //O(D)
            {
                var cluster = clusters[i];
                for (int j = 0; j < cluster.Count; j++)
                {
                    clusterIndices.Add(RGBPixel.Hash(cluster[j]), (short)i);
                }
            }

            Console.WriteLine("finished PopulateClusterIndicies at " + (MainForm.stopWatch.Elapsed).ToString());
            return clusterIndices;
        }

        public static List<RGBPixel> GetColorPallette(List<List<RGBPixel>> clusters) //O(D)
        {
            // for every member of cluster sum all values and get the mean for the sum
            List<RGBPixel> colorPallete = new List<RGBPixel>();
            for (int clusterIndex = 0; clusterIndex < clusters.Count; clusterIndex++)
            //this will loop over all nodes in all clusters,
            //since number of nodes is D then this whole loop takes
            //O(D)
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
                colorPallete.Add(representitaveColor);
            }

            return colorPallete;

        }

        private static List<int> GetDistinctColorsList(RGBPixel[,] ImageMatrix) //O(N^2)
        {
            HashSet<int> distinctColors = new HashSet<int>();

            foreach (RGBPixel pixel in ImageMatrix)
                distinctColors.Add(RGBPixel.Hash(pixel)); // O(N^2) if C# handles resizing well

            List<int> colorsList = distinctColors.ToList();

            Console.WriteLine("finished GetDistinctColorsList at " + (MainForm.stopWatch.Elapsed).ToString());
            Console.WriteLine("Number of distinct colors is " + distinctColors.Count);
            return colorsList;
        }

        public static int GetWeight(int a, int b) //O(1)
        {
            RGBPixel aPixel = RGBPixel.UnHash(a);
            RGBPixel bPixel = RGBPixel.UnHash(b);
            int differenceRed = (aPixel.red - bPixel.red) * (aPixel.red - bPixel.red);
            int differenceGreen = (aPixel.green - bPixel.green) * (aPixel.green - bPixel.green);
            int differenceBlue = (aPixel.blue - bPixel.blue) * (aPixel.blue - bPixel.blue);
            return differenceRed + differenceGreen + differenceBlue;
        }

        public static double GetDistance(int weight) //O(1)
        {
            return Math.Sqrt(weight);
        }

        private static void ReduceImageColors(RGBPixel[,] ImageMatrix, List<RGBPixel> ColorPallette, Dictionary<int, short> clusterIndices)
        {
            int rows = ImageOperations.GetHeight(ImageMatrix);
            int columns = ImageOperations.GetWidth(ImageMatrix);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    RGBPixel currentColor = ImageMatrix[i, j];

                    int currentColorClusterIndex = clusterIndices[RGBPixel.Hash(currentColor)];

                    RGBPixel newColor = ColorPallette[currentColorClusterIndex];
                    ImageMatrix[i, j] = newColor;

                }
            }
            Console.WriteLine("finished ReduceImageColors at " + (MainForm.stopWatch.Elapsed).ToString());

        }
        public static List<List<RGBPixel>> Truecluster(List<List<RGBPixel>> cluster, int num_of_clusters)
        {
            if (cluster.Count < num_of_clusters)
            {
                List<RGBPixel> ColorPallette = GetColorPallette(cluster);
                List<RGBPixel> colorsrep = new List<RGBPixel>();
                foreach (var color in ColorPallette)
                {
                    colorsrep.Add(color);
                }

                for (int i = 0; i < num_of_clusters - cluster.Count; i++)
                {
                    
                    cluster.Add(colorsrep);
                }

            }
            return cluster;
        }

        internal static void Reset() //O(1)
        {
            distinctColorsList = null;
            noise = 0;
            MST.edges = null;
            MST.sum = 0;
        }
    }
}

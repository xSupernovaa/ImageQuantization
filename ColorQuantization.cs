using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ImageQuantization.MST;

namespace ImageQuantization
{
    class ColorQuantization
    {
        // this is bad for memory
        private static Dictionary<int, int> colorIndices;
        public static List<int> distinctColorsList;


        public static int noise = 0;

        public static bool[] visited;        
        public static RGBPixel[,] ColorQuantize(RGBPixel[,] ImageMatrix, int number_of_clusters)
        {

            distinctColorsList = GetDistinctColorsList(ImageMatrix);

            int[] parent = Prim(distinctColorsList);
            
            List<Edge> edges = ConstructMSTEdges(distinctColorsList, parent);
            
            List<int> roots = ClusterEdges(number_of_clusters);

            Graph graph = new Graph(distinctColorsList.Count);
            graph.PopulateAdjList();

            graph.BFS(roots);
            Dictionary<int, List<RGBPixel>> clusters = graph.GetClusters();
            //Dictionary<int, List<int>> children = BuildChildren(parent, distinctColorsList.Count);



            //Dictionary<int, List<RGBPixel>> clusters = GetClusters(children, distinctColorsList);

            //visited = new bool[distinctColorsList.Count];
            //for (int i = 0; i < distinctColorsList.Count; i++)
            //    visited[i] = false;

            //Dictionary<int, List<RGBPixel>> clusters = GetClusters(children, distinctColorsList, roots);
            Console.WriteLine("number of CLUSTERS: " + clusters.Count);

            Dictionary<int, RGBPixel> colorPallette = GetColorPallette(clusters);

            ReduceImageColors(ImageMatrix, colorPallette, distinctColorsList, clusters);

            int countColorsBefore = distinctColorsList.Count;
            int countColorsAfter = colorPallette.Count;
            Console.WriteLine("Reduced number of colors in image from " + countColorsBefore + " to " + countColorsAfter);
            Console.WriteLine("Noise: " + noise.ToString());
            return ImageMatrix;
        }
        
        public static List<RGBPixel> BFS(int root, int V, List<int> childrenOfRoot, List<int> distinctColors)
        {

            // Mark all the vertices as not
            // visited(By default set as false)
            

            // Create a queue for BFS
            LinkedList<int> queue = new LinkedList<int>();

            // Mark the current node as
            // visited and enqueue it
            visited[root] = true;
            queue.AddLast(root);

            List<RGBPixel> cluster = new List<RGBPixel>();

            while (queue.Any())
            {

                // Dequeue a vertex from queue
                // and print it
                root = queue.First();
                if (!visited[root])
                    break;
                //Console.Write(root + " ");
                cluster.Add(RGBPixel.UnHash(distinctColors[root]));
                queue.RemoveFirst();

                // Get all adjacent vertices of the
                // dequeued vertex s. If a adjacent
                // has not been visited, then mark it
                // visited and enqueue it
                LinkedList<int> list = new LinkedList<int>(childrenOfRoot);

                foreach (var val in list)
                {
                    if (!visited[val])
                    {
                        visited[val] = true;
                        queue.AddLast(val);
                    }
                }
            }
            return cluster;
        }

        private static Dictionary<int, List<RGBPixel>> GetClusters(Dictionary<int, List<int>> children, List<int> distinctColors, List<int> roots)
        {
            Dictionary<int, List<RGBPixel>> clusters = new Dictionary<int, List<RGBPixel>>();
            int index = -1;
            foreach (int root in roots)
            {
                index++;
                List<RGBPixel> cluster = BFS(root, distinctColors.Count, children[root], distinctColors);
                if (cluster.Count > 0)
                    clusters.Add(index, cluster);


            }

            return clusters;
        }


        //private static Dictionary<int, List<RGBPixel>> GetClusters(Dictionary<int, List<int>> children, List<int> distinctColors)
        //{
        //    Dictionary<int, List<RGBPixel>> clusters = new Dictionary<int, List<RGBPixel>>();
        //    int index = -1;
        //    foreach (var list in children)
        //    {
        //        if (list.Key == -1)
        //            continue;
        //        index++;

        //        List<RGBPixel> cluster = new List<RGBPixel>();

        //        RGBPixel p = RGBPixel.UnHash(distinctColors[list.Key]);
        //        cluster.Add(p);

        //        foreach (var child in list.Value)
        //        {
        //            RGBPixel x = RGBPixel.UnHash(distinctColors[child]);
        //            cluster.Add(x);
        //        }
        //        clusters.Add(index, cluster);
        //    }

        //    return clusters;
        //}

        public static bool IsRGBPixelEqual(RGBPixel a, RGBPixel b)
        {
            return a.red == b.red && a.green == b.green && a.blue == b.blue;
        }

        public static int FindClusterIndex(int currColorIndex, List<int> distinctColors, Dictionary<int, List<RGBPixel>> clusters)
        {
            RGBPixel currColor = RGBPixel.UnHash(distinctColors[currColorIndex]);
            foreach (var cluster in clusters)
            {
                if (cluster.Value.Contains(currColor))
                    return cluster.Key;
            }
            noise++;
            return 0;
            throw new Exception("Could not find cluster index");
        }

        public static void PrintClusters(Dictionary<int, List<RGBPixel>> clusters)
        {
            foreach (var cluster in clusters)
            {
                Console.WriteLine("cluster " + cluster.Key.ToString());
                foreach (var p in cluster.Value)
                {
                    Console.WriteLine(p.red + ", " + p.green + ", " + p.blue);
                }
            }
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

            Console.WriteLine("finished GetColorPallette at " + (MainForm.stopWatch.Elapsed).ToString());
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
            return distinctColors.ToList();
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

        private static void ReduceImageColors(RGBPixel[,] ImageMatrix, Dictionary<int, RGBPixel> ColorPallette, List<int> distinctColors, Dictionary<int, List<RGBPixel>> clusters)
        //this function previously took VertexSet as a parameter which is not used anymore
        {
            int rows = ImageOperations.GetHeight(ImageMatrix);
            int columns = ImageOperations.GetWidth(ImageMatrix);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    RGBPixel currentColor = ImageMatrix[i, j];

                    int currentColorIndex = colorIndices[RGBPixel.Hash(currentColor)];
                    int currentColorClusterIndex = FindClusterIndex(currentColorIndex, distinctColors, clusters);//WAS: set.FindSet(currentColorIndex);

                    RGBPixel newColor = ColorPallette[currentColorClusterIndex];
                    ImageMatrix[i, j] = newColor;

                }
            }
            Console.WriteLine("finished ReduceImageColors at " + (MainForm.stopWatch.Elapsed).ToString());

        }


    }
}

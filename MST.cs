using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public class MST
    {
        public static List<Edge> edges;
        //MST SUM
        public static double totalWeight;

        public static void Prim(List<int> distinctColors, int num_of_clusters)
        {
            int V = distinctColors.Count;
            int[] parent = new int[V];
            int[] key = new int[V];
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
                int u = GetMinimumKey(key, mstSet);
                mstSet[u] = true;
                //relax all edges connected to u
                for (int v = 0; v < V; v++)
                {
                    //double distance = graph(u, v);
                    int distance = ColorQuantization.GetWeight(
                        distinctColors[u],
                        distinctColors[v]
                        );
                    if (distance != 0
                        && mstSet[v] == false
                        && distance < key[v]
                        )
                    {
                        parent[v] = u;
                        key[v] = distance;
                    }
                }
            }
            
            ConstructMSTEdges(distinctColors, parent);

            ClusterEdges(num_of_clusters);
            
            Console.WriteLine("Total weight of MST is " + totalWeight);

            Console.WriteLine("finished Prim at " + (MainForm.stopWatch.Elapsed).ToString());

        }
        public static List<Edge> ConstructMSTEdges(List<int> distinctColors, int[] parent)
        {
            List<Edge> edges = new List<Edge>();
            double sum = 0;
            int V = distinctColors.Count;
            for (int i = 1; i < V; i++)
            {
                int weight = ColorQuantization.GetWeight(distinctColors[i], distinctColors[parent[i]]);
                sum += ColorQuantization.GetDistance(weight);
                Edge e = new Edge(parent[i], i, weight);
                edges.Add(e);
            }
            totalWeight = sum;
            MST.edges = edges;
            Console.WriteLine("number of edges before clustering: = " + edges.Count);
            return edges;
        }
        public static List<Edge> ClusterEdges(int num_of_clusters)
        {
            if (edges == null)
            {
                throw new Exception("MST is not constructed");
            }
            edges.Sort();
            for (int i = 0; i < num_of_clusters - 1; i++)
            {
                edges.RemoveAt(edges.Count - 1);
            }
            Console.WriteLine("number of edges after clustering: = " + edges.Count);
            return edges;
        }

        //Probably not useful only for refrence
        public static List<List<int>> GetClusterdNodes(List<int> distinctColors, int num_of_clusters)
        {
            List<List<int>> clusters = new List<List<int>>();
            bool[] visited = new bool[distinctColors.Count];
            for (int i = 0; i < distinctColors.Count; i++)
            {
                visited[i] = false;
            }
            foreach (Edge e in edges)
            {
                List<int> cluster = new List<int>();
                cluster.Add(e.from);
                cluster.Add(e.to);
                visited[e.from] = true;
                visited[e.to] = true;
            }

            for (int i = 0; i < distinctColors.Count; i++)
            {
                if (visited[i] == false)
                {
                    List<int> cluster = new List<int>();
                    cluster.Add(i);
                    clusters.Add(cluster);
                }
            }
            Console.WriteLine("computed number of clusters: = " + clusters.Count);
            return clusters;
        }

        //Probably useful
        public static void printClusters(List<List<int>> clusters)
        {
            for(int i = 0; i < clusters.Count; i++)
            {
                var currCluster = clusters[i];
                Console.WriteLine("cluster " + (i + 1).ToString());
                foreach(int ColorIndex in currCluster)
                {
                    RGBPixel p = RGBPixel.UnHash(ColorIndex);
                    Console.WriteLine(p.red + ", " + p.green + ", " + p.blue);
                }
            }

        }

        //probably useful
        public static int FindClusterIndex(List<List<int>> clusters, int index)
        {
            for(int i = 0; i < clusters.Count; i++)
            {
                List<int> list = clusters[i];
                if (list.Contains(index))
                {
                    return i;
                }
            }
            throw new Exception("index not found in cluster");
        }

        private static int GetMinimumKey(int[] key, bool[] mstSet)
        {
            int min = int.MaxValue;
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
        
    }
}

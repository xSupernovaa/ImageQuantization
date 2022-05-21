using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageQuantization
{
    public class MST
    {
        public static List<Edge> edges;
        //MST SUM
        public static double sum;

        public static int[] Prim(List<int> distinctColors) //O(D^2)
        {
            int V = distinctColors.Count;
            int[] parent = new int[V];
            int[] key = new int[V];
            bool[] mstSet = new bool[V];
            //initialize all keys as infinite
            for (int i = 0; i < V; i++)             //O(D)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }
            key[0] = 0;
            parent[0] = -1;
            for (int i = 0; i < V - 1; i++)             //O(D^2)
            {
                int u = GetMinimumKey(key, mstSet); //(D)
                mstSet[u] = true;
                //relax all edges connected to u
                for (int v = 0; v < V; v++)         //O(D)
                {
                    int distance = ColorQuantization.GetWeight( //O(1)
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

            Console.WriteLine("finished Prim at " + (MainForm.stopWatch.Elapsed).ToString());
            return parent;
        }

        public static void ConstructEdges(List<int> distinctColors, int[] parent) //O(D)
        {
            edges = new List<Edge>();
            int V = distinctColors.Count;
            for (int i = 1; i < V; i++)             //O(D)
            {
                int weight = ColorQuantization.GetWeight(distinctColors[i], distinctColors[parent[i]]);     //O(1)
                double distance = ColorQuantization.GetDistance(weight);        //O(1)
                sum += distance;
                Edge e = new Edge(parent[i], i, weight, distance);              //O(1)
                edges.Add(e);
            }
            Console.WriteLine("Total weight of MST is " + MST.sum);
        }

        public static List<Edge> ClusterEdges(int num_of_clusters)  //O(DlogD)
        {
            if (edges == null)
            {
                throw new Exception("MST is not constructed");
            }
            edges.Sort();      //O(DlogD)
            
            int numOfEdgesBefore = edges.Count;
            for (int i = 0; i < num_of_clusters - 1; i++)
                //O(K) where K is num_of_clusters <= D
                
            {
                int lastIndex = edges.Count - 1;
                edges.RemoveAt(lastIndex);
            }
            Console.WriteLine("number of edges removed = " + (numOfEdgesBefore - edges.Count).ToString());
            return edges;
        }

        public static int GetNumberOfClusters()
        {
            int edgesCount = edges.Count;

            double[] weights = new double[edgesCount];

            for (int i = 0; i < edges.Count; i++)
            {
                weights[i] = edges[i].weightAfterSR;
            }

            int numberOfClusters = Stats.MSDR(weights);

            return numberOfClusters;
        }






        private static int GetMinimumKey(int[] key, bool[] mstSet)  //O(D)
        {
            int min = int.MaxValue;
            int min_index = -1;
            for (int v = 0; v < key.Length; v++)        //O(D)
            {
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }
            }
            return min_index;
        }
        //BOUNSE NUM2
         public static double mean=0;
         public static long StandardDeviation(List<Edge> edges)
        {
            long stdDv = 0;
            double sum = 0;//after take squareroot
            long sumAllQ = 0;//after take square root and ^2

          
           for (int i = 0; i < edges.Count; i++)
             {
           
                  sum +=edges[i].weightAfterSR ;
                   sumAllQ +=edges[i].weightBeforeSR;
              }
            mean = sum / (long)edges.Count;
        //Standard deviation
           stdDv = (long)System.Math.Sqrt((sumAllQ - (sum * sum) / edges.Count) * (1.0 / (edges.Count - 1)));

             return stdDv;
             }
         public static List<Edge> EdgesOfclustersB(int num_of_clusters)
          {
            long stDV=StandardDeviation(edges);
            List<Edge> lastdelete=new List<Edge>();
            int Q=1;
            for (int i = 0; i < edges.Count; i++)
            { 
                if (edges[i].weightAfterSR > mean + stDV)
                {
                    Q++;
                   lastdelete.Add(edges[i]);
                  edges.RemoveAt(i);
                }
             }
            if (edges.Count > num_of_clusters-1)
            {
                edges.Sort();
                 for (int i = 0; i < num_of_clusters -1; i++)
                 {
                int lastIndex = edges.Count-1 ;
                edges.RemoveAt(lastIndex);
                 }

            }
        
        return edges;
        
        }
   

       
   
        
    }
}

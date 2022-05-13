﻿using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    public class MST
    {
        public static List<Edge> edges;
        //MST SUM
        public static double totalWeight;

        public static int[] Prim(List<int> distinctColors)
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

            Console.WriteLine("finished Prim at " + (MainForm.stopWatch.Elapsed).ToString());
            return parent;
        }

        public static void ConstructMSTEdges(List<int> distinctColors, int[] parent)
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
            MST.totalWeight = sum;
            MST.edges = edges;
            Console.WriteLine("Total weight of MST is " + totalWeight);
        }
        public static List<Edge> ClusterEdges(int num_of_clusters)
        {
            if (edges == null)
            {
                throw new Exception("MST is not constructed");
            }
            edges.Sort();
            int numOfEdgesBefore = edges.Count;
            for (int i = 0; i < num_of_clusters - 1; i++)
            {
                int lastIndex = edges.Count - 1;
                edges.RemoveAt(lastIndex);
            }
            Console.WriteLine("number of edges removed = " + (numOfEdgesBefore - edges.Count).ToString());
            return edges;
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

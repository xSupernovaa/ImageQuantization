
using ImageQuantization;
using System;
using System.Collections.Generic;

class Graph2
{
    //Implementing graph using Dictionary 
    static Dictionary<int, List<int>> graph =
            new Dictionary<int, List<int>>();
    public static void PopulateGraph()
    {
        foreach (Edge edge in MST.edges)
        {
            addEdge(edge.from, edge.to);
        }
    }
    //utility function to add edge in an undirected graph 
    public static void addEdge(int a, int b)
    {
        if (graph.ContainsKey(a))
        {
            List<int> l = graph[a];
            l.Add(b);
            if (graph.ContainsKey(a))
                graph[a] = l;
            else
                graph.Add(a, l);
        }
        else
        {
            List<int> l = new List<int>();
            l.Add(b);
            graph.Add(a, l);
        }
    }

    // Helper function for BFS 
    public static List<RGBPixel> bfshelp(int s, List<bool> visited)
    {

        List<RGBPixel> cluster = new List<RGBPixel>();

        // Create a queue for BFS 
        List<int> q = new List<int>();

        // Mark the current node as visited and enqueue it 
        q.Add(s);
        visited.RemoveAt(s);
        visited.Insert(s, true);

        while (q.Count != 0)
        {
            // Dequeue a vertex from queue and print it 
            int f = q[0];
            q.RemoveAt(0);
            //Console.Write(f + " ");
            cluster.Add(IToPixel(f));

            //Check whether the current node is 
            //connected to any other node or not 
            if (graph.ContainsKey(f))
            {

                // Get all adjacent vertices of the dequeued 
                // vertex f. If an adjacent has not been visited, 
                // then mark it visited and enqueue it 

                foreach (int iN in graph[f])
                {
                    int n = iN;
                    if (!visited[n])
                    {
                        visited.RemoveAt(n);
                        visited.Insert(n, true);
                        q.Add(n);
                    }
                }
            }
        }
        return cluster;

    }

    private static RGBPixel IToPixel(int index)
    {
        return RGBPixel.UnHash(ColorQuantization.distinctColorsList[index]);
    }

    // BFS function to check each node 
    public static Dictionary<int, List<RGBPixel>> bfs(int vertex)
    {
        List<bool> visited = new List<bool>();
        Dictionary<int, List<RGBPixel>> clusters = new Dictionary<int, List<RGBPixel>>();
        // Marking each node as unvisited 
        for (int i = 0; i < vertex; i++)
        {
            visited.Insert(i, false);
        }
        int index = 0;
        for (int i = 0; i < vertex; i++)
        {
            // Checking whether the node is visited or not 
            if (!visited[i])
            {
                var cluster = bfshelp(i, visited);
                index++;
                clusters.Add(index, cluster);
            }
        }
        //bool allVisted = true;
        for (int i = 0; i < vertex; i++)
        {
            if (!visited[i])
            {
                Console.WriteLine("NOT ALL VISTED");
                break;
            }
        }
        return clusters;
    }

}
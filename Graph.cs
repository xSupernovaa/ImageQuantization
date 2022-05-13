// C# program to print BFS traversal
// from a given source vertex.
// BFS(int s) traverses vertices
// reachable from s.
using ImageQuantization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// This class represents a directed
// graph using adjacency list
// representation
class Graph
{

    // No. of vertices    
    private int _V;

    //Adjacency Lists
    LinkedList<int>[] _adj;
    bool[] visited;

    Dictionary<int, List<RGBPixel>> clusters;
    public Graph(int V)
    {
        _adj = new LinkedList<int>[V];
        for (int i = 0; i < _adj.Length; i++)
        {
            _adj[i] = new LinkedList<int>();
        }
        _V = V;
        visited = new bool[V];
        for (int i = 0; i < V; i++)
            visited[i] = false;
        clusters = new Dictionary<int, List<RGBPixel>>();
    }

    public void PopulateAdjList()
    {
        int num = 0;
        foreach (Edge edge in MST.edges)
        {
            num++;
            AddEdge(edge.from, edge.to);
        }
        Console.WriteLine("Number of edges ADDED TO ADJLIST: " + num);
    }

    // Function to add an edge into the graph
    public void AddEdge(int v, int w)
    {
        _adj[v].AddLast(w);
        _adj[w].AddLast(v);

    }

    private static RGBPixel IToPixel(int index)
    {
        return RGBPixel.UnHash(ColorQuantization.distinctColorsList[index]);
    }

    public Dictionary<int, List<RGBPixel>> GetClusters()
    {
        return clusters;
    }

    // Prints BFS traversal from a given source s
    public void BFS(List<int> roots)
    {
        int index = -1;
        foreach (int root in roots)
        {
            index++;
            List<RGBPixel> cluster = new List<RGBPixel>();
            // Create a queue for BFS
            LinkedList<int> queue = new LinkedList<int>();
            //bool[] visited = new bool[_V];
            // Mark the current node as
            // visited and enqueue it
            //if (visited[root])
            //    continue;
                //throw new Exception("Root already visited");
            visited[root] = true;
            queue.AddLast(root);

            while (queue.Any())
            {

                // Dequeue a vertex from queue
                // and print it
                int currRoot = queue.First();

                cluster.Add(IToPixel(currRoot));

                queue.RemoveFirst();

                // Get all adjacent vertices of the
                // dequeued vertex s. If a adjacent
                // has not been visited, then mark it
                // visited and enqueue it
                LinkedList<int> list = _adj[root];

                foreach (var val in list)
                {
                    if (!visited[val])
                    {
                        visited[val] = true;
                        queue.AddLast(val);
                    }
                }
            }
            clusters.Add(index, cluster);
        }
        int nodesVisited = 0;
        int nodesNotVisited = 0;
        for (int i = 0; i < visited.Length; i++)
        {
            if (!visited[i])
                nodesNotVisited++;
            else
                nodesVisited++;
        }

        Console.WriteLine("All nodes VISITED " + nodesVisited);
        Console.WriteLine("NODES NOT VISITED " + nodesNotVisited);
        Console.WriteLine("BOTH " + (nodesVisited + nodesNotVisited));
    }
}
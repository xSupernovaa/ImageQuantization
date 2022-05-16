using ImageQuantization;
using System.Collections.Generic;

class Graph
{
    static Dictionary<int, List<int>> graph =
            new Dictionary<int, List<int>>();
    public static void PopulateGraph()
    {
        foreach (Edge edge in MST.edges)
        {
            addEdge(edge.from, edge.to);
            addEdge(edge.to, edge.from);
        }
    }
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

    public static List<RGBPixel> bfshelp(int s, List<bool> visited)
    {

        List<RGBPixel> cluster = new List<RGBPixel>();

        List<int> q = new List<int>();

        q.Add(s);
        visited.RemoveAt(s);
        visited.Insert(s, true);

        while (q.Count != 0)
        {
            int f = q[0];
            q.RemoveAt(0);
            cluster.Add(IToPixel(f));

            if (graph.ContainsKey(f))
            {

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

    public static Dictionary<int, List<RGBPixel>> bfs(int vertex)
    {
        List<bool> visited = new List<bool>();
        Dictionary<int, List<RGBPixel>> clusters = new Dictionary<int, List<RGBPixel>>();
        for (int i = 0; i < vertex; i++)
        {
            visited.Insert(i, false);
        }
        int index = 0;
        for (int i = 0; i < vertex; i++)
        {
            if (!visited[i])
            {
                var cluster = bfshelp(i, visited);
                index++;
                clusters.Add(index, cluster);
            }
        }
        return clusters;
    }

}
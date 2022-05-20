using ImageQuantization;
using System.Collections.Generic;

class Forest
{
    static Dictionary<int, List<int>> Trees;
    public Forest(List<Edge> Edges)
    {
        Trees = new Dictionary<int, List<int>>();
        foreach (Edge edge in Edges)
        {
            addEdge(edge.from, edge.to);
            addEdge(edge.to, edge.from);
        }
    }

    private void addEdge(int a, int b)
    {
        if (Trees.ContainsKey(a))
        {
            List<int> l = Trees[a];
            l.Add(b);
            if (Trees.ContainsKey(a))
                Trees[a] = l;
            else
                Trees.Add(a, l);
        }
        else
        {
            List<int> l = new List<int>();
            l.Add(b);
            Trees.Add(a, l);
        }
    }

    public static List<RGBPixel> BFS_HELP(int s, List<bool> visited)
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

            if (Trees.ContainsKey(f))
            {

                foreach (int iN in Trees[f])
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

    public static Dictionary<int, List<RGBPixel>> BFS(int V)
    {
        List<bool> visited = new List<bool>();
        Dictionary<int, List<RGBPixel>> clusters = new Dictionary<int, List<RGBPixel>>();

        for (int i = 0; i < V; i++)
        {
            visited.Insert(i, false);
        }

        int index = 0;
        for (int i = 0; i < V; i++)
        {
            if (!visited[i])
            {
                var cluster = BFS_HELP(i, visited);
                index++;
                clusters.Add(index, cluster);
            }
        }
        return clusters;
    }
    

}
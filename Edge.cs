using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public class Edge : IComparable
    {
        public int from;
        public int to;
        // weight before square root
        public long weightBeforeSR;


        public Edge(int from, int to, long weightBeforeSR)
        {
            this.from = from;
            this.to = to;
            this.weightBeforeSR = weightBeforeSR;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Edge otherEdge = obj as Edge;
            return this.weightBeforeSR.CompareTo(otherEdge.weightBeforeSR);
        }
    }
}

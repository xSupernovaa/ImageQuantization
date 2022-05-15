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

        public double weightAfterSR;


        public Edge(int from, int to, long weightBeforeSR, double weightAfterSR)
        {
            this.from = from;
            this.to = to;
            this.weightBeforeSR = weightBeforeSR;
            this.weightAfterSR = weightAfterSR;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Edge otherEdge = obj as Edge;
            return this.weightAfterSR.CompareTo(otherEdge.weightAfterSR);
        }
    }
}

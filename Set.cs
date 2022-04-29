using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    /// <summary>
    /// Simple array based set
    /// </summary>
    internal class Set
    {
        private double[] members;
        private int membersSize = 0;

        Set()
        {
            members = new double[];
        }
        int num_of_sets = 0;
        public Set MakeSet(double u)
        {
            
            num_of_sets++
            return new Set(u);
        }
    }
}

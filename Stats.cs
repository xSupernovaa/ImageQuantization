using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Stats
    {
        // not very clean i know
        private static int MAX_WEIGHT_INDEX;

        private static double Mean(double[] weights, int count)
        {
            double sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] == -1)
                    continue;

                sum += weights[i];
            }
            sum /= count;
            return sum;
        }

        private static double Stddev(double[] weights, int count)
        {
            double mean = Mean(weights, count);
            double sum = 0;
            double max = double.MinValue;

            double sample;
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] == -1)
                    continue;

                sample = Math.Pow(weights[i] - mean, 2);

                if (sample > max)
                {
                    max = sample;
                    MAX_WEIGHT_INDEX = i;
                }

                sum += sample;
            }

            sum /= count;
            sum = Math.Sqrt(sum);
            return sum;
        }

        public static int MSDR(double[] weights)
        {
            int N = weights.Length;
            double old_stddev = Stddev(weights, N);
            double current_stddev;

            double diff;

            do
            {
                weights[MAX_WEIGHT_INDEX] = -1;
                N--;

                current_stddev = Stddev(weights, N);
                diff = old_stddev - current_stddev;

                old_stddev = current_stddev;
            }
            while (diff > 0.0001 && N > 1);

            return weights.Length - N;
        }

    }
}

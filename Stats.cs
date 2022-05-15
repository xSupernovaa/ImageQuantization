using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Stats
    {
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

        private static double Stddev(double[] weights, int count, double mean)
        {
            double sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] == -1)
                    continue;

                sum += Math.Pow(weights[i] - mean, 2);
            }

            sum /= count;
            sum = Math.Sqrt(sum);
            return sum;
        }

        private static double[] ZScore_ABS(double[] weights, double mean, double stddev)
        {
            double[] ZScores = new double[weights.Length];

            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] == -1)
                    continue;
                ZScores[i] = Math.Abs(((weights[i] - mean) / stddev));
            }

            return ZScores;
        }


        public static int MSDR(double[] weights)
        {
            int N = weights.Length;
            double old_mean = Mean(weights, N);
            double old_stddev = Stddev(weights, N, old_mean);
            double[] ZScores = ZScore_ABS(weights, old_mean, old_stddev);

            double current_mean;
            double current_stddev;

            double diff;

            do
            {
                // Twice the time i know, will do it manually if i have to
                double maxValue = ZScores.Max();
                int maxIndex = ZScores.ToList().IndexOf(maxValue);


                weights[maxIndex] = -1;
                N--;

                current_mean = Mean(weights, N);
                current_stddev = Stddev(weights, N, current_mean);
                ZScores = ZScore_ABS(weights, current_mean, current_stddev);

                diff = old_stddev - current_stddev;

                old_stddev = current_stddev;
            }
            while (diff > 0.0001);


            return N;
        }

    }
}

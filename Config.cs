using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public class Config
    {
        public const bool AUTOTEST = true;
        public const bool BOUNS = false;

        public const string testcasesPath = @"D:\Education\6th\Analysis and Design of Algorithms\Project\Project Sol\ImageQuantization\Testcases\";
        public const string outputPath = @"D:\Education\6th\Analysis and Design of Algorithms\Project\Project Sol\ImageQuantization\OUTPUT\";
        public const string dashes = "--------------------------------------------------------------------------------\n";

        public static string GetTestCaseResultString(string currImageName, string distinctColorsTextBox, string mst_sum_text_box, string totalTimeTextBox, string numOfClustersTextBox)
        {
            string testCaseResult = "Image name: " + currImageName + "\nNumber of distinct colors: " + distinctColorsTextBox + "\nMST sum: " + mst_sum_text_box + "\nTotal time: " + totalTimeTextBox + "\nCaluculated number of clusters: " + numOfClustersTextBox + "\n\n";
            return testCaseResult;
        }

        public static void VerifyOutputDirExistEmpty()
        {
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            else
            {
                string[] files = Directory.GetFiles(outputPath);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
        }
    }
}

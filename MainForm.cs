using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ImageQuantization;
using System.Drawing.Imaging;


namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public static Dictionary<string, string> requiredK; //makes testing easier
        public static string currImageName;

        void populateRequiredK()
        {
            string samplePath = Config.testcasesPath + @"Sample\Sample Test\";
            string completePath = Config.testcasesPath + @"Complete\Complete Test\";
            requiredK = new Dictionary<string, string>
            {
                //Sample
                { samplePath + @"Case1\Sample.Case1.bmp", "3" },
                { samplePath + @"Case2\Sample.Case2.bmp", "2" },
                { samplePath + @"Case3\Sample.Case3.bmp", "500" },
                { samplePath + @"Case4\Sample.Case4.bmp", "10" },
                { samplePath + @"Case5\Sample.Case5.bmp", "32" },
                //Complete
                //Small
                { completePath + @"Small\Small.Case1.bmp", "192" },
                { completePath + @"Small\Small.Case2.bmp", "2160" },
                ////Meduim
                //{ completePath + @"Medium\Medium.Case1.bmp", "1737" },
                //{ completePath + @"Medium\Medium.Case2.bmp", "2284" },
                ////Large
                //{ completePath + @"Large\Large.Case1.bmp", "3829" },
                //{ completePath + @"Large\Large.Case2.bmp", "25666" },
            };
        }

        public MainForm()
        {
            populateRequiredK();

            InitializeComponent();
            
            Config.VerifyOutputDirExistEmpty();
            if (Config.AUTOTEST)
                AutoTest();

        }

        private void AutoTest()
        {

            File.WriteAllText(Config.outputPath + "Test_Results.txt", Config.dashes);
            foreach (var path in requiredK.Keys)
            {
                OriginalImageMatrix = ImageOperations.OpenImage(path);
                currImageName = path;
                currImageName = currImageName.Substring(currImageName.LastIndexOf("\\") + 1);
                txtClusters.Text = requiredK[path];
                btnGaussSmooth_Click(null, EventArgs.Empty);
            }
            File.AppendAllText(Config.outputPath + "Test_Results.txt", Config.dashes + Environment.NewLine);
            ImageOperations.DisplayImage(OriginalImageMatrix, pictureBox1);

        }

        RGBPixel[,] OriginalImageMatrix;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                currImageName = OpenedFilePath;
                currImageName = currImageName.Substring(currImageName.LastIndexOf("\\"));

                OriginalImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(OriginalImageMatrix, pictureBox1);
                if (requiredK.ContainsKey(OpenedFilePath)) //makes testing easier
                {
                    txtClusters.Text = requiredK[OpenedFilePath];
                }
                else
                {
                    txtClusters.Text = "";
                }
            }
            txtWidth.Text = ImageOperations.GetWidth(OriginalImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(OriginalImageMatrix).ToString();
        }
        public static Stopwatch stopWatch;
        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            int clusters = int.Parse(txtClusters.Text);

            RGBPixel[,] QuantizedImageMatrix = (RGBPixel[,])OriginalImageMatrix.Clone();

            stopWatch = new Stopwatch();
            // Get the elapsed time as a TimeSpan value.
            stopWatch.Start();
            ColorQuantization.ColorQuantize(QuantizedImageMatrix, clusters);
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            distinctColorsTextBox.Text = ColorQuantization.distinctColorsList.Count.ToString();
            mst_sum_text_box.Text = String.Format("{0:0.00}", MST.sum);
            String tsString = ts.ToString();
            tsString = tsString.Substring(tsString.IndexOf(':')+1, 8);
            totalTimeTextBox.Text = tsString;
            numOfClustersTextBox.Text = ColorQuantization.k.ToString();
            
            string testCaseResult = Config.GetTestCaseResultString(currImageName, distinctColorsTextBox.Text, mst_sum_text_box.Text, totalTimeTextBox.Text, numOfClustersTextBox.Text);

            File.AppendAllText(Config.outputPath + "Test_Results.txt", testCaseResult + Environment.NewLine);


            if (pictureBox2.Image != null)
            {
                pictureBox2.Image = null;
                pictureBox2.Update();
                ColorQuantization.reset();
                if(!Config.AUTOTEST)
                    MessageBox.Show("Cleared!");
            }
            Bitmap ImageBMP = ImageOperations.DisplayImage(QuantizedImageMatrix, null);
            if(Config.AUTOTEST)
                ImageBMP.Save(Config.outputPath + "Result_" + currImageName, ImageFormat.Bmp);
            pictureBox2.Image = ImageBMP;
            if (!Config.AUTOTEST)
                MessageBox.Show(ts.ToString());
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_2(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void distinctColorsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void totalTimeTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

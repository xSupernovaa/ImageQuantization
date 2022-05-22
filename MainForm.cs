using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ImageQuantization;
using System.Drawing.Imaging;
using System.Windows.Threading;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public static Dictionary<string, string> requiredK; //makes testing easier
        public static string currImageName;
        public static Stopwatch stopWatch;

        void populateRequiredK()
        {
            string samplePath = Paths.testcasesPath + @"Sample\Sample Test\";
            string completePath = Paths.testcasesPath + @"Complete\Complete Test\";
            requiredK = new Dictionary<string, string>
            {
                ////Sample
                //{ samplePath + @"Case1\Sample.Case1.bmp", "3" },
                //{ samplePath + @"Case2\Sample.Case2.bmp", "2" },
                //{ samplePath + @"Case3\Sample.Case3.bmp", "500" },
                //{ samplePath + @"Case4\Sample.Case4.bmp", "10" },
                //{ samplePath + @"Case5\Sample.Case5.bmp", "32" },
                ////Complete
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

        private async void AutoTest()
        {

            File.WriteAllText(Paths.outputPath + "Test_Results.txt", Config.dashes);
            foreach (var path in requiredK.Keys)
            {
                OriginalImageMatrix = ImageOperations.OpenImage(path);
                ImageOperations.DisplayImage(OriginalImageMatrix, pictureBox1);
                txtWidth.Text = ImageOperations.GetWidth(OriginalImageMatrix).ToString();
                txtHeight.Text = ImageOperations.GetHeight(OriginalImageMatrix).ToString();
                currImageName = path;
                currImageName = currImageName.Substring(currImageName.LastIndexOf("\\") + 1);
                txtClusters.Text = requiredK[path];
                await RunColorQuantize();
            }
            File.AppendAllText(Paths.outputPath + "Test_Results.txt", Config.dashes + Environment.NewLine);

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
        private async void btnQuantize_Click(object sender, EventArgs e)
        {
            await RunColorQuantize();
        }
        
        private async Task RunColorQuantize()
        {
            ColorQuantization.Reset();

            int clusters = int.Parse(txtClusters.Text);

            RGBPixel[,] QuantizedImageMatrix = (RGBPixel[,])OriginalImageMatrix.Clone();

            stopWatch = new Stopwatch();
            // Get the elapsed time as a TimeSpan value.
            btnQuantize.Enabled = false;
            btnOpen.Enabled = false;
            string origText = quantizedLabel.Text;
            txtClusters.Enabled = false;
            quantizedLabel.Text = "Quantizing... Please wait";
            stopWatch.Start();
            //ColorQuantization.ColorQuantize(QuantizedImageMatrix, clusters);
            await Task.Run(() =>
            {
                ColorQuantization.ColorQuantize(QuantizedImageMatrix, clusters);
            });
            stopWatch.Stop();
            txtClusters.Enabled = true;
            quantizedLabel.Text = origText;
            btnQuantize.Enabled = true;
            btnOpen.Enabled = true;

            TimeSpan ts = stopWatch.Elapsed;
            distinctColorsTextBox.Text = ColorQuantization.distinctColorsList.Count.ToString();
            mst_sum_text_box.Text = String.Format("{0:0.00}", MST.sum);
            String tsString = ts.ToString();
            tsString = tsString.Substring(tsString.IndexOf(':') + 1, 8);
            totalTimeTextBox.Text = tsString;
            numOfClustersTextBox.Text = ColorQuantization.k.ToString();

            string testCaseResult = Config.GetTestCaseResultString(currImageName, distinctColorsTextBox.Text, mst_sum_text_box.Text, totalTimeTextBox.Text, numOfClustersTextBox.Text);

            File.AppendAllText(Paths.outputPath + "Test_Results.txt", testCaseResult + Environment.NewLine);


            if (pictureBox2.Image != null)
            {
                pictureBox2.Image = null;
                pictureBox2.Update();
                if (!Config.AUTOTEST)
                    MessageBox.Show("Cleared!");
            }
            Bitmap ImageBMP = ImageOperations.DisplayImage(QuantizedImageMatrix, null);
            if (Config.AUTOTEST)
                ImageBMP.Save(Paths.outputPath + "Result_" + currImageName, ImageFormat.Bmp);
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] OriginalImageMatrix;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                OriginalImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(OriginalImageMatrix, pictureBox1);
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


            if (pictureBox2.Image != null)
            {
                pictureBox2.Image = null;
                pictureBox2.Update();
                MessageBox.Show("Cleared!");
            }

            ImageOperations.DisplayImage(QuantizedImageMatrix, pictureBox2);

            MessageBox.Show(ts.ToString());
        }

    }
}
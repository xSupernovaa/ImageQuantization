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
            distinctColorsTextBox.Text = ColorQuantization.distinctColorsList.Count.ToString();
            mst_sum_text_box.Text = String.Format("{0:0.00}", MST.sum);
            String tsString = ts.ToString();
            tsString = tsString.Substring(tsString.IndexOf(':')+1, 8);
            totalTimeTextBox.Text = tsString;
            //numOfClustersTextBox.Text = //.ToString();

            if (pictureBox2.Image != null)
            {
                pictureBox2.Image = null;
                pictureBox2.Update();
                //ColorQuantization.reset();
                MessageBox.Show("Cleared!");
            }

            ImageOperations.DisplayImage(QuantizedImageMatrix, pictureBox2);

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
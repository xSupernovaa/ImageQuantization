namespace ImageQuantization
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.quantizedLabel = new System.Windows.Forms.Label();
            this.btnQuantize = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtClusters = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mst_sum_text_box = new System.Windows.Forms.TextBox();
            this.distinctColorsTextBox = new System.Windows.Forms.TextBox();
            this.numDisColorLabel = new System.Windows.Forms.Label();
            this.mstSumTextBox = new System.Windows.Forms.Label();
            this.totalTimeLabel = new System.Windows.Forms.Label();
            this.totalTimeTextBox = new System.Windows.Forms.TextBox();
            this.numOfClustersLabel = new System.Windows.Forms.Label();
            this.numOfClustersTextBox = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(579, 452);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(556, 452);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(452, 526);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(109, 76);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open Image";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(217, 484);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original Image";
            // 
            // quantizedLabel
            // 
            this.quantizedLabel.AutoSize = true;
            this.quantizedLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantizedLabel.Location = new System.Drawing.Point(807, 484);
            this.quantizedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.quantizedLabel.Name = "quantizedLabel";
            this.quantizedLabel.Size = new System.Drawing.Size(185, 24);
            this.quantizedLabel.TabIndex = 4;
            this.quantizedLabel.Text = "Quantized Image";
            // 
            // btnQuantize
            // 
            this.btnQuantize.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuantize.Location = new System.Drawing.Point(588, 526);
            this.btnQuantize.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuantize.Name = "btnQuantize";
            this.btnQuantize.Size = new System.Drawing.Size(135, 76);
            this.btnQuantize.TabIndex = 5;
            this.btnQuantize.Text = "Quantize";
            this.btnQuantize.UseVisualStyleBackColor = true;
            this.btnQuantize.Click += new System.EventHandler(this.btnQuantize_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(193, 576);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 21);
            this.label4.TabIndex = 9;
            this.label4.Text = "Clusters";
            // 
            // txtHeight
            // 
            this.txtHeight.Enabled = false;
            this.txtHeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeight.Location = new System.Drawing.Point(97, 573);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.ReadOnly = true;
            this.txtHeight.Size = new System.Drawing.Size(75, 27);
            this.txtHeight.TabIndex = 8;
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWidth
            // 
            this.txtWidth.Enabled = false;
            this.txtWidth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWidth.Location = new System.Drawing.Point(97, 526);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.ReadOnly = true;
            this.txtWidth.Size = new System.Drawing.Size(75, 27);
            this.txtWidth.TabIndex = 11;
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 529);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 21);
            this.label5.TabIndex = 12;
            this.label5.Text = "Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 576);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 21);
            this.label6.TabIndex = 13;
            this.label6.Text = "Height";
            // 
            // txtClusters
            // 
            this.txtClusters.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClusters.Location = new System.Drawing.Point(336, 576);
            this.txtClusters.Margin = new System.Windows.Forms.Padding(4);
            this.txtClusters.Name = "txtClusters";
            this.txtClusters.Size = new System.Drawing.Size(75, 27);
            this.txtClusters.TabIndex = 14;
            this.txtClusters.Text = "1";
            this.txtClusters.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 456);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(628, 15);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 456);
            this.panel2.TabIndex = 16;
            // 
            // mst_sum_text_box
            // 
            this.mst_sum_text_box.Enabled = false;
            this.mst_sum_text_box.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mst_sum_text_box.Location = new System.Drawing.Point(1083, 570);
            this.mst_sum_text_box.Margin = new System.Windows.Forms.Padding(4);
            this.mst_sum_text_box.Name = "mst_sum_text_box";
            this.mst_sum_text_box.ReadOnly = true;
            this.mst_sum_text_box.Size = new System.Drawing.Size(105, 27);
            this.mst_sum_text_box.TabIndex = 18;
            this.mst_sum_text_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // distinctColorsTextBox
            // 
            this.distinctColorsTextBox.Enabled = false;
            this.distinctColorsTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distinctColorsTextBox.Location = new System.Drawing.Point(336, 526);
            this.distinctColorsTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.distinctColorsTextBox.Name = "distinctColorsTextBox";
            this.distinctColorsTextBox.ReadOnly = true;
            this.distinctColorsTextBox.Size = new System.Drawing.Size(75, 27);
            this.distinctColorsTextBox.TabIndex = 19;
            this.distinctColorsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.distinctColorsTextBox.TextChanged += new System.EventHandler(this.distinctColorsTextBox_TextChanged);
            // 
            // numDisColorLabel
            // 
            this.numDisColorLabel.AutoSize = true;
            this.numDisColorLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDisColorLabel.Location = new System.Drawing.Point(193, 529);
            this.numDisColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numDisColorLabel.Name = "numDisColorLabel";
            this.numDisColorLabel.Size = new System.Drawing.Size(135, 21);
            this.numDisColorLabel.TabIndex = 20;
            this.numDisColorLabel.Text = "Distinct Colors";
            this.numDisColorLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // mstSumTextBox
            // 
            this.mstSumTextBox.AutoSize = true;
            this.mstSumTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mstSumTextBox.Location = new System.Drawing.Point(986, 573);
            this.mstSumTextBox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mstSumTextBox.Name = "mstSumTextBox";
            this.mstSumTextBox.Size = new System.Drawing.Size(89, 21);
            this.mstSumTextBox.TabIndex = 21;
            this.mstSumTextBox.Text = "MST Sum";
            // 
            // totalTimeLabel
            // 
            this.totalTimeLabel.AutoSize = true;
            this.totalTimeLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalTimeLabel.Location = new System.Drawing.Point(731, 573);
            this.totalTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalTimeLabel.Name = "totalTimeLabel";
            this.totalTimeLabel.Size = new System.Drawing.Size(138, 21);
            this.totalTimeLabel.TabIndex = 22;
            this.totalTimeLabel.Text = "Time (min:sec)";
            this.totalTimeLabel.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // totalTimeTextBox
            // 
            this.totalTimeTextBox.Enabled = false;
            this.totalTimeTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalTimeTextBox.Location = new System.Drawing.Point(869, 570);
            this.totalTimeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.totalTimeTextBox.Name = "totalTimeTextBox";
            this.totalTimeTextBox.ReadOnly = true;
            this.totalTimeTextBox.Size = new System.Drawing.Size(109, 27);
            this.totalTimeTextBox.TabIndex = 23;
            this.totalTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.totalTimeTextBox.TextChanged += new System.EventHandler(this.totalTimeTextBox_TextChanged);
            // 
            // numOfClustersLabel
            // 
            this.numOfClustersLabel.AutoSize = true;
            this.numOfClustersLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numOfClustersLabel.Location = new System.Drawing.Point(790, 529);
            this.numOfClustersLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numOfClustersLabel.Name = "numOfClustersLabel";
            this.numOfClustersLabel.Size = new System.Drawing.Size(265, 21);
            this.numOfClustersLabel.TabIndex = 24;
            this.numOfClustersLabel.Text = "Calculated number of clusters";
            this.numOfClustersLabel.Click += new System.EventHandler(this.label3_Click_2);
            // 
            // numOfClustersTextBox
            // 
            this.numOfClustersTextBox.Enabled = false;
            this.numOfClustersTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numOfClustersTextBox.Location = new System.Drawing.Point(1083, 526);
            this.numOfClustersTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.numOfClustersTextBox.Name = "numOfClustersTextBox";
            this.numOfClustersTextBox.ReadOnly = true;
            this.numOfClustersTextBox.Size = new System.Drawing.Size(105, 27);
            this.numOfClustersTextBox.TabIndex = 25;
            this.numOfClustersTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numOfClustersTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(630, 485);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(556, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 615);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.numOfClustersTextBox);
            this.Controls.Add(this.numOfClustersLabel);
            this.Controls.Add(this.totalTimeTextBox);
            this.Controls.Add(this.totalTimeLabel);
            this.Controls.Add(this.mstSumTextBox);
            this.Controls.Add(this.numDisColorLabel);
            this.Controls.Add(this.distinctColorsTextBox);
            this.Controls.Add(this.mst_sum_text_box);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtClusters);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.btnQuantize);
            this.Controls.Add(this.quantizedLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpen);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Image Quantization...";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label quantizedLabel;
        private System.Windows.Forms.Button btnQuantize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtClusters;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox mst_sum_text_box;
        private System.Windows.Forms.TextBox distinctColorsTextBox;
        private System.Windows.Forms.Label numDisColorLabel;
        private System.Windows.Forms.Label mstSumTextBox;
        private System.Windows.Forms.Label totalTimeLabel;
        private System.Windows.Forms.TextBox totalTimeTextBox;
        private System.Windows.Forms.Label numOfClustersLabel;
        private System.Windows.Forms.TextBox numOfClustersTextBox;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}


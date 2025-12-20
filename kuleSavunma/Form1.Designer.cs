namespace kuleSavunma
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            lblCan = new Label();
            pictureBox2 = new PictureBox();
            lblPara = new Label();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            lblDalga = new Label();
            label3 = new Label();
            panel3 = new Panel();
            lblSkor = new Label();
            panel4 = new Panel();
            pictureBox6 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            toolTip1 = new ToolTip(components);
            btnBaslat = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(lblCan);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(lblPara);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(213, 34);
            panel1.TabIndex = 0;
            // 
            // lblCan
            // 
            lblCan.AutoSize = true;
            lblCan.BackColor = Color.Transparent;
            lblCan.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCan.ForeColor = SystemColors.Control;
            lblCan.Location = new Point(66, 11);
            lblCan.Name = "lblCan";
            lblCan.Size = new Size(29, 20);
            lblCan.TabIndex = 2;
            lblCan.Text = "12";
            lblCan.Click += label2_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(19, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(41, 35);
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // lblPara
            // 
            lblPara.AutoSize = true;
            lblPara.BackColor = Color.Transparent;
            lblPara.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPara.ForeColor = SystemColors.Control;
            lblPara.Location = new Point(153, 11);
            lblPara.Name = "lblPara";
            lblPara.Size = new Size(29, 20);
            lblPara.TabIndex = 1;
            lblPara.Text = "12";
            lblPara.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(107, -7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 41);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BackgroundImage = (Image)resources.GetObject("panel2.BackgroundImage");
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(lblDalga);
            panel2.Controls.Add(label3);
            panel2.Location = new Point(31, 57);
            panel2.Name = "panel2";
            panel2.Size = new Size(175, 31);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // lblDalga
            // 
            lblDalga.AutoSize = true;
            lblDalga.BackColor = Color.Transparent;
            lblDalga.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDalga.ForeColor = SystemColors.Control;
            lblDalga.Location = new Point(88, 4);
            lblDalga.Name = "lblDalga";
            lblDalga.Size = new Size(29, 20);
            lblDalga.TabIndex = 2;
            lblDalga.Text = "12";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Enabled = false;
            label3.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(18, 4);
            label3.Name = "label3";
            label3.Size = new Size(58, 18);
            label3.TabIndex = 0;
            label3.Text = "wawe";
            label3.Click += label3_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.BackgroundImageLayout = ImageLayout.Stretch;
            panel3.Controls.Add(lblSkor);
            panel3.Location = new Point(49, 82);
            panel3.Name = "panel3";
            panel3.Size = new Size(138, 35);
            panel3.TabIndex = 2;
            // 
            // lblSkor
            // 
            lblSkor.AutoSize = true;
            lblSkor.BackColor = Color.Transparent;
            lblSkor.Font = new Font("Stencil", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSkor.ForeColor = SystemColors.Control;
            lblSkor.Location = new Point(46, 9);
            lblSkor.Name = "lblSkor";
            lblSkor.Size = new Size(29, 20);
            lblSkor.TabIndex = 3;
            lblSkor.Text = "12";
            lblSkor.Click += label1_Click_1;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Transparent;
            panel4.Controls.Add(pictureBox6);
            panel4.Controls.Add(pictureBox5);
            panel4.Controls.Add(pictureBox4);
            panel4.Controls.Add(pictureBox3);
            panel4.Location = new Point(12, 632);
            panel4.Name = "panel4";
            panel4.Size = new Size(339, 77);
            panel4.TabIndex = 3;
            panel4.Paint += panel4_Paint;
            // 
            // pictureBox6
            // 
            pictureBox6.BackgroundImage = (Image)resources.GetObject("pictureBox6.BackgroundImage");
            pictureBox6.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox6.Cursor = Cursors.Hand;
            pictureBox6.Location = new Point(243, 0);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(74, 70);
            pictureBox6.TabIndex = 3;
            pictureBox6.TabStop = false;
            toolTip1.SetToolTip(pictureBox6, "Lazer Kulesi (350 Altın)");
            pictureBox6.Click += pictureBox6_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.BackgroundImage = (Image)resources.GetObject("pictureBox5.BackgroundImage");
            pictureBox5.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox5.Cursor = Cursors.Hand;
            pictureBox5.Location = new Point(163, 0);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(74, 70);
            pictureBox5.TabIndex = 2;
            pictureBox5.TabStop = false;
            toolTip1.SetToolTip(pictureBox5, "Top Kulesi | Hasar: 50 | Fiyat: 250 Altın");
            pictureBox5.Click += btnKuleTop_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackgroundImage = (Image)resources.GetObject("pictureBox4.BackgroundImage");
            pictureBox4.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox4.Cursor = Cursors.Hand;
            pictureBox4.Location = new Point(83, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(74, 70);
            pictureBox4.TabIndex = 1;
            pictureBox4.TabStop = false;
            toolTip1.SetToolTip(pictureBox4, "Büyü Kulesi | Hasar: 25 | Fiyat: 200 Altın");
            pictureBox4.Click += btnKuleBuyu_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Location = new Point(3, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(74, 70);
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            toolTip1.SetToolTip(pictureBox3, "Ok Kulesi | Hasar: 15 | Fiyat: 100 Altın");
            pictureBox3.Click += btnKuleOkcu_Click;
            // 
            // btnBaslat
            // 
            btnBaslat.BackColor = Color.Transparent;
            btnBaslat.BackgroundImage = Properties.Resources.buton;
            btnBaslat.BackgroundImageLayout = ImageLayout.Stretch;
            btnBaslat.CausesValidation = false;
            btnBaslat.Cursor = Cursors.Hand;
            btnBaslat.FlatAppearance.BorderSize = 0;
            btnBaslat.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBaslat.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBaslat.FlatStyle = FlatStyle.Flat;
            btnBaslat.ForeColor = SystemColors.ButtonFace;
            btnBaslat.Location = new Point(549, 580);
            btnBaslat.Name = "btnBaslat";
            btnBaslat.Size = new Size(385, 113);
            btnBaslat.TabIndex = 4;
            btnBaslat.Text = "OYUNA BAŞLA";
            btnBaslat.UseVisualStyleBackColor = false;
            btnBaslat.Click += btnBaslat_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1006, 721);
            Controls.Add(btnBaslat);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            DoubleBuffered = true;
            ForeColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            toolTip1.SetToolTip(this, " ");
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            MouseClick += Form1_MouseClick;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label lblPara;
        private Label lblCan;
        private PictureBox pictureBox2;
        private Label label3;
        private Panel panel3;
        private Panel panel4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private ToolTip toolTip1;
        private Label lblDalga;
        private Button btnBaslat;
        private Label lblSkor;
        private PictureBox pictureBox6;
    }
}

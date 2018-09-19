namespace Project_03
{
    partial class mainFrm
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
            this.btn_LoadFile = new System.Windows.Forms.Button();
            this.cbb_Scale = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbb_StartCity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Distance = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Path = new System.Windows.Forms.Label();
            this.btn_Calculate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1000, 1000);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btn_LoadFile
            // 
            this.btn_LoadFile.Location = new System.Drawing.Point(1074, 16);
            this.btn_LoadFile.Name = "btn_LoadFile";
            this.btn_LoadFile.Size = new System.Drawing.Size(181, 46);
            this.btn_LoadFile.TabIndex = 1;
            this.btn_LoadFile.Text = "Load File";
            this.btn_LoadFile.UseVisualStyleBackColor = true;
            this.btn_LoadFile.Click += new System.EventHandler(this.btn_LoadFile_Click);
            // 
            // cbb_Scale
            // 
            this.cbb_Scale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_Scale.FormattingEnabled = true;
            this.cbb_Scale.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cbb_Scale.Location = new System.Drawing.Point(1074, 119);
            this.cbb_Scale.Name = "cbb_Scale";
            this.cbb_Scale.Size = new System.Drawing.Size(181, 26);
            this.cbb_Scale.TabIndex = 2;
            this.cbb_Scale.SelectedIndexChanged += new System.EventHandler(this.cbb_Scale_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1071, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Scale:";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 1010);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1022, 613);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(447, 135);
            this.label2.TabIndex = 6;
            this.label2.Text = "Note: You have to reselect the \r\npath, every time you change the\r\nscale or starti" +
    "ng city. In order to reduce unnece-\r\nssary calculation, choose the right\r\nscale " +
    "then calculate the path";
            // 
            // cbb_StartCity
            // 
            this.cbb_StartCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_StartCity.FormattingEnabled = true;
            this.cbb_StartCity.Location = new System.Drawing.Point(1074, 473);
            this.cbb_StartCity.Name = "cbb_StartCity";
            this.cbb_StartCity.Size = new System.Drawing.Size(181, 26);
            this.cbb_StartCity.TabIndex = 7;
            this.cbb_StartCity.SelectedIndexChanged += new System.EventHandler(this.cbb_StartCity_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1074, 449);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Starting City:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1049, 823);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Total Distance:";
            // 
            // lbl_Distance
            // 
            this.lbl_Distance.AutoSize = true;
            this.lbl_Distance.Location = new System.Drawing.Point(1049, 860);
            this.lbl_Distance.Name = "lbl_Distance";
            this.lbl_Distance.Size = new System.Drawing.Size(62, 18);
            this.lbl_Distance.TabIndex = 10;
            this.lbl_Distance.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1049, 910);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "Path:";
            // 
            // lbl_Path
            // 
            this.lbl_Path.AutoSize = true;
            this.lbl_Path.Location = new System.Drawing.Point(1049, 945);
            this.lbl_Path.Name = "lbl_Path";
            this.lbl_Path.Size = new System.Drawing.Size(62, 18);
            this.lbl_Path.TabIndex = 12;
            this.lbl_Path.Text = "label7";
            // 
            // btn_Calculate
            // 
            this.btn_Calculate.Location = new System.Drawing.Point(1074, 542);
            this.btn_Calculate.Name = "btn_Calculate";
            this.btn_Calculate.Size = new System.Drawing.Size(181, 46);
            this.btn_Calculate.TabIndex = 13;
            this.btn_Calculate.Text = "Calculate Path";
            this.btn_Calculate.UseVisualStyleBackColor = true;
            this.btn_Calculate.Click += new System.EventHandler(this.btn_Calculate_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1335, 1057);
            this.Controls.Add(this.btn_Calculate);
            this.Controls.Add(this.lbl_Path);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbl_Distance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbb_StartCity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbb_Scale);
            this.Controls.Add(this.btn_LoadFile);
            this.MaximizeBox = false;
            this.Name = "mainFrm";
            this.Text = "Project 03";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_LoadFile;
        private System.Windows.Forms.ComboBox cbb_Scale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbb_StartCity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Distance;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Path;
        private System.Windows.Forms.Button btn_Calculate;
    }
}


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
            this.btn_CalcPath = new System.Windows.Forms.Button();
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
            this.btn_LoadFile.Location = new System.Drawing.Point(1046, 12);
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
            this.cbb_Scale.Location = new System.Drawing.Point(1046, 408);
            this.cbb_Scale.Name = "cbb_Scale";
            this.cbb_Scale.Size = new System.Drawing.Size(181, 26);
            this.cbb_Scale.TabIndex = 2;
            this.cbb_Scale.SelectedIndexChanged += new System.EventHandler(this.cbb_Scale_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1043, 387);
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
            // btn_CalcPath
            // 
            this.btn_CalcPath.Location = new System.Drawing.Point(1046, 115);
            this.btn_CalcPath.Name = "btn_CalcPath";
            this.btn_CalcPath.Size = new System.Drawing.Size(181, 46);
            this.btn_CalcPath.TabIndex = 5;
            this.btn_CalcPath.Text = "Calculate Path";
            this.btn_CalcPath.UseVisualStyleBackColor = true;
            this.btn_CalcPath.Click += new System.EventHandler(this.btn_CalcPath_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1254, 1057);
            this.Controls.Add(this.btn_CalcPath);
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
        private System.Windows.Forms.Button btn_CalcPath;
    }
}


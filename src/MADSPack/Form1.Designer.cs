namespace MADSPack
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnFindFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileToOpen = new System.Windows.Forms.TextBox();
            this.lblAvailableSprites = new System.Windows.Forms.Label();
            this.btnExportImage = new System.Windows.Forms.Button();
            this.txtString = new System.Windows.Forms.TextBox();
            this.lblText = new System.Windows.Forms.Label();
            this.btnRenderText = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(580, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 110);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(670, 351);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Enabled = false;
            this.trackBar1.Location = new System.Drawing.Point(13, 78);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(416, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Visible = false;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // btnFindFile
            // 
            this.btnFindFile.Location = new System.Drawing.Point(475, 7);
            this.btnFindFile.Name = "btnFindFile";
            this.btnFindFile.Size = new System.Drawing.Size(56, 23);
            this.btnFindFile.TabIndex = 3;
            this.btnFindFile.Text = "Find...";
            this.btnFindFile.UseVisualStyleBackColor = true;
            this.btnFindFile.Click += new System.EventHandler(this.btnFindFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "File to Open:";
            // 
            // txtFileToOpen
            // 
            this.txtFileToOpen.Enabled = false;
            this.txtFileToOpen.Location = new System.Drawing.Point(86, 9);
            this.txtFileToOpen.Name = "txtFileToOpen";
            this.txtFileToOpen.Size = new System.Drawing.Size(383, 20);
            this.txtFileToOpen.TabIndex = 5;
            // 
            // lblAvailableSprites
            // 
            this.lblAvailableSprites.AutoSize = true;
            this.lblAvailableSprites.Location = new System.Drawing.Point(12, 62);
            this.lblAvailableSprites.Name = "lblAvailableSprites";
            this.lblAvailableSprites.Size = new System.Drawing.Size(88, 13);
            this.lblAvailableSprites.TabIndex = 6;
            this.lblAvailableSprites.Text = "Available Sprites:";
            this.lblAvailableSprites.Visible = false;
            // 
            // btnExportImage
            // 
            this.btnExportImage.Location = new System.Drawing.Point(580, 44);
            this.btnExportImage.Name = "btnExportImage";
            this.btnExportImage.Size = new System.Drawing.Size(102, 31);
            this.btnExportImage.TabIndex = 7;
            this.btnExportImage.Text = "Export Image";
            this.btnExportImage.UseVisualStyleBackColor = true;
            this.btnExportImage.Visible = false;
            this.btnExportImage.Click += new System.EventHandler(this.btnExportImage_Click);
            // 
            // txtString
            // 
            this.txtString.Location = new System.Drawing.Point(86, 35);
            this.txtString.Name = "txtString";
            this.txtString.Size = new System.Drawing.Size(383, 20);
            this.txtString.TabIndex = 9;
            this.txtString.Visible = false;
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(13, 38);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(71, 13);
            this.lblText.TabIndex = 8;
            this.lblText.Text = "Text to Write:";
            this.lblText.Visible = false;
            // 
            // btnRenderText
            // 
            this.btnRenderText.Location = new System.Drawing.Point(475, 33);
            this.btnRenderText.Name = "btnRenderText";
            this.btnRenderText.Size = new System.Drawing.Size(88, 23);
            this.btnRenderText.TabIndex = 10;
            this.btnRenderText.Text = "Render Text";
            this.btnRenderText.UseVisualStyleBackColor = true;
            this.btnRenderText.Visible = false;
            this.btnRenderText.Click += new System.EventHandler(this.btnRenderText_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Image File|*.pik|Sprite Series File|*.ss|Font File|*.ff";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Image File|*.png";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 469);
            this.Controls.Add(this.btnRenderText);
            this.Controls.Add(this.txtString);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnExportImage);
            this.Controls.Add(this.lblAvailableSprites);
            this.Controls.Add(this.txtFileToOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFindFile);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Colonization Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnFindFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileToOpen;
        private System.Windows.Forms.Label lblAvailableSprites;
        private System.Windows.Forms.Button btnExportImage;
        private System.Windows.Forms.TextBox txtString;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnRenderText;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}


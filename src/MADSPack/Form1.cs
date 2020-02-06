using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MADSPack
{
    public partial class Form1 : Form
    {
        MADSPack.Compression.MadsPackImageSS ss;
        MADSPack.Compression.MadsPackImagePIK pik;
        MADSPack.Compression.MadsPackFont ff;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtFileToOpen.Text.Trim() == "")
            {
                MessageBox.Show("Please select a valid Colonization file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtFileToOpen.Text.ToLowerInvariant().EndsWith(".pik"))
            {
                MADSPack.Compression.MadsPackReader r = new Compression.MadsPackReader(txtFileToOpen.Text);
                pik = new Compression.MadsPackImagePIK(r.getItems());
                pik.pathtoCol = Path.GetDirectoryName(txtFileToOpen.Text);
                Bitmap bmp = pik.GetImage();
                pictureBox1.Image = bmp;
                ss = null;
                ff = null;
                lblText.Visible = false;
                txtString.Visible = false;
                btnRenderText.Visible = false;
                lblAvailableSprites.Visible = false;
                trackBar1.Visible = false;
                btnExportImage.Visible = true;

            }
            else if (txtFileToOpen.Text.ToLowerInvariant().EndsWith(".ss"))
            {
                MADSPack.Compression.MadsPackReader r = new Compression.MadsPackReader(txtFileToOpen.Text);
                ss = new Compression.MadsPackImageSS(r.getItems(), 0);
                ss.pathtoCol = Path.GetDirectoryName(txtFileToOpen.Text);
                Bitmap bmp = ss.GetImage(0);
                pictureBox1.Image = bmp;
                trackBar1.Minimum = 0;
                trackBar1.Maximum = ss.getPictureCount() - 1;
                trackBar1.Enabled = true;
                pik = null;
                ff = null;

                lblText.Visible = false;
                txtString.Visible = false;
                btnRenderText.Visible = false;
                lblAvailableSprites.Visible = true;
                trackBar1.Visible = true;
                btnExportImage.Visible = true;
            }
            else if (txtFileToOpen.Text.ToLowerInvariant().EndsWith(".ff"))
            {
                MADSPack.Compression.MadsPackReader r = new Compression.MadsPackReader(txtFileToOpen.Text);
                ff = new Compression.MadsPackFont(r.getItems()[0]);
                ff.pathtoCol = Path.GetDirectoryName(txtFileToOpen.Text);
                Bitmap b = ff.GenerateFontMap();
                pictureBox1.Image = b;
                pik = null;
                ss = null;
                lblText.Visible = true;
                txtString.Visible = true;
                btnRenderText.Visible = true;
                lblAvailableSprites.Visible = false;
                trackBar1.Visible = false;
                btnExportImage.Visible = true;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Bitmap bmp = ss.GetImage(trackBar1.Value);
            pictureBox1.Image = bmp;
        }

        private void btnFindFile_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                txtFileToOpen.Text = openFileDialog1.FileName;
            }
        }

        private void btnRenderText_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(500,200);
            ff.writeString(ref bmp, txtString.Text, new Point(0, 0), 9, 500);
            pictureBox1.Image = bmp;
        }

        private void btnExportImage_Click(object sender, EventArgs e)
        {
            DialogResult res = saveFileDialog1.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("Image saved at " + saveFileDialog1.FileName + " !!!", "Image Saved", MessageBoxButtons.OK);
                }
                catch
                {
                    MessageBox.Show("Error saving image " + saveFileDialog1.FileName + " . Check the extension and check if the folder have sufficient permission!", "Error saving image.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

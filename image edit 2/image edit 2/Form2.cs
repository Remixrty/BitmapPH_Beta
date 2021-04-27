using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_edit_2
{
    public partial class Form2 : Form
    {
        protected Bitmap img = null;
        public Form2()
        {
            InitializeComponent();
            img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img;
        }

        public void button1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Файлы JPG и PNG (*.png;*.jpg;*.bmp;*.gif) | *.png;*.jpg;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (img != null)
                    {
                        pictureBox1.Image = null;
                        img.Dispose();
                    }
                    img = new Bitmap(ofd.FileName);

                    pictureBox1.Image = img;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }


        }

        protected void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Файлы JPG и PNG (*.png;*.jpg;*.bmp;*.gif) | *.png;*.jpg;*.bmp;*.gif";


                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    if (img != null)
                    {
                        img.Save(sfd.FileName);
                    }
                }

            }
        }

        #region Trackbars
        public int trackBar1_Int(int redd)
        {
            int red = trackBar1.Value * 30 + redd;
            if (red <= 255 && red >= 0)
                return red;
            else if (red > 255)
                return 255;
            else return 0;
        }

        protected int trackBar2_Int(int greenn)
        {
            int green = trackBar2.Value * 30 + greenn;
            if (green <= 255 && green >= 0)
                return green;
            else if (green > 255)
                return 255;
            else return 0;
        }

        private int trackBar3_Int(int bluee)
        {
            int blue = trackBar3.Value * 30 + bluee;
            if (blue <= 255 && blue >= 0)
                return blue;
            else if (blue > 255)
                return 255;
            else return 0;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {

        }
        #endregion

		private void button3_Click_1(object sender, EventArgs e)
		{

            DateTime time = DateTime.Now;
            label4.Text = "timer";
            for (int i = 0; i < img.Height; ++i)
            {
                for (int j = 0; j < img.Width; ++j)
                {

                    var pix = img.GetPixel(j, i);

                    int r = pix.R;
                    int g = pix.G;
                    int b = pix.B;

                    r = trackBar1_Int(r);
                    g = trackBar2_Int(g);
                    b = trackBar3_Int(b);

                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);



                }
            }
            pictureBox1.Refresh();
            TimeSpan now = DateTime.Now - time;
            label4.Visible = true;
            label4.Text = time.ToLongTimeString();
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
        }

		private void trackBar1_Scroll_1(object sender, EventArgs e)
		{

		}
	}



}

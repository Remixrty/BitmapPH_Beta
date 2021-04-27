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
    public partial class Form1 : Form
    {
        protected Bitmap img1 = null;
        protected Bitmap img2 = null;
        protected Bitmap img = null;
        protected int ii = 0;
        protected int jj = 0;
        protected int fl = 0;
        public Form1()
        {
            InitializeComponent();
            img1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img1;
            img2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox2.Image = img2;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

        }

        #region Buttons
        private void button1_Click(object sender, EventArgs e) //кнопка "открыть" (открывает 2 картинки по очереди)
        {
            using (OpenFileDialog ofd1 = new OpenFileDialog()) //открытие картинки 1
            {
                ofd1.Filter = "Файлы JPG и PNG (*.png;*.jpg;*.bmp;*.gif) | *.png;*.jpg;*.bmp;*.gif";

                if (ofd1.ShowDialog() == DialogResult.OK)
                {
                    if (img1 != null)
                    {
                        pictureBox1.Image = null;
                        img1.Dispose();
                    }
                    img1 = new Bitmap(ofd1.FileName);
                    pictureBox1.Image = img1;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                }
                using (OpenFileDialog ofd2 = new OpenFileDialog()) //открытие картинки 2 (при условии, что 1ая открылась)
                {
                    ofd2.Filter = "Файлы JPG и PNG (*.png;*.jpg;*.bmp;*.gif) | *.png;*.jpg;*.bmp;*.gif";

                    if (ofd2.ShowDialog() == DialogResult.OK)
                    {
                        if (img2 != null)
                        {
                            pictureBox2.Image = null;
                            img2.Dispose();
                        }
                        img2 = new Bitmap(ofd2.FileName);
                        pictureBox2.Image = img2;
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                    }
                }
                //замер картинок 1 и 2 относительно друг друга и создание размеров для выходной картинки
                if (img1.Width > img2.Width && img1.Height > img2.Height)
                {
                    img = new Bitmap(img1.Width, img1.Height);
                    pictureBox3.Image = img;
                    jj = img1.Width - img2.Width;
                    ii = img1.Height - img2.Height;
                    fl = 1;
                }
                else if (img1.Width > img2.Width && img2.Height > img1.Height)
                {
                    img = new Bitmap(img1.Width, img2.Height);
                    pictureBox3.Image = img;
                    jj = img1.Width - img2.Width;
                    ii = img2.Height - img1.Height;
                    fl = 2;
                }
                else if (img2.Width > img1.Width && img1.Height > img2.Height)
                {
                    img = new Bitmap(img2.Width, img1.Height);
                    pictureBox3.Image = img;
                    jj = img2.Width - img1.Width;
                    ii = img1.Height - img2.Height;
                    fl = 3;
                }
                else if (img2.Width > img1.Width && img2.Height > img1.Height)
                {
                    img = new Bitmap(img2.Width, img2.Height);
                    pictureBox3.Image = img;
                    jj = img2.Width - img1.Width;
                    ii = img2.Height - img1.Height;
                    fl = 4;
                }
                else if (img1.Width > img2.Width && img1.Height == img2.Height)
                {
                    img = new Bitmap(img1.Width, img1.Height);
                    pictureBox3.Image = img;
                    jj = img1.Width - img2.Width;
                    ii = img1.Height - img2.Height;
                    fl = 5;
                }
                else if (img2.Width > img1.Width && img2.Height == img1.Height)
                {
                    img = new Bitmap(img2.Width, img2.Height);
                    pictureBox3.Image = img;
                    jj = img2.Width - img1.Width;
                    ii = img1.Height - img2.Height;
                    fl = 6;
                }
                else if (img1.Width == img2.Width && img1.Height > img2.Height)
                {
                    img = new Bitmap(img1.Width, img1.Height);
                    pictureBox3.Image = img;
                    jj = img1.Width - img2.Width;
                    ii = img1.Height - img2.Height;
                    fl = 7;
                }
                else if (img2.Width == img1.Width && img2.Height > img1.Height)
                {
                    img = new Bitmap(img2.Width, img2.Height);
                    pictureBox3.Image = img;
                    jj = img1.Width - img2.Width;
                    ii = img2.Height - img1.Height;
                    fl = 8;
                }
                else
                {
                    img = new Bitmap(img1.Width, img1.Height);
                    pictureBox3.Image = img;
                    jj = 0;
                    ii = 0;
                    fl = 9;
                }


            }

        }

        private void button2_Click(object sender, EventArgs e) //кнопка "сохранить"
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

        private void button3_Click(object sender, EventArgs e) //кнопка "сумма попиксельно" (складывает каждый пиксель, где картинки пересекаются;
                                                               //там, где не пересекаются - выводит большую картинку
        {
            int gl = 1;

            #region fl1 fl4 5 parts
            if (fl == 1 || fl == 4)
            {
                if (fl == 1)
                {
                    flag1(gl);
                }

                if (fl == 4)
                {
                    flag4(gl);
                }
            }
            #endregion

            #region fl2 fl3 9 parts
            if (fl == 2 || fl == 3)
            {
                if (fl == 2)
                {
                    flag2(gl);
                }

                if (fl == 3)
                {
                    flag3(gl);
                }
            }
            #endregion

            #region fl5 fl6 3 parts
            if (fl == 5 || fl == 6)
            {
                if (fl == 5)
                {
                    flag5(gl);
                }

                if (fl == 6)
                {
                    flag6(gl);
                }
            }

            #endregion

            #region fl7 fl8 3 parts
            if (fl == 7 || fl == 8)
            {
                if (fl == 7)
                {
                    flag7(gl);
                }

                if (fl == 8)
                {
                    flag8(gl);
                }
            }
            #endregion

            #region fl9
            if (fl == 9)
            {
                flag9(gl);
            }
            #endregion

            pictureBox3.Refresh();
        }

        private void button4_Click(object sender, EventArgs e) //кнопка "произведение" (умножает каждый пиксель, где картинки пересекаются;
                                                               //там, где не пересекаются - выводит большую картинку
        {

            int gl = 2;

            #region fl1 fl4 5 parts
            if (fl == 1 || fl == 4)
            {
                if (fl == 1)
                {
                    flag1(gl);
                }

                if (fl == 4)
                {
                    flag4(gl);
                }
            }
            #endregion

            #region fl2 fl3 9 parts
            if (fl == 2 || fl == 3)
            {
                if (fl == 2)
                {
                    flag2(gl);
                }

                if (fl == 3)
                {
                    flag3(gl);
                }
            }
            #endregion

            #region fl5 fl6 3 parts
            if (fl == 5 || fl == 6)
            {
                if (fl == 5)
                {
                    flag5(gl);
                }

                if (fl == 6)
                {
                    flag6(gl);
                }
            }

            #endregion

            #region fl7 fl8 3 parts
            if (fl == 7 || fl == 8)
            {
                if (fl == 7)
                {
                    flag7(gl);
                }

                if (fl == 8)
                {
                    flag8(gl);
                }
            }
            #endregion

            #region fl9
            if (fl == 9)
            {
                flag9(gl);
            }
            #endregion

            pictureBox3.Refresh();
        }

        private void button5_Click(object sender, EventArgs e) //кнопка "среднее арифметическое" (сумма пикселей /2, где картинки пересекаются;
                                                               //там, где не пересекаются - выводит большую картинку
        {

            int gl = 3;

            #region fl1 fl4 5 parts
            if (fl == 1 || fl == 4)
            {
                if (fl == 1)
                {
                    flag1(gl);
                }

                if (fl == 4)
                {
                    flag4(gl);
                }
            }
            #endregion

            #region fl2 fl3 9 parts
            if (fl == 2 || fl == 3)
            {
                if (fl == 2)
                {
                    flag2(gl);
                }

                if (fl == 3)
                {
                    flag3(gl);
                }
            }
            #endregion

            #region fl5 fl6 3 parts
            if (fl == 5 || fl == 6)
            {
                if (fl == 5)
                {
                    flag5(gl);
                }

                if (fl == 6)
                {
                    flag6(gl);
                }
            }

            #endregion

            #region fl7 fl8 3 parts
            if (fl == 7 || fl == 8)
            {
                if (fl == 7)
                {
                    flag7(gl);
                }

                if (fl == 8)
                {
                    flag8(gl);
                }
            }
            #endregion

            #region fl9
            if (fl == 9)
            {
                flag9(gl);
            }
            #endregion

            pictureBox3.Refresh();
        }

        private void button6_Click(object sender, EventArgs e) //кнопка "минимум" (выводит минимальное значение пикселей, где картинки пересекаются;
                                                               //там, где не пересекаются - выводит большую картинку
        {

            int gl = 4;

            #region fl1 fl4 5 parts
            if (fl == 1 || fl == 4)
            {
                if (fl == 1)
                {
                    flag1(gl);
                }

                if (fl == 4)
                {
                    flag4(gl);
                }
            }
            #endregion

            #region fl2 fl3 9 parts
            if (fl == 2 || fl == 3)
            {
                if (fl == 2)
                {
                    flag2(gl);
                }

                if (fl == 3)
                {
                    flag3(gl);
                }
            }
            #endregion

            #region fl5 fl6 3 parts
            if (fl == 5 || fl == 6)
            {
                if (fl == 5)
                {
                    flag5(gl);
                }

                if (fl == 6)
                {
                    flag6(gl);
                }
            }

            #endregion

            #region fl7 fl8 3 parts
            if (fl == 7 || fl == 8)
            {
                if (fl == 7)
                {
                    flag7(gl);
                }

                if (fl == 8)
                {
                    flag8(gl);
                }
            }
            #endregion

            #region fl9
            if (fl == 9)
            {
                flag9(gl);
            }
            #endregion

            pictureBox3.Refresh();
        }

        private void button7_Click(object sender, EventArgs e) //кнопка "максимум" (выводит максимальное значение пикселей, где картинки пересекаются;
                                                               //там, где не пересекаются - выводит большую картинку
        {

            int gl = 5;

            #region fl1 fl4 5 parts
            if (fl == 1 || fl == 4)
            {
                if (fl == 1)
                {
                    flag1(gl);
                }

                if (fl == 4)
                {
                    flag4(gl);
                }
            }
            #endregion

            #region fl2 fl3 9 parts
            if (fl == 2 || fl == 3)
            {
                if (fl == 2)
                {
                    flag2(gl);
                }

                if (fl == 3)
                {
                    flag3(gl);
                }
            }
            #endregion

            #region fl5 fl6 3 parts
            if (fl == 5 || fl == 6)
            {
                if (fl == 5)
                {
                    flag5(gl);
                }

                if (fl == 6)
                {
                    flag6(gl);
                }
            }

            #endregion

            #region fl7 fl8 3 parts
            if (fl == 7 || fl == 8)
            {
                if (fl == 7)
                {
                    flag7(gl);
                }

                if (fl == 8)
                {
                    flag8(gl);
                }
            }
            #endregion

            #region fl9
            if (fl == 9)
            {
                flag9(gl);
            }
            #endregion

            pictureBox3.Refresh();
        }

        #endregion




        protected void flag1(int gl)
        {
            for (int i = 0; i < ii / 2; ++i) //part 1 fl1
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j, i);
                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2; ++i) //part 2 
            {
                for (int j = 0; j < jj / 2; j++)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j, i);
                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2; ++i) //part 3
            {
                for (int j = img.Width - jj / 2; j < img.Width; j++)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j, i);
                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i) //part 4
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j, i);
                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 5
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {

                    int r = 0;
                    int g = 0;
                    int b = 0;
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j - jj / 2, i - ii / 2);


                    if (gl == 1) { r = pix1.R + pix2.R; g = pix1.G + pix2.G; b = pix1.B + pix2.B; }
                    else if (gl == 2) { r = pix1.R * pix2.R; g = pix1.G * pix2.G; b = pix1.B * pix2.B; }
                    else if (gl == 3) { r = (pix1.R + pix2.R) / 2; g = (pix1.G + pix2.G) / 2; b = (pix1.B + pix2.B) / 2; }
                    else if (gl == 4)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix1.R;
                        }
                        else r = pix2.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix1.G;
                        }
                        else g = pix2.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix1.B;
                        }
                        else b = pix2.B;
                    }
                    else if (gl == 5)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix2.R;
                        }
                        else r = pix1.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix2.G;
                        }
                        else g = pix1.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix2.B;
                        }
                        else b = pix1.B;
                    }

                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }

        }


        protected void flag4(int gl)
        {
            for (int i = 0; i < ii / 2; ++i) //part 1 fl4
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i);
                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2; ++i) //part 2 
            {
                for (int j = 0; j < jj / 2; j++)
                {
                    var pix = img.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i);
                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2; ++i) //part 3
            {
                for (int j = img.Width - jj / 2; j < img.Width; j++)
                {
                    var pix = img.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i);
                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i) //part 4
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i);
                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 5
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {

                    int r = 0;
                    int g = 0;
                    int b = 0;
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j - jj / 2, i - ii / 2);
                    var pix2 = img2.GetPixel(j, i);
                    if (gl == 1) { r = pix1.R + pix2.R; g = pix1.G + pix2.G; b = pix1.B + pix2.B; }
                    else if (gl == 2) { r = pix1.R * pix2.R; g = pix1.G * pix2.G; b = pix1.B * pix2.B; }
                    else if (gl == 3) { r = (pix1.R + pix2.R) / 2; g = (pix1.G + pix2.G) / 2; b = (pix1.B + pix2.B) / 2; }
                    else if (gl == 4)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix1.R;
                        }
                        else r = pix2.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix1.G;
                        }
                        else g = pix2.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix1.B;
                        }
                        else b = pix2.B;
                    }
                    else if (gl == 5)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix2.R;
                        }
                        else r = pix1.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix2.G;
                        }
                        else g = pix1.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix2.B;
                        }
                        else b = pix1.B;
                    }

                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
        }


        protected void flag2(int gl)
        {
            for (int i = 0; i < ii / 2; ++i) //part 1 fl2
            {
                for (int j = 0; j < jj / 2; ++j)
                {
                    var pix = img.GetPixel(j, i);

                    int r = 0;
                    int g = 255;
                    int b = 0;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = 0; i < ii / 2; ++i) //part 2 
            {
                for (int j = img.Width - jj / 2; j < img.Width; j++)
                {
                    var pix = img.GetPixel(j, i);

                    int r = 255;
                    int g = 0;
                    int b = 0;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i) //part 3
            {
                for (int j = 0; j < jj / 2; j++)
                {
                    var pix = img.GetPixel(j, i);

                    int r = 0;
                    int g = 0;
                    int b = 255;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i) //part 4
            {
                for (int j = img.Width - jj / 2; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);

                    int r = 0;
                    int g = 255;
                    int b = 0;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = 0; i < ii / 2; ++i) //part 5
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j - jj / 2, i);
                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 6
            {
                for (int j = 0; j < jj / 2; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j, i - ii / 2);
                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 7
            {
                for (int j = img.Width - jj / 2; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j - jj / 2, i - ii / 2);
                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i) //part 8
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j - jj / 2, i - ii / 2);
                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 9
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {


                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j - jj / 2, i - ii / 2);
                    var pix2 = img2.GetPixel(j - jj / 2, i - ii / 2);

                    int r = 0;
                    int g = 0;
                    int b = 0;

                    if (gl == 1) { r = pix1.R + pix2.R; g = pix1.G + pix2.G; b = pix1.B + pix2.B; }
                    else if (gl == 2) { r = pix1.R * pix2.R; g = pix1.G * pix2.G; b = pix1.B * pix2.B; }
                    else if (gl == 3) { r = (pix1.R + pix2.R) / 2; g = (pix1.G + pix2.G) / 2; b = (pix1.B + pix2.B) / 2; }
                    else if (gl == 4)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix1.R;
                        }
                        else r = pix2.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix1.G;
                        }
                        else g = pix2.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix1.B;
                        }
                        else b = pix2.B;
                    }
                    else if (gl == 5)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix2.R;
                        }
                        else r = pix1.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix2.G;
                        }
                        else g = pix1.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix2.B;
                        }
                        else b = pix1.B;
                    }

                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);

                }
            }
            for (int i = img.Height - 1; i < img.Height; ++i) //part 10
            {
                for (int j = img.Width - 1; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    int r = 255;
                    int g = 255;
                    int b = 255;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
        }


        protected void flag3(int gl)
        {
            for (int i = 0; i < ii / 2; ++i) //part 1 fl3
            {
                for (int j = 0; j < jj / 2; ++j)
                {
                    var pix = img.GetPixel(j, i);

                    int r = 0;
                    int g = 255;
                    int b = 0;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = 0; i < ii / 2; ++i) //part 2 
            {
                for (int j = img.Width - jj / 2; j < img.Width; j++)
                {
                    var pix = img.GetPixel(j, i);

                    int r = 255;
                    int g = 0;
                    int b = 0;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i) //part 3
            {
                for (int j = 0; j < jj / 2; j++)
                {
                    var pix = img.GetPixel(j, i);

                    int r = 0;
                    int g = 0;
                    int b = 255;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i) //part 4
            {
                for (int j = img.Width - jj / 2; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);

                    int r = 0;
                    int g = 255;
                    int b = 0;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = 0; i < ii / 2; ++i) //part 5
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j - jj / 2, i);
                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 6
            {
                for (int j = 0; j < jj / 2; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i - ii / 2);
                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 7
            {
                for (int j = img.Width - jj / 2; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j - jj / 2, i - ii / 2);
                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i) //part 8
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j - jj / 2, i - ii / 2);
                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 9
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j - jj / 2, i - ii / 2);
                    var pix2 = img2.GetPixel(j - jj / 2, i - ii / 2);

                    int r = 0;
                    int g = 0;
                    int b = 0;

                    if (gl == 1) { r = pix1.R + pix2.R; g = pix1.G + pix2.G; b = pix1.B + pix2.B; }
                    else if (gl == 2) { r = pix1.R * pix2.R; g = pix1.G * pix2.G; b = pix1.B * pix2.B; }
                    else if (gl == 3) { r = (pix1.R + pix2.R) / 2; g = (pix1.G + pix2.G) / 2; b = (pix1.B + pix2.B) / 2; }
                    else if (gl == 4)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix1.R;
                        }
                        else r = pix2.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix1.G;
                        }
                        else g = pix2.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix1.B;
                        }
                        else b = pix2.B;
                    }
                    else if (gl == 5)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix2.R;
                        }
                        else r = pix1.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix2.G;
                        }
                        else g = pix1.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix2.B;
                        }
                        else b = pix1.B;
                    }

                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - 1; i < img.Height; ++i) //part 10
            {
                for (int j = img.Width - 1; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    int r = 255;
                    int g = 255;
                    int b = 255;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
        }


        protected void flag5(int gl)
        {
            for (int i = 0; i < img.Height; ++i)  //part 1 fl5
            {
                for (int j = 0; j < jj / 2; ++j)
                {
                    var pix1 = img1.GetPixel(j, i);
                    var pix = img.GetPixel(j, i);

                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = 0; i < img.Height; ++i)  //part 2
            {
                for (int j = img.Width - jj / 2; j < img.Width; ++j)
                {
                    var pix1 = img1.GetPixel(j, i);
                    var pix = img.GetPixel(j, i);

                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = 0; i < img.Height; ++i) //part 3
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j - jj / 2, i);
                    int r = 0;
                    int g = 0;
                    int b = 0;

                    if (gl == 1) { r = pix1.R + pix2.R; g = pix1.G + pix2.G; b = pix1.B + pix2.B; }
                    else if (gl == 2) { r = pix1.R * pix2.R; g = pix1.G * pix2.G; b = pix1.B * pix2.B; }
                    else if (gl == 3) { r = (pix1.R + pix2.R) / 2; g = (pix1.G + pix2.G) / 2; b = (pix1.B + pix2.B) / 2; }
                    else if (gl == 4)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix1.R;
                        }
                        else r = pix2.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix1.G;
                        }
                        else g = pix2.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix1.B;
                        }
                        else b = pix2.B;
                    }
                    else if (gl == 5)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix2.R;
                        }
                        else r = pix1.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix2.G;
                        }
                        else g = pix1.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix2.B;
                        }
                        else b = pix1.B;
                    }

                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);

                }
            }

        }


        protected void flag6(int gl)
		{
            for (int i = 0; i < img.Height; ++i)  //part 1 fl6
            {
                for (int j = 0; j < jj / 2; ++j)
                {
                    var pix2 = img2.GetPixel(j, i);
                    var pix = img.GetPixel(j, i);

                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = 0; i < img.Height; ++i)  //part 2
            {
                for (int j = img.Width - jj / 2; j < img.Width; ++j)
                {
                    var pix2 = img2.GetPixel(j, i);
                    var pix = img.GetPixel(j, i);

                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = 0; i < img.Height; ++i) //part 3
            {
                for (int j = jj / 2; j < img.Width - jj / 2 - 1; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j - jj / 2, i);
                    var pix2 = img2.GetPixel(j, i);

                    int r = 0;
                    int g = 0;
                    int b = 0;

                    if (gl == 1) { r = pix1.R + pix2.R; g = pix1.G + pix2.G; b = pix1.B + pix2.B; }
                    else if (gl == 2) { r = pix1.R * pix2.R; g = pix1.G * pix2.G; b = pix1.B * pix2.B; }
                    else if (gl == 3) { r = (pix1.R + pix2.R) / 2; g = (pix1.G + pix2.G) / 2; b = (pix1.B + pix2.B) / 2; }
                    else if (gl == 4)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix1.R;
                        }
                        else r = pix2.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix1.G;
                        }
                        else g = pix2.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix1.B;
                        }
                        else b = pix2.B;
                    }
                    else if (gl == 5)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix2.R;
                        }
                        else r = pix1.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix2.G;
                        }
                        else g = pix1.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix2.B;
                        }
                        else b = pix1.B;
                    }

                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);


                }
            }
        }


        protected void flag7(int gl)
		{
            for (int i = 0; i < ii / 2 - 1; ++i)  //part 1 fl7
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix1 = img1.GetPixel(j, i);
                    var pix = img.GetPixel(j, i);

                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i)  //part 2
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix1 = img1.GetPixel(j, i);
                    var pix = img.GetPixel(j, i);

                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 3
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i - ii / 2);
                    int r = 0;
                    int g = 0;
                    int b = 0;

                    if (gl == 1) { r = pix1.R + pix2.R; g = pix1.G + pix2.G; b = pix1.B + pix2.B; }
                    else if (gl == 2) { r = pix1.R * pix2.R; g = pix1.G * pix2.G; b = pix1.B * pix2.B; }
                    else if (gl == 3) { r = (pix1.R + pix2.R) / 2; g = (pix1.G + pix2.G) / 2; b = (pix1.B + pix2.B) / 2; }
                    else if (gl == 4)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix1.R;
                        }
                        else r = pix2.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix1.G;
                        }
                        else g = pix2.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix1.B;
                        }
                        else b = pix2.B;
                    }
                    else if (gl == 5)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix2.R;
                        }
                        else r = pix1.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix2.G;
                        }
                        else g = pix1.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix2.B;
                        }
                        else b = pix1.B;
                    }

                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);

                }

            }
        }


        protected void flag8(int gl)
        {
            for (int i = 0; i < ii / 2 - 1; ++i)  //part 1 fl8
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix2 = img2.GetPixel(j, i);
                    var pix = img.GetPixel(j, i);

                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = img.Height - ii / 2; i < img.Height; ++i)  //part 2
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix2 = img2.GetPixel(j, i);
                    var pix = img.GetPixel(j, i);

                    int r = pix2.R;
                    int g = pix2.G;
                    int b = pix2.B;
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);
                }
            }
            for (int i = ii / 2; i < img.Height - ii / 2 - 1; ++i) //part 3
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    var pix1 = img1.GetPixel(j, i - ii / 2);
                    var pix2 = img2.GetPixel(j, i);
                    int r = 0;
                    int g = 0;
                    int b = 0;

                    if (gl == 1) { r = pix1.R + pix2.R; g = pix1.G + pix2.G; b = pix1.B + pix2.B; }
                    else if (gl == 2) { r = pix1.R * pix2.R; g = pix1.G * pix2.G; b = pix1.B * pix2.B; }
                    else if (gl == 3) { r = (pix1.R + pix2.R) / 2; g = (pix1.G + pix2.G) / 2; b = (pix1.B + pix2.B) / 2; }
                    else if (gl == 4)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix1.R;
                        }
                        else r = pix2.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix1.G;
                        }
                        else g = pix2.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix1.B;
                        }
                        else b = pix2.B;
                    }
                    else if (gl == 5)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix2.R;
                        }
                        else r = pix1.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix2.G;
                        }
                        else g = pix1.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix2.B;
                        }
                        else b = pix1.B;
                    }

                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);

                }
            }
        }


        protected void flag9(int gl)
		{
            for (int i = 0; i < img.Height; ++i)
            {
                for (int j = 0; j < img.Width; ++j)
                {

                    var pix1 = img1.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i);
                    var pix = img.GetPixel(j, i);
                    int r = 0;
                    int g = 0;
                    int b = 0;

                    if (gl == 1) { r = pix1.R + pix2.R; g = pix1.G + pix2.G; b = pix1.B + pix2.B; }
                    else if (gl == 2) { r = pix1.R * pix2.R; g = pix1.G * pix2.G; b = pix1.B * pix2.B; }
                    else if (gl == 3) { r = (pix1.R + pix2.R) / 2; g = (pix1.G + pix2.G) / 2; b = (pix1.B + pix2.B) / 2; }
                    else if (gl == 4)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix1.R;
                        }
                        else r = pix2.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix1.G;
                        }
                        else g = pix2.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix1.B;
                        }
                        else b = pix2.B;
                    }
                    else if (gl == 5)
                    {
                        if (pix1.R <= pix2.R)
                        {
                            r = pix2.R;
                        }
                        else r = pix1.R;

                        if (pix1.G <= pix2.G)
                        {
                            g = pix2.G;
                        }
                        else g = pix1.G;

                        if (pix1.B <= pix2.B)
                        {
                            b = pix2.B;
                        }
                        else b = pix1.B;
                    }

                    if (r > 255)
                    {
                        r = 255;
                    }
                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                    pix = Color.FromArgb(r, g, b);
                    img.SetPixel(j, i, pix);

                }
            }
        }

		private void button9_Click(object sender, EventArgs e)
		{
            Form2 op = new Form2();
            op.Show(this);
		}
        private void button10_Click(object sender, EventArgs e)
        {
        }

		private void button10_Click_1(object sender, EventArgs e)
		{

            Form3 kop = new Form3();
            kop.Show(this);
        }

		private void button11_Click(object sender, EventArgs e)
		{
            Form4 top = new Form4();
            top.Show(this);
        }
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_edit_2
{
	public partial class Form3 : Form
	{
		private Bitmap image = null;
		private static Bitmap img = null;
		private static Bitmap img1 = null;
		int[] hist;
		string globalFlagRGB = "RGB";

		public Form3()
		{
			InitializeComponent();

			image = (Bitmap)pictureBox1.Image;
			img = new Bitmap(pictureBox2.Width, pictureBox2.Height);
			img1 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
			pictureBox2.Image = img;


		}

		List<Point> point = new List<Point> { new Point(0, 0), new Point(255, 255) };
		List<double> k = new List<double> { };

		protected void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			int ffl = 0;
			label1.Text = Convert.ToString(e.X);
			label2.Text = Convert.ToString(255 - e.Y);
			for (int i = 0; i < point.Count; i++) //проверка, что мы кликнули около точки
			{
				if ((e.X - point[i].X < 5) && (255 - e.Y - point[i].Y < 5) && (e.X - point[i].X > -5) && (255 - e.Y - point[i].Y > -5))
				{
					ffl = i;
					break;
				}
			}
			if (ffl == 0) //если не около, то добавляем новую
			{
				point.Add(new Point(e.X, 255 - e.Y));
			}
			else //иначе, удаляем ее
			{
				point.RemoveAt(ffl);
			}
			point.Sort((x, y) => x.X.CompareTo(y.X)); //сортировка
			k.Clear();

			double kk = 0.00;
			if (point.Count > 2)
			{
				for (int i = 1; i < point.Count; ++i)
				{
					kk = ((double)point[i].Y - (double)point[i - 1].Y) / ((double)point[i].X - (double)point[i - 1].X);
					k.Add(kk);
				}

				double bb = 0.00;

				for (int i = point.Count + 1; i < point.Count * 2; ++i)
				{
					bb = (double)point[i - point.Count].Y - (k[i - point.Count - 1] * (double)point[i - point.Count].X);
					k.Add(bb);
				}
			}

			ffl = 0;
			using (Graphics g = Graphics.FromImage(image)) //обнуляем график
			{
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.Clear(Color.White);

			}
			pictureBox1.Refresh();
			drawing();
			Ma();
		}

		private void drawing() //рисуем график
		{
			using (Graphics g = Graphics.FromImage(image))
			{

				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.SmoothingMode = SmoothingMode.HighQuality;
				var p = Pens.Gray.Clone() as Pen;
				p.Width = 1;
				for (int i = 0; i < point.Count - 1; i++)
				{
					PointF point1 = new PointF(point[i].X, 255 - point[i].Y);
					PointF point2 = new PointF(point[i + 1].X, 255 - point[i + 1].Y);
					Rectangle rect = new Rectangle(point[i].X - 2, 255 - point[i].Y - 2, 3, 3);
					g.DrawLine(p, point1, point2);
					g.DrawEllipse(p, rect);
				}
			}
			pictureBox1.Refresh();
		}


		protected void Ma() //сюда не смотрите, тут все очень плохо............................
		{

			for (int ii = 0; ii < img.Height; ++ii)
			{
				for (int jj = 0; jj < img.Width; ++jj)
				{
					var pix1 = img.GetPixel(jj, ii);
					var pix2 = img1.GetPixel(jj, ii);

					int r2 = pix2.R;

					int g2 = pix2.G;

					int b2 = pix2.B;



					pix1 = Color.FromArgb(r2, g2, b2);
					img.SetPixel(jj, ii, pix1);
				}
			}


			if (k.Count != 0)
			{
				for (int ii = 0; ii < img.Height; ++ii)
				{
					for (int jj = 0; jj < img.Width; ++jj)
					{
						var pix = img.GetPixel(jj, ii);
						int r = pix.R;
						if ((globalFlagRGB == "R")||(globalFlagRGB == "RGB"))
						{
							
							for (int i = 0; i < point.Count - 1; i++)
							{
								if ((r >= point[i].X) && (r <= point[i + 1].X))
								{
									if (point[i].X > point[i].Y)
										r = Convert.ToInt32(k[i] * r - k[i + point.Count - 1]);
									else
										r = Convert.ToInt32(k[i] * r + k[i + point.Count - 1]);
									if (r > 255)
									{
										r = 255;
									}
									if (r < 0)
									{
										r = 0;
									}
									break;
								}
							}
						}
						int g = pix.G;
						if ((globalFlagRGB == "G") || (globalFlagRGB == "RGB"))
						{
							
							for (int i = 0; i < point.Count - 1; i++)
							{
								if ((g >= point[i].X) && (g <= point[i + 1].X))
								{
									if (point[i].X > point[i].Y)
										g = Convert.ToInt32(k[i] * g - k[i + point.Count - 1]);
									else
										g = Convert.ToInt32(k[i] * g + k[i + point.Count - 1]);
									if (g > 255)
									{
										g = 255;
									}
									if (g < 0)
									{
										g = 0;
									}
									break;
								}
							}
						}
						int b = pix.B;
						if ((globalFlagRGB == "B") || (globalFlagRGB == "RGB"))
						{
							
							for (int i = 0; i < point.Count - 1; i++)
							{
								if ((b >= point[i].X) && (b <= point[i + 1].X))
								{
									if (point[i].X > point[i].Y)
										b = Convert.ToInt32(k[i] * b - k[i + point.Count - 1]);
									else
										b = Convert.ToInt32(k[i] * b + k[i + point.Count - 1]);
									if (b > 255)
									{
										b = 255;
									}
									if (b < 0)
									{
										b = 0;
									}
									break;
								}
							}
						}
						pix = Color.FromArgb(r, g, b);
						img.SetPixel(jj, ii, pix);
					}

				}
				pictureBox2.Image = img;
			}
			gisteasy();

		}
		private void gisteasy()
		{
			if (comboBox1.SelectedItem == "R")
			{
				hist = GetHistogrammR(img as Bitmap);
				globalFlagRGB = "R";
			}
			else if (comboBox1.SelectedItem == "G")
			{
				hist = GetHistogrammG(img as Bitmap);
				globalFlagRGB = "G";
			}
			else if (comboBox1.SelectedItem == "B")
			{
				hist = GetHistogrammB(img as Bitmap);
				globalFlagRGB = "B";
			}
			else
			{
				globalFlagRGB = "RGB";
				hist = GetHistogrammRGB(img as Bitmap);
			}
			this.pictureBox3.Paint += (o, e) => DrawHistogramm(e.Graphics, pictureBox3.ClientRectangle, hist, pictureBox3.Height);
			this.pictureBox3.Resize += (o, e) => Refresh();
			Refresh();
		}
		private void button1_Click(object sender, EventArgs e)
		{

			Ma();
			gisteasy();



		}
		private void chart2_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			string filename_2 = ofd.FileName;

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				pictureBox2.Image = new Bitmap(ofd.FileName);
				img = (Bitmap)Image.FromFile(ofd.FileName);
				
				img1 = (Bitmap)Image.FromFile(ofd.FileName);
				
				pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
				gisteasy();
			}
		}

		static void DrawHistogramm(Graphics g, Rectangle rect, int[] hist, int Height)
		{

			float max = hist.Max();
			if (max > 0)
				for (int i = 0; i < hist.Length; i++)
				{
					float h = rect.Height * hist[i] / (float)max;
					g.FillRectangle(Brushes.Gray, i * rect.Width / (float)hist.Length, rect.Height - h, rect.Width / (float)hist.Length, h);
				}
		}
		static int[] GetHistogrammRGB(Bitmap image)
		{
			int[] result = new int[256];
			Color color;
			for (int x = 0; x < img.Width; x++)
				for (int y = 0; y < img.Height; y++)
				{
					color = img.GetPixel(x, y);
					int r = color.R;

					int g = color.G;

					int b = color.B;

					int height = (r + g + b) / 3;
					result[height]++;
				}

			return result;
		}
		static int[] GetHistogrammR(Bitmap image)
		{
			int[] result = new int[256];
			Color color;
			for (int x = 0; x < img.Width; x++)
				for (int y = 0; y < img.Height; y++)
				{
					color = img.GetPixel(x, y);
					int r = color.R;
					int height = r;
					result[height]++;
				}

			return result;
		}

		static int[] GetHistogrammG(Bitmap image)
		{
			int[] result = new int[256];
			Color color;
			for (int x = 0; x < img.Width; x++)
				for (int y = 0; y < img.Height; y++)
				{
					color = img.GetPixel(x, y);
					int g = color.G;

					int height = g;
					result[height]++;
				}

			return result;
		}

		static int[] GetHistogrammB(Bitmap image)
		{
			int[] result = new int[256];
			Color color;
			for (int x = 0; x < img.Width; x++)
				for (int y = 0; y < img.Height; y++)
				{
					color = img.GetPixel(x, y);
					int b = color.B;

					int height = b;
					result[height]++;
				}

			return result;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			k.Clear();
			for (int i = 1; i < point.Count - 1; i++)
				point.RemoveAt(i);
			using (Graphics g = Graphics.FromImage(image)) //обнуляем график
			{
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.Clear(Color.White);

			}
			pictureBox1.Refresh();
			drawing();

			Ma();

		}

		private void button4_Click(object sender, EventArgs e)
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

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			gisteasy();
			Ma();
		}
	}
}

	

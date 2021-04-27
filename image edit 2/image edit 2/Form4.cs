using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace image_edit_2
{
	public partial class Form4 : Form
	{
		private static Bitmap img = null;
		private Bitmap image = null;
		List<double> rgbValues = new List<double>();
		List<byte> rgbValuesb = new List<byte>();
		public Form4()
		{
			InitializeComponent();
			
		}

		//кнопки
		#region buttons
		private void button3_Click(object sender, EventArgs e) //критерий Гаврилова
		{
			double sum = 0;
			double res = 0;
			
			for (int i = 0; i< rgbValues.Count; i++)
			{
				sum += rgbValues[i];
			}
			res = sum / rgbValues.Count;

			for (int i = 0; i < rgbValues.Count; i+=3)
			{
				if ((rgbValues[i] + rgbValues[i+1] + rgbValues[i+2])/3 <= res)
				{
					rgbValuesb.Add(0);					
				}
				else
				{
					rgbValuesb.Add(1);
				}
			}
			outbytes();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			string filename_2 = ofd.FileName;


			if (ofd.ShowDialog() == DialogResult.OK)
			{
				
				image = new Bitmap(ofd.FileName);
				pictureBox1.Image = image;
				img = new Bitmap(image.Width, image.Height);
				pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
				inbytes();
			}
			
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

		private void button1_Click(object sender, EventArgs e) //критерий Отсу
		{
			math(image);
		}

		private void button9_Click(object sender, EventArgs e)
		{

		}

		#endregion


		//кривая работа с байтами
		#region inout
		public void inbytes()
		{
			rgbValues.Clear();
			rgbValuesb.Clear();
			for (int x = 0; x < image.Height; x++)
				for (int y = 0; y < image.Width; y++)
				{
					var pix = image.GetPixel(y, x);
					rgbValues.Add(0.2125*pix.R);
					rgbValues.Add(0.7154*pix.G);
					rgbValues.Add(0.0721*pix.B);
				}
		}

		void outbytes()
		{
			int l;
			int i = -1;
			

			for (int x = 0; x < image.Height; x++)
				for (int y = 0; y < image.Width; y++)
				{
					++i;
					if (rgbValuesb[i] == 0)
						l = 0;
					else
						l = 255;
					var pix = image.GetPixel(y, x);
					pix = Color.FromArgb(l, l, l);
					img.SetPixel(y, x, pix);
				}
			pictureBox2.Image = img;
			pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
		}
		#endregion

		


		//работа с байтами от Гаврилова
		#region bytesGav
		/*public static Bitmap math(Bitmap image)
		{
			int width = image.Width;
			int height = image.Height;
			using (Bitmap _tmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
			{
				_tmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
				using var g = Graphics.FromImage(_tmp);
					g.DrawImageUnscaled(image, 0, 0);


					byte[] input_bytes = getImgBytes(_tmp);
					byte[] out_bytes = new byte[input_bytes.Length];
					for (int i = 0; i < input_bytes.Length; i += 3)
					{
						input_bytes[i] = clmp(0.2125 * input_bytes[i + 2] + 0.7154 * input_bytes[i + 1] + 0.0721 * input_bytes[0]);
						input_bytes[i + 2] = input_bytes[i + 1] = input_bytes[i];
					}

					var t = input_bytes.Average(x => x);

					var bytes = input_bytes.Select(x => (x < t) ? (byte)0 : (byte)255).ToArray();
					int a = 10;
					var ty = a << 2;

					Bitmap img_ret = new Bitmap(width, height, PixelFormat.Format24bppRgb);
					img_ret.SetResolution(image.HorizontalResolution, image.VerticalResolution);
					writeImageBytes(img_ret, bytes);

					return img_ret; 
			}
		
		}

		static byte clmp(double d)
		{
			if (d > 255)
				return 255;
			if (d < 0)
				return 0;
			return (byte)d;
		}


		static byte[] getImgBytes(Bitmap img)
		{
			byte[] bytes = new byte[img.Width * img.Height * 3];  //выделяем память под массив байтов

			var data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),  //блокируем участок памати, занимаемый изображением
				ImageLockMode.ReadOnly,
				img.PixelFormat);
			Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);  //копируем байты изображения в массив
			img.UnlockBits(data);   //разблокируем изображение
			return bytes; //возвращаем байты
		}
		static void writeImageBytes(Bitmap img, byte[] bytes)
		{
			var data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),  //блокируем участок памати, занимаемый изображением
				ImageLockMode.WriteOnly,
				img.PixelFormat);
			Marshal.Copy(bytes, 0, data.Scan0, bytes.Length); //копируем байты массива в изображение

			img.UnlockBits(data);  //разблокируем изображение
		}*/
		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using Conversor_de_texto_a_codigo_de_barras.Properties;
using System.Drawing.Printing;

namespace Conversor_de_texto_a_codigo_de_barras
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Generador_Click(object sender, EventArgs e)
        {
            string Barcode = textBox1.Text;
            Bitmap bitmap = new Bitmap(Barcode.Length * 50, 200);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Font ofont = new System.Drawing.Font("IDAutomationHC39M", 35);
                PointF point = new PointF(2f, 20f);
                SolidBrush black = new SolidBrush(Color.Black);
                SolidBrush White = new SolidBrush(Color.White);
                graphics.FillRectangle(White,0,0,bitmap.Width, bitmap.Height);
                graphics.DrawString( Barcode , ofont, black, point);
            }
            using(MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                pictureBox1.Image = bitmap;
                pictureBox1.Height = bitmap.Height;
                pictureBox1.Width = bitmap.Width;
            }

        }

        private void Imprimir_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += myPrintPage;
            pd.Document = doc;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }
        
        private void myPrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(pictureBox1.Width , pictureBox1.Height );
            pictureBox1.DrawToBitmap(bm, new Rectangle(15,0,pictureBox1.Width , pictureBox1.Height ));
            e.Graphics.DrawImage(bm , 0, 0);
            bm.Dispose();
        }

    }
}

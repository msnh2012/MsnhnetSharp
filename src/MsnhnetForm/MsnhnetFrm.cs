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
using MsnhnetSharp;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace MsnhnetForm
{

    public partial class MsnhnetFrm : Form
    {
        Msnhnet net = new Msnhnet();
        bool netInited = false;
        string labels = "";
        string savedImagePath = "";

        public MsnhnetFrm()
        {
            InitializeComponent();
            resetImgBtn.Enabled = false;
        }
        public static string picOpenFilter = "all Image File | *.bmp; *.pcx; *.png; *.jpg; *.gif;" +
                                             "*.tif; *.ico; *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf|" +
                                             "Bitmap( *.bmp; *.jpg; *.png;...) | *.bmp; *.pcx; *.png; *.jpg; *.gif; *.tif; *.ico|" +
                                             "Vector map( *.wmf; *.eps; *.emf;...) | *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf";


        private void readImgBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = picOpenFilter;
            if (of.ShowDialog() == DialogResult.OK)
            {
                string temp = of.FileName;
                savedImagePath = temp;
                resetImgBtn.Enabled = true;
                try
                {
                    pictureBox1.Image = Image.FromFile(temp);
                    pictureBox1.Image = ImgPro.ConvertTo24bpp(pictureBox1.Image);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void classfiyBtn_Click(object sender, EventArgs e)
        {
            if (!netInited)
            {
                MessageBox.Show("Network is not inited");
                return;
            }
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("there is no pic in picBox!");
                return;
            }

            Rectangle rect = new Rectangle(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height);

            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            BitmapData bmpdata = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int best = net.RunClassifyList(bmpdata, MsnhnetDef.PredDataType.PRE_DATA_FC32_C3, false,false);

            bitmap.UnlockBits(bmpdata);

            string[] labelList = labels.Split('\n');

            float time = net.GetCpuForwardTime();

            richTextBox1.AppendText("CPU inference time:" + time.ToString() + " ms\n");

            richTextBox1.AppendText("CPU inferece result: " + labelList[best] + "\n");

        }

        private void msnhnetPathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "msnhnet | *.msnhnet";
            if (of.ShowDialog() == DialogResult.OK)
            {
                string temp = of.FileName;
                msnhnetPathTxt.Text = temp;
            }
        }

        private void msnhnetBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "msnhbin | *.msnhbin";
            if (of.ShowDialog() == DialogResult.OK)
            {
                string temp = of.FileName;
                msnhbinPathTxt.Text = temp;
            }
        }

        private void labelPathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "names | *.names";
            if (of.ShowDialog() == DialogResult.OK)
            {
                string temp = of.FileName;
                labelPathTxt.Text = temp;
            }
        }

        private void initBtn_Click(object sender, EventArgs e)
        {
            if(msnhnetPathTxt.Text == "" || msnhbinPathTxt.Text == "" || labelPathTxt.Text == "")
            {
                MessageBox.Show("Params error, please check params config");
                return;
            }
            net.InitNet();
            net.BuildNet(msnhnetPathTxt.Text, msnhbinPathTxt.Text, false, false);
            StreamReader sr = new StreamReader(labelPathTxt.Text);
            labels = sr.ReadToEnd();
            sr.Close();
            netInited = true;
            richTextBox1.AppendText("Init done!\n");
        }

        private void classfiyGPUBtn_Click(object sender, EventArgs e)
        {
            if(!Msnhnet.WithGPU())
            {
                MessageBox.Show("Msnhnet not build with GPU");
                return;
            }
            if (!netInited)
            {
                MessageBox.Show("Network is not inited");
                return;
            }
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("there is no pic in picBox!");
                return;
            }

            Rectangle rect = new Rectangle(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height);// image的长宽,非pictureBox的长宽

            Bitmap bitmap = (Bitmap)pictureBox1.Image;//image转bitmap

            BitmapData bmpdata = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int best = net.RunClassifyList(bmpdata, MsnhnetDef.PredDataType.PRE_DATA_FC32_C3,false,true);

            bitmap.UnlockBits(bmpdata);

            string[] labelList = labels.Split('\n');

            float time = net.GetGpuForwardTime();

            richTextBox1.AppendText("GPU inference time:" + time.ToString() + " ms\n");

            richTextBox1.AppendText("GPU inferece result: " + labelList[best] + "\n");
        }

        private void MsnhnetFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            net.Dispose();
        }


        /// <summary>
        /// use c# to do preprocess, the result will a little different from others 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void classifyPreBtn_Click(object sender, EventArgs e)
        {
            MsnhnetDef.Dim dim = net.GetInputDim();
            Bitmap bitmap = (Bitmap)pictureBox1.Image;//image转bitmap
            Bitmap outBitmap;
            ImgPro.ReSize(bitmap, dim.width, dim.height, out outBitmap, true);// a little different from opencv
            bitmap = ImgPro.ConvertTo24bpp(outBitmap);

            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            float[] inputData = new float[3 * bmpData.Width * bmpData.Height];

            unsafe
            {
                byte* ptr = (byte*)(bmpData.Scan0);

                for (int i = 0; i < bmpData.Height; i++)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        for (int j = 0; j < bmpData.Width; j++)
                        {
                            inputData[(k * bmpData.Width * bmpData.Height + i * bmpData.Width + j)] = ptr[i * 3* bmpData.Width + j * 3 + k] / 255.0f;
                        }
                    }
                }
            }

            bitmap.UnlockBits(bmpData);

            int best = net.RunClassifyNoPred(inputData, false);

            string[] labelList = labels.Split('\n');

            // actually time need to add preprocess time , you can do it by your self
            float time = net.GetCpuForwardTime();

            richTextBox1.AppendText("CPU inference time:" + time.ToString() + " ms\n");

            richTextBox1.AppendText("CPU inferece result: " + labelList[best] + "\n");

        }

        private void yoloDetectBtn_Click(object sender, EventArgs e)
        {
            if (!netInited)
            {
                MessageBox.Show("Network is not inited");
                return;
            }

            Rectangle rect = new Rectangle(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height);// image的长宽,非pictureBox的长宽

            Bitmap bitmap = (Bitmap)pictureBox1.Image;//image转bitmap

            BitmapData bmpdata = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            List<Msnhnet.BBox> bboxes = net.RunYoloList(bmpdata, false, false);

            bitmap.UnlockBits(bmpdata);

            for (int i = 0; i < bboxes.Count; i++)
            {
                bboxes[i] = ImgPro.bboxResize2Org(bboxes[i], net.GetInputDim().width, net.GetInputDim().height, bitmap.Width, bitmap.Height);
            }

            pictureBox1.Image = drawYolo(bitmap, bboxes,labels);

            float time = net.GetCpuForwardTime();

            richTextBox1.AppendText("CPU inference time:" + time.ToString() + " ms\n");
        }

        private Bitmap drawYolo(Bitmap bitmap, List<Msnhnet.BBox> bboxes, string mlabels)
        {
            Bitmap bm = bitmap;

            string[] mLabelList = mlabels.Split('\n');

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                Font font = new Font("Source Sans Pro", 10,FontStyle.Regular);
                for (int i = 0; i < bboxes.Count; i++)
                {
                    int bestClsIdx = (int)bboxes[i].bestClsIdx;
                    MsnhnetDef.Vec3 color = MsnhnetDef.colors[bestClsIdx];
                    using (Pen thick_pen = new Pen(Color.FromArgb(255, color.z, color.y, color.x), 3))
                    {
                        string label = ((int)(bboxes[i].conf*100.0f)).ToString() + "% " + mLabelList[bestClsIdx];
                        SolidBrush brush = new SolidBrush(Color.FromArgb(150, color.z, color.y, color.x));
                        gr.FillRectangle(brush, bboxes[i].x - bboxes[i].w / 2  - 3, bboxes[i].y - bboxes[i].h / 2 - 20, 20 + 10 * label.Length, 20);
                        gr.DrawString(label, font, Brushes.White, bboxes[i].x - bboxes[i].w / 2, 
                                                                  bboxes[i].y - bboxes[i].h / 2 - 20);

                        float x = bboxes[i].x;
                        float y = bboxes[i].y;
                        float w = bboxes[i].w;
                        float h = bboxes[i].h;

                        double orgAngle = bboxes[i].angle / 180.0 * Math.PI;

                        double angle = orgAngle;

                        double v =   Math.Sqrt(w * w + h * h) / 2;

                        double rad = Math.Atan(h/w);

                        double dx = v * Math.Cos(rad);
                        double dy = v * Math.Sin(rad);


                        double p1X = - dx;
                        double p1Y = + dy;

                        double p2X = + dx;
                        double p2Y = + dy;
 
                        double p3X = + dx;
                        double p3Y = - dy;
  
                        double p4X = - dx;
                        double p4Y = - dy;

                        p1X = p1X * Math.Cos(angle)  + p1Y * Math.Sin(angle);
                        p1Y = -p1X * Math.Sin(angle) + p1Y * Math.Cos(angle);

                        p2X = p2X * Math.Cos(angle)  + p2Y * Math.Sin(angle);
                        p2Y = -p2X * Math.Sin(angle) + p2Y * Math.Cos(angle);

                        p3X = p3X * Math.Cos(angle)  + p3Y * Math.Sin(angle);
                        p3Y = -p3X * Math.Sin(angle) + p3Y * Math.Cos(angle);

                        p4X = p4X * Math.Cos(angle)  + p4Y * Math.Sin(angle);
                        p4Y = -p4X * Math.Sin(angle) + p4Y * Math.Cos(angle);


                        gr.DrawLine(thick_pen, new Point((int)(p1X + x), (int)(p1Y+y)), new Point((int)(p2X + x), (int)(p2Y+y)));
                        gr.DrawLine(thick_pen, new Point((int)(p2X + x), (int)(p2Y+y)), new Point((int)(p3X + x), (int)(p3Y+y)));
                        gr.DrawLine(thick_pen, new Point((int)(p3X + x), (int)(p3Y+y)), new Point((int)(p4X + x), (int)(p4Y+y)));
                        gr.DrawLine(thick_pen, new Point((int)(p4X + x), (int)(p4Y+y)), new Point((int)(p1X + x), (int)(p1Y+y)));
                    }
                }
            }

            return bm;
        }

        private void yoloGPUBtn_Click(object sender, EventArgs e)
        {
            if (!netInited)
            {
                MessageBox.Show("Network is not inited");
                return;
            }

            Rectangle rect = new Rectangle(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height);// image的长宽,非pictureBox的长宽

            Bitmap bitmap = (Bitmap)pictureBox1.Image;//image转bitmap

            BitmapData bmpdata = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            List<Msnhnet.BBox> bboxes = net.RunYoloList(bmpdata, false, true);

            bitmap.UnlockBits(bmpdata);

            for (int i = 0; i < bboxes.Count; i++)
            {
                bboxes[i] = ImgPro.bboxResize2Org(bboxes[i], net.GetInputDim().width, net.GetInputDim().height, bitmap.Width, bitmap.Height);
            }

            pictureBox1.Image = drawYolo(bitmap, bboxes, labels);

            float time = net.GetGpuForwardTime();

            richTextBox1.AppendText("GPU inference time:" + time.ToString() + " ms\n");
        }

        private void resetImgBtn_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(savedImagePath);
                pictureBox1.Image = ImgPro.ConvertTo24bpp(pictureBox1.Image);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

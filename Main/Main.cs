using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
namespace Main
{
   
    public partial class Main : Form
    {
        private static Image<Bgr, byte> My_Image;
        
        //private static string _Link_Image = @"C:\Users\Admin\Desktop\temto\IMG0001.jpg";
        private static string _Link_Image = @"C:\Users\Admin\Desktop\Baumer Image Records\4\IMG0005.jpg";
        int xxx = 5;
        public Main()
        {
            InitializeComponent();
            
        }

        private void iStart_Click(object sender, EventArgs e)
        {
            if (iStart.Text == "Start")
            {
                iStart.Text = "Stop";
                timer1.Enabled = true;
                
            }
            else
            {
                iStart.Text = "Start";
                timer1.Enabled = false;
            }
            iHandling();
        }
        private void iHandling()
        {
            MyCamera _Camera = new MyCamera();
            Mat im;
            //read image
            if (iFromCamera.Checked == false)
            {
                im = CvInvoke.Imread(_Link_Image);
                My_Image = im.ToImage<Bgr, byte>();
                im.Dispose();
            }
            else
                My_Image = _Camera.Capture_Image();
            My_Image = Vision.RoiImage(My_Image);
            using (VectorOfVectorOfPoint contours = Vision.FindContours(My_Image))
            {
                CvInvoke.DrawContours(My_Image, contours, -1, new MCvScalar(0, 0, 255));
                iLog.Text = My_Image.Size.ToString();
                var M = CvInvoke.Moments(contours[1]);
                
                Point[] P =  Vision.GetCenter(contours);
                for (int i = 0; i < P.Length; i++)
                    CvInvoke.Circle(My_Image, P[i], 2, new MCvScalar(0, 255, 0), 2);
                int kc = Vision.CalculatorDistance(P);



            }
            iShow.Image = My_Image.ToBitmap();
        }
        private void iChooseFolder_Click(object sender, EventArgs e)
        {
            MyCamera _Camera = new MyCamera();
            _Link_Image = _Camera.Read_Image();
            iLinkFolder.Text = _Link_Image;
        }

        private void iFromFolder_CheckedChanged(object sender, EventArgs e)
        {
                iLinkFolder.Enabled = iFromFolder.Checked;
                iChooseFolder.Enabled = iFromFolder.Checked;
        }

        private void iFromCamera_CheckedChanged(object sender, EventArgs e)
        {
            iListCamera.Enabled = iFromCamera.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string link = @"C:\Users\Admin\Desktop\Baumer Image Records\4\IMG" + xxx.ToString("D4") + ".JPG";
            _Link_Image = link;
            iHandling();
            xxx += 5;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyCamera _Camera = new MyCamera();
            Mat im;
            if (iFromCamera.Checked == false)
            {
                im = CvInvoke.Imread(_Link_Image);
                My_Image = im.ToImage<Bgr, byte>();
                im.Dispose();
            }
            else
                My_Image = _Camera.Capture_Image();
            iShow.Image = Vision.TestRoi(My_Image).ToBitmap();
        }
    }
}

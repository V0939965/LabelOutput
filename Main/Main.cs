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
        private static string _Link_Image = @"C:\Users\Admin\Desktop\temnho\IMG0002.jpg";
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
            // handling image
            My_Image = Vision.RoiImage(My_Image);
            Mat aa = new Mat();
            using (VectorOfVectorOfPoint contours = Vision.FindContours(My_Image))
            {
                CvInvoke.DrawContours(My_Image, contours,-1, new MCvScalar(0, 0, 255));
                iLog.Text = My_Image.Size.ToString();
                
                for (int i=0;i<contours.Size;i++)
                {
                    CvInvoke.FitLine(contours[i], aa, DistType.C, 0.1, 0.1, 0.1);
                }



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
            string link = @"C:\Users\Admin\Desktop\temnho\VLG-12M_8765441318_190309-164404\IMG" + xxx.ToString("D4") + ".JPG";
            _Link_Image = link;
            iHandling();
            xxx += 5;
        }
    }
}

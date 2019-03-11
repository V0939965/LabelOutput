using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.Util;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using DirectShowLib;

namespace Main
{
    class MyCamera
    {
        public string Read_Image()
        {
            Image<Bgr, byte> img;
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                //img = new Image<Bgr, byte>(Openfile.FileName);
                return Openfile.FileName;
            }
            return null;
        }
        public Image<Bgr, byte> Capture_Image()
        {
            Image<Bgr, byte> img;
            return null;
        }
    }
}

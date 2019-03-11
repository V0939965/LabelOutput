using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;
using System.Windows.Forms;

namespace Main
{
    class Vision
    {
        public static double Threshold_Value = 150;
        private static int x = 400;
        private static int y = 510;
        private static int width = 600;
        private static int height = 340;


        public static Image<Bgr, Byte> RoiImage(Image<Bgr, Byte> scr)
        {
            scr.ROI = Rectangle.Empty;
            Rectangle roi = new Rectangle(x, y, width, height); // set the roi
            scr.ROI = roi;
            return scr;
        }
        public static VectorOfVectorOfPoint FindContours(Image<Bgr,Byte> scr)
        {
            using (Image<Gray, byte> Gray_Image = scr.Convert<Gray, byte>())
            {
                using (Image<Gray, byte> Threshold_Image = new Image<Gray, byte>(Gray_Image.Size))
                {
                    CvInvoke.Threshold(Gray_Image, Threshold_Image, Threshold_Value, 255, ThresholdType.BinaryInv);
                    VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                    CvInvoke.FindContours(Threshold_Image, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                        for (int i = 0; i < contours.Size; i++)
                        {
                            CircleF cir = CvInvoke.MinEnclosingCircle(contours[i]);
                            System.Console.WriteLine(cir.Radius);
                            if (cir.Radius < 80)
                                contours[i].Dispose();
                        }
                        return contours;
                }
            }
        }
    }
}

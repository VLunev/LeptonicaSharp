using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LeptonicaSharp;

namespace TestNew
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var pix0 = Pix.Open(@"G:\ЛВА\Г 105-300БЦМ\ПОЛНЫЙ\Сканированный вариант\АК.08.00.04000.000 СБ - Доска фирменная\MYSCAN_20111129_0007.TIF");
            var pixGray = pix0.Convert1To8(0, 255);
            pix0.Dispose();
            var eee = pixGray.GetColormap();
            //var pix_scaled = pixGray.ScaleGrayMinMax((int)(1 / 0.5), (int)(1 / 0.5), (int)L_CHOOSE_M.L_CHOOSE_MIN);
                       var pix_scaled = pixGray.ScaleToGrayFast((float)0.5);
            pixGray.Dispose();
            */


            var pix = Pix.Open(@"C:\Users\vlunev\Desktop\test_img\222.tif");
            //var pix = Pix.Open(@"C:\Users\vlunev\Desktop\test_img\IconSetArrows5_32x32_2.png");

            //var bitmap = pix.ToBitmap();
            //var bitmap = pix.ConvertTo1BPPBMP();
            //var ptr = pix.GetWindowsHBITMAP(pix);
            //var bitmap = Image.FromHbitmap(ptr);

            //bitmap.Save(@"C:\Users\vlunev\Desktop\test_img\IconSetArrows5_32x32_3.png");

            pix.Display();
        }
    }
}

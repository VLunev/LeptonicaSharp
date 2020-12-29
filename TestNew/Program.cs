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

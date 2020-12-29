using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeptonicaSharp;

namespace TestNew
{
    class Program
    {
        static void Main(string[] args)
        {
            var pix = new Pix(@"C:\Users\vlunev\Desktop\test_img\111.tif");
            pix.Display();

        }
    }
}

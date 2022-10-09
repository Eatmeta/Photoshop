using System;
using System.Windows.Forms;
using MyPhotoshop.Parameters;

namespace MyPhotoshop
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var window = new MainWindow();
            window.AddFilter(new PixelFilter<LighteningParameters>(
                "Осветление/затемнение",
                (original, parameters) => original * parameters.Coefficient
            ));
            
            window.AddFilter(new PixelFilter<EmptyParameters>(
                "Оттенки серого",
                (original, parameters) =>
                {
                    var lightness = (original.R + original.G + original.B) / 3;
                    return new Pixel(lightness,lightness,lightness);
                }
            ));
            Application.Run(window);
        }
    }
}
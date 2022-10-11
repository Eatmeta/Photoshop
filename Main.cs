using System;
using System.Drawing;
using System.Windows.Forms;
using MyPhotoshop.Filters.Transform;
using MyPhotoshop.Parameters;
using MyPhotoshop.Transform;

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
                    return new Pixel(lightness, lightness, lightness);
                }
            ));

            window.AddFilter(new TransformFilter(
                   "Отражение по горизонтали",
                   (originalSize) => originalSize,
                   (originalSize, originalPixel) =>
                       new Point(originalSize.Width - 1 - originalPixel.X, originalPixel.Y)
               ));
   
               window.AddFilter(new TransformFilter(
                   "Поворот на 90 против часовой стрелке",
                   (originalSize) => new Size(originalSize.Height, originalSize.Width),
                   (originalSize, originalPixel) =>
                       new Point(originalSize.Width - originalPixel.Y - 1, originalPixel.X)
               ));
               
               window.AddFilter(new TransformFilter<RotationParameters>("Свободное вращение", new RotateTransformer()));
            Application.Run(window);
        }
    }
}
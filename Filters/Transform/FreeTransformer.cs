using System;
using System.Drawing;
using MyPhotoshop.Interfaces;
using MyPhotoshop.Parameters;

namespace MyPhotoshop.Filters.Transform
{
    public class FreeTransformer : ITransformer<EmptyParameters>
    {
        public FreeTransformer(Func<Size, Size> sizeTransformer, Func<Size, Point, Point> pixelTransformer)
        {
            SizeTransform = sizeTransformer;
            PixelTransform = pixelTransformer;
        }

        public Size ResultSize { get; set; }
        public Size OriginalSize { get; set; }
        private Func<Size, Size> SizeTransform { get; }
        private Func<Size, Point, Point> PixelTransform { get; }

        public void Prepare(Size size, EmptyParameters parameters)
        {
            OriginalSize = size;
            ResultSize = SizeTransform(size);
        }

        public Point? MapPoint(Point newPoint)
        {
            return PixelTransform(OriginalSize, newPoint);
        }
    }
}
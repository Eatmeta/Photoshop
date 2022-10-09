using System;
using System.Drawing;
using MyPhotoshop.Interfaces;
using MyPhotoshop.Parameters;

namespace MyPhotoshop
{
    public class TransformFilter<TParameters> : ParametrizedFilter<EmptyParameters>
        where TParameters : IParameters, new()
    {
        public string Name { get; }
        public Func<Size, Size> SizeTransform { get; }
        public Func<Size, Point, Point> PixelTransform { get; }

        public TransformFilter(string name, Func<Size, Size> sizeTransform, Func<Size, Point, Point> pixelTransform)
        {
            Name = name;
            SizeTransform = sizeTransform;
            PixelTransform = pixelTransform;
        }

        protected override Photo Process(Photo original, EmptyParameters values)
        {
            var originalSize = new Size(original.Width, original.Height);
            var result = new Photo(SizeTransform(originalSize).Width, SizeTransform(originalSize).Height);
            for (var x = 0; x < result.Width; x++)
            for (var y = 0; y < result.Height; y++)
            {
                var newPixel = PixelTransform(originalSize, new Point(x, y));
                result[x, y] = original[newPixel.X, newPixel.Y];
            }

            return result;
        }

        public override string ToString() => Name;
    }
}
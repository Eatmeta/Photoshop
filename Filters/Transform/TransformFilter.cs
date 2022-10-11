using System;
using System.Drawing;
using MyPhotoshop.Interfaces;
using MyPhotoshop.Parameters;

namespace MyPhotoshop
{
    public class TransformFilter<TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new()
    {
        public string Name { get; }
        public Func<Size, TParameters, Size> SizeTransform { get; }
        public Func<Size, Point, TParameters, Point?> PixelTransform { get; }

        public TransformFilter(string name, Func<Size, TParameters, Size> sizeTransform,
            Func<Size, Point, TParameters, Point?> pixelTransform)
        {
            Name = name;
            SizeTransform = sizeTransform;
            PixelTransform = pixelTransform;
        }

        protected override Photo Process(Photo original, TParameters values)
        {
            var originalSize = new Size(original.Width, original.Height);
            var result = new Photo(SizeTransform(originalSize, values).Width, SizeTransform(originalSize, values).Height);
            for (var x = 0; x < result.Width; x++)
            for (var y = 0; y < result.Height; y++)
            {
                var newPixel = PixelTransform(originalSize, new Point(x, y), values);
                if (newPixel.HasValue)
                    result[x, y] = original[newPixel.Value.X, newPixel.Value.Y];
            }

            return result;
        }

        public override string ToString() => Name;
    }
}
using System;
using MyPhotoshop.Interfaces;
using MyPhotoshop.Parameters;

namespace MyPhotoshop
{
    public class GrayscaleFilter : PixelFilter<EmptyParameters>
    {
        public override string ToString() => "Оттенки серого";
        protected override Pixel ProcessPixel(Pixel original, EmptyParameters parameters)
        {
            var lightness = (original.R + original.G + original.B) / 3;
            return new Pixel(lightness,lightness,lightness);
        }
    }
}
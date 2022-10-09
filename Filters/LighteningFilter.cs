using System;
using MyPhotoshop.Interfaces;

namespace MyPhotoshop
{
    public class LighteningFilter : PixelFilter<LighteningParameters>
    {
        public override string ToString() => "Осветление/затемнение";

        protected override Pixel ProcessPixel(Pixel original, LighteningParameters parameters)
            => original * parameters.Coefficient;
    }
}
using System;
using System.Drawing;
using MyPhotoshop.Filters.Transform;
using MyPhotoshop.Parameters;


namespace MyPhotoshop
{
    public class TransformFilter : TransformFilter<EmptyParameters>
    {
        public TransformFilter(string name, Func<Size, Size> sizeTransformer, Func<Size, Point, Point> pixelTransformer)
            : base(name, new FreeTransformer(sizeTransformer, pixelTransformer))
        {
        }
    }
}
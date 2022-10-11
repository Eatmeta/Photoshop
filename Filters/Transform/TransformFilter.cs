using System;
using System.Drawing;
using MyPhotoshop.Interfaces;
using MyPhotoshop.Parameters;
using MyPhotoshop.Transform;

namespace MyPhotoshop
{
    public class TransformFilter<TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new()
    {
        public string Name { get; }
        public ITransformer<TParameters> Transformer { get; }

        public TransformFilter(string name, ITransformer<TParameters> transformer)
        {
            Name = name;
            Transformer = transformer;
        }
        
        protected override Photo Process(Photo original, TParameters values)
        {
            var originalSize = new Size(original.Width, original.Height);
            Transformer.Prepare(originalSize, values);
            var result = new Photo(Transformer.ResultSize.Width, Transformer.ResultSize.Height);
            for (var x = 0; x < result.Width; x++)
            for (var y = 0; y < result.Height; y++)
            {
                var newPixel = Transformer.MapPoint(new Point(x, y));
                if (newPixel.HasValue)
                    result[x, y] = original[newPixel.Value.X, newPixel.Value.Y];
            }

            return result;
        }

        public override string ToString() => Name;
    }
}
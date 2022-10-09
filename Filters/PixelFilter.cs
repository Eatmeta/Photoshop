using System;
using MyPhotoshop.Interfaces;

namespace MyPhotoshop
{
    public class PixelFilter<TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new()
    {
        public string Name { get; }
        public Func<Pixel, TParameters, Pixel> Processor { get; }
        
        public PixelFilter(string name, Func<Pixel, TParameters, Pixel> processor)
        {
            Name = name;
            Processor = processor;
        }
        
        public override string ToString() => Name;
        
        protected override Photo Process(Photo original, TParameters values)
        {
            var result = new Photo(original.Width, original.Height);
            for (var x = 0; x < result.Width; x++)
            for (var y = 0; y < result.Height; y++)
                result[x, y] = Processor(original[x, y], values);
            return result;
        }
    }
}
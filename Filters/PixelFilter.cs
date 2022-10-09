using MyPhotoshop.Interfaces;

namespace MyPhotoshop
{
    /// <summary>
    /// Класс, отвечающий за создание изображения того же размера, наполнения его пикселями, т.е. за подготовку
    /// к применению фильтра.
    /// </summary>
    public abstract class PixelFilter<TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new()
    {
        protected abstract Pixel ProcessPixel(Pixel original, TParameters parameters);

        protected override Photo Process(Photo original, TParameters values)
        {
            var result = new Photo(original.Width, original.Height);
            for (var x = 0; x < result.Width; x++)
            for (var y = 0; y < result.Height; y++)
                result[x, y] = ProcessPixel(original[x, y], values);
            return result;
        }
    }
}
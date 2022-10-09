using MyPhotoshop.Interfaces;

namespace MyPhotoshop
{
    /// <summary>
    /// Класс, отвечающий за параметры фильтра перед его применением. Работает по аналогии
    /// с PixelFilter.cs, отвечающим за подготовку изображения перед применением фильтра.
    /// </summary>
    public abstract class ParametrizedFilter<TParameters> : IFilter
        where TParameters : IParameters, new()
    {
        public ParameterInfo[] GetParameters() => new TParameters().GetDescription();

        public Photo Process(Photo original, double[] values)
        {
            var parameters = new TParameters();
            parameters.SetValues(values);
            return Process(original, parameters);
        }

        protected abstract Photo Process(Photo photo, TParameters parameters);
    }
}
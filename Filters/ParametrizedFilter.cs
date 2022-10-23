using MyPhotoshop.Interfaces;
using MyPhotoshop.Parameters;

namespace MyPhotoshop
{
    /// <summary>
    /// Класс, отвечающий за параметры фильтра перед его применением. Работает по аналогии
    /// с PixelFilter.cs, отвечающим за подготовку изображения перед применением фильтра.
    /// </summary>
    public abstract class ParametrizedFilter<TParameters> : IFilter
        where TParameters : IParameters, new()
    {
        private IParametersHandler<TParameters> Handler { get; } = new SimpleParametersHandler<TParameters>();
        public ParameterInfo[] GetParameters() => Handler.GetDescription();

        public Photo Process(Photo original, double[] values)
        {
            var parameters = Handler.CreateParameters(values);
            return Process(original, parameters);
        }

        protected abstract Photo Process(Photo photo, TParameters parameters);
        
    }
}
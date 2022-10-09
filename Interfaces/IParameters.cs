namespace MyPhotoshop.Interfaces
{
    public interface IParameters
    {
        /// <summary>
        /// Этот метод возвращает описание параметров, которые появляются в NumericUpDown-контроле
        /// снизу от контрола выбора фильтра. Он аналогичен по смыслу GetParameters() из IFilter.
        /// </summary>
        /// <returns></returns>
        ParameterInfo[] GetDescription();

        /// <summary>
        /// Этот метод принимает введенные пользователем значения и распределяет их по полям класса.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        void SetValues(double[] inputs);
    }
}
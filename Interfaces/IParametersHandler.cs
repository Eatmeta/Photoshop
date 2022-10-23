namespace MyPhotoshop.Interfaces
{
    public interface IParametersHandler<TParameters>
    {
        ParameterInfo[] GetDescription();
        TParameters CreateParameters(double[] values);
    }
}
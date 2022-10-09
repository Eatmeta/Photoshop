using MyPhotoshop.Interfaces;

namespace MyPhotoshop.Parameters
{
    public class EmptyParameters : IParameters
    {
        public ParameterInfo[] GetDescription()
        {
            return new ParameterInfo[0];
        }

        public void SetValues(double[] inputs)
        {
        }
    }
}
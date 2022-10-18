using System.Linq;
using MyPhotoshop.Interfaces;

namespace MyPhotoshop.Parameters
{
    public static class ParametersExtensions
    {
        public static ParameterInfo[] GetDescription(this IParameters parameters)
        {
            return parameters
                .GetType()
                .GetProperties()
                .Select(z => z.GetCustomAttributes(typeof(ParameterInfo), false))
                .Where(z => z.Length > 0)
                .Select(z => z[0])
                .Cast<ParameterInfo>()
                .ToArray();
        }

        public static void SetValues(this IParameters parameters, double[] inputs)
        {
            var properties = parameters
                .GetType()
                .GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();

            for (var i = 0; i < inputs.Length; i++)
                properties[i].SetValue(parameters, inputs[i], new object[0]);
        }
    }
}
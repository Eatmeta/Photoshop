using System;
using System.Linq;
using System.Reflection;
using MyPhotoshop.Interfaces;

namespace MyPhotoshop.Parameters
{
    public class StaticParametersHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        private static PropertyInfo[] Properties { get; }
        private static ParameterInfo[] Descriptions { get; }

        static StaticParametersHandler()
        {
            Properties = typeof(TParameters)
                .GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();
            
            Descriptions = typeof(TParameters)
                .GetProperties()
                .Select(z => z.GetCustomAttributes(typeof(ParameterInfo), false))
                .Where(z => z.Length > 0)
                .Select(z => z[0])
                .Cast<ParameterInfo>()
                .ToArray();
        }

        public ParameterInfo[] GetDescription() => Descriptions;

        public TParameters CreateParameters(double[] values)
        {
            var parameters = new TParameters();
            
            if (Properties.Length != values.Length)
                throw new ArgumentException();

            for (var i = 0; i < values.Length; i++)
                Properties[i].SetValue(parameters, values[i], new object[0]);
            
            return parameters;
        }
    }
}
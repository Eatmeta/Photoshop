using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyPhotoshop.Interfaces;

namespace MyPhotoshop.Parameters
{
    public class ExpressionsParametersHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        private static Func<double[], TParameters> Parser { get; }
        private static ParameterInfo[] Descriptions { get; }

        static ExpressionsParametersHandler()
        {
            Descriptions = typeof(TParameters)
                .GetProperties()
                .Select(z => z.GetCustomAttributes(typeof(ParameterInfo), false))
                .Where(z => z.Length > 0)
                .Select(z => z[0])
                .Cast<ParameterInfo>()
                .ToArray();
            
            var properties = typeof(TParameters)
                .GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();

            // = values[]
            var array = Expression.Parameter(typeof(double[]), "values"); 

            var bindings = new List<MemberBinding>();
            for (var i = 0; i < properties.Length; i++)
            {
                // = Coefficient = values[0]
                var binding = Expression.Bind(
                    properties[i],
                    Expression.ArrayIndex(
                        array,
                        Expression.Constant(i))); 

                bindings.Add(binding);
            }

            // = new LighteningParameters {Coefficient = values[0]}
            var body = Expression.MemberInit(
                Expression.New(
                    typeof(TParameters).GetConstructor(new Type[0])),   // конструктор у которого ноль аргументов
                bindings);

            // = values => new LighteningParameters {Coefficient = values[0]};
            var lambda = Expression.Lambda<Func<double[], TParameters>>(
                body,
                array);
            
            Parser = lambda.Compile();
        }

        public ParameterInfo[] GetDescription() => Descriptions;

        public TParameters CreateParameters(double[] values)
        {
           return Parser(values);
        }
    }
}
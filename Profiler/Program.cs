using System;
using System.Diagnostics;
using MyPhotoshop;
using MyPhotoshop.Parameters;

namespace Profiler
{
    class Program
    {
        static void Test(string description, Func<double[], LighteningParameters> action, int n)
        {
            var array = new double[] {0};
            action(array);
            var watch = new Stopwatch();
            watch.Start();
            for (var i = 0; i < n; i++)
            {
                action(array);
            }
            watch.Stop();
            Console.WriteLine(description + ": " + 1000 * (double) watch.ElapsedMilliseconds / n + " ns");
        }

        static void Main(string[] args)
        {
            var handler = new SimpleParametersHandler<LighteningParameters>();
            Test("Test with reflection without static handler", values => handler.CreateParameters(values), 100_000);
            var staticHandler = new StaticParametersHandler<LighteningParameters>();
            Test("Test with reflection with static handler", values => staticHandler.CreateParameters(values), 100_000);
            Test("Test without reflection", values => new LighteningParameters {Coefficient = values[0]}, 100_000);
        }
    }
}
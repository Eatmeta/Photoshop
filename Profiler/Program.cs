using System;
using System.Diagnostics;
using MyPhotoshop;
using MyPhotoshop.Parameters;

namespace Profiler
{
    class Program
    {
        static void Test(string description, Action<double[], LighteningParameters> action, int n)
        {
            var array = new double[] {0};
            var obj = new LighteningParameters();
            action(array, obj);
            var watch = new Stopwatch();
            watch.Start();
            for (var i = 0; i < n; i++)
            {
                action(array, obj);
            }
            watch.Stop();
            Console.WriteLine(description + ": " + 1000 * (double) watch.ElapsedMilliseconds / n + " ms");
        }

        static void Main(string[] args)
        {
            Test("Test with reflection", (values, pars) => pars.SetValues(values), 100_000);
            Test("Test without reflection", (values, pars) => pars.Coefficient = values[0], 100_000);
        }
    }
}
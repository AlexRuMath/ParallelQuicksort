
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace ParallelQuicksort
{
    public class Program
    {
        private static bool DEBUG_MODE = false;

        private static void Main(string[] args)
        {
            if (DEBUG_MODE)
            {
                BenchmarkSwitcher.FromAssembly(typeof(QuicksortBenchmark).Assembly).Run(args, new DebugInProcessConfig());
            }
            else
            {
                BenchmarkRunner.Run<QuicksortBenchmark>();
            }
        }
    }
}
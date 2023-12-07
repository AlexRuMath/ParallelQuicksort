using BenchmarkDotNet.Attributes;
using ParallelQuicksort.sorts;

namespace ParallelQuicksort
{
    [WarmupCount(5)]
    [IterationCount(5)]
    public class QuicksortBenchmark
    {
        public const int NUM_OF_ELEMENT = 10_000_000;

        public IEnumerable<int[]> Values()
        {
            int[] arr = Enumerable.Range(0, NUM_OF_ELEMENT).ToArray();
            var rand = new Random();
            rand.Shuffle(arr);

            yield return arr;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public async Task Parallel(int[] arr)
        {
            var quicksort = new ParallelQuicksort.sorts.ParallelQuicksort();
            await quicksort.QuickSort(arr, 10_000);
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public void Sequance(int[] arr)
        {
            var quicksort = new SequanceQuicksort();
            quicksort.Sort(arr, 0, arr.Length - 1);
        }
    }
}

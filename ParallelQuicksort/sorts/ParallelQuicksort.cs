namespace ParallelQuicksort.sorts
{
    internal class Arr
    {
        public required int[] a;
        public int ptr;
    }

    public class ParallelQuicksort
    {
        public ParallelQuicksort() { }

        public async Task QuickSort(int[] a, int divided)
        {
            var arrs = Divide(a, divided);
            var seqSort = new SequanceQuicksort();
            int numbers_of_tasks = arrs.Length;

            List<Task> tasks = [];
            foreach (var arr in arrs)
            {
                var tmp = arr;
                tasks.Add(Task.Run(() => { seqSort.Sort(tmp, 0, tmp.Length - 1, 5_000); }));
            }

            await Task.WhenAll([.. tasks]).ConfigureAwait(false);
            List<Arr> arrayList = [];
            for(int i = 0; i < arrs.Length; i++)
            {
                arrayList.Add(new() { a = arrs[i], ptr = 0 });
            }

            Merge(a, arrayList);
        }

        private static void Merge(int[] destArr, List<Arr> arrs)
        {
            int minValue;
            Arr min;

            for (int i = 0; i < destArr.Length; i++)
            {
                var firstArr = arrs.First();
                minValue = firstArr.a[firstArr.ptr];
                min = firstArr;

                for (int j = 1; j < arrs.Count; j++)
                {
                    if (arrs[j].a[arrs[j].ptr].CompareTo(minValue) < 0)
                    {
                        minValue = arrs[j].a[arrs[j].ptr];
                        min = arrs[j];
                    }
                }

                destArr[i] = minValue;
                min.ptr++;

                if (min.ptr >= min.a.Length)
                {
                    arrs.Remove(min);
                }
            }
        }

        private int[][] Divide(int[] a, int divided)
        {
            int steps = a.Length / divided;
            int[][] arrs = new int[steps][];
            for(int i = 0; i < steps; i++)
            {
                arrs[i] = (i == steps - 1 ? new int[a.Length - i * divided] : new int[divided]);
                Array.Copy(a, divided * i, arrs[i], 0, arrs[i].Length);
            }

            return arrs;
        }
    }
}

using System.Drawing;

namespace ParallelQuicksort.sorts
{
    public class SequanceQuicksort
    {
        private const int DIFF = 10_000;
        public SequanceQuicksort() { }

        private int[] LinearSort(int[] arr)
        {
            int j;
            int step = arr.Length / 2;
            while (step > 0)
            {
                for (int i = 0; i < (arr.Length - step); i++)
                {
                    j = i;
                    while ((j >= 0) && (arr[j] > arr[j + step]))
                    {
                        (arr[j + step], arr[j]) = (arr[j], arr[j + step]);
                        j -= step;
                    }
                }
                step /= 2;
            }

            return arr;
        }

        public void Sort(int[] arr, int left, int right, int depth=12_000)
        {
            if(depth == 0)
            {
                /*
                int[] tmp = LinearSort(arr[left..(right + 1)]);
                Array.Copy(tmp, 0, arr, left, tmp.Length);
                */

                int[] tmp = arr[left..(right + 1)];
                Array.Sort(tmp);
                Array.Copy(tmp, 0, arr, left, tmp.Length);
                return;
            }

            var i = left;
            var j = right;
            var pivot = arr[left];
            while (i <= j)
            {
                while (arr[i] < pivot)
                {
                    i++;
                }

                while (arr[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    (arr[j], arr[i]) = (arr[i], arr[j]);
                    i++;
                    j--;
                }
            }

            if (left < j)
                Sort(arr, left, j, depth - 1);
            if (i < right)
                Sort(arr, i, right, depth - 1);
        }
    }
}

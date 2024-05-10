public class Solution
{
    public int[] KthSmallestPrimeFraction(int[] arr, int k)
    {
        double lo = 0.0;
        double hi = 1.0;
        int[] result = new int[2];

        while (lo < hi)
        {
            double mid = (lo + hi) / 2;
            int count = 0;
            double maxFraction = 0.0;
            int j = 1;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                while (j < arr.Length && arr[i] > mid * arr[j])
                    j++;

                count += arr.Length - j;
                if (j < arr.Length && arr[i] / (double)arr[j] > maxFraction)
                {
                    maxFraction = arr[i] / (double)arr[j];
                    result[0] = arr[i];
                    result[1] = arr[j];
                }
            }

            if (count == k)
                return result;
            else if (count < k)
                lo = mid;
            else
                hi = mid;
        }

        return result;
    }
}

public class Solution
{
    public int RemoveElement(int[] nums, int val)
    {
        var finalPointer = nums.Count() - 1;
        var i = 0;
        while (i <= finalPointer)
        {
            if (nums[finalPointer] == val)
                finalPointer--;
            else
            {
                if (nums[i] == val)
                {
                    nums[i] = nums[finalPointer];
                    finalPointer--;
                }
                i++;
            }
        }
        return i;
    }
}

public class Review()
{
    public bool TwoSumSorted(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            if (nums[left] + nums[right] == target)
            {
                return true;
            }

            if (nums[left] + nums[right] < target)
                left++;
            else
                right--;
        }

        return false;
    }

}
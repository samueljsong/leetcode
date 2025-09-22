public class TwoPointer
{

    public int ContainerWithMostWater(int[] heights)
    {
        int left = 0;
        int right = heights.Count() - 1;
        int max_area = 0;

        while (left < right)
        {
            max_area = Math.Max(Math.Min(heights[left], heights[right]) * (right - left), max_area);

            if (heights[left] < heights[right])
                left += 1;
            else
                right -= 1;
        }

        return max_area;
    }

    public bool TwoSumSorted(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Count() - 1;

        while (left < right)
        {
            if (nums[left] + nums[right] == target)
                return true;

            if (nums[left] + nums[right] < target)
                left += 1;
            else
                right -= 1;
        }

        return false;
    }

    public List<List<int>> ThreeSum(int[] nums)
    {
        var results = new List<List<int>>();
        Array.Sort(nums);

        for (int i = 0; i < nums.Length; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
                continue;

            int left = i + 1;
            int right = nums.Length - 1;

            while (left < right)
            {
                int sum = nums[i] + nums[left] + nums[right];

                if (sum == 0)
                {
                    results.Add(new List<int> { nums[i], nums[left], nums[right] });

                    while (left < right && nums[left] == nums[left + 1]) left++;
                    while (left < right && nums[right] == nums[right - 1]) right--;

                    left++;
                    right--;
                }
                else if (sum < 0)
                    left++;
                else
                    right--;
            }
        }

        return results;
    }

    public int TriangleNumber(int[] nums)
    {
        //Valid Triangle means that 2 sides added is larger than the third.

        
        return 0;
    }

}
/// <summary>
/// General tips for the two pointer approach
/// 
/// 1. Identify the brute force 
/// 2. Check to see if the two pointers can help
/// 3. Decide the movement of the two pointers. what condition makes the left and right pointer move?
/// 
/// 
/// General tips for adapting
/// 1. Does the solution depend on 2 ends or 2 moving pointers
/// 2. What invariant am I maintaining
///     - in trapped water it is maxLeft and maxRight
///     - a condition that is always true at a specific point in a program's execution, acting as a logical contract
/// 3. Which pointer should move
/// 4. Which condition am I checking while moving?
/// 
/// 
/// GENERIC THINGS
///     When looping throught a whole array we use for (int i = 0; i < [].Count; i++)
///     - Count will give the actual 'n' of the length of the array. 
///     - but because we do < it will not go out of bounds.
/// 
///     When we want the last element we use .Count - 1 because we want n - 1
/// </summary>

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
        int result = 0;

        Array.Sort(nums);

        for (int i = nums.Length - 1; i >= 2; i--) // since the array is sorted, we can assign the end of the array the largest side of the triangle.
        {
            int left = 0;       // then we just need to compare the numbers under the largest side.
            int right = i - 1;  // That is why right is i - 1

            while (left < right)
            {
                if (nums[left] + nums[right] > nums[i]) // we are comparing the lowest (left) and the highest (right). If it is greater than nums[i]
                {
                    result += (right - left);           // Then we know the rest of the values in between will also work as a valid triangle
                    right--;                            // now we lower the top border.
                }
                else
                {
                    left++;                             // if it is not > nums[i] we raise the lowest value by 1
                }
            }
        }
        return result;
    }

    public void MoveZeroes(int[] nums)
    {
        int left = 0;
        int right = 0;

        while (right < nums.Length)
        {
            if (nums[right] != 0)
            {
                int temp = nums[right];
                nums[right] = nums[left];
                nums[left] = temp;

                left++;
            }

            right++;
        }
    }

    // This uses the Dutch National Flag (DNF) algorithm
    // it is a 3 way partitioning algorithm
    public void SortColors(int[] nums)
    {                                       // It uses 3 pointers
        int left = 0;                       // the lower bound
        int mid = 0;                        // current element being checked
        int right = nums.Length - 1;        // the upper bound

        while (mid <= right)                // Algorithm terminates once mid > right
        {
            int temp = 0;

            if (nums[mid] == 0)
            {
                temp = nums[left];
                nums[left] = nums[mid];
                nums[mid] = temp;

                left++;
                mid++;
            }
            else if (nums[mid] == 1)
            {
                mid++;
            }
            else if (nums[mid] == 2)
            {
                temp = nums[right];
                nums[right] = nums[mid];
                nums[mid] = temp;
                right--;
            }
        }
    }


    /// <summary>
    /// Water can only be trapped if both the left and right of height[i] is greater than height[i]
    /// To calculate, we get the minimum from the left and right and subtract it with height[i]
    /// But remember that no matter what heights we have in between, when we are calculating the trapped water
    /// as long as we know that the opposite side has a side equal to or higher side, then we can just use the max of one end to calculate the trapped water
    /// 
    /// example [3, 2, 6, 4, 5]
    /// 
    /// when looking at i = 1, we even though we have a 4 on the right of 6
    /// because we understand that if the right side has a side that is equal to or above 3, water WILL be trapped.
    /// 
    /// 
    /// Applying the general tips
    /// 1. for each index i, find the highest wall on left and right then use water[i] = min(maxLeft, maxRight) - height[i]
    /// 2. water at i depends on both sides, instead of recomputing every time, what if we track the max values by pointers moving inwards?
    /// 3. we only need the smaller max side to compute trapped water.
    ///     so if (maxLeft < maxRight) -> the left side controls the water value.
    /// </summary>
    public int TrappingRainWater(int[] height)
    {
        int left = 0;
        int right = height.Length - 1;
        int leftMax = 0;
        int rightMax = 0;
        int trappedWater = 0;

        while (left < right)
        {
            if (height[left] < height[right])                   // Compare left and right to see which one is lower. 
            {                                                   // remember, we use lower side to calculate the trapped water.
                if (height[left] >= leftMax)
                    leftMax = height[left];                     // We use leftMax as the reference for calculating trapped water.
                else
                    trappedWater += leftMax - height[left];

                left++;
            }
            else                                                // Right side is lower so we will calculate the trapped water here.
            {
                if (height[right] >= rightMax)
                    rightMax = height[right];
                else
                    trappedWater += rightMax - height[right];

                right--;
            }
        }

        return trappedWater;
    }

    public int FindTheIndexOfTheFirstOccurrenceInAString(string haystack, string needle)
    {
        //Invariant: for a given i we are checking if haystack[i -- i + needle.Length - 1] == needle
        for (int i = 0; i <= haystack.Length - needle.Length; i++)      // pointer i = start of potential match in haystack: move 1 step whenever match fails
        {
            int j = 0;                                                  // pointer j = iterates over needle: move 1 step when match succeeds

            while (j < needle.Length && haystack[i + j] == needle[j])   // Condition: j will move when match succeeds
            {
                j++;
            }

            if (j == needle.Length)
                return i;
        }

        return -1;
    }

    /// <summary>
    /// This uses the Floyd's Tortoise and Hare approach
    ///     slow will move one by one fast will move by 2's
    ///     The idea is that if there is a cycle, fast and slow will equal each other.
    ///     if there is no cycle, fast will reach null.
    /// 
    ///     if you want to find the node it starts from, after you find fast == slow, assign one of the pointers back to the original head.
    ///     continue to find the next for both nodes and you will find it equal again. That is your starting point.
    /// 
    /// It is a special algorithm of the two pointer approach
    ///     - used for cycle detection.
    /// 
    /// 
    /// </summary>
    public bool LinkedListCycle(ListNode head)
    {
        if (head.next == null || head == null)
            return false;

        ListNode slow = head;
        ListNode fast = head;

        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;

            if (slow == fast)
                return true;
        }

        return false;
    }

    public bool IsPalindrome(ListNode head)
    {
        if (head.next == null || head == null)
            return true;

        ListNode slow = head;
        ListNode fast = head;

        while (fast != null && fast.next != null)   // once fast == null or fast.next == null, slow is in the middle of the linked list.
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        // Because we have found the middle point, we need to create the reversed linked list from the end to the mid point.
        ListNode prev = null;                       // this will be our first value of the reversed second half of the linked list
        ListNode curr = slow;

        while (curr != null)
        {
            ListNode nextTemp = curr.next;          // temporary holder for the next value
            curr.next = prev;                       // we change the curr.next value to be the prev value
            prev = curr;                            // update prev value to be the current value
            curr = nextTemp;                        // change the current value to be the nextTemporary node
        }

        ListNode first = head;
        ListNode second = prev;
        while (second != null)
        {
            if (first.val != second.val)
                return false;

            first = first.next;
            second = second.next;
        }

        return true;
    }

    public string ReverseVowelsOfAString(string s)
    {
        var vowels = new HashSet<char> { 'a', 'i', 'e', 'o', 'u', 'A', 'I', 'E', 'O', 'U' };

        List<char> word = s.ToList();

        int left = 0;
        int right = word.Count() - 1;

        while (left < right)
        {
            if (vowels.Contains(word[left]) && vowels.Contains(word[right]))
            {
                char temporaryCharacter = word[left];
                word[left] = word[right];
                word[right] = temporaryCharacter;
                left++;
                right--;
            }

            while (left < word.Count && !vowels.Contains(word[left]))
                left++;

            while (right > 0 && !vowels.Contains(word[right]))
                right--;
        }

        return string.Join("", word);
    }
    
    public int[] IntersectionOfTwoArrays(int[] nums1, int[] nums2) {
        Array.Sort(nums1);
        Array.Sort(nums2);

        int firstPointer = 0;
        int secondPointer = 0;

        HashSet<int> intersection = new HashSet<int> ();

        while (firstPointer < (nums1.Length) && secondPointer < (nums2.Length))
        {
            if (nums1[firstPointer] == nums2[secondPointer])
            {
                intersection.Add(nums1[firstPointer]);
                firstPointer++;
                secondPointer++;
            }
            else if (nums1[firstPointer] < nums2[secondPointer])
            {
                firstPointer++;
            }
            else{
                secondPointer++;
            }
        }

        return intersection.ToArray();
    }
}

public class ListNode
{
    public int val;
    public ListNode next;

    public ListNode(int val)
    {
        val = val;
        next = null;
    }
}
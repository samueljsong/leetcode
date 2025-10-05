/// <summary>
/// Sliding Window is a specialized version of the two pointer.
/// 
/// Use when we want to find a min / max / sum of a substring.
/// 
/// Sliding window has 2 techniques
/// 1. Variable Length Window
/// 2. Fixed Size Window
/// 
/// The basic format is for variable length window.
/// 
/// for (i in range(arr))
///     while (condition is not met)
///         shrink window
/// 
/// For fixed size, for every iteration you both add and remove elements to maintain the window size.
/// 
/// int left = 0;
/// int windowSum = 0;
/// int maxSum = 0;
/// 
/// for (int right = 0; right < arr.Length; right++)
///     windowSum += arr[right];
/// 
///     if (right - left + 1 == k)                      //invariant: the size of the window will be right - left + 1 == k
///         maxSum = Math.Max(windowSum, maxSum);
///         windowSum -= arr[left];
///         left++;
///     
///     we want to use while before we increase the window size or do calculations because 
///     we want to ENSURE the window is VALID before doing any calculations or adding elements.
/// 
///     if the addition of a new element to that window size can possibly 'break' or make it invalid, check and shrink the window then add
///     if the addition of a new element to that window size can 'fix it' or 'improve' the window, then add it before.
/// 
/// For Fixed windows, the most important thing is where is your starting and where is your ending pointers.
/// 
/// ❌ : need review
/// ✅ : mastered
/// ⭐ : difficult
/// </summary>
public class SlidingWindow()
{

    /// <summary>
    /// ❌
    /// 
    /// Notes: remember the base design for variable window size
    /// </summary>
    public int LengthOfLongestSubstring(string s)
    {

        var history = new HashSet<char>();
        int left = 0;
        int longestSubstring = 0;

        for (int right = 0; right < s.Length; right++)
        {

            while (history.Contains(s[right]))          // run the while loop before doing history.Add() to make it valid.
            {
                history.Remove(s[left]);
                left++;
            }

            history.Add(s[right]);

            longestSubstring = Math.Max(longestSubstring, right - left + 1);
        }

        return longestSubstring;
    }

    /// <summary>
    /// Things I learned.
    /// 
    /// 1. figure out a way to keep track of the maxFrequency of the letter for the window.
    /// 2. if you do window size - frequency, you get the number of characters that are different.
    /// 3. compare that with the k value
    /// 
    /// ** Ensure that your window is VALID before you 
    /// 
    ///     In this question, the addition of a new element can possible 'improve' the window as it can be a valid addition.
    ///     Therefor, we add it before shrinking the window.
    /// </summary>
    public int LongestRepeatingCharacterReplacement(string s, int k)
    {
        var history = new Dictionary<char, int>();
        int left = 0;
        int longestSubstring = 0;
        int maxFrequency = 0;

        for (int right = 0; right < s.Length; right++)
        {
            history[s[right]] = history.ContainsKey(s[right]) ? history[s[right]] + 1 : 1;  // This could improve the window.

            maxFrequency = Math.Max(maxFrequency, history[s[right]]);

            while ((right - left + 1) - maxFrequency > k)                                   // Shrink the window size.
            {
                history[s[left]] = history[s[left]] - 1;
                left++;
            }

            longestSubstring = Math.Max(longestSubstring, right - left + 1);
        }

        return longestSubstring;
    }


    /// <summary>
    /// This is a split window problem.
    ///     not a simple left / right ++ but shifting the boundaries from the front and back.
    ///         - they are separate values.
    /// 
    /// Things I have learned
    ///     1. Understand that we cannot use normal start and ending points.
    ///     2. Get k elements from the left
    ///     3. Remove one by one from left and add one by one from right.
    /// </summary>
    public int MaximumPointsYouCanObtainFromCards(int[] cardPoints, int k)
    {
        if (cardPoints.Length == k)             // This is for edge cases
            return cardPoints.Sum();

        int start = k - 1;                      // Start with the kth element
        int windowScore = 0;

        while (start >= 0)                      // Get the initial windowScore
        {
            windowScore += cardPoints[start];
            start--;
        }

        int maxScore = windowScore;             // Set the maxScore as the current windowScore
        start = k - 1;                          // reset start as we need to remove one by one from left

        for (int end = cardPoints.Length - 1; end >= cardPoints.Length - k; end--)      // The window is already k size so we can do calculations right away.
        {
            windowScore -= cardPoints[start];               // Remove from left value
            start--;                                        // Reduce start pointer
            windowScore += cardPoints[end];                 // Add one from right value
            maxScore = Math.Max(windowScore, maxScore);     // We now have our new window so compare with current max
        }

        return maxScore;    // return maxScore
    }
    
    public long MaximumSubarraySum(int[] nums, int k) {
        var history = new HashSet<int> ();

        int left       = 0;
        long maxSum    = 0;
        long windowSum = 0;

        for (int right = 0; right < nums.Length; right++)
        {
            while (history.Contains(nums[right]))
            {
                history.Remove(nums[left]);
                windowSum -= nums[left];
                left++;
            }

            history.Add(nums[right]);
            windowSum += nums[right];

            if (right - left + 1 == k)
            {
                maxSum = Math.Max(maxSum, windowSum);
                history.Remove(nums[left]);
                windowSum -= nums[left];
                left++;
            }
        }

        return maxSum;
    }
}
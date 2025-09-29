/// <summary>
/// Sliding Window is a specialized version of the two pointer.
/// 
/// Use when we want to find a min / max / sum of a substring.
/// 
/// The basic format is
/// 
/// for (i in range(arr))
///     while (condition is not met)
///         shrink window
///     
///     we want to use while before we increase the window size or do calculations because 
///     we want to ENSURE the window is VALID before doing any calculations or adding elements.
/// </summary>
public class SlidingWindow()
{

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
}
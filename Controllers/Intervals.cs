/// <summary>
/// Interval problems are given as a list of [start, end] times
///     - Typically involves sorting the given intervals and then processing each interval in sorted order.
/// 
/// Sorting by Start Time:
///     - makes it easier to merge two intervals that are overlapping.
///     - overlapping intervals: 
///     - After sorting by start time, an interval may overlap a previous interval if the start time happens before the previous intervals end time.
///         - Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
///           for (int i = 0 ; i < intervals.Length; i++)
///           {
///               if (intervals[i][0] < intervals[i - 1][1])
///                   return false
///           }
///           return true
///
///     - Merging intervals:
///         - when an interval overlaps, with the previous interval they can be merged into a single interval.
///         - to merge: set the end time of the previous interval to be the max of either time.
///             
///           
///           Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
///           List<int[]> merged = new List<int[]>();
///           foreach (var interval in intervals)
///           {
///               if (merged.Count == 0 || interval[0] > merged[^1][1])
///               {
///                   merged.Add(interval);
///               }
///               else
///               {
///                   merged[^1][1] = Math.Max(merged[^1][1], interval[1]);         // ^1 : get the last element
///               }
///           }
/// 
///           return merged
/// 
/// Sorting by End Time:
///     - we risk adding an interval that starts early but ends late to the set of non overlapping intervals.
/// 
///         Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
///         int end = intervals[0][1];
///         int count = 1;                                              // number of non overlapping intervals.
///         for (int i = 0; i < intervals.Length; i++)
///         {
///             if (intervals[i][0] >= end)
///             {
///                 end = intervals[i][1];
///                 count++;
///             }
///         }
///         return intervals.Length - count;
/// 
/// </summary>
public class Intervals()
{

    /// <summary>
    /// 
    /// </summary>
    public int[][] InsertIntervals(int[][] intervals, int[] newInterval)
    {

        var merged = new List<int[]>();
        int i      = 0;
        int n      = intervals.Length;

        while (i < n && intervals[i][1] < newInterval[0])
        {
            merged.Add(intervals[i]);
            i++;
        }

        while (i < n && intervals[i][0] <= newInterval[1])
        {
            newInterval[0] = Math.Min(newInterval[0], intervals[i][0]);
            newInterval[1] = Math.Max(newInterval[1], intervals[i][1]);
            i++;
        }

        merged.Add(newInterval);

        while (i < n)
        {
            merged.Add(intervals[i]);
            i++;
        }

        return merged.ToArray();
    }


}
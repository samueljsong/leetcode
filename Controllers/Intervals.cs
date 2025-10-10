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
    /// Write a function to check if a person can attend all the meetings scheduled without any time conflicts. 
    /// Given an array intervals, where each element [s1, e1] represents a meeting starting at time s1 and ending at time e1, 
    /// determine if there are any overlapping meetings. 
    /// 
    /// If there is no overlap between any meetings, return true; otherwise, return false.
    /// </summary>
    public bool canAttendMeetings(int[][] intervals)
    {
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        for (int i = 0; i < intervals.Length; i++)
        {
            if (intervals[i][0] < intervals[i - 1][1])      // remember that we are comparing the start of an interval with the end of the next.
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// The Solution operates in 3 different phases
    ///     1. Add all the intervals ending before the newInterval starts into merged
    ///     2. Merge all overlapping intervals with newInterval and add that merged interval to merged
    ///     3. Add all intervals starting after newInterval to merged
    /// </summary>
    public int[][] InsertIntervals(int[][] intervals, int[] newInterval)
    {

        var merged = new List<int[]>();
        int i = 0;
        int n = intervals.Length;

        // Phase 1: start adding all the intervals ending before the newInterval starting time.
        while (i < n && intervals[i][1] < newInterval[0])
        {
            merged.Add(intervals[i]);
            i++;
        }

        // Phase 2: Merge all overlapping intervals with newInterval and add that merged interval to merged.
        // Here we are resetting the starting point and ending point of the newInterval to act as a 'merged' interval
        while (i < n && intervals[i][0] <= newInterval[1])                  // we want this to be <= because if the start is equal to the ending, it is overlapping.
        {
            newInterval[0] = Math.Min(newInterval[0], intervals[i][0]);     // we want the minimum value for the starting point.
            newInterval[1] = Math.Max(newInterval[1], intervals[i][1]);     // we want the maximum value for the ending point.
            i++;
        }

        merged.Add(newInterval);        // We now add the new 'merged' interval.

        // Phase 3: Add the remaining intervals.
        while (i < n)
        {
            merged.Add(intervals[i]);
            i++;
        }

        return merged.ToArray();
    }

    /// <summary>
    /// MY SOLUTION
    /// For this question, we want to sort the intervals based on the end time.
    /// 
    ///     By doing so, we end up finding the maximum number of non-overlapping intervals in a given list of intervals.
    ///     if we had [ [1, 8], [2, 3], [3, 5] ], we risk processing 1 - 8 first. because the range is so big, it will block us from adding the correct amount of intervals.
    ///     if we were to sort by ending first, we can start adding by the intervals as early as possible.
    /// </summary>
    public int EraseOverlapIntervals(int[][] intervals)
    {
        Array.Sort(intervals, (a, b) => a[1].CompareTo(b[1]));

        int overlappingIntervals = 0;
        int currentEndingMax = intervals[0][0];

        for (int i = 0; i < intervals.Length; i++)
        {
            if (intervals[i][0] < currentEndingMax)
            {
                overlappingIntervals++;
            }
            else
            {
                currentEndingMax = intervals[i][1];
            }
        }

        return overlappingIntervals;
    }
    
    /// <summary>
    /// The greedy solution
    /// 
    ///     This solution tries to find the maximum number of non overlapping intervals and then 
    ///     subtracts the count of intervals with that number. That will give you overlapping intervals.
    /// </summary>
    public int EraseOverlapIntervals2(int[][] intervals)
    {
         // If there are no intervals, there are no overlaps to erase
        if (intervals == null || intervals.Length == 0)
            return 0;

        // Sort intervals by their end time (ascending)
        Array.Sort(intervals, (a, b) => a[1].CompareTo(b[1]));

        int end = intervals[0][1]; // Track the end time of the last non-overlapping interval
        int count = 1;             // At least one interval is always non-overlapping

        // Iterate through the remaining intervals
        for (int i = 1; i < intervals.Length; i++)
        {
            // If current interval starts after or exactly when the previous one ends
            if (intervals[i][0] >= end)
            {
                // Non-overlapping interval found
                end = intervals[i][1];
                count++;
            }
        }

        // Total intervals - non-overlapping ones = intervals to remove
        return intervals.Length - count;
    }
}
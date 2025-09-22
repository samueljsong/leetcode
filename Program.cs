var twopointer = new TwoPointer();

var results = twopointer.ThreeSum([-1, 0, 1, 2, -1, -1]);

results.ForEach(triplet => 
    Console.WriteLine($"[{string.Join(", ", triplet)}]"));


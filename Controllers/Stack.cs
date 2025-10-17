using System.Text;

/// <summary>
/// Stack data structure is a collection of elements that follow the last in first out LIFO principle.
///     - Elements can be
///         1. Pushed
///         2. Popped
/// 
/// we usually use an array to represent a stack. []
///     - in C# we can use the Stack Class.
/// 
/// If you need a data structure that provides O(1) access of the first element inserted then use a queue. 
/// If you need O(1) access of the last element then use a stack.
/// 
/// Queues will be interconnected with BFS
/// Stack will be interconnecrted with DFS
/// 
/// When do we want to use it?
/// - anytime we work with anything "nested", safe to assume to use a stack.
/// 
/// ❌ : need review
/// ✅ : mastered
/// ⭐ : difficult
/// </summary>
public class Stack()
{
    /// <summary>
    /// 
    /// </summary>
    public bool IsValidParenthesis(string s)
    {
        var parenthesisPairs = new Dictionary<char, char>
        {
            {')', '('},
            {'}', '{'},
            {']', '['}
        };

        var stack = new Stack<char>();

        for (int i = 0; i < s.Length; i++)
        {
            if (parenthesisPairs.ContainsKey(s[i]))
            {
                if (stack.Count == 0 || stack.Peek() != parenthesisPairs[s[i]])     // Before popping, we need to make sure the length is not 0.
                    return false;                                                   // we also use Peek() to only see but not return the value.

                stack.Pop();                                                        // Once we check everything we can now POP
            }
            else
            {
                stack.Push(s[i]);
            }
        }

        return stack.Count == 0;
    }

    /// <summary>
    /// We know to use a stack here because
    ///     1. we are managing nested data: a[4]
    ///     2. you need to revert to the previous context when met with closing parenthesis
    ///     3. Each starting parenthesis starts a new context.
    /// 
    /// 
    /// Things I learned when working on this
    ///     - List has Insert(idx, val): will insert and keep/push anything else
    ///     - to convert Stack into string: new String(stack.ToArray());
    ///         - it seems if we have an array, we can make it a string by using new String()
    ///     - char has a method to check if it is a letter: IsLetter() and number: IsDigit();
    ///     - int.TryParse() only works on strings.
    /// 
    ///     (c - '0') will convert to a number.
    ///         - char is basically a numeric type representing a Unicode code point. char 0 = 48, char 1 = 49
    ///         - so if we have char 'c' the unicode = 51. 
    ///         - if we do c - '0' we are doing 51 - 48.
    /// 
    ///     you can also access last element using [^1]
    /// 
    ///     StringBuilder = class in System.Text designed for efficient manipulation of strings.
    /// 
    ///     Enumerable.Repeat: LINQ method that creates a sequence 
    /// 
    /// 
    ///     Why this works
    ///     --------------
    ///     
    /// 
    ///     if you think of it like:
    ///         - Each '[' pushes a save point / we are starting a new level of decoding.
    ///         - Each ']' restores that save point
    ///     then the logic will feel much more natural.
    /// 
    ///     if input = 3[a2[c]]
    ///     
    ///     think of it like
    ///     repeat 3 times {
    ///         "a" + repeat 2 times { "c" }
    ///     }
    /// 
    /// ❌ ⭐
    /// </summary>
    public string DecodeString(string s)
    {
        var stack = new Stack<object>(); // Stack to hold previous strings and numbers
        int currentNumber = 0;                   // Current number (repetition count)
        StringBuilder currString = new StringBuilder(); // Current working string

        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];

            if (c == '[')
            {
                // Push current string and number onto stack
                stack.Push(currString.ToString());
                stack.Push(currentNumber);

                // Reset current string and number
                currString.Clear();
                currentNumber = 0;
            }
            else if (c == ']')
            {
                // Pop number and previous string
                int num = (int)stack.Pop();
                string prevString = (string)stack.Pop();

                // Repeat current string num times and append to previous string
                currString = new StringBuilder(prevString + string.Concat(Enumerable.Repeat(currString.ToString(), num)));
            }
            else if (char.IsDigit(c))
            {
                // Build the number (handle multi-digit numbers)
                currentNumber = currentNumber * 10 + (c - '0');
            }
            else
            {
                // Append character to current string
                currString.Append(c);
            }
        }

        return currString.ToString(); // Return final decoded string
    }

    /// <summary>
    /// At a high level, we can solve this problem by iterating over each index of the string, 
    /// and then calculating the length of the longest valid parentheses substring that ends at that index. 
    /// We can then take the maximum of these lengths to get the final answer.
    /// 
    /// The Stack is keeping track of the index rather than the '(' or ')'
    /// 
    /// we calculate valid parenthesis with (currentIndex - the last unmatched '(' index: by using stack.Peek())
    /// ❌ ⭐
    /// </summary>
    public int LongestValidParentheses(string s)
    {
        int maxLength = 0;
        var stack = new Stack<int>();       // will only have the index of the index's of '(' or a start to a new valid parenthesis index.
        stack.Push(-1);

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
                stack.Push(i);      // These are the index's of unmatched pairs.

            if (s[i] == ')')        // if we find a ')' we know the top element in the stack is the pair.
            {
                stack.Pop();        // so we pop it because the opening '(' found its match.

                if (stack.Count > 0)    // if we have more in stack, we can now calculate the length of the valid parenthesis.
                    maxLength = Math.Max(maxLength, (i - stack.Peek()));    // we peek because that index has yet to find its pair.
                else
                    stack.Push(i);
            }
        }

        return maxLength;
    }
}


/// <summary>
/// A monotonic stack is a special type of stack in which all elements are either of descending or ascending order
/// usually to solve problems that require finding the next greater or smaller element in the array.
/// </summary>
public class MonotonicStack()
{
    /// <summary>
    /// 
    /// </summary>
    public int[] DailyTemperatures(int[] temperatures) {
        var daysToWait = Enumerable.Repeat(0, temperatures.Length).ToList();
        var stack      = new Stack<int>();

        for (int i = 0; i < temperatures.Length; i++)
        {
            while (stack.Count > 0 && temperatures[i] > temperatures[stack.Peek()])
            {
                int index = stack.Pop();
                daysToWait[index] = i - index;
            }
            
            stack.Push(i);
        }

        return daysToWait.ToArray();
    }

}
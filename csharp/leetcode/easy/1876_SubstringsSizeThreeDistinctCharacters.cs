public class Solution
{
    public int CountGoodSubstrings(string s)
    {
        var counter = 0;
        if (s.Length < 3)
            return 0;
        var firstElement = s[0];
        var secondElement = s[1];
        for (var i = 2; i < s.Length; i++)
        {
            if (s[i] == firstElement || s[i] == secondElement || firstElement == secondElement)
            {
                firstElement = s[i - 1];
                secondElement = s[i];
                continue;
            }
            firstElement = s[i - 1];
            secondElement = s[i];
            counter++;
        }

        return counter;
    }
}

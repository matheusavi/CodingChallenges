public class Solution
{
    public int SecondHighest(string s)
    {
        var hashSet = new HashSet<char>(s);
        bool digitFound = false;
        for (var i = '9'; i >= '0'; i--)
        {
            if (hashSet.Contains(i))
            {
                if (digitFound)
                    return i - '0';
                digitFound = true;
            }
        }
        return -1;
    }
}

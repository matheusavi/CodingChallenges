public class Solution
{
    public int MinFlips(string target)
    {
        int counter = 0;
        char lastDigit = ' ';
        for (var i = target.Length - 1; i >= 0; i--)
        {
            if (target[i] != lastDigit)
            {
                counter++;
                lastDigit = target[i];
            }
        }

        if (lastDigit == '0' && counter > 0)
            counter--;

        return counter;
    }
}

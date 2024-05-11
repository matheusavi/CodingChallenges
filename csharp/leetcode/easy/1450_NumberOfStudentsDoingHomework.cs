public class Solution
{
    public int BusyStudent(int[] startTime, int[] endTime, int queryTime)
    {
        var counter = 0;
        for (var i = 0; i < startTime.Length; i++)
            if (queryTime >= startTime[i] && queryTime <= endTime[i])
                counter++;
        return counter;
    }
}

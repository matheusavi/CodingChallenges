public class Solution
{
    public int[][] Merge(int[][] intervals)
    {
        var result = intervals.ToList();
        return Compress(result).ToArray();
    }

    private List<int[]> Compress(List<int[]> list)
    {
        if (list.Count() == 1)
            return list;
        for (var i = 0; i < list.Count() - 1; i++)
        {
            for (var k = i + 1; k < list.Count(); k++)
            {
                if (
                    (list[i][0] <= list[k][0] && list[i][1] >= list[k][0])
                    || list[i][0] >= list[k][0] && list[i][0] <= list[k][1]
                )
                {
                    var itemsToMerge = new List<int>();
                    itemsToMerge.Add(list[i][0]);
                    itemsToMerge.Add(list[i][1]);
                    itemsToMerge.Add(list[k][0]);
                    itemsToMerge.Add(list[k][1]);

                    list.RemoveAt(k);
                    list.RemoveAt(i);

                    list.Add(new int[] { itemsToMerge.Min(), itemsToMerge.Max() });
                    return Compress(list);
                }
            }
        }

        return list;
    }
}

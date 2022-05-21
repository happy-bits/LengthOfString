namespace LengthOfString.Core;

public class MathUtils
{
    public static List<List<int>> Flow(List<int> widths, int maxWidth)
    {
        if (widths.Any(w => w > maxWidth))
        {
            throw new ArgumentException();
        }

        var ret = new List<List<int>>() { new List<int>() };

        var x = 0;

        for (int i = 0; i < widths.Count; i++)
        {
            var width = widths[i];

            if (x + width > maxWidth)
            {
                ret.Add(new List<int> { width });
                x = width;
                continue;
            }
            ret.Last().Add(width);
            x += width;
        }

        return ret;
    }
}

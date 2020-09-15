// Ascending (00:00 -> 00:45 -> 01:23 -> 05:43 -> 05:49)
// Descending (02:30 -> 02:00 -> 01:20 -> 00:40 -> 00:30)
// Descending Hour Ascending Minutes (02:00 -> 02:30 -> 02:45 -> 01:00 -> 01:20 -> 01:30)

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

public static class ClockComparer
{
    public static Ascending Ascending => new Ascending();
    public static Descending Descending => new Descending();
    public static DescendingHourAscendingMinutes DescendingHourAscendingMinutes =>
        new DescendingHourAscendingMinutes();


}

public class Ascending : IComparer<Clock>
{
    public int Compare(Clock x, Clock y)
    {
        if (x is null || y is null)
            throw new ArgumentException("Cannot work with null.");

        if (x.ToMinutes() > y.ToMinutes())
            return 1;

        if (x.ToMinutes() < y.ToMinutes())
            return -1;
        return 0;
    }

};


public class Descending : IComparer<Clock>
{
    public int Compare(Clock x, Clock y)
    {
        if (x is null || y is null)
            throw new ArgumentException("Cannot work with null.");

        if (x.ToMinutes() < y.ToMinutes())
            return 1;

        if (x.ToMinutes() > y.ToMinutes())
            return -1;
        return 0;
    }


}

public class DescendingHourAscendingMinutes : IComparer<Clock>
{
    public int Compare(Clock x, Clock y)
    {
        if (x is null || y is null)
            throw new ArgumentException("Cannot work with null.");

        if (x.Hours > y.Hours)
            return -1;

        if (x.Hours < y.Hours)
            return 1;

        if (x.Minutes < y.Minutes)
            return -1;

        if (x.Minutes > y.Minutes)
            return -1;
        return 0;
    }

}



/*public class Ascending : IComparer<Clock>, IComparer
{

    public int Compare([AllowNull] Clock clock, [AllowNull] Clock other)
    {
        if (this Clock > )
    }

    public int Compare(object clock, object other)
    {
        if (!(clock is Clock) && !(other is Clock))
            throw new ArgumentException();

        return (Compare(clock, other));
    }
}*/
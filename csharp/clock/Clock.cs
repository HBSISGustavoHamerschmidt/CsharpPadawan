using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;

public partial class Clock
{
    private int Hours { get;}
    private int Minutes { get;}

    public Clock(int minutes):this(0, minutes)
    {
    }

    public Clock(int hours, int minutes)
    {
        Hours = (hours + (minutes / 60)) % 24;
        Minutes = minutes % 60;

        if (Minutes < 0)
        {
            Hours--;
            Minutes = 60 + Minutes;
        }
        if (Hours < 0)
            Hours = 24 + Hours;
    }

    public int ToMinutes() => Hours * 60 + Minutes;
    public Clock Add(int minutesToAdd) => new Clock(Hours, Minutes + minutesToAdd);
    public Clock Subtract(int minutesToSubtract) => new Clock(Hours, Minutes - minutesToSubtract);

    public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is Clock clock && Equals(clock);
    public override int GetHashCode() => HashCode.Combine(Hours, Minutes);
    public override string ToString() => $"{Hours:0#}:{Minutes:0#}";
}

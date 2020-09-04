using System;

public class Clock : IEquatable<Clock>
{
    private int Hours { get; }
    private int Minutes { get; }
    public Clock(int hours, int minutes)
    {
        this.Hours = (hours  + (minutes / 60)) % 24;
        this.Minutes = minutes % 60;

        if (Hours < 0)
            Hours = 24 - Hours;

        if (Minutes < 0)
            Minutes = 60 + Minutes;
    }
    public Clock Add(int minutesToAdd) => new Clock(Hours, Minutes + minutesToAdd);
    public Clock Subtract(int minutesToSubtract) => new Clock(Hours, Minutes + minutesToSubtract);

    public override bool Equals(object obj)
    {

    }

    public override string ToString() => $"{Hours:0#}:{Minutes:0#}";
}

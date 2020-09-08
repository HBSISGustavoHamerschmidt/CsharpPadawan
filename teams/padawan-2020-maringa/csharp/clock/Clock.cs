using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;

public class Clock : IEquatable<Clock>
{
    private int Hours { get; set; }
    private int Minutes { get; set; }

    public Clock(int minutes)
    {
        Hours = Minutes / 60;
        Minutes = minutes % 60;
        Verification();
    }

    public Clock(int hours, int minutes)
    {
        Hours = (hours + (minutes / 60)) % 24;
        Minutes = minutes % 60;
        Verification();
    }

    private void Verification()
    {
        if (Minutes < 0)
        {
            Hours--;
            Minutes = 60 + Minutes;
        }
        if (Hours < 0)
            Hours = 24 + Hours;
    }

    public Clock Add(int minutesToAdd) => new Clock(Hours, Minutes + minutesToAdd);
    public Clock Subtract(int minutesToSubtract) => new Clock(Hours, Minutes - minutesToSubtract);

     public bool Equals([AllowNull] Clock obj) => 
         !(obj is null) // Checks if obj is null, if it is return false
         && (ReferenceEquals(this, obj) || Hours == obj.Hours && Minutes == obj.Minutes);

     public override bool Equals(object obj)
     {

     }
     

     public override string ToString() => $"{Hours:0#}:{Minutes:0#}";
}

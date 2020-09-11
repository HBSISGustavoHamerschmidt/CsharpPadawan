using System;
using System.Diagnostics.CodeAnalysis;

public partial class Clock : IEquatable<Clock>
{
    public bool Equals([AllowNull] Clock other) =>
        !(other is null) // Checks if other is null, if it is return false
        && (ReferenceEquals(this, other) || Hours == other.Hours && Minutes == other.Minutes);
}

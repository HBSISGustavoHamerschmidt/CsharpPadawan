using System;
using System.Linq;

public static class Hamming
{
    public static int Distance(string firstStrand, string secondStrand) =>
        firstStrand.Length != secondStrand.Length
            ? throw new ArgumentException()
            : firstStrand.Where((t, i) => t != secondStrand[i]).Count();
}
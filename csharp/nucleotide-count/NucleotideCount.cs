using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

public static class NucleotideCount
{

    private readonly static IDictionary<char, int> list = new Dictionary<char, int>();

    public static IDictionary<char, int> Count(string sequence)
    {
        list['A'] = 0;
        list['C'] = 0;
        list['G'] = 0;
        list['T'] = 0;

        foreach (var t in sequence)
        {
            if (!list.ContainsKey(t))
                throw new ArgumentException();
            list[t]++;
        }
        return list;
    }
}
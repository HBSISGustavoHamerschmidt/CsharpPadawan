using System;
using System.Collections.Generic;
using Xunit.Abstractions;

public static class Strain
{
    public static IEnumerable<TInput> Keep<TInput>(this IEnumerable<TInput> collection, Func<TInput, bool> predicate)
    {
        var list = new List<TInput>();
        foreach (TInput item in collection)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
        
    }

    public static IEnumerable<TInput> Discard<TInput>(this IEnumerable<TInput> collection, Func<TInput, bool> predicate)
    {
        var list = new List<TInput>();
        foreach (TInput item in collection)
        {
            if (!predicate(item))
            {
                yield return item;
            }
        }
    }
}
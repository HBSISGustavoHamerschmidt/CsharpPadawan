using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class ScrabbleScore
{
    public static int Score(string input)
    {
        var rgx = new Regex("(?<valemUm>A|E|I|O|U|L|N|R|S|T)");
        var valeUm = input.ToUpper()
            .Select(t => rgx.Match(t.ToString()).Groups["valemUm"])
            .Count(match => match.Success);
    }
}
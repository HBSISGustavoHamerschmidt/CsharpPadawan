using System;
using System.Text.RegularExpressions;

public static class ScrabbleScore
{
    public static int Score(string input)
    {
        int score = 0;
        input = input.ToUpper();

        score += (Regex.Matches(input, "{|A|E|I|O|U|L|N|R|S|T|}").Count) * 1;
        score += (Regex.Matches(input, "{|D|G|}").Count) * 2;
        score += (Regex.Matches(input, "{|B|C|M|P|}").Count) * 3;
        score += (Regex.Matches(input, "{|F|H|V|W|Y|}").Count) * 4;
        score += (Regex.Matches(input, "{|K|}").Count) * 5;
        score += (Regex.Matches(input, "{|J|X|}").Count) * 8;
        score += (Regex.Matches(input, "{|Q|Z|}").Count) * 10;

        return score;
    }
}
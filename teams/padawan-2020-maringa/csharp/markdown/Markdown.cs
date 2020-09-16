using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class Markdown
{
    // Used string interpolation to reduce the size of the string
    private static string Wrap(string text, string tag) => $"<{tag}>{text}</{tag}>";

    // Removed "IsTag" Method that was not being used
    // Used string interpolation and inserted it directly to the "Method" Replace to make the code easier on the eyes

    private static string Parse(string markdown, string delimiter, string tag) =>
        Regex.Replace(markdown, $"{delimiter}(.+){delimiter}", $"<{tag}>$1</{tag}>");

    private static string ParseText(string markdown, bool isSpecialCharacter)
    {
        // Inserted the previously existing Methods "Parse_(ParseItalic) and Parse__(ParseStrong) directly into
        // parsedText, where they were being used.
        var parsedText = Parse(Parse(markdown, "__", "strong"), "_", "em");
        // To Ternary Operator
        return isSpecialCharacter ? parsedText : Wrap(parsedText, "p");
    }

    private static string ParseHeader(string markdown, bool list, out bool inListAfter)
    {
        // Changed for loop to While, eliminating the use of "Break"
        var count = 0;

        while (markdown[count] == '#')
            count++;

        if (count == 0)
        {
            inListAfter = list;
            return null;
        }
        // Simplified statement and removed double attribution of false in inListAfter
        var headerHtml = Wrap(markdown.Substring(count + 1), $"h{count}");
        inListAfter = false;
        // Finally, simplified return operation using Ternary Operator
        return list ? $"</ul>{headerHtml}" : headerHtml;
    }
    private static string ParseLineItem(string markdown, bool list, out bool inListAfter)
    {
        if (markdown.StartsWith("*"))
        {
            var innerHtml = Wrap(ParseText(markdown.Substring(2), true), "li");
            // Simplified statement, attributed inListAfter to true always and used Ternary Operator to define
            // the return instead of If.
            inListAfter = true;
            return list ? innerHtml : $"<ul>{innerHtml}";
        }

        inListAfter = list;
        return null;
    }

    private static string ParseParagraph(string markdown, bool list, out bool inListAfter)
    {
        // Simplified statement, attributed inListAfter to false always and used Ternary Operator to define
        // the return instead of If.
        inListAfter = false;
        return list ? $"</ul>{ParseText(markdown, false)}" : ParseText(markdown, false);
    }
    // Changed all the Ifs for the operator "??" and made it so that the command fits in only one line
    private static string ParseLine(string markdown, bool list, out bool inListAfter) =>
        ParseHeader(markdown, list, out inListAfter) ??
        ParseLineItem(markdown, list, out inListAfter) ??
        ParseParagraph(markdown, list, out inListAfter) ??
        throw new ArgumentException("Invalid markdown");

    public static string Parse(string markdown)
    {
        // Changed For to Foreach to increase readability and changed to StringBuilder

        var lines = markdown.Split(Environment.NewLine);
        var result = new StringBuilder();
        var list = false;

        foreach (var v in lines)
        {
            var lineResult = ParseLine(v, list, out list);
            result.Append(lineResult);
        }
        return list ? $"{result}</ul>" : result.ToString();
        // result = lines.Select(t => ParseLine(t, list, out list))
        //   .Aggregate(result, (current, lineResult) => $"{current}{lineResult}");
    }
}
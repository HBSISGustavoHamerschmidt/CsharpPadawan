using System;
using System.Numerics;
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

        var headerTag = "h" + count;
        var headerHtml = Wrap(markdown.Substring(count + 1), headerTag);

        if (list)
        {
            inListAfter = false;
            return "</ul>" + headerHtml;
        }

        inListAfter = false;
        return headerHtml;

    }

    private static string ParseLineItem(string markdown, bool list, out bool inListAfter)
    {
        if (markdown.StartsWith("*"))
        {
            var innerHtml = Wrap(ParseText(markdown.Substring(2), true), "li");

            if (list)
            {
                inListAfter = true;
                return innerHtml;
            }

            inListAfter = true;
            return "<ul>" + innerHtml;

        }

        inListAfter = list;
        return null;
    }

    private static string ParseParagraph(string markdown, bool list, out bool inListAfter)
    {
        if (!list)
        {
            inListAfter = false;
            return ParseText(markdown, list);
        }
        else
        {
            inListAfter = false;
            return "</ul>" + ParseText(markdown, false);
        }
    }
    // Changed all the Ifs for the operator "??" and made it so that the command fits in only one line
    private static string ParseLine(string markdown, bool list, out bool inListAfter) =>
        ParseHeader(markdown, list, out inListAfter) ??
        ParseLineItem(markdown, list, out inListAfter) ??
        ParseParagraph(markdown, list, out inListAfter) ??
        throw new ArgumentException("Invalid markdown");

    public static string Parse(string markdown)
    {
        var lines = markdown.Split('\n');
        var result = string.Empty;
        var list = false;



        for (int i = 0; i < lines.Length; i++)
        {
            var lineResult = ParseLine(lines[i], list, out list);
            result += lineResult;
        }

        return list ? result + "</ul>" : result;
    }
}
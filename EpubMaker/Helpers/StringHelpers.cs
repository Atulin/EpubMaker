using System.Text;

namespace EpubMaker.Helpers;

public static class StringHelpers
{
    public static string Friendlify(this string source)
    {
        var sb = new StringBuilder();
        foreach (var ch in source.AsSpan())
        {
            if (char.IsLetterOrDigit(ch))
            {
                sb.Append(ch);
            }
            else if (ch == ' ')
            {
                sb.Append('-');
            }
        }

        return sb.ToString();
    }

    public static string Minify(this string source)
    {
        var sb = new StringBuilder();
        foreach (var line in source.Split('\n', '\r', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            sb.Append(line);
        }

        return sb.ToString();
    }
}
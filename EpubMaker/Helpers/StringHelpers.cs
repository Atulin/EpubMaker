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
}
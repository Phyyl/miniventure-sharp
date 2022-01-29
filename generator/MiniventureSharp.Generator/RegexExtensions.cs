using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniventureSharp.Generator
{
    static class RegexExtensions
    {
        internal static string RegexReplace(this string str, string pattern, string replace, bool ignoreWhitespace = false, bool multiline = false)
        {
            RegexOptions options = GetOptions(ignoreWhitespace, multiline);

            return Regex.Replace(str, pattern, replace, options);
        }

        internal static string[] RegexFind(this string str, string pattern, int group = -1, bool ignoreWhitespace = false, bool multiline = false)
        {
            RegexOptions options = GetOptions(ignoreWhitespace, multiline);

            return Regex.Matches(str, pattern, options).Select(m =>
            {
                int g = group == -1 ? m.Groups.Count - 1 : group;
                return m.Groups.Count > group ? m.Groups[g].Value : m.Value;
            }).ToArray();
        }

        private static RegexOptions GetOptions(bool ignoreWhitespace, bool multiline)
        {
            return (ignoreWhitespace ? RegexOptions.IgnorePatternWhitespace : RegexOptions.None) |
                (multiline ? RegexOptions.Multiline : RegexOptions.None);
        }

        internal static string? RegexFindFirst(this string str, string pattern, int group, bool ignoreWhitespace = false, bool multiline = false)
        {
            return RegexFind(str, pattern, group, ignoreWhitespace, multiline).FirstOrDefault();
        }

        internal static string RegexRemove(this string str, string pattern, bool removeNewLine = true, bool ignoreWhitespace = false, bool multiline = false)
        {
            if (removeNewLine)
            {
                pattern += @"\r?\n?";
            }

            return RegexReplace(str, pattern, "", ignoreWhitespace, multiline);
        }
    }
}

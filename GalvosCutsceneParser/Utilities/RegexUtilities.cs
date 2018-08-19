using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GalvosCutsceneParser
{
    public static class RegexUtilities
    {
        public static string WhitespaceCleanupXML(this string original)
        {
            // Trim whitespace
            Regex whitespace = new Regex(@"([\s]{2,}|[\r\n\t])");
            string output = whitespace.Replace(original, "");

            // Trim trailing whitespace
            return Regex.Replace(output, @"\s+>", ">");
        }

        public static string ConvertORKToValidXML(this string original)
        {
            // Replace the first weird tag name :: <0
            var replaced = Regex.Replace(original, @"(<)(\d)", "$1" + Parser.XML_PREFIX + "$2");

            // Replace the closing weird tag name
            replaced = Regex.Replace(replaced, @"(\/)(\d)(>)", "$1" + Parser.XML_PREFIX + "$2$3");

            // Replace bad attribute names
            replaced = Regex.Replace(replaced, 
                @"(<\s*\w+)\s+(\d+)\s+(\d+)", 
                "$1 " + Parser.XML_PREFIX + "x=\"$2\" " + Parser.XML_PREFIX + "y=\"$3\"");

            return replaced;
        }

        public static string ConvertValidXMLToORK(this string original)
        {
            // Replace the opening tag
            var replaced = Regex.Replace(original, @"<" + Parser.XML_PREFIX + @"(\d)", "<$1");

            // Replace the closing tag
            replaced = Regex.Replace(replaced, Parser.XML_PREFIX + @"(\d)>", "$1>");

            // Replace the attribute names (just X and Y for now)
            replaced = Regex.Replace(replaced, Parser.XML_PREFIX + @"[xy]=""(\d+)""", "$1");

            return replaced;
        }

        public static string PullOutTextInsideQuotes(ref string original)
        {
            string pattern = "\"([^\"]*)\"";

            var match = Regex.Match(original, pattern);
            
            if (match.Groups[1].Success)
            {
                original = Regex.Replace(original, pattern, "");
                return match.Groups[1].Value;
            }

            return null;
        }

        public static int GetDigitsFromString(string original)
        {
            string pattern = @"(\d+)";

            var match = Regex.Match(original, pattern);

            if (match.Groups[1].Success)
            {
                return Convert.ToInt32(match.Groups[1].Value);
            }

            return 0;
        }

        public static string GetTextBetween(string input, string from, string to)
        {
            int startIndex = input.IndexOf(from) + from.Length;
            int endIndex = input.IndexOf(to);
            return input.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}

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

        public static string SanitizeORKXml(this string original)
        {
            // Replace the first weird tag name :: <0
            var replaced = Regex.Replace(original, @"(<)(\d)", "$1" + Parser.XML_PREFIX + "$2");

            // Replace the closing weird tag name
            replaced = Regex.Replace(replaced, @"(\/)(\d)(>)", "$1" + Parser.XML_PREFIX + "$2$3");

            // Replace bad attribute names
            replaced = Regex.Replace(replaced, @"(<\w+) (\d+)\s+(\d+)", "$1 x=\"$2\" y=\"$3\"");

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
    }
}

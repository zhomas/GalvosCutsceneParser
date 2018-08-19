using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

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
            var output = Regex.Replace(original, @"(<)(\d)", "$1" + Parser.XML_DELIMITER + "$2");

            // Replace the closing weird tag name
            output = Regex.Replace(output, @"(\/)(\d)(>)", "$1" + Parser.XML_DELIMITER + "$2$3");

            
            // Replace bad attribute names
            Regex loneDigitsFinder = new Regex(@" (-*\d+)");
            string[] replacements = new string[] { "a", "b", "c", "d" };

            int i = 0;
            while(loneDigitsFinder.IsMatch(output))
            {
                if (i > replacements.Length) break;
                output = loneDigitsFinder.Replace(
                    output, 
                    " " + Parser.XML_DELIMITER + replacements[i] + "=\"$1\"", 
                1);
                i++;
            }

            return output;
        }

        public static string ConvertValidXMLToORK(this string original)
        {
            // Replace the opening tag
            var output = Regex.Replace(original, @"<" + Parser.XML_DELIMITER + @"(\d)", "<$1");

            // Replace the closing tag
            output = Regex.Replace(output, Parser.XML_DELIMITER + @"(\d)>", "$1>");

            // Replace bad attribute names
            output = Regex.Replace(output, Parser.XML_DELIMITER + @"\w=""(-*\d+)""", "$1");

            return output;
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

        public static Vector3 GetVector3FromString(string original)
        {
            string pattern = @"(\d+),\s*(\d+)";

            var match = Regex.Match(original, pattern);

            if (match.Groups.Count == 3 && match.Groups[1].Success && match.Groups[2].Success)
            {
                return new Vector3(
                    float.Parse(match.Groups[1].Value),
                    0,
                    float.Parse(match.Groups[2].Value)
                );
            }

            throw new Exception();
        }

        public static string GetTextBetween(string input, string from, string to)
        {
            int startIndex = input.IndexOf(from) + from.Length;
            int endIndex = input.IndexOf(to);
            return input.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}

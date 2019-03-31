using System;
using System.Collections.Generic;
using System.IO;
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
            string output = original;

            // Trim whitespace
            output = Regex.Replace(output, @"([\s]{2,}|[\r\n\t])", "");

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
            string pattern = @"(-*\d+),\s*(-*\d+),*\s*(-*\d)*";

            var match = Regex.Match(original, pattern);


            if (match.Groups[1].Success && match.Groups[1].Value != "" &&
                match.Groups[2].Success && match.Groups[2].Value != "" &&
                match.Groups[3].Success && match.Groups[3].Value != "")
            {
                return new Vector3(
                    float.Parse(match.Groups[1].Value),
                    float.Parse(match.Groups[2].Value),
                    float.Parse(match.Groups[3].Value)
                );
            }

            if (match.Groups[1].Success && match.Groups[1].Value != "" &&
                match.Groups[2].Success && match.Groups[2].Value != "")
            {
                return new Vector3(
                    float.Parse(match.Groups[1].Value),
                    0,
                    float.Parse(match.Groups[2].Value)
                );
            }


            return Vector3.zero;
        }

        public static string GetTextBetween(string input, string from, string to)
        {
            int startIndex = input.IndexOf(from) + from.Length;
            int endIndex = input.IndexOf(to);
            return input.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}

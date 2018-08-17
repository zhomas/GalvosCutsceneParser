using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace GalvosCutsceneParser
{
    public static class Utilities
    {
        public static string FormattedXML(this string original)
        {
            Regex whitespace = new Regex(@"([\s]{2,}|[\r\n\t])");
            string output = whitespace.Replace(original, "");
            return Regex.Replace(output, @"\s+>", ">");
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

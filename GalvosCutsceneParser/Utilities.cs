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
    }
}

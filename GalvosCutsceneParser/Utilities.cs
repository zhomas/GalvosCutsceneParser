using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GalvosCutsceneParser
{
    public static class Utilities
    {
        public static string FormattedXML(this string original)
        {
            Regex rgx = new Regex("[\r\t\n]");
            return rgx.Replace(original, "");
        }
    }
}

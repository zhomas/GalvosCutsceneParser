using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GalvosCutsceneParser
{
    public class StepFlags
    {
        private List<string> flags;

        public StepFlags(string inputLine)
        {
            this.flags = new List<string>();

            string pattern = @" --(\w+)";

            var matches = Regex.Matches(inputLine, pattern);

            foreach (Match match in matches)
            {
                if (match.Groups[1].Success)
                {
                    this.flags.Add(match.Groups[1].Value);
                }
            }
        }

        public bool Contains(string flag)
        {
            return this.flags.Any(f => f == flag);
        }
    }
}

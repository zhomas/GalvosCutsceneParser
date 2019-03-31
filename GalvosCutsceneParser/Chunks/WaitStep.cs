using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public class WaitStep : BaseStep
    {
        public int milliseconds { get; private set; }

        public WaitStep(int milliseconds)
        {
            this.milliseconds = milliseconds;
        }

        public float Seconds
        {
            get
            {
                return ((float)this.milliseconds / 1000);
            }
        }

        public static int ParseMillisecondsFromInputLine(string inputLine)
        {
            if (inputLine.Contains("wait"))
            {
                return RegexUtilities.GetDigitsFromString(inputLine);
            }

            return 0;
        }
    }
}

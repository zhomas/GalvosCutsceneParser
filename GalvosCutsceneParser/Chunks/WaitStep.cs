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

        public static bool IsMatch(StepInput input)
        {
            return input.chunks.Count > 1 && input.chunks[0] == "wait";
        }
        
        public WaitStep(StepInput input)
        {
            this.milliseconds = RegexUtilities.GetDigitsFromString(input.chunks[1]);
        }
    }
}

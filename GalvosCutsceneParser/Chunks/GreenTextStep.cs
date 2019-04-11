using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class GreenTextStep : BaseStep
    {
        public string Message { get; private set; }

        public static bool IsMatch(StepInput input)
        {
            return input.chunks[0] == ">>";

        }

        public GreenTextStep(StepInput input)
        {
            string s = String.Join(" ", input.chunks.ToArray());
            string withoutquotes = RegexUtilities.PullOutTextInsideQuotes(ref s);
            this.Message = withoutquotes;
        }
    }
}

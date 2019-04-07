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

        public static bool IsMatch(List<string> chunks)
        {
            return chunks[0] == ">>";

        }

        public GreenTextStep(List<string> chunks, IEntitySupplier entitySupplier)
        {
            string s = String.Join(" ", chunks.ToArray());
            string withoutquotes = RegexUtilities.PullOutTextInsideQuotes(ref s);
            this.Message = withoutquotes;
        }
    }
}

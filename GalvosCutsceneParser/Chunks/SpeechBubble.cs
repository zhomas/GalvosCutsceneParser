using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using GalvosCutsceneParser;
using GalvosCutsceneParser.Entities;

namespace GalvosCutsceneParser
{
    public class SpeechBubble : BaseStep
    {
        public string Message { get; private set; }

        public static bool IsMatch(StepInput input)
        {
            return input.chunks.Count > 1 && input.supplier.IsEntity(input.chunks[0]) && input.chunks[1] == "say";
        }

        public SpeechBubble(StepInput input)
        {
            string message = RegexUtilities.PullOutTextInsideQuotes(ref input.line);
            this.entity = input.supplier.GetEntityByAlias(input.chunks[0]);
            this.Message = message;
        }
    }
}

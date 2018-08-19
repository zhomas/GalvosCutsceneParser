using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class Service
    {
        private string gpl;
        private string parentXML;

        public Service(string gpl, string parentEventXML)
        {
            this.gpl = gpl;
            this.parentXML = parentEventXML;
        }

        public string GetEventXML()
        {
            var parser = new Parser();
            var aliasBuilder = new AliasBuilder(this.gpl);
            var stepbuilder = new StepBuilder(aliasBuilder);

            var steps = stepbuilder.GetStepsFromInput(this.gpl);

            return parser.LoadEventXML(this.parentXML)
              .ReplaceXMLStepsWithGPLSteps(steps)
              .GetXML();
        }

    }
}

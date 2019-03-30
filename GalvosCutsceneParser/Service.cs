using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class Service
    {
        private string gpl;
        private Func<int, GameObject> goGetter;


        public Service(string gpl, Func<int, GameObject> goGetter)
        {
            this.gpl = gpl;
            this.goGetter = goGetter;
        }

        public string GetEventXML(string parentXML)
        {
            var parser = new Parser();
            var aliasBuilder = new AliasBuilder(this.gpl, this.goGetter);
            var stepbuilder = new StepBuilder(aliasBuilder);

            var steps = stepbuilder.GetStepsFromInput(this.gpl);

            return parser.LoadEventXML(parentXML)
              .ReplaceXMLStepsWithGPLSteps(steps)
              .GetXML();
        }

        public List<BaseStep> GetSteps()
        {
            var aliasBuilder = new AliasBuilder(this.gpl, this.goGetter);
            var stepbuilder = new StepBuilder(aliasBuilder);
            return stepbuilder.GetStepsFromInput(this.gpl);
        }

    }
}

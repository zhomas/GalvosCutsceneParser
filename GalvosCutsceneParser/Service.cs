using GalvosCutsceneParser.Entities;
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

        public List<BaseStep> GetSteps()
        {
            var aliasBuilder = new AliasBuilder(this.gpl, this.goGetter);
            var stepbuilder = new StepBuilder(aliasBuilder);
            return stepbuilder.GetStepsFromInput(this.gpl);
        }

    }
}

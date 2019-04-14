using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class ActivateStep : BaseStep
    {
        public static bool IsMatch(StepInput input)
        {
            return input.chunks.Count > 1 && input.chunks[0] == "activate";
        }

        public ActivateStep(StepInput input)
        {
            this.entity = input.supplier.GetEntityByAlias(input.chunks[1]);
        }
    }
}

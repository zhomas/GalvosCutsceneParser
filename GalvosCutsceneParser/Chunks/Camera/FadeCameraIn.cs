using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class FadeCameraIn : BaseStep
    {
        public bool Follow { get; private set; } = true;

        public static bool IsMatch(StepInput input)
        {
            return input.chunks[0] == "cam" && input.args.ContainsKey("ease");
        }

        public FadeCameraIn(StepInput input)
        {
            this.entity = input.supplier.GetEntityByAlias(input.chunks[2]);
            this.Follow = !input.args.ContainsKey("nofollow");
        }
    }
}

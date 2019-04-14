using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class FadeScreenStep : BaseStep
    {
        public float Alpha { get; set; }
        public SpeedType SpeedType { get; }

        public static bool IsMatch(StepInput input)
        {
            return input.chunks[0] == "fade";
        }

        public FadeScreenStep(StepInput input)
        {
            this.Alpha = float.Parse(input.chunks[2]);
            this.SpeedType = StepUtilities.SpeedTypeFromString(input.chunks[1]);
        }
    }
}

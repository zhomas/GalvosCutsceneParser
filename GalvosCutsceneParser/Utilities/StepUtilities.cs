using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public static class StepUtilities
    {
        public static SpeedType SpeedTypeFromString(string str)
        {
            if (str == "=>")
            {
                return SpeedType.Slow;
            }

            if (str == "=>>")
            {
                return SpeedType.Medium;
            }

            if (str == "=>>>")
            {
                return SpeedType.Fast;
            }

            throw new MisformedStepException(str);
        }
    }

    public enum SpeedType
    {
        Slow,
        Medium,
        Fast
    }
}

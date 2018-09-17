using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public enum StepAction
    {
        Speech,
        Wait,
        Move,
        Camera,
        Turn,
        TurnTo,
        Pose,
        OpenDoor,
        Undefined
    }
}

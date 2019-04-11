using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class MoveAiInDirectionStep : BaseStep
    {
        public MoveSpeedType SpeedType { get; protected set; }
        public Vector3 Direction { get; private set; }

        public static bool IsMatch(StepInput input)
        {
            return input.chunks.Count > 2 && input.chunks[1].StartsWith("=>") &&
                   input.supplier.IsEntity(input.chunks[0]) &&
                   RegexUtilities.GetVector3FromString(input.line) != Vector3.zero;
        }

        public MoveAiInDirectionStep(StepInput input)
        {
            this.Direction = RegexUtilities.GetVector3FromString(input.line);
            this.SpeedType = BaseMoveStep.SpeedTypeFromString(input.chunks[1]);
            this.entity = input.supplier.GetEntityByAlias(input.chunks[0]);
        }
    }
}

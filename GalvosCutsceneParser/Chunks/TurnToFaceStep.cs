using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class TurnToFaceStep : BaseStep
    {
        public IEntity FaceTarget { get; private set; }
        
        public static bool IsMatch(StepInput input)
        {
            return input.chunks.Count == 3 && input.chunks[1] == "=:";
        }

        public TurnToFaceStep(StepInput input)
        {
            this.entity = input.supplier.GetEntityByAlias(input.chunks[0]);
            this.FaceTarget = input.supplier.GetEntityByAlias(input.chunks[2]);
        }

    }
}

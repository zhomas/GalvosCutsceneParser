using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class MoveToPositionStep : BaseMoveStep
    {
        public Vector3 Position
        {
            get { return this.targetObject.Target.transform.position; }
        }


        private IEntity targetObject;

        public static MoveToPositionStep CreateFromInputString(string inputLine, IEntitySupplier supplier)
        {
            string[] chunks = inputLine.Split(' ');

            var mover = supplier.GetEntityByAlias(chunks.First());
            var target = supplier.GetEntityByAlias(chunks.Last());
            var speedType = StepUtilities.SpeedTypeFromString(chunks[1]);

            return new MoveToPositionStep(mover, target, speedType);
        }

        public MoveToPositionStep(IEntity mover, IEntity target, SpeedType speedType) : base()
        {
            this.entity = mover;
            this.targetObject = target;
            this.SpeedType = speedType;
        }
    }
}

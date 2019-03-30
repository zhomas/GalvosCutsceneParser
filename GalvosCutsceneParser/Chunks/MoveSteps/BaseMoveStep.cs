using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public abstract class BaseMoveStep : BaseStep
    {
        public MoveSpeedType SpeedType { get; protected set; }

        protected XElement GetMovingObjectDefinition (int aID)
        {
            return new XElement("movingObject",
                    new XAttribute("type", 0),
                    new XAttribute("aID", aID),
                    new XAttribute("wID", 0),
                    new XAttribute("pID", 0),
                    new XAttribute("pID2", -1),
                new XElement("_string", this.GetEmptyCData("childName")),
                new XElement("objectKey",
                        new XAttribute("type", 0),
                    new XElement("_string", this.GetEmptyCData("value")))
            );
        }

        protected XElement GetMoveSpeedDefinition(float speed)
        {
            return new XElement("moveSpeed",
                    new XAttribute("type", 3),
                new XElement("_float",
                    new XAttribute("speed", speed))
            );
        }

        protected float MoveSpeed
        {
            get
            {
                switch (this.SpeedType)
                {
                    case MoveSpeedType.Walk:
                        return 32f;
                    case MoveSpeedType.Run:
                        return 64f;
                    case MoveSpeedType.Sprint:
                    default:
                        return 111f;
                }
            }
        }

        public enum MoveSpeedType
        {
            Walk,
            Run,
            Sprint
        }

        public static MoveSpeedType SpeedTypeFromString(string str)
        {
            if (str == "=>")
            {
                return MoveSpeedType.Walk;
            }

            if (str == "=>>")
            {
                return MoveSpeedType.Run;
            }

            if (str == "=>>>")
            {
                return MoveSpeedType.Sprint;
            }

            throw new MisformedStepException(str);
        }
    }
}

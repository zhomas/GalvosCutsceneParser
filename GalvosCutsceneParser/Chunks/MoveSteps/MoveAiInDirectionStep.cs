using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class MoveAiInDirectionStep : BaseMoveStep
    {
        public Vector3 Direction { get; private set; }

        public MoveAiInDirectionStep(IEntity entity, Vector3 direction, MoveSpeedType speedType) : base()
        {
            this.entity = entity;
            this.Direction = direction;
            this.SpeedType = speedType;
        }

        public static MoveAiInDirectionStep CreateFromInputString(IEntity entity, string inputLine)
        {
            Vector3 dir = GetDirectionFromInputString(inputLine);

            if (dir != Vector3.zero)
            {
                string[] chunks = inputLine.Split(' ');
                MoveSpeedType moveSpeed = SpeedTypeFromString(chunks[1]);
                return new MoveAiInDirectionStep(entity, dir, moveSpeed);
            }

            return null;
        }

        public static Vector3 GetDirectionFromInputString(string inputLine)
        {
            Vector3 dir = RegexUtilities.GetVector3FromString(inputLine);

            if (dir != Vector3.zero)
            {
                return dir;
            }

            return Vector3.zero;
        }
    }
}

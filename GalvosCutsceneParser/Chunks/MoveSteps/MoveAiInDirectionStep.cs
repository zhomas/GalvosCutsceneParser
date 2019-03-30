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
        private Vector3 direction;

        public MoveAiInDirectionStep(IEntity entity, Vector3 direction, MoveSpeedType speedType) : base()
        {
            this.entity = entity;
            this.direction = direction;
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

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("WaitUntilComplete", true.ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            };
        }

        protected override List<XElement> GetFloatArrayElements()
        {
            return new List<XElement>()
            {
                new XElement("Direction",
                    new XAttribute(Parser.XML_DELIMITER + "a", this.direction.x),
                    new XAttribute(Parser.XML_DELIMITER + "b", this.direction.y),
                    new XAttribute(Parser.XML_DELIMITER + "c", this.direction.z))
            };
        }

        protected override string GetNodeType()
        {
            return "MoveAIByDirection";
        }
        
        protected override List<XElement> GetNodeSpecialElements()
        {
            return new List<XElement>()
            {
                this.GetMovingObjectDefinition(this.entity.ID),
                this.GetMoveSpeedDefinition(this.MoveSpeed)
            };
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>();
        }

    }
}

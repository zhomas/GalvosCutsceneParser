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
        private CutsceneEntity entity;
        private Vector3 direction;

        public MoveSpeedType SpeedType { get; private set; }

        public MoveAiInDirectionStep(CutsceneEntity entity, Vector3 direction, MoveSpeedType speedType)
        {
            this.entity = entity;
            this.direction = direction;
            this.SpeedType = speedType;
        }

        public static MoveAiInDirectionStep CreateFromInputString(CutsceneEntity entity, string inputLine)
        {
            Vector3 dir = GetDirectionFromInputString(inputLine);
            string[] chunks = inputLine.Split(' ');

            if (chunks[1] == "=>")
            {
                return new MoveAiInDirectionStep(entity, dir, MoveSpeedType.Walk);
            }

            if (chunks[1] == "=>>")
            {
                return new MoveAiInDirectionStep(entity, dir, MoveSpeedType.Run);
            }

            if (chunks[1] == "=>>>")
            {
                return new MoveAiInDirectionStep(entity, dir, MoveSpeedType.Sprint);
            }

            throw new MisformedStepException();
        }

        public static Vector3 GetDirectionFromInputString(string inputLine)
        {
            Vector3 dir = RegexUtilities.GetVector3FromString(inputLine);

            if (dir != Vector3.zero)
            {
                return dir;
            }

            throw new MisformedStepException();
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

        protected override List<XElement> GetStringElements()
        {
            var nodeType = new XElement("_type");
            nodeType.Add(new XCData("MoveAIByDirection"));

            return base.GetStringElements().Concat(new List<XElement>()
            {
                nodeType
            }).ToList();
        }

        protected override List<XElement> GetNodeSpecialElements()
        {
            var emptyChildName = new XElement("childName");
            emptyChildName.Add(new XCData(""));

            var emptyValue = new XElement("value");
            emptyValue.Add(new XCData(""));

            return new List<XElement>()
            {
                new XElement("movingObject",
                        new XAttribute("type", 0),
                        new XAttribute("aID", this.entity.ID),
                        new XAttribute("wID", 0),
                        new XAttribute("pID", 0),
                        new XAttribute("pID2", -1),
                    new XElement("_string", emptyChildName),
                    new XElement("objectKey",
                            new XAttribute("type", 0),
                        new XElement("_string", emptyValue))),
                new XElement("moveSpeed",
                        new XAttribute("type", 3),
                    new XElement("_float", 
                        new XAttribute("speed", this.MoveSpeed)))
            };
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>()
            {

            };
        }

        private float MoveSpeed
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
                        return 96f;
                }
            }
        }

        public enum MoveSpeedType
        {
            Walk,
            Run,
            Sprint
        }
    }
}

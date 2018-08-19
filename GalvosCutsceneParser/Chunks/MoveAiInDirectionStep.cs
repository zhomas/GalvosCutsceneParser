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
        private Vector3 direction;

        public MoveAiInDirectionStep(Vector3 direction)
        {
            this.direction = direction;
        }

        public static Vector3 GetDirectionFromInputString(string inputLine)
        {
            try
            {
                return RegexUtilities.GetVector3FromString(inputLine);
            }
            catch (Exception e)
            {
                throw new MisformedStepException();
            }
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
                        new XAttribute("aID", 0),
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
                        new XAttribute("speed", 32)))
            };
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>()
            {

            };
        }
    }
}

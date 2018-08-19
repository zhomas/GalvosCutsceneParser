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
                new XAttribute("WaitUntilComplete", true),
                new XAttribute("active", true),
                new XAttribute("overrideNodeName", false)
            };
        }

        protected override List<XElement> GetFloatArrayElements()
        {
            return new List<XElement>()
            {
                new XElement("Direction",
                    new XAttribute("x", this.direction.x),
                    new XAttribute("y", this.direction.y),
                    new XAttribute("z", this.direction.z))
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

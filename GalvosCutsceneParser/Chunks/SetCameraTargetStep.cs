﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class SetCameraTargetStep : BaseStep
    {
        public CutsceneEntity Target { get; private set; }
        public Vector3 CamRotation { get; private set; }

        public static SetCameraTargetStep GetFromInputString(CutsceneEntity target, string inputLine)
        {
            return new SetCameraTargetStep(target, RegexUtilities.GetVector3FromString(inputLine));
        }

        public SetCameraTargetStep(CutsceneEntity target, Vector3 rotation)
        {
            this.Target = target;
            this.CamRotation = rotation;
        }

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("reset", false.ToString()),
                new XAttribute("cameraRotation", (this.CamRotation != Vector3.zero).ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            };
        }

        protected override List<XElement> GetStringElements()
        {
            var nodeType = new XElement("_type");
            nodeType.Add(new XCData("CameraControlTargetStep"));

            return base.GetStringElements().Concat(new List<XElement>()
            {
                nodeType
            }).ToList();
        }

        protected override List<XElement> GetNodeSpecialElements()
        {
            var name = new XElement("childName");
            name.Add(new XCData(""));

            var value = new XElement("value");
            value.Add(new XCData(""));

            return new List<XElement>()
            {
                new XElement("onObject",
                        new XAttribute("type", 0),
                        new XAttribute("aID", this.Target.ID),
                        new XAttribute("wID", 0),
                        new XAttribute("pID", 0),
                        new XAttribute("pID2", -1),
                    new XElement("_string", name), 
                    new XElement("objectKey", 
                            new XAttribute("type", 0), 
                        new XElement("_string", 
                            value)))
            };
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>();
        }

        protected override List<XElement> GetFloatArrayElements()
        {
            var list = base.GetFloatArrayElements();

            if (this.CamRotation != Vector3.zero)
            {
                XElement rotation = new XElement("cameraRotationEuler",
                    new XAttribute(Parser.XML_DELIMITER + "a", this.CamRotation.x),
                    new XAttribute(Parser.XML_DELIMITER + "b", this.CamRotation.y),
                    new XAttribute(Parser.XML_DELIMITER + "c", this.CamRotation.z)
                );

                list.Add(rotation);
            }

            return list;
        }
    }
}

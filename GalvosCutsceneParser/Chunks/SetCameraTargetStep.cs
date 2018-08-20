using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public class SetCameraTargetStep : BaseStep
    {
        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("reset", false.ToString()),
                new XAttribute("ownControlTargetTransition", false.ToString()),
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
                        new XAttribute("aID", 1),
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
    }
}

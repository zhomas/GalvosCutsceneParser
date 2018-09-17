using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public class TurnToFaceStep : BaseStep
    {
        private CutsceneEntity rotator;
        private CutsceneEntity target;

        public static TurnToFaceStep CreateFromInputString(string inputString, IEntitySupplier supplier)
        {
            string[] chunks = inputString.Split(' ');

            var rotator = supplier.GetEntityByAlias(chunks[0]);
            var target = supplier.GetEntityByAlias(chunks[2]);

            return new TurnToFaceStep(rotator, target);
        }

        public TurnToFaceStep(CutsceneEntity rotator, CutsceneEntity target)
        {
            this.rotator = rotator;
            this.target = target;
        }

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("toObject", true.ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString()),
            };
        }

        protected override string GetNodeType()
        {
            return "GalvosTurnTo";
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>() { };
        }

        protected override List<XElement> GetNodeSpecialElements()
        {
            var emptyChildName = new XElement("childName");
            emptyChildName.Add(new XCData(""));

            var emptyValue = new XElement("value");
            emptyValue.Add(new XCData(""));

            return new List<XElement>()
            {
                new XElement("rotateObject",
                        new XAttribute("type", 0),
                        new XAttribute("aID", this.rotator.ID),
                        new XAttribute("wID", 0),
                        new XAttribute("pID", 0),
                        new XAttribute("pID2", -1),
                    new XElement("_string", emptyChildName),
                    new XElement("objectKey",
                            new XAttribute("type", 0),
                        new XElement("_string", emptyValue))),
                new XElement("targetObject",
                        new XAttribute("type", 0),
                        new XAttribute("aID", this.target.ID),
                        new XAttribute("wID", 0),
                        new XAttribute("pID", 0),
                        new XAttribute("pID2", -1),
                    new XElement("_string", emptyChildName),
                    new XElement("objectKey",
                            new XAttribute("type", 0),
                        new XElement("_string", emptyValue))),
            };
        }
    }
}

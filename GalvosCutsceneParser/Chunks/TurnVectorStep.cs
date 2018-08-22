using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class TurnVectorStep : BaseStep
    {
        public enum Direction
        {
            North = 0, South = 1, East = 2, West = 3
        }

        private CutsceneEntity entity;
        private Direction dir;

        public TurnVectorStep(CutsceneEntity entity, Direction dir)
        {
            this.dir = dir;
            this.entity = entity;
        }

        protected override List<XAttribute> GetRootNodeAttributes(int index, bool isFinal)
        {
            return new List<XAttribute>()
            {
                new XAttribute("direction", (int)this.dir)
            }.Concat(base.GetRootNodeAttributes(index, isFinal)).ToList();
        }

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            };
        }

        protected override List<XElement> GetStringElements()
        {
            var nodeType = new XElement("_type");
            nodeType.Add(new XCData("GalvosTurnVector"));

            return base.GetStringElements().Concat(new List<XElement>()
            {
                nodeType
            }).ToList();
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>();
        }

        protected override List<XElement> GetNodeSpecialElements()
        {
            var emptyChildName = new XElement("childName");
            emptyChildName.Add(new XCData(""));

            var emptyValue = new XElement("value");
            emptyValue.Add(new XCData(""));

            return new List<XElement>()
            {
                new XElement("usedObject",
                        new XAttribute("type", 0),
                        new XAttribute("aID", this.entity.ID),
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

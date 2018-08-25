using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public class MoveToPositionStep : BaseMoveStep
    {
        public MoveToPositionStep(CutsceneEntity entity)
        {
            this.entity = entity;
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

        protected override string GetNodeType()
        {
            return "SetMoveAITargetStep";
        }

        protected override List<XElement> GetNodeSpecialElements()
        {
            return new List<XElement>()
            {
                this.GetMovingObjectDefinition(this.entity.ID),
                this.GetMoveSpeedDefinition(this.MoveSpeed),
                new XElement("targetObject",
                        new XAttribute("type", 0),
                        new XAttribute("aID", 1),
                        new XAttribute("wID", 0),
                        new XAttribute("pID", 0),
                        new XAttribute("pID2", -1),
                    new XElement("_string", this.GetEmptyCData("childName")), 
                    new XElement("objectKey", 
                            new XAttribute("type", 0),
                        new XElement("_string", this.GetEmptyCData("value")))
                )
            };
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>();
        }
    }
}

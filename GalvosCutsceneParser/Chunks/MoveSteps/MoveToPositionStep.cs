using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public class MoveToPositionStep : BaseMoveStep
    {
        private CutsceneEntity targetObject;

        public static MoveToPositionStep CreateFromInputString(string inputLine, IEntitySupplier supplier)
        {
            string[] chunks = inputLine.Split(' ');

            var mover = supplier.GetEntityByAlias(chunks.First());
            var target = supplier.GetEntityByAlias(chunks.Last());
            var speedType = SpeedTypeFromString(chunks[1]);

            return new MoveToPositionStep(mover, target, speedType);
        }

        public MoveToPositionStep(CutsceneEntity mover, CutsceneEntity target, MoveSpeedType speedType)
        {
            this.entity = mover;
            this.targetObject = target;
            this.SpeedType = speedType;
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
                        new XAttribute("aID", this.targetObject.ID),
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

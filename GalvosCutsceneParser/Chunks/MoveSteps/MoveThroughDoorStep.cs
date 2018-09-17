using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public class MoveThroughDoorStep : BaseStep
    {
        private CutsceneEntity user;
        private CutsceneEntity door;
        private StepFlags flags;

        public static MoveThroughDoorStep CreateFromInputString(string inputString, IEntitySupplier supplier)
        {
            string[] chunks = inputString.Split(' ');

            if (chunks.Count() >= 3)
            {
                var user = supplier.GetEntityByAlias(chunks[0]);
                var door = supplier.GetEntityByAlias(chunks[2]);
                return new MoveThroughDoorStep(user, door, new StepFlags(inputString));
            }

            return null;
        }

        public MoveThroughDoorStep(CutsceneEntity user, CutsceneEntity door, StepFlags flags)
        {
            this.user = user;
            this.door = door;
            this.flags = flags;
        }

        protected override List<XAttribute> GetFloatAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("standInDoorway", 0),
                new XAttribute("delayAfterDoor", 0)
            };
        }

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("cameraTarget", this.flags.Contains("camtarget").ToString()),
                new XAttribute("completeOnceOnNavmesh", this.flags.Contains("asap").ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString()),
            };
        }

        protected override List<XAttribute> GetRootNodeAttributes(int index, bool isFinalStep)
        {
            return new List<XAttribute>()
            {
                new XAttribute("aID", this.user.ID),
                new XAttribute("dID", this.door.ID)
            }.Concat(base.GetRootNodeAttributes(index, isFinalStep)).ToList();
        }

        protected override string GetNodeType()
        {
            return "WalkThroughDoorStep";
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>() { };
        }
    }
}

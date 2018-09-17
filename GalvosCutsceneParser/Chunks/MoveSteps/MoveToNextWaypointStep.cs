using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public class MoveToNextWaypointStep : BaseMoveStep
    {
        public static MoveToNextWaypointStep CreateFromInputString(string inputLine, IEntitySupplier supplier)
        {
            string[] chunks = inputLine.Split(' ');

            var mover = supplier.GetEntityByAlias(chunks.First());
            var speedType = SpeedTypeFromString(chunks[1]);
            var flags = new StepFlags(inputLine);

            return new MoveToNextWaypointStep(mover, speedType, flags);
        }

        public MoveToNextWaypointStep(CutsceneEntity mover, MoveSpeedType speedType, StepFlags flags)
        {
            this.entity = mover;
            this.SpeedType = speedType;
            this.flags = flags;
        }

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("WaitForMovementComplete", (!this.flags.Contains("nowait")).ToString()),
                new XAttribute("MakeCameraTarget", false.ToString()),
                new XAttribute("Instant", false.ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            };
        }

        protected override string GetNodeType()
        {
            return "MoveToNextWaypointStep";
        }

        protected override List<XElement> GetNodeSpecialElements()
        {
            return new List<XElement>()
            {
                this.GetMovingObjectDefinition(this.entity.ID),
                this.GetMoveSpeedDefinition(this.MoveSpeed),
            };
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>();
        }
    }
}

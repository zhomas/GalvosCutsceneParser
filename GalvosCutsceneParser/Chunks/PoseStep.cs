using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using GalvosCutsceneParser.Entities;

namespace GalvosCutsceneParser
{
    public class PoseMasterStep : BaseStep
    {
        public PosemasterPose Pose { get; private set; }
        public bool RemovePose { get; private set; }
        

        public static PoseMasterStep CreateFromInputString(IEntity entity, string inputString)
        {
            PosemasterPose pose = PoseMasterStep.GetFromString(inputString);
            bool remove = inputString.Contains(" --remove");
            return new PoseMasterStep(entity, pose, remove);
        }

        public PoseMasterStep(IEntity entity, PosemasterPose pose, bool remove) : base()
        {
            this.entity = entity;
            this.Pose = pose;
            this.RemovePose = remove;
        }

        protected override List<XAttribute> GetRootNodeAttributes(int index, bool isFinalStep)
        {
            return new List<XAttribute>()
            {
                new XAttribute("aID", this.entity.ID),
                new XAttribute("pose", (int)this.Pose)
            }.Concat(base.GetRootNodeAttributes(index, isFinalStep)).ToList();
        }

        protected override List<XAttribute> GetFloatAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("Duration", 2f)
            };
        }

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("indefinite", true.ToString()),
                new XAttribute("Remove", this.RemovePose.ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            };
        }

        protected override string GetNodeType()
        {
            return "PosemasterStep";
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>();
        }

        public enum PosemasterPose
        {
            Think,
            Celebrate,
            Sad,
            Happy,
            Collapse,
            Damage
        }

        private static PosemasterPose GetFromString(string str)
        {
            str = str.ToLower();

            if (str.Contains("think")) return PosemasterPose.Think;
            if (str.Contains("celebrate")) return PosemasterPose.Celebrate;
            if (str.Contains("sad")) return PosemasterPose.Sad;
            if (str.Contains("happy")) return PosemasterPose.Happy;
            if (str.Contains("collapse")) return PosemasterPose.Collapse;

            throw new MisformedStepException(str);
        }
    }
}

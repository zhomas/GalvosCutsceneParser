using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class NextCameraStep : BaseFunctionStep
    {
        public static NextCameraStep CreateFromInputString(string inputString, IEntitySupplier supplier)
        {
            return new NextCameraStep(supplier.GetFirstEntityOnLine(inputString));
        }

        public NextCameraStep(CutsceneEntity owner)
        {
            this.owner = owner;
        }

        protected override string ComponentName 
        {
            get { return "CutsceneStarter"; }
        }

        protected override string FunctionName
        {
            get { return "AdoptNextCamera"; }
        }
    }
}

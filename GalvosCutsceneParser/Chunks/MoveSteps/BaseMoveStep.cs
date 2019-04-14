using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public abstract class BaseMoveStep : BaseStep
    {
        public SpeedType SpeedType { get; protected set; }

        protected float MoveSpeed
        {
            get
            {
                switch (this.SpeedType)
                {
                    case SpeedType.Slow:
                        return 32f;
                    case SpeedType.Medium:
                        return 64f;
                    case SpeedType.Fast:
                    default:
                        return 111f;
                }
            }
        }



    }


}

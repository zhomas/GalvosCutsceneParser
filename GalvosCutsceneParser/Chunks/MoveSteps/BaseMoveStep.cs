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
        public MoveSpeedType SpeedType { get; protected set; }

        protected float MoveSpeed
        {
            get
            {
                switch (this.SpeedType)
                {
                    case MoveSpeedType.Walk:
                        return 32f;
                    case MoveSpeedType.Run:
                        return 64f;
                    case MoveSpeedType.Sprint:
                    default:
                        return 111f;
                }
            }
        }


        public static MoveSpeedType SpeedTypeFromString(string str)
        {
            if (str == "=>")
            {
                return MoveSpeedType.Walk;
            }

            if (str == "=>>")
            {
                return MoveSpeedType.Run;
            }

            if (str == "=>>>")
            {
                return MoveSpeedType.Sprint;
            }

            throw new MisformedStepException(str);
        }
    }

    public enum MoveSpeedType
    {
        Walk,
        Run,
        Sprint
    }

}

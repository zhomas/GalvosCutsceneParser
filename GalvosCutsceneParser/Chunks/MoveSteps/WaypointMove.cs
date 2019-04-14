using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class WaypointMove : BaseStep
    {
        public int Increment { get; private set; }
        public bool Instant { get; private set; }

        public static bool IsMatch(StepInput input)
        {
            bool actionMatch = input.chunks.Count == 2 && GetIncrement(input.chunks[1]) != 0;
            return actionMatch && input.supplier.IsEntity(input.chunks[0]);
        }

        public WaypointMove(StepInput input)
        {
            this.Increment = GetIncrement(input.chunks[1]);
            this.Instant = input.chunks[1].StartsWith("!");
            this.entity = input.supplier.GetEntityByAlias(input.chunks[0]);
        }

        private static int GetIncrement(string verb)
        {
            if (verb.Contains("=>"))
            {
                return 1;
            }

            if (verb.Contains("<="))
            {
                return -1;
            }

            return 0;
        }
    }
}

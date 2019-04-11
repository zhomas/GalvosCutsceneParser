using GalvosCutsceneParser.Entities;
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
        private Direction dir;

        public static bool IsMatch(StepInput input)
        {
            return input.chunks.Count == 3 && (input.chunks[1] == "turn"); 
        }

        public TurnVectorStep(StepInput input)
        {
            this.entity = input.supplier.GetEntityByAlias(input.chunks[0]);
            this.dir = CreateFromInputString(input.chunks[2]);
        }

        public enum Direction
        {
            North = 0, South = 1, East = 2, West = 3
        }

        public static Direction CreateFromInputString(string dir)
        {
            if (dir.ToLower() == ("north"))
                return Direction.North;
            if (dir.ToLower() == ("south"))
                return Direction.South;
            if (dir.ToLower() == ("east"))
                return Direction.East;
            if (dir.ToLower() == ("west"))
                return Direction.West;

            throw new MisformedStepException(dir);
        }

        public Vector3 Forward
        {
            get
            {
                switch (this.dir)
                {
                    case Direction.North:
                        return Vector3.forward;
                    case Direction.South:
                        return Vector3.back;
                    case Direction.East:
                        return Vector3.right;
                    case Direction.West:
                        return Vector3.left;
                    default:
                        throw new MisformedStepException("Could not find a direction.");
                }
            }
        }

    }
}

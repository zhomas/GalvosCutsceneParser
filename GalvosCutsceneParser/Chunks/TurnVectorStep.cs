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
        public enum Direction
        {
            North = 0, South = 1, East = 2, West = 3
        }
        
        private Direction dir;

        public static TurnVectorStep CreateFromInputString(IEntity entity, string inputString)
        {
            string[] chunks = inputString.Split(' ');

            if (chunks.Last().ToLower().Contains("north"))
                return new TurnVectorStep(entity, Direction.North);
            if (chunks.Last().ToLower().Contains("south"))
                return new TurnVectorStep(entity, Direction.South);
            if (chunks.Last().ToLower().Contains("east"))
                return new TurnVectorStep(entity, Direction.East);
            if (chunks.Last().ToLower().Contains("west"))
                return new TurnVectorStep(entity, Direction.West);

            throw new MisformedStepException(inputString);
        }

        public Direction LookDirection
        {
            get { return dir; }
        }

        public Vector3 Forward
        {
            get
            {
                switch (this.LookDirection)
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

        public TurnVectorStep(IEntity entity, Direction dir) : base()
        {
            this.dir = dir;
            this.entity = entity;
        }
    }
}

using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class CameraTarget : BaseStep
    {
        public float Distance { get; private set; }
        public string Pose { get; private set; }
        public float Speed { get; private set; }
        public bool Instant { get; private set; }
        public bool Follow { get; private set; } = true;

        public static bool IsMatch(StepInput input)
        {
            bool match = input.chunks[0] == "cam";
            return match;
        }

        public CameraTarget(StepInput input)
        {
            if (input.chunks.Count > 2 && input.chunks[1].Contains("=>"))
            {
                this.entity = input.supplier.GetEntityByAlias(input.chunks[2]);
                this.Instant = input.chunks[1].StartsWith("!");
            }

            if (input.args.ContainsKey("distance"))
            {
                this.Distance = float.Parse(input.args["distance"]);
            }

            if (input.args.ContainsKey("speed"))
            {
                this.Speed = float.Parse(input.args["speed"]);
            }
            
            if (input.args.ContainsKey("low"))
            {
                this.Pose = "low";
            }

            if (input.args.ContainsKey("high"))
            {
                this.Pose = "high";
            }

            if (input.args.ContainsKey("left"))
            {
                this.Pose = "left";
            }

            if (input.args.ContainsKey("right"))
            {
                this.Pose = "right";
            }

            this.Follow = !input.args.ContainsKey("nofollow");
        }
    }
}

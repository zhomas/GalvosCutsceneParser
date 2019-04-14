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

        public static bool IsMatch(StepInput input)
        {
            bool match = input.chunks[0] == "cam";

            foreach (var chunk in input.chunks)
            {
                System.Diagnostics.Debug.WriteLine(chunk);
            }

            System.Diagnostics.Debug.WriteLine("Testing cam match... " + match.ToString());
            return match;
        }

        public CameraTarget(StepInput input)
        {
            if (input.chunks.Count > 2 && input.chunks[1] == "=>")
            {
                this.entity = input.supplier.GetEntityByAlias(input.chunks[2]);
            }

            if (input.args.ContainsKey("distance"))
            {
                this.Distance = float.Parse(input.args["distance"]);
            }

            if (input.args.ContainsKey("speed"))
            {
                this.Speed = float.Parse(input.args["speed"]);
            }

            foreach (var item in input.args.Keys)
            {
                System.Diagnostics.Debug.WriteLine(item);
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
        }
    }
}

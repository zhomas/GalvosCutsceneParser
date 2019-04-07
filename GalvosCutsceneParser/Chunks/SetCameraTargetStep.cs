using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class SetCameraTargetStep : BaseStep
    {
        public Vector3 CamRotation { get; private set; }
        public float Distance { get; private set; }

        public static SetCameraTargetStep GetFromInputString(IEntity target, string inputLine)
        {
            Vector3 rotation = RegexUtilities.GetVector3FromString(inputLine);
            float distance = SniffCameraDistanceFromInputString(inputLine);
            return new SetCameraTargetStep(target, rotation, distance);
        }

        public SetCameraTargetStep(IEntity target, Vector3 rotation, float distance) : base()
        {
            this.entity = target;
            this.CamRotation = rotation;
            this.Distance = distance;
        }

        private static float SniffCameraDistanceFromInputString(string inputLine)
        {
            if (inputLine.Contains(" --distance="))
            {
                string pattern = @"--distance=(\d+)";

                var match = Regex.Match(inputLine, pattern);

                if (match.Groups[1].Success)
                {
                    return float.Parse(match.Groups[1].Value);
                }
            }

            return 0f;
        }
    }
}

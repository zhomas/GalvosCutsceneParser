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
        public Vector3 CamRotation { get; private set; }
        public float Distance { get; private set; }

        public static bool IsMatch(List<string> chunks)
        {
            return chunks.Count > 2 && chunks[0] == "cam" && chunks[1] == "=>";
        }

        public CameraTarget(StepInput input)
        {
            this.entity = input.supplier.GetEntityByAlias(input.chunks[2]);

            if (input.args.ContainsKey("distance"))
            {
                this.Distance = float.Parse(input.args["distance"]);
            }

            if (input.args.ContainsKey("angle"))
            {
                var arr = input.args["angle"].TrimStart('(').TrimEnd(')').Split(',');

                foreach (var item in arr)
                {
                    System.Diagnostics.Debug.Write(item);
                }
                
                this.CamRotation = new Vector3(
                    float.Parse(arr[0]), float.Parse(arr[1]), float.Parse(arr[2])
                );
            }
        }
    }
}

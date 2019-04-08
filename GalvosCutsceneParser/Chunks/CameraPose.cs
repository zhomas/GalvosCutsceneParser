using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class CameraPose : BaseStep
    {
        public string Pose { get; private set; }

        public static bool IsMatch(List<string> chunks)
        {
            return chunks.Count > 2 && chunks[0] == "cam" && chunks[1] == "pose";
        }

        public CameraPose(List<string> chunks, IEntitySupplier entitySupplier)
        {
            this.Pose = chunks[2];
        }
    }
}

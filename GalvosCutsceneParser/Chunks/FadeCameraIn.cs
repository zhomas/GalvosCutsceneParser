﻿using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class FadeCameraIn : BaseStep
    {
        public static bool IsMatch(List<string> chunks)
        {
            return chunks[0] == "fadeIn";
        }

        public FadeCameraIn(StepInput input)
        {
            this.entity = input.supplier.GetEntityByAlias(input.chunks[1]);
        }
    }
}

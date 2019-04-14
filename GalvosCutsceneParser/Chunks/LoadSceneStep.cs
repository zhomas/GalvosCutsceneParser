using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class LoadSceneStep : BaseStep
    {
        public string SceneName { get; private set; }

        public static bool IsMatch(StepInput input)
        {
            return input.chunks.Count == 2 && input.chunks[0] == "load_scene";
        }

        public LoadSceneStep(StepInput input)
        {
            this.SceneName = input.chunks[1];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class YieldStep : BaseStep
    {
        public List<string> IDs { get; private set; }

        public static bool IsMatch(StepInput input)
        {
            return input.chunks[0] == "yield";
        }

        public YieldStep(StepInput input)
        {
            this.IDs = input.chunks.Skip(1).ToList();
            foreach (var item in this.IDs)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }

            
        }
    }
}

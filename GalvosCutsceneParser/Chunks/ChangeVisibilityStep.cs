using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class ChangeVisibilityStep : BaseStep
    {
        public bool Visible { get; private set; }

        public static bool IsMatch(StepInput input)
        {
            return input.supplier.IsEntity(input.chunks[0]) && (input.args.ContainsKey("show") || input.args.ContainsKey("hide"));
        }

        public ChangeVisibilityStep(StepInput input)
        {
            this.entity = input.supplier.GetEntityByAlias(input.chunks[0]);

            if (input.args.ContainsKey("show"))
            {
                this.Visible = true;
            }
        }
    }
}

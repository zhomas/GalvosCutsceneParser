using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class InstantMove : BaseStep
    {
        public IEntity Target  { get; private set; }

        public static bool IsMatch(StepInput input)
        {
            return input.chunks.Count >= 3
                   && input.chunks[1] == "!=>"
                   && input.supplier.IsEntity(input.chunks[0])
                   && input.supplier.IsEntity(input.chunks[2]);
        }

        public InstantMove(StepInput input)
        {
            this.entity = input.supplier.GetEntityByAlias(input.chunks[0]);
            this.Target = input.supplier.GetEntityByAlias(input.chunks[2]);
        }
    }
}

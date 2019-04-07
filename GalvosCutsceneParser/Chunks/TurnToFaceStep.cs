using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class TurnToFaceStep : BaseStep
    {
        public IEntity FaceTarget { get; private set; }
        
        public static bool IsMatch(List<string> chunks)
        {
            return chunks.Count == 3 && chunks[1] == "=:";
        }

        public TurnToFaceStep(List<string> chunks, IEntitySupplier entitySupplier)
        {
            this.entity = entitySupplier.GetEntityByAlias(chunks[0]);
            this.FaceTarget = entitySupplier.GetEntityByAlias(chunks[2]);
        }

    }
}

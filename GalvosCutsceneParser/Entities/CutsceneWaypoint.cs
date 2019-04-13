using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GalvosCutsceneParser.Entities
{
    public class CutsceneWaypoint : IEntity
    {
        public GameObject Target => throw new NotImplementedException();

        public string Alias { get; private set; }
        public int ID { get; private set; }

    }
}

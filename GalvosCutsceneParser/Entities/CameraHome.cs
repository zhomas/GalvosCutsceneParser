using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GalvosCutsceneParser.Entities
{
    public class CameraHome : IEntity
    {
        private Func<int, GameObject> goGetter;

        public CameraHome(Func<int, GameObject> goGetter)
        {
            this.goGetter = goGetter;
        }

        public GameObject Target
        {
            get { return this.goGetter(this.ID); }
        }

        public string Alias
        {
            get { return "home"; }
        }

        public int ID
        {
            get { return -1; }
        }
    }
}

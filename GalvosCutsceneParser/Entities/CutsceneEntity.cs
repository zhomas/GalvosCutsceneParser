using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class CutsceneEntity
    {
        public string Alias { get; private set; }
        public int ID { get; private set; }

        private Func<int, GameObject> goGetter;

        public CutsceneEntity(int id, Func<int, GameObject> goGetter)
        {
            this.ID = id;
            this.goGetter = goGetter;
        }

        public CutsceneEntity(string alias, int id, Func<int, GameObject> goGetter)
        {
            this.Alias = alias;
            this.ID = id;
            this.goGetter = goGetter;
        }

        public GameObject Target
        {
            get
            {
                return this.goGetter(this.ID);
            }
        }
    }
}

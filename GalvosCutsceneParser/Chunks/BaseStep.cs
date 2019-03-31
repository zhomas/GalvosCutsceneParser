using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using GalvosCutsceneParser.Entities;

namespace GalvosCutsceneParser
{
    public abstract class BaseStep
    {
        protected IEntity entity;

        protected BaseStep()
        {
            this.entity = new NullEntity();
        }

        public GameObject Target
        {
            get { return this.entity.Target; }
        }
    }
}

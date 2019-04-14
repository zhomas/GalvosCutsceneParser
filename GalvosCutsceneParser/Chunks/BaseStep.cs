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

        public bool Wait { get; set; } = true;
        public string RefID { get; set; } = "";
    }

    public struct StepInput
    {
        public List<string> chunks;
        public Dictionary<string, string> args;
        public IEntitySupplier supplier;
        public string line;
    }
}

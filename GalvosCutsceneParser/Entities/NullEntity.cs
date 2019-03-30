using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GalvosCutsceneParser.Entities
{
    public class NullEntity : IEntity
    {
        public GameObject Target
        {
            get { return null; }
        }

        public string Alias
        {
            get { return ""; }
        }

        public int ID
        {
            get { return -1; }
        }
    }

    public interface IEntity
    {
        GameObject Target { get; }
        string Alias { get; }
        int ID { get; }
    }
}

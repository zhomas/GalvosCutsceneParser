using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class CutsceneEntity
    {
        public string Alias { get; private set; }
        public int ID { get; private set; }

        public CutsceneEntity(int id)
        {
            this.ID = id;
        }

        public CutsceneEntity(string alias, int id)
        {
            this.Alias = alias;
            this.ID = id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public abstract class BaseEntity
    {
        public abstract string Alias { get; }
        public abstract int ID { get; }
    }
}

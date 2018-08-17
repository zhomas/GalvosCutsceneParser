using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvosCutsceneParser.Chunks
{
    public abstract class BaseStep
    {
        public const string ID_PREFIX = "____";

        public abstract string ToXML();
    }
}

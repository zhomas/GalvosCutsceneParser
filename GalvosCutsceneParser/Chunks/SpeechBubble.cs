using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using GalvosCutsceneParser;

namespace GalvosCutsceneParser.Chunks
{
    public class SpeechBubble : BaseStep
    {
        private int aID;
        private int next;
        private string message;

        public SpeechBubble(int aID, string message, int next)
        {
            this.aID = aID;
            this.message = message;
            this.next = -1;
        }

        protected override XAttribute[] GetBooleanParams()
        {
            return new List<XAttribute>()
            {
                new XAttribute("cameraTarget", false.ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            }.ToArray();
        }

        protected override XAttribute[] GetRootNodeParams()
        {
            return new List<XAttribute>()
            {
                new XAttribute("aID", this.aID), 
                new XAttribute("guiBoxID", 0), 
                new XAttribute("next", this.next)
            }.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvosCutsceneParser.Chunks
{
    public class SpeechBubble : BaseStep
    {
        private int aID;
        private string message;

        public SpeechBubble(int aID, string message)
        {
            this.aID = aID;
            this.message = message;
        }

        public override string ToXML()
        {
            return this.aID.ToString();
        }
    }
}

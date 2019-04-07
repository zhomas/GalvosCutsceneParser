using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using GalvosCutsceneParser;
using GalvosCutsceneParser.Entities;

namespace GalvosCutsceneParser
{
    public class SpeechBubble : BaseStep
    {
        private int aID;
        public string Message { get; private set; }

        public SpeechBubble(IEntity entity, string message) : base()
        {
            this.entity = entity;
            this.Message = message;
        }
    }
}

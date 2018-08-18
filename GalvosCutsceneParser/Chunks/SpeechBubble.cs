using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("cameraTarget", false.ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            };
        }

        protected override List<XAttribute> GetRootNodeAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("aID", this.aID),
                new XAttribute("guiBoxID", 0),
                new XAttribute("next", this.next)
            };
        }

        protected override List<XElement> GetStringElements()
        {
            var nodeType = new XElement("_type");
            nodeType.Add(new XCData("SpeechBubbleStep"));

            return base.GetStringElements().Concat(new List<XElement>()
            {
                nodeType
            }).ToList();
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>()
            {
                new XElement("message",
                    new XElement(Parser.XML_PREFIX + "0", 
                        new XCData(this.message)))
            };
        }
    }
}

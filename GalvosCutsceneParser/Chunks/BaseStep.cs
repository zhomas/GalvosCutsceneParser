using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser.Chunks
{
    public abstract class BaseStep
    {

        protected abstract XAttribute[] GetRootNodeParams();

        protected abstract XAttribute[] GetBooleanParams();

        public string ToXML()
        {
            XElement element = new XElement(Parser.XML_PREFIX + "0", this.GetRootNodeParams());

            element.Add(new XElement("_bool", this.GetBooleanParams()));

            return element.ToString().Replace(Parser.XML_PREFIX, "").WhitespaceCleanupXML();
        }
    }
}

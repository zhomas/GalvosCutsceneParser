using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser.Chunks
{
    public abstract class BaseStep
    {
        protected abstract List<XAttribute> GetRootNodeAttributes();

        protected abstract List<XAttribute> GetBooleanAttributes();

        protected abstract List<XElement> GetStringArrayElements();

        protected virtual List<XElement> GetStringElements()
        {
            var nodeName = new XElement("nodeName");
            nodeName.Add(new XCData(""));

            return new List<XElement>()
            {
                nodeName
            };
        }

        public XElement ToXML(int index)
        {
            XElement element = new XElement(Parser.XML_PREFIX + index.ToString(), this.GetRootNodeAttributes());

            var bools = this.GetBooleanAttributes();
            var strings = this.GetStringElements();
            var stringArrays = this.GetStringArrayElements();

            if (bools.Count > 0)
            {
                element.Add(new XElement("_bool", bools));
            }

            if (strings.Count > 0)
            {
                element.Add(new XElement("_string", strings));
            }

            if (stringArrays.Count > 0)
            {
                element.Add(new XElement("_stringarrays", stringArrays));
            }

            return element;
        }
    }
}

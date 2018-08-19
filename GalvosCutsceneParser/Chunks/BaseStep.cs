using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public abstract class BaseStep
    {
        protected virtual List<XAttribute> GetRootNodeAttributes(int index, bool isFinalStep)
        {
            return new List<XAttribute>()
            {
                new XAttribute("next", isFinalStep ? -1 : index + 1)
            };
        }

        protected abstract List<XAttribute> GetBooleanAttributes();

        protected abstract List<XElement> GetStringArrayElements();

        protected virtual List<XElement> GetNodeSpecialElements()
        {
            return new List<XElement>();
        }

        protected virtual List<XElement> GetStringElements()
        {
            var nodeName = new XElement("nodeName");
            nodeName.Add(new XCData(""));

            return new List<XElement>()
            {
                nodeName
            };
        }

        public XElement ToXML(int index, bool isFinalStep)
        {
            XElement element = new XElement(Parser.XML_PREFIX + index.ToString(), this.GetRootNodeAttributes(index, isFinalStep));

            var bools = this.GetBooleanAttributes();
            var strings = this.GetStringElements();
            var stringArrays = this.GetStringArrayElements();
            var special = this.GetNodeSpecialElements();

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

            if (special.Count > 0)
            {
                foreach (var item in special)
                {
                    element.Add(item);
                }
            }

            return element;
        }
    }
}

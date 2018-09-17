using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public abstract class BaseStep
    {
        protected StepFlags flags;

        protected virtual List<XAttribute> GetRootNodeAttributes(int index, bool isFinalStep)
        {
            return new List<XAttribute>()
            {
                new XAttribute("next", isFinalStep ? -1 : index + 1)
            };
        }

        protected abstract List<XAttribute> GetBooleanAttributes();

        protected abstract List<XElement> GetStringArrayElements();

        protected virtual List<XAttribute> GetFloatAttributes()
        {
            return new List<XAttribute>();
        }

        protected virtual List<XElement> GetFloatArrayElements()
        {
            return new List<XElement>();
        }

        protected virtual List<XElement> GetNodeSpecialElements()
        {
            return new List<XElement>();
        }

        protected abstract string GetNodeType();

        protected XElement GetEmptyCData(string tagName)
        {
            var emptyChildName = new XElement(tagName);
            emptyChildName.Add(new XCData(""));
            return emptyChildName;
        }

        protected virtual List<XElement> GetStringElements()
        {
            var nodeName = new XElement("nodeName");
            nodeName.Add(new XCData(""));

            var nodeType = new XElement("_type");
            nodeType.Add(new XCData(this.GetNodeType()));

            return new List<XElement>()
            {
                nodeName,
                nodeType
            };
        }

        public XElement ToXML(int index, bool isFinalStep)
        {
            XElement element = new XElement(Parser.XML_DELIMITER + index.ToString(), this.GetRootNodeAttributes(index, isFinalStep));

            var bools = this.GetBooleanAttributes();

            var floats = this.GetFloatAttributes();
            var floatArrays = this.GetFloatArrayElements();

            var strings = this.GetStringElements();
            var stringArrays = this.GetStringArrayElements();

            var special = this.GetNodeSpecialElements();


            if (floats.Count > 0)
            {
                element.Add(new XElement("_float", floats));
            }

            if (bools.Count > 0)
            {
                element.Add(new XElement("_bool", bools));
            }

            if (floatArrays.Count > 0)
            {
                element.Add(new XElement("_floatarrays", floatArrays));
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

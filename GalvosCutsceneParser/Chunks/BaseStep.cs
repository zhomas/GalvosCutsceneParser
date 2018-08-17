using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GalvosCutsceneParser.Chunks
{
    public abstract class BaseStep
    {
        public const string ID_PREFIX = "____";

        protected abstract XAttribute[] GetRootNodeParams();

        protected abstract XAttribute[] GetBooleanParams();

        public string ToXML()
        {
            XElement element = new XElement(ID_PREFIX + "0", this.GetRootNodeParams());

            element.Add(new XElement("_bool", this.GetBooleanParams()));

            return element.ToString().Replace(ID_PREFIX, "").FormattedXML();
        }
    }
}

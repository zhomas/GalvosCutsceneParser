using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GalvosCutsceneParser
{
    public static class XmlUtilities
    {
        public static XmlDocument RemoveByTagName(this XmlDocument doc, string exclude)
        {
            var steps = doc.DocumentElement.GetElementsByTagName(exclude);
            for (int i = 0; i < steps.Count; i++)
            {
                steps[i].ParentNode.RemoveChild(steps[i]);
            }

            return doc;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class Parser
    {
        public const string XML_PREFIX = "____";

        private string inputXml;
        private XDocument document;

        public Parser LoadEventXML(string xml)
        {
            this.inputXml = xml.ConvertORKToValidXML();
            this.document = XDocument.Parse(this.inputXml);
            this.document.Root.SetAttributeValue("startIndex", 0);
            return this;
        }

        public Parser ReplaceXMLStepsWithGPLSteps(List<BaseStep> steps)
        {
            if (this.document == null)
            {
                throw new Exception("No Document Intialised!");
            }

            var stepParent = this.document.Descendants("step").First().Parent;

            this.document.Descendants("step").Remove();
            XElement step = new XElement("step",
                steps.Select((s, i) => s.ToXML(i, i == steps.Count - 1))
            );
            stepParent.Add(step);
            return this;
        }

        public XDocument Document
        {
            get
            {
                return this.document;
            }
        }

        public string GetXML()
        {
            string validXml = this.document.ToString();
            return validXml.ConvertValidXMLToORK();
        }

        private void WriteXMLToDesktop(string xml)
        {
            string desktop = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

            File.WriteAllText(desktop + "/xml.txt", xml);
        }
    }
}

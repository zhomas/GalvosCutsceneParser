using GalvosCutsceneParser.Chunks;
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
            Console.WriteLine(xml);
            Console.ReadKey();

            this.inputXml = xml.ConvertORKToValidXML();
            this.document = XDocument.Parse(this.inputXml);
            return this;
        }

        public Parser ReplaceXMLStepsWithGPLSteps(string gpl)
        {
            if (this.document == null)
            {
                throw new Exception("No Document Intialised!");
            }

            var stepParent = this.document.Descendants("step").First().Parent;

            this.document.Descendants("step").Remove();

            var step = new XElement("step",
                this.GetStepsFromInput(gpl).Select((s, i) => s.ToXML(i))
            );

            stepParent.Add(step);

            Console.Clear();
            Console.ReadKey();
            Console.Write(this.document.ToString());
            Console.ReadKey();
            return this;
        }

        public string GetXML()
        {
            return "Lenny...";
        }

        private void WriteXMLToDesktop(string xml)
        {
            string desktop = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

            File.WriteAllText(desktop + "/xml.txt", xml);
        }

        public List<BaseStep> GetStepsFromInput(string input)
        {
            List < BaseStep > list = new List<BaseStep>();
            StepBuilder builder = new StepBuilder();

            using (StringReader reader = new StringReader(input))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        BaseStep step = builder.BuildStep(line);
                        list.Add(step);
                    }
                }
                while (line != null);
            }

            return list;
        }
    }
}

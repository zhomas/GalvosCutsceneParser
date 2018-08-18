using GalvosCutsceneParser.Chunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class Parser
    {
        public const string XML_PREFIX = "____";

        private string inputXml;

        public void LoadEventXML(string xml)
        {
            this.inputXml = xml.SanitizeORKXml();

            
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.InnerXml = xml.SanitizeORKXml();
            doc.RemoveByTagName("step");

            Console.WriteLine(doc.InnerXml);
            Console.ReadKey();
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

        public List<BaseStep> ProcessInputString(string input)
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

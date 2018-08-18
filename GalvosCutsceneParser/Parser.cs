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
            doc.InnerXml = xml.SanitizeORKXml();
            
            var steps = doc.DocumentElement.GetElementsByTagName("step");

            for (int i = 0; i < steps.Count; i++)
            {
                var childs = steps[i].ChildNodes;

                for (int j = 0; j < childs.Count; j++)
                {
                    Console.WriteLine(childs[j].OuterXml);
                    Console.WriteLine();
                }

                
            }

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

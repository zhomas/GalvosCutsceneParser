using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalvosCutsceneParser;

namespace Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            var stepBuilder = new StepBuilder();

            string desktop = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

            string xml = System.IO.File.ReadAllText(desktop + "/xml.txt");
            string gpl = "Joey say \"Wuuut wuuuuut!\"";


            var steps = stepBuilder.GetStepsFromInput(gpl);

            var result = parser.LoadEventXML(xml)
                      .ReplaceXMLStepsWithGPLSteps(steps)
                      .GetXML();


            Console.Write(result);
            Console.ReadLine();
        }
    }
}

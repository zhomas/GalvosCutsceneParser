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

            string desktop = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

            string text = System.IO.File.ReadAllText(desktop + "/xml.txt");

            parser.LoadEventXML(text);

        }
    }
}

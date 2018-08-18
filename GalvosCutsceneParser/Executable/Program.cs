﻿using System;
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
            string desktop = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

            string xml = System.IO.File.ReadAllText(desktop + "/xml.txt");
            string gpl = "Joey say \"Wuuut wuuuuut!\"";

            var service = new Service(gpl, xml);

            Console.Write(service.GetEventXML());
            Console.ReadLine();
        }
    }
}
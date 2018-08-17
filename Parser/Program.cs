using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Joey => WP1");
            sb.AppendLine("Lucy => WP3");

            var parser = new GalvosCutsceneParser(sb.ToString());


            Console.ReadKey();

        }
    }
}

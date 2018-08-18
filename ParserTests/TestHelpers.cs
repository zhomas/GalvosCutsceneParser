using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserTests
{
    public class TestHelpers
    {
        public static string GetSampleGPL(int entityCount)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("#alias");

            if (entityCount > 0) sb.AppendLine("Joey = 0");
            if (entityCount > 1) sb.AppendLine("Lucy = 1");


            sb.AppendLine("#endalias");

            sb.AppendLine("Joey say \"Hello\"");
            sb.AppendLine("Lucy say \"Goodbye\"");

            return sb.ToString();
        }

    }
}

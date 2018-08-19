using GalvosCutsceneParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserTests
{
    public class TestHelpers
    {
        public static string GetSampleGPL(int entityCount, int stepCount)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("");

            if (entityCount > 0)
            {
                sb.AppendLine("#alias");

                if (entityCount > 0) sb.AppendLine("Joey = 0");
                if (entityCount > 1) sb.AppendLine("Lucy = 1");


                sb.AppendLine("#endalias");
            }

            if (stepCount > 0)
            {
                sb.AppendLine("#steps");

                if (stepCount > 0) sb.AppendLine("Joey say \"Hello\"");
                if (stepCount > 1) sb.AppendLine("Lucy say \"Goodbye\"");
                if (stepCount > 2) sb.AppendLine("Lucy say \"Goodbye\"");

                sb.AppendLine("#endsteps");

            }

            return sb.ToString();
        }

    }

    public class MockEntitySupplier : IEntitySupplier
    {
        public List<CutsceneEntity> Entities => throw new NotImplementedException();

        public CutsceneEntity GetEntityByAlias(string alias)
        {
            return new CutsceneEntity(0);
        }
    }
}

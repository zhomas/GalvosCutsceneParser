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

        public static string GetCompleteGPL()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# alias");
            sb.AppendLine("Joey = 0");
            sb.AppendLine("Lucy = 1");
            sb.AppendLine("Magus = 2");
            sb.AppendLine("Bow = 3");
            sb.AppendLine("#endalias");
            sb.AppendLine("#steps");
            sb.AppendLine("Joey say \"Citan told me all about your story.Do you want to talk about it ? Why didn't you tell me? It sounds pretty rough. \"");
            sb.AppendLine("Joey say \"Hello!This text comes from Galvos Parsing!\"");
            sb.AppendLine("Lucy say \"Woohoo! Second line - a - roo\"");
            sb.AppendLine("Lucy camtarget");
            sb.AppendLine("wait 1000");
            sb.AppendLine("Joey say \"And a third line of text is mine\"");
            sb.AppendLine("Magus camtarget 15, -25, 0");
            sb.AppendLine("Magus say \"So...we meet again, Joey\"");
            sb.AppendLine("Joey => 0, 100");
            sb.AppendLine("wait 500");
            sb.AppendLine("Joey camtarget 15, 0, 0");
            sb.AppendLine("wait 100");
            sb.AppendLine("Joey look south");
            sb.AppendLine("Joey celebrate");
            sb.AppendLine("wait 3000");
            sb.AppendLine("Joey look west");
            sb.AppendLine("Joey think");
            sb.AppendLine("wait 3000");
            sb.AppendLine("Joey look north");
            sb.AppendLine("Joey sad");
            sb.AppendLine("wait 3000");
            sb.AppendLine("Joey look east");
            sb.AppendLine("wait 3000");
            sb.AppendLine("Lucy => -100, 0");
            sb.AppendLine("Lucy say \"That was a good cutscene!\"");
            sb.AppendLine("#endsteps");

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

        public CutsceneEntity GetFirstEntityOnLine(string line)
        {
            return new CutsceneEntity(0);
        }
    }
}

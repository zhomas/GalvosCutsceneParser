using GalvosCutsceneParser.Chunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalvosCutsceneParser
{
    public class Parser
    {
        public void ProcessInputString(string input)
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
        }
    }
}

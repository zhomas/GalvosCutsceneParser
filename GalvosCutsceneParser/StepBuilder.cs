using GalvosCutsceneParser.Actions;
using GalvosCutsceneParser.Chunks;
using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class StepBuilder
    {
        public List<BaseStep> GetStepsFromInput(string input)
        {
            List<BaseStep> list = new List<BaseStep>();

            using (StringReader reader = new StringReader(input))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        BaseStep step = this.BuildStep(line);
                        list.Add(step);
                    }
                }
                while (line != null);
            }

            return list;
        }

        public BaseEntity GetEntityFromInput(string inputLine)
        {
            var chunks = inputLine.Split(' ');
            if (chunks[0] == "Joey")
            {
                return new Humanoid();
            }

            return null;
        }

        public string GetParameterFromInput(string inputLine)
        {
            string parameter = RegexUtilities.PullOutTextInsideQuotes(ref inputLine);
            return parameter;
        }

        public StepAction GetActionFromInput(string inputLine)
        {
            var chunks = inputLine.Split(' ');

            if (chunks[1] == "say")
            {
                return StepAction.Speech;
            }

            return StepAction.Undefined;
        }

        public BaseStep BuildStep(string inputLine)
        {
            BaseEntity entity = this.GetEntityFromInput(inputLine);
            string parameter = RegexUtilities.PullOutTextInsideQuotes(ref inputLine);
            StepAction action = this.GetActionFromInput(inputLine);

            switch (action)
            {
                case StepAction.Speech:
                    return new SpeechBubble(entity.ID, parameter, -1);
            }

            return null;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class StepBuilder
    {
        public const string START_STEP = "#steps";
        public const string END_STEP = "#endsteps";

        private IEntitySupplier entitySupplier;

        public StepBuilder(IEntitySupplier entitySupplier)
        {
            this.entitySupplier = entitySupplier;
        }

        public List<BaseStep> GetStepsFromInput(string input)
        {
            List<BaseStep> list = new List<BaseStep>();

            string section = "";

            try
            {
                section = RegexUtilities.GetTextBetween(input, START_STEP, END_STEP);
            }
            catch (Exception e)
            {
                throw new NoStepsException();
            }

            using (StringReader reader = new StringReader(section))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        list.Add(this.BuildStep(line));
                    }
                }
                while (line != null);
            }

            if (list.Count == 0)
            {
                throw new NoStepsException();
            }

            return list;
        }

        public CutsceneEntity GetEntityFromInput(string inputLine)
        {
            var chunks = inputLine.Split(' ');
            return this.entitySupplier.GetEntityByAlias(chunks[0]);
        }

        public string GetParameterFromInput(string inputLine)
        {
            string parameter = RegexUtilities.PullOutTextInsideQuotes(ref inputLine);
            return parameter;
        }

        public StepAction GetActionFromInput(string inputLine)
        {
            var chunks = inputLine.Split(' ');

            if (chunks[0] == "wait")
            {
                return StepAction.Wait;
            }

            if (chunks[1] == "say")
            {
                return StepAction.Speech;
            }

            return StepAction.Undefined;
        }

        public BaseStep BuildStep(string inputLine)
        {
            CutsceneEntity entity = this.GetEntityFromInput(inputLine);
            StepAction action = this.GetActionFromInput(inputLine);

            switch (action)
            {
                case StepAction.Speech:
                    string message = RegexUtilities.PullOutTextInsideQuotes(ref inputLine);
                    return new SpeechBubble(entity.ID, message);
                case StepAction.Wait:
                    int time = WaitStep.ParseMillisecondsFromInputLine(inputLine);
                    return new WaitStep(time);
            }

            return null;
        }
    }

    public class NoStepsException : Exception
    {

    }
}

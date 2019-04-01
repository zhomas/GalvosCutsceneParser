using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

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

        public IEntity GetEntityFromInput(string inputLine)
        {
            var chunks = inputLine.Trim().Split(' ');
            return this.entitySupplier.GetEntityByAlias(chunks[0]);
        }

        public string GetParameterFromInput(string inputLine)
        {
            string parameter = RegexUtilities.PullOutTextInsideQuotes(ref inputLine);
            return parameter;
        }

        public StepAction GetActionFromInput(string inputLine)
        {
            var chunks = inputLine.Trim().Split(' ');

            if (chunks[0] == "wait") return StepAction.Wait;
            if (chunks[0] == "fadeIn") return StepAction.FadeIn;
            if (chunks[1] == "say") return StepAction.Speech;
            if (chunks[1].Contains("=>")) return StepAction.Move;
            if (chunks[1] =="camtarget") return StepAction.Camera;
            if (chunks[1] == "turn") return StepAction.Turn;
            if (chunks[1] == "look") return StepAction.Turn;



            foreach (var item in Enum.GetValues(typeof(PoseMasterStep.PosemasterPose)))
            {
                if (chunks[1].ToLower() == item.ToString().ToLower())
                {
                    return StepAction.Pose;
                }
            }

            return StepAction.Undefined;
        }

        public BaseStep BuildStep(string inputLine)
        {
            inputLine = inputLine.Trim();

            var split = inputLine.Split(' ').ToList();

            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.BaseType == typeof(BaseStep));

            foreach (var type in types)
            {
                var method = type.GetMethod("IsMatch");

                if (method != null)
                {
                    bool isMatch = (bool)method.Invoke(null, new [] {split});

                    if (isMatch)
                    {
                        var constructor = type.GetConstructor(new[] {typeof(List<string>), typeof(IEntitySupplier)});
                        BaseStep step = (BaseStep)constructor.Invoke(new object[] {split, this.entitySupplier});
                        return step;
                    }
                }
                    
            }

            IEntity entity = this.GetEntityFromInput(inputLine);
            StepAction action = this.GetActionFromInput(inputLine);

            switch (action)
            {
                case StepAction.Speech:
                    string message = RegexUtilities.PullOutTextInsideQuotes(ref inputLine);
                    return new SpeechBubble(entity, message);
                case StepAction.Wait:
                    int time = WaitStep.ParseMillisecondsFromInputLine(inputLine);
                    return new WaitStep(time);
                case StepAction.Move:

                    BaseMoveStep moveStep = MoveAiInDirectionStep.CreateFromInputString(
                        entity, 
                        inputLine
                    );

                    if (moveStep != null)
                    {
                        return moveStep;
                    }

                    moveStep = MoveToPositionStep.CreateFromInputString(inputLine, this.entitySupplier);

                    if (moveStep != null)
                    {
                        return moveStep;
                    }

                    throw new MisformedStepException(inputLine);
                case StepAction.Camera:
                    return SetCameraTargetStep.GetFromInputString(entity, inputLine);
                case StepAction.Pose:
                    return PoseMasterStep.CreateFromInputString(entity, inputLine);
            }

            throw new MisformedStepException(inputLine);
        }
    }

    public class NoStepsException : Exception
    {

    }
}

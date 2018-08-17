using System;
using GalvosCutsceneParser;
using GalvosCutsceneParser.Actions;
using GalvosCutsceneParser.Chunks;
using GalvosCutsceneParser.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class StepBuilderTest
    {
        [TestMethod]
        public void TestSpeechBubbleCreated()
        {
            var builder = new StepBuilder();
            BaseStep step = builder.BuildStep("Joey say \"Hello! Good to meet you sir!\"");
            Assert.AreEqual(typeof(SpeechBubble), step.GetType());
        }

        [TestMethod]
        public void TestEntityIsParsedFromString()
        {
            var builder = new StepBuilder();
            var capitalJoey = builder.GetEntityFromInput("Joey say \"Hello! Good to meet you sir!\"");
            Assert.AreEqual(typeof(Humanoid), capitalJoey.GetType());
        }

        [TestMethod]
        public void TestActionIsParsedFromString()
        {
            var builder = new StepBuilder();
            StepAction maybeSpeech = builder.GetActionFromInput("Joey say \"Hello! Good to meet you sir!\"");
            StepAction maybeFart = builder.GetActionFromInput("Joey fart \"Hello! Good to meet you sir!\"");
            Assert.AreEqual(StepAction.Speech, maybeSpeech);
            Assert.AreEqual(StepAction.Undefined, maybeFart);
        }

        [TestMethod]
        public void TestParameterIsParsedFromString()
        {
            var builder = new StepBuilder();
            var value = builder.GetParameterFromInput("Joey say \"Hello! Good to meet you sir!\"");

            Assert.AreEqual("Hello! Good to meet you sir!", value);
        }
    }
}

﻿using System;
using System.Text;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class StepBuilderTest
    {
        [TestMethod]
        [ExpectedException(typeof(NoStepsException))]
        public void TestExceptionThrownIfNoSteps()
        {
            var stepbuilder = new StepBuilder(new MockEntitySupplier());
            var s = TestHelpers.GetSampleGPL(2, 0);
            stepbuilder.GetStepsFromInput(s);
        }

        [TestMethod]
        public void TestParserBuildsMultipleSteps()
        {
            var step = new StepBuilder(new MockEntitySupplier());
            var result = step.GetStepsFromInput(TestHelpers.GetSampleGPL(0, 2));

            Assert.AreEqual(typeof(SpeechBubble), result[0].GetType());
            Assert.AreEqual(typeof(SpeechBubble), result[1].GetType());

            Assert.AreEqual(2, result.Count);
        }


        [TestMethod]
        public void TestSpeechBubbleCreated()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("Joey say \"Hello! Good to meet you sir!\"");
            Assert.AreEqual(typeof(SpeechBubble), step.GetType());
        }

        [TestMethod]
        public void TestWaitStepCreated()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("wait 1000");
            Assert.AreEqual(typeof(WaitStep), step.GetType());
        }

        [TestMethod]
        public void TestCamTargetStepCreated()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("Joey camtarget");
            Assert.AreEqual(typeof(SetCameraTargetStep), step.GetType());
        }

        [TestMethod]
        public void TestMoveInDirectionStepCreated()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep walk = builder.BuildStep("Joey => 100, 100");
            BaseStep run = builder.BuildStep("Joey =>> 100, 100");
            BaseStep sprint = builder.BuildStep("Joey =>>> 100, 100");
            Assert.AreEqual(typeof(MoveAiInDirectionStep), walk.GetType());
            Assert.AreEqual(typeof(MoveAiInDirectionStep), run.GetType());
            Assert.AreEqual(typeof(MoveAiInDirectionStep), sprint.GetType());
        }

        [TestMethod]
        public void TestEntityIsParsedFromString()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var capitalJoey = builder.GetEntityFromInput("Joey say \"Hello! Good to meet you sir!\"");
            Assert.AreEqual(typeof(CutsceneEntity), capitalJoey.GetType());
        }

        [TestMethod]
        public void TestSpeechActionParsedFromString()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            StepAction maybeSpeech = builder.GetActionFromInput("Joey say \"Hello! Good to meet you sir!\"");
            StepAction maybeFart = builder.GetActionFromInput("Joey fart \"Hello! Good to meet you sir!\"");
            Assert.AreEqual(StepAction.Speech, maybeSpeech);
            Assert.AreEqual(StepAction.Undefined, maybeFart);
        }



        [TestMethod]
        public void TestParameterIsParsedFromString()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var value = builder.GetParameterFromInput("Joey say \"Hello! Good to meet you sir!\"");

            Assert.AreEqual("Hello! Good to meet you sir!", value);
        }
    }
}

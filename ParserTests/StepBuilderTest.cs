using System;
using System.Text;
using GalvosCutsceneParser;
using GalvosCutsceneParser.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;


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
        public void TestParserParsesCompleteString()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var result = builder.GetStepsFromInput(TestHelpers.GetCompleteGPL());
        }

        [TestMethod]
        public void TestSpeechBubbleCreated()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("Joey say \"Citan told me all about your story. Do you want to talk about it? Why didn't you tell me? It sounds pretty rough.\"");
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
        public void TestTurnVectorStepCreated()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("Joey turn south");
            Assert.AreEqual(typeof(TurnVectorStep), step.GetType());
        }

        [TestMethod]
        public void TestPosemasterStepCreated()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("Joey celebrate");
            Assert.AreEqual(typeof(PoseMasterStep), step.GetType());
        }

        [TestMethod]
        public void TestCamTargetRotationCreate()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var zero = builder.BuildStep("Joey camtarget");
            var ten = builder.BuildStep("Joey camtarget 10, 10, 0");
            Assert.AreEqual(typeof(SetCameraTargetStep), ten.GetType());
            Assert.AreEqual(new Vector3(10, 10, 0), ((SetCameraTargetStep)ten).CamRotation);
            Assert.AreEqual(Vector3.zero, ((SetCameraTargetStep)zero).CamRotation);
        }

        [TestMethod]
        public void TestCamTargetDistanceCreate()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var step = builder.BuildStep("Joey camtarget 10, 10, 0 --distance=400");
            Assert.AreEqual(typeof(SetCameraTargetStep), step.GetType());
            Assert.AreEqual(400f, ((SetCameraTargetStep)step).Distance);
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
        public void TestMoveToPositionStepCreated()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("Joey => Lucy");
            Assert.AreEqual(typeof(MoveToPositionStep), step.GetType());
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
        public void TestParsingAction()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            string input = "Magus say \"Citan told me all about your story. Do you want to talk about it?#>Why didn't you tell me? It sounds pretty rough.\"";
            builder.GetActionFromInput(input);
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

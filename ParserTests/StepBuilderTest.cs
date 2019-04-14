using System;
using System.Collections.Generic;
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
        public void TestIfWaitCanBeSkipped()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            WaitStep step = builder.BuildStep("wait 1000 !!") as WaitStep;
            Assert.IsFalse(step.Wait);
        }

        [TestMethod]
        public void TestCamTargetStepCreated()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("cam => Joey");
            Assert.AreEqual(typeof(CameraTarget), step.GetType());
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
            var zero = builder.BuildStep("cam => Joey");
            var ten = builder.BuildStep("cam => Joey --low");
            Assert.AreEqual("low", ((CameraTarget)ten).Pose);
        }

        [TestMethod]
        public void TestCamPose()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var c = builder.BuildStep("cam --low");
            Assert.AreEqual("low", ((CameraTarget)c).Pose);
        }

        [TestMethod]
        public void TestWaypointMove()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var step = builder.BuildStep("Joey =>");
            Assert.AreEqual(typeof(WaypointMove), step.GetType());
        }

        [TestMethod]
        public void TestWaypointMoveNoWaiting()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var step = builder.BuildStep("Joey => !!");
            Assert.AreEqual(typeof(WaypointMove), step.GetType());
            Assert.AreEqual(step.Wait, false);
        }

        [TestMethod]
        public void TestStepRef()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var step = builder.BuildStep("Joey say \"Hello\" --ref=banana");
            Assert.AreEqual("banana", step.RefID);
        }

        [TestMethod]
        public void TestWaypointMoveBack()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var step = builder.BuildStep("Joey <=") as WaypointMove;
            Assert.AreEqual(step.Increment, -1);
        }

        [TestMethod]
        public void TestActivateStep()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var step = builder.BuildStep("activate Door");
            Assert.AreEqual(typeof(ActivateStep), step.GetType());
        }

        [TestMethod]
        public void TestCamSpeedMatch()
        {
            StepInput i = new StepInput()
            {
                chunks = new System.Collections.Generic.List<string>() {"cam", "speed=180"}
            };

            bool match = CameraTarget.IsMatch(i);
            Assert.IsTrue(match);
        }

        [TestMethod]
        public void TestCamSpeed()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var c = builder.BuildStep("cam --speed=180") as CameraTarget;
            Assert.AreEqual(180f, (c.Speed));


        }

        [TestMethod]
        public void TestCamPoseWithSpeed()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var c = builder.BuildStep("cam --low --speed=180");
            Assert.AreEqual("low", ((CameraTarget)c).Pose);
            Assert.AreEqual(180, ((CameraTarget)c).Speed);
        }

        [TestMethod]
        public void TestCamTargetDistanceCreate()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            var step = builder.BuildStep("cam => Joey --angle=(10,10,0) --distance=400 --invert");
            Assert.AreEqual(typeof(CameraTarget), step.GetType());
            Assert.AreEqual(400f, ((CameraTarget)step).Distance);
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
        public void TestYieldStep()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("fade => 1");
            Assert.AreEqual(typeof(FadeScreenStep), step.GetType());

            Assert.IsTrue((step as FadeScreenStep).Alpha == 1);
            Assert.IsTrue((step as FadeScreenStep).SpeedType == SpeedType.Slow);
        }

        [TestMethod]
        public void TestFadeScreenStep()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("yield banana cherry");
            Assert.AreEqual(typeof(YieldStep), step.GetType());
            Assert.IsTrue((step as YieldStep).IDs.Contains("banana"));
            Assert.IsTrue((step as YieldStep).IDs.Contains("cherry"));
        }

        [TestMethod]
        public void TestLoadSceneStep()
        {
            var builder = new StepBuilder(new MockEntitySupplier());
            BaseStep step = builder.BuildStep("load_scene test_scene");
            Assert.AreEqual(typeof(LoadSceneStep), step.GetType());
            Assert.IsTrue((step as LoadSceneStep).SceneName == "test_scene");
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

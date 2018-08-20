using System;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

namespace ParserTests
{
    [TestClass]
    public class MoveAiInDirectionTest
    {
        [TestMethod]
        public void TestXAxisParsedFromInputString()
        {
            Vector3 left = new Vector3(-100, 0, 0);
            Vector3 right = new Vector3(100, 0, 0);
            Assert.AreEqual(left, MoveAiInDirectionStep.GetDirectionFromInputString("Joey => -100, 0"));
            Assert.AreEqual(right, MoveAiInDirectionStep.GetDirectionFromInputString("Joey => 100, 0"));
        }

        [TestMethod]
        public void TestZAxisParsedFromInputString()
        {
            Vector3 forward = new Vector3(0, 0, 100);
            Vector3 back = new Vector3(0, 0, -100);
            Assert.AreEqual(forward, MoveAiInDirectionStep.GetDirectionFromInputString("Joey => 0, 100"));
            Assert.AreEqual(back, MoveAiInDirectionStep.GetDirectionFromInputString("Joey => 0, -100"));
        }

        [TestMethod]
        [ExpectedException(typeof(MisformedStepException))]
        public void TestBadSyntaxThrowsException()
        {
            MoveAiInDirectionStep.GetDirectionFromInputString("Joey => 0");
        }

        [TestMethod]
        public void TestStepProducesCorrectXML()
        {
            
            string expected = "<6 next=\"7\" >" +
                "<_bool WaitUntilComplete=\"True\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_floatarrays>" +
                    "<Direction -32 0 0 />" +
                "</_floatarrays>" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[MoveAIByDirection]]></_type>" +
                "</_string>" +
                "<movingObject type=\"0\" aID=\"0\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</movingObject>" +
                "<moveSpeed type=\"3\" >" +
                    "<_float speed=\"64\" />" +
                "</moveSpeed>" +
            "</6>";

            expected = expected.WhitespaceCleanupXML();

            var step = new MoveAiInDirectionStep(new CutsceneEntity(0), new Vector3(-32, 0, 0), MoveAiInDirectionStep.MoveSpeedType.Run);

            string actual = step.ToXML(6, false).ToString().ConvertValidXMLToORK().WhitespaceCleanupXML();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSpeedIsParsedFromInputString()
        {
            CutsceneEntity entity = new CutsceneEntity(0);
            var walker = MoveAiInDirectionStep.GetFromInputString(entity, "Joey => 100, 100");
            var runner = MoveAiInDirectionStep.GetFromInputString(entity, "Joey =>> 100, 100");
            var sprinter = MoveAiInDirectionStep.GetFromInputString(entity, "Joey =>>> 100, 100");

            Assert.AreEqual(MoveAiInDirectionStep.MoveSpeedType.Walk, walker.SpeedType);
            Assert.AreEqual(MoveAiInDirectionStep.MoveSpeedType.Run, runner.SpeedType);
            Assert.AreEqual(MoveAiInDirectionStep.MoveSpeedType.Sprint, sprinter.SpeedType);
        }
    }
}

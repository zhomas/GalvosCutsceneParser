using System;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests.Chunks
{
    [TestClass]
    public class MoveThroughDoorTest
    {
        [TestMethod]
        public void TestStepCreated()
        {
            string expected = "<7 aID=\"0\" dID=\"0\" next=\"8\" >" +
                "<_float standInDoorway=\"0\" delayAfterDoor=\"0\" />" +
                "<_bool cameraTarget=\"False\" completeOnceOnNavmesh=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[WalkThroughDoorStep]]></_type>" +
                "</_string>" +
            "</7>".WhitespaceCleanupXML();

            var step = MoveThroughDoorStep.CreateFromInputString("Joey enter Door", new MockEntitySupplier());
            Assert.AreEqual(expected, step.ToXML(7, false).ToString().ConvertValidXMLToORK());

        }

        [TestMethod]
        public void TestRespectsOnceOnNavmesh()
        {
            string expected = "<7 aID=\"0\" dID=\"0\" next=\"8\" >" +
                "<_float standInDoorway=\"0\" delayAfterDoor=\"0\" />" +
                "<_bool cameraTarget=\"False\" completeOnceOnNavmesh=\"True\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[WalkThroughDoorStep]]></_type>" +
                "</_string>" +
            "</7>".WhitespaceCleanupXML();

            var step = MoveThroughDoorStep.CreateFromInputString("Joey enter Door --asap", new MockEntitySupplier());
            Assert.AreEqual(expected, step.ToXML(7, false).ToString().ConvertValidXMLToORK());

        }
    }
}

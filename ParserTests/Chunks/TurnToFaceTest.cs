using System;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests.Chunks
{
    [TestClass]
    public class TurnToFaceTest
    {
        [TestMethod]
        public void TestXMLIsGenerated()
        {
            string expected = "<4 next=\"-1\" >" +
            "<_bool toObject=\"True\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[GalvosTurnTo]]></_type>" +
                "</_string>" +
                "<rotateObject type=\"0\" aID=\"0\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</rotateObject>" +
                "<targetObject type=\"0\" aID=\"0\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</targetObject>" +
            "</4>".WhitespaceCleanupXML();

            var step = TurnToFaceStep.CreateFromInputString("Joey turnto Lucy", new MockEntitySupplier());
            Assert.AreEqual(expected, step.ToXML(4, true).ToString().ConvertValidXMLToORK());
        }

        [TestMethod]
        public void TestStepBuilderBuildsStep()
        {
            StepBuilder sb = new StepBuilder(new MockEntitySupplier());
            BaseStep step = sb.BuildStep("Joey turnto Painting");
            Assert.AreEqual(typeof(TurnToFaceStep), step.GetType());
        }
    }
}

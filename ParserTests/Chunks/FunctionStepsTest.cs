using System;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class FunctionStepsTests
    {
        [TestMethod]
        public void NextCameraStepTest()
        {
            string expected = "<8 next=\"9\" >" +
                "<_float timeBetween=\"0\" />" +
                "<_bool isStatic=\"False\" waitBetween=\"True\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<name><![CDATA[CutsceneStarter]]></name>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[CallFunctionStep]]></_type>" +
                "</_string>" +
                "<onObject type=\"0\" aID=\"2\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</onObject>" +
                "<method>" +
                    "<_string>" +
                        "<functionName><![CDATA[AdoptNextCamera]]></functionName>" +
                    "</_string>" +
                    "<_subarrays>" +
                        "<parameter>" +
                        "</parameter>" +
                    "</_subarrays>" +
                "</method>" +
            "</8>".WhitespaceCleanupXML();

            StepBuilder sb = new StepBuilder(new MockEntitySupplier(2));
            var step = sb.BuildStep("Starter nextcam");
            Assert.AreEqual(expected, step.ToXML(8, false).ToString().ConvertValidXMLToORK());
        }
    }
}

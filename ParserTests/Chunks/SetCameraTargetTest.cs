using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserTests
{
    [TestClass]
    public class SetCameraTargetTest
    {
        [TestMethod]
        public void TestSetCameraTargetProducesCorrectOutput()
        {
            string expected = "<6 next=\"7\">" +
                "<_bool reset=\"False\" ownControlTargetTransition=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[CameraControlTargetStep]]></_type>" +
                "</_string>" +
                "<onObject type=\"0\" aID=\"1\" wID=\"0\" pID=\"0\" pID2=\"-1\">" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\">" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</onObject>" +
            "</6>";

            var step = new SetCameraTargetStep();

            Assert.AreEqual(expected, step.ToXML(6, false).ToString().ConvertValidXMLToORK());
        }

    }
}

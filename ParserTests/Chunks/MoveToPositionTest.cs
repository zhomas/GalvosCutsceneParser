using System;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class MoveToPositionTest
    {
        [TestMethod]
        public void TestXMLIsRendered()
        {
            string expected = "<7 next=\"-1\">" +
				"<_bool WaitUntilComplete=\"True\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[SetMoveAITargetStep]]></_type>" +
                "</_string>" +
                "<movingObject type=\"0\" aID=\"0\" wID=\"0\" pID=\"0\" pID2=\"-1\">" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\">" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</movingObject>" +
                "<moveSpeed type=\"3\">" +
                    "<_float speed=\"32\" />" +
                "</moveSpeed>" +
                "<targetObject type=\"0\" aID=\"1\" wID=\"0\" pID=\"0\" pID2=\"-1\">" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\">" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</targetObject>" +
            "</7>".WhitespaceCleanupXML();

            var step = new MoveToPositionStep(new CutsceneEntity(0));
            string actual = step.ToXML(7, true).ToString().ConvertValidXMLToORK().WhitespaceCleanupXML();

            Assert.AreEqual(expected, actual);
        }
    }
}

using System;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class TurnVectorStepTest
    {
        [TestMethod]
        public void TestCorrectXMLGenerated()
        {
            string expected = "<8 direction=\"0\" next=\"-1\" >" +
                "<_bool active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[GalvosTurnVector]]></_type>" +
                "</_string>" +
                "<usedObject type=\"0\" aID=\"0\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</usedObject>" +
            "</8>";

            var step = new TurnVectorStep(new CutsceneEntity(0), TurnVectorStep.Direction.North);
            Assert.AreEqual(expected, step.ToXML(8, true).ToString().ConvertValidXMLToORK());
        }

        [TestMethod]
        public void InputLineParsingTest()
        {
            string north = "Joey turn North";
            string south = "Joey turn south";
            string east = "Joey turn east";
            string west = "Joey turn west";

            var supplier = new MockEntitySupplier();

            Assert.AreEqual(TurnVectorStep.Direction.North, TurnVectorStep.CreateFromInputString(north, supplier).LookDirection);
            Assert.AreEqual(TurnVectorStep.Direction.South, TurnVectorStep.CreateFromInputString(south, supplier).LookDirection);
            Assert.AreEqual(TurnVectorStep.Direction.East, TurnVectorStep.CreateFromInputString(east, supplier).LookDirection);
            Assert.AreEqual(TurnVectorStep.Direction.West, TurnVectorStep.CreateFromInputString(west, supplier).LookDirection);
        }
    }
}

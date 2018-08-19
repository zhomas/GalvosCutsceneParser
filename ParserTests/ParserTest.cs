using System;
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ParserTests
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void TestStepReplacement()
        {
            var parser = new Parser();
            List<BaseStep> steps = new List<BaseStep>()
            {
                new SpeechBubble(0, "Hi"),
                new SpeechBubble(1, "Wooo")
            };

            string xml =
            @"<a>" +
                "<b>" +
                    "<step>" +
                        "<cheese></cheese>" +
                    "</step>" +
                "</b>" +
            "</a>";

            var before = parser
                    .LoadEventXML(xml)
                    .Document;

            Assert.IsTrue(before.Descendants("cheese").Count() == 1);
            Assert.IsTrue(before.Descendants(Parser.XML_PREFIX + "0").Count() == 0);

            var after = parser
                .ReplaceXMLStepsWithGPLSteps(steps)
                .Document;

            Assert.IsTrue(after.Descendants("cheese").Count() == 0);
            Assert.IsTrue(after.Descendants(Parser.XML_PREFIX + "0").Count() > 0);
        }

        [TestMethod]
        public void TestInvalidORKXMLParsedOkay()
        {
            Parser parser = new Parser();

            string invalid = "<0 aID=\"5\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" /></0>";

            try
            {
                parser.LoadEventXML(invalid);
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void TestValidXMLCanBeConvertedToInvalid()
        {
            string valid = 
            "<" + Parser.XML_PREFIX + "0 aID=\"5\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_floatarrays>" + 
                    "< nodePosition " + Parser.XML_PREFIX + "x=\"38\" " + Parser.XML_PREFIX + "y=\"38\" />" +
                "</ _floatarrays > " +
            "</" + Parser.XML_PREFIX + "0>".WhitespaceCleanupXML();

            string invalid =
            "<0 aID=\"5\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_floatarrays>" +
                    "< nodePosition 38 38 />" +
                "</ _floatarrays > " +
            "</0>".WhitespaceCleanupXML();


            Assert.AreEqual(invalid, valid.ConvertValidXMLToORK());
        }

        [TestMethod]
        public void TestInvalidXMLCanBeConvertedToValid()
        {
            string valid =
            "<" + Parser.XML_PREFIX + "0 aID=\"5\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_floatarrays>" +
                    "< nodePosition " + Parser.XML_PREFIX + "x=\"38\" " + Parser.XML_PREFIX + "y=\"38\" />" +
                "</ _floatarrays > " +
            "</" + Parser.XML_PREFIX + "0>".WhitespaceCleanupXML();

            string invalid =
            "<0 aID=\"5\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_floatarrays>" +
                    "< nodePosition 38 38 />" +
                "</ _floatarrays > " +
            "</0>".WhitespaceCleanupXML();


            Assert.AreEqual(valid, invalid.ConvertORKToValidXML());
        }

        [TestMethod]
        public void TestSaniziterSanitizesXML()
        {
            string input = "<5 aID=\"0\" guiBoxID=\"0\" next=\"-1\" >" +
                 "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                 "<_floatarrays>" +
                     "<nodePosition 893 171 />" +
                 "</_floatarrays>" +
                 "<_string>" +
                     "<nodeName><![CDATA[]]></nodeName>" +
                     "<_type><![CDATA[SpeechBubbleStep]]></_type>" +
                 "</_string>" +
                 "<_stringarrays>" +
                     "<message>" +
                         "<0><![CDATA[Hello!]]></0>" +
                     "</message>" +
                 "</_stringarrays>" +
             "</5>";

            string a = input.WhitespaceCleanupXML().ConvertORKToValidXML();
            string b = input.ConvertORKToValidXML().WhitespaceCleanupXML();

            Assert.IsTrue(input.IndexOf("<5") == 0);
            Assert.IsTrue(a.IndexOf("<5") == -1);
            Assert.IsTrue(b.IndexOf("<5") == -1);

        }

        [TestMethod]
        public void TestStepsHaveCorrectIndeces()
        {
            var parser = new Parser();
            var step = new StepBuilder(new MockEntitySupplier());

            string gpl = TestHelpers.GetSampleGPL(0, 3);
            
            var steps = step.GetStepsFromInput(gpl);

            var testDoc = parser
                .LoadEventXML("<base><step></step></base>")
                .ReplaceXMLStepsWithGPLSteps(steps)
                .Document;

            var zeros = testDoc.Descendants("step").Elements(Parser.XML_PREFIX + "0");
            var ones = testDoc.Descendants("step").Elements(Parser.XML_PREFIX + "1");
            var twos = testDoc.Descendants("step").Elements(Parser.XML_PREFIX + "2");

            Assert.IsTrue(zeros.Count() == 1);
            Assert.IsTrue(ones.Count() == 1);
            Assert.IsTrue(twos.Count() == 1);
        }

        [TestMethod]
        public void TestStepsHaveCorrectNextSteps()
        {
            var parser = new Parser();
            var step = new StepBuilder(new MockEntitySupplier());

            string gpl = TestHelpers.GetSampleGPL(2, 3);
            var steps = step.GetStepsFromInput(gpl);

            var testDoc = parser
                .LoadEventXML("<base><step></step></base>")
                .ReplaceXMLStepsWithGPLSteps(steps)
                .Document;

            var zeros = testDoc.Descendants("step").Elements(Parser.XML_PREFIX + "0");
            var ones = testDoc.Descendants("step").Elements(Parser.XML_PREFIX + "1");
            var twos = testDoc.Descendants("step").Elements(Parser.XML_PREFIX + "2");

            Assert.IsTrue(zeros.First().Attribute("next").Value == "1");
            Assert.IsTrue(ones.First().Attribute("next").Value == "2");
            Assert.IsTrue(twos.First().Attribute("next").Value == "-1");
        }

    }
}

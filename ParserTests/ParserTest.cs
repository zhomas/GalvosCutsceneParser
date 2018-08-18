using System;
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using GalvosCutsceneParser;
using GalvosCutsceneParser.Chunks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void TestParserBuildsMultipleSteps()
        {
            Parser parser = new Parser();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Joey say \"Hello Good bean!\"");
            builder.AppendLine("Joey say \"Hello Good bean!\"");

            var result = parser.GetStepsFromInput(builder.ToString());

            Assert.AreEqual(typeof(SpeechBubble), result[0].GetType());
            Assert.AreEqual(typeof(SpeechBubble), result[1].GetType());

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestInvalidXMLCanBeMadeValid()
        {
            string invalid = "<0 aID=\"5\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" /></0>";


            string valid = invalid.ConvertORKToValidXML();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.InnerXml = valid;
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
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
    }
}

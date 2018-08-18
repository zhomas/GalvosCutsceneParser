using System;
using System.Text;
using System.Xml;
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

            var result = parser.ProcessInputString(builder.ToString());

            Assert.AreEqual(typeof(SpeechBubble), result[0].GetType());
            Assert.AreEqual(typeof(SpeechBubble), result[1].GetType());

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestInvalidXMLCanBeMadeValid()
        {
            string invalid = "<0 aID=\"5\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" /></0>";


            string valid = invalid.SanitizeORKXml();

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
    }
}

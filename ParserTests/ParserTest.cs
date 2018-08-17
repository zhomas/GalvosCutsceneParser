using System;
using System.Text;
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
    }
}

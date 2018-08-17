using System;
using GalvosCutsceneParser;
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

            string input = "Joey say \"Hello Good bean!\"" +
                "Joey say \"Hello Good bean!\"";

            parser.ProcessInputString(input);
        }
    }
}

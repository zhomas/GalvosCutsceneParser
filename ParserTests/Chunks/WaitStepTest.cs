using System;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests.Chunks
{
    [TestClass]
    public class WaitStepTest
    {
        [TestMethod]
        public void TestWaitStepProducesCorrectXML()
        {
            string expected = "<2 next=\"3\">" +
				"<_bool random=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
				"<_string>" +
					"<nodeName><![CDATA[]]></nodeName>" +
					"<_type><![CDATA[WaitStep]]></_type>" +
				"</_string>" +
				"<time type=\"0\" origin=\"1\" multiVariableUseType=\"0\" formulaID=\"0\" rounding=\"0\">" +
					"<_float initialValue=\"0\" offset=\"0\" value=\"1\" value2=\"0\" />" +
					"<_bool useObject=\"True\" isInt=\"False\" isUTC=\"False\" />" +
					"<_string>" +
						"<objectID><![CDATA[]]></objectID>" +
						"<name><![CDATA[]]></name>" +
					"</_string>" +
				"</time>" +
			"</2>";

            expected = expected.ConvertORKToValidXML().WhitespaceCleanupXML();

            WaitStep waitStep = new WaitStep(1000);

            string actual = waitStep.ToXML(2, false).ToString().WhitespaceCleanupXML();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestTimeIsParsedFromInputString()
        {
            int time = WaitStep.ParseMillisecondsFromInputLine("wait 1000");
            Assert.AreEqual(1000, time);
        }

        [TestMethod]
        public void TestValueIsParsed()
        {
            var ws = new WaitStep(150);
            Assert.AreEqual(.15f, ws.Seconds);
        }
    }
}

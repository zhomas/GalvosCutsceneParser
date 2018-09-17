using System;
using System.Xml;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class SpeechBubbleTest
    {
        [TestMethod]
        public void TestSpeechBubbleProducesValidOutputFull()
        {
            var sb = new SpeechBubble("Joey say \"Hello!\"", new MockEntitySupplier());
            string actual = sb.ToXML(5, true).ToString().WhitespaceCleanupXML();

            Assert.AreEqual(("<5 aID=\"0\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[SpeechBubbleStep]]></_type>" +
                "</_string>" +
                "<_stringarrays>" +
                    "<message>" +
                        "<0><![CDATA[Hello!]]></0>" +
                    "</message>" +
                "</_stringarrays>" +
            "</5>").ConvertORKToValidXML().WhitespaceCleanupXML(), actual);
        }

        [TestMethod]
        public void TestSpeechBubbleRespectsNextIndex()
        {
            var sb = new SpeechBubble("Joey say \"Hello!\"", new MockEntitySupplier());
            string actual = sb.ToXML(5, true).ToString().WhitespaceCleanupXML();

            Assert.AreEqual(("<5 aID=\"0\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[SpeechBubbleStep]]></_type>" +
                "</_string>" +
                "<_stringarrays>" +
                    "<message>" +
                        "<0><![CDATA[Hello!]]></0>" +
                    "</message>" +
                "</_stringarrays>" +
            "</5>").ConvertORKToValidXML().WhitespaceCleanupXML(), actual);
        }

        [TestMethod]
        public void TestSpeechBubbleMessage()
        {
            var sb = new SpeechBubble("Joey say \"Suck My Balls!\"", new MockEntitySupplier());
            string expected = "<5 aID=\"0\" guiBoxID=\"0\" next=\"-1\">" +
                "<_bool cameraTarget=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[SpeechBubbleStep]]></_type>" +
                "</_string>" +
                "<_stringarrays>" +
                    "<message>" +
                        "<0><![CDATA[Suck My Balls!]]></0>" +
                    "</message>" +
                "</_stringarrays>" +
            "</5>";

            expected = expected.ConvertORKToValidXML().WhitespaceCleanupXML();

            string actual = sb.ToXML(5, true).ToString().WhitespaceCleanupXML();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLongStringsAreParsedCorrectly()
        {

        }
    }
}

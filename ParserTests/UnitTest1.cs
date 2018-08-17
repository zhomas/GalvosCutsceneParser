using System;
using System.Xml;
using GalvosCutsceneParser;
using GalvosCutsceneParser.Chunks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSpeechBubbleProducesValidOutput()
        {
            var parser = new Parser();

            // Given input 
            // Joey say 'hello'

            // Produces correct XML
            XmlDocument expected = new XmlDocument();
            
            SpeechBubble sb = new SpeechBubble(0, "Hello!");

            expected.LoadXml("<5 aID=\"0\" guiBoxID=\"0\" next=\"-1\" >" +
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
			"</5>");
            
            Assert.AreEqual(expected, sb.ToXML());

        }
    }
}

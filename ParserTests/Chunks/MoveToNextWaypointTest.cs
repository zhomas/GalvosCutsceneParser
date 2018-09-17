using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserTests
{
    [TestClass]
    public class MoveToNextWaypointTest
    {
        [TestMethod]
        public void TestMoveToNextWaypoint()
        {
            string expected = "<8 next=\"9\" >" +
                "<_bool WaitForMovementComplete=\"True\" MakeCameraTarget=\"False\" Instant=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[MoveToNextWaypointStep]]></_type>" +
                "</_string>" +
                "<movingObject type=\"0\" aID=\"0\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</movingObject>" +
                "<moveSpeed type=\"3\" >" +
                    "<_float speed=\"32\" />" +
                "</moveSpeed>" +
            "</8>".WhitespaceCleanupXML();

            var step = MoveToNextWaypointStep.CreateFromInputString("Joey => nextWP", new MockEntitySupplier());

            Assert.AreEqual(expected, step.ToXML(8, false).ToString().ConvertValidXMLToORK());
        }

        [TestMethod]
        public void TestNoWait()
        {
            string expected = "<8 next=\"9\" >" +
                "<_bool WaitForMovementComplete=\"False\" MakeCameraTarget=\"False\" Instant=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[MoveToNextWaypointStep]]></_type>" +
                "</_string>" +
                "<movingObject type=\"0\" aID=\"0\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</movingObject>" +
                "<moveSpeed type=\"3\" >" +
                    "<_float speed=\"32\" />" +
                "</moveSpeed>" +
            "</8>".WhitespaceCleanupXML();

    
            var step = MoveToNextWaypointStep.CreateFromInputString("Joey => nextWP --nowait", new MockEntitySupplier());

            Assert.AreEqual(expected, step.ToXML(8, false).ToString().ConvertValidXMLToORK());
        }
    }
}

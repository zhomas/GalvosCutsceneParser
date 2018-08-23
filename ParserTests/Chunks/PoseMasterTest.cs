using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserTests.Chunks
{
    [TestClass]
    public class PoseMasterTest
    {
        [TestMethod]
        public void TestOutputXML()
        {
            string expected = "<7 aID=\"0\" pose=\"0\" next=\"-1\">" +
                "<_float Duration=\"2\" />" +
                "<_bool indefinite=\"True\" Remove=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[PosemasterStep]]></_type>" +
                "</_string>" +
            "</7>";

            var step = new PoseMasterStep(new CutsceneEntity(0), PoseMasterStep.PosemasterPose.Think);

            Assert.AreEqual(expected, step.ToXML(7, true).ToString().ConvertValidXMLToORK());
        }

        [TestMethod]
        public void TestCreatedFromInputString()
        {
            var step = PoseMasterStep.CreateFromInputString(new CutsceneEntity(0), "Joey celebrate");
            Assert.AreEqual(PoseMasterStep.PosemasterPose.Celebrate, step.Pose);
        }
    }
}

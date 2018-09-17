using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ParserTests
{
    [TestClass]
    public class SetCameraTargetTest
    {
        [TestMethod]
        public void TestSetCameraTargetProducesCorrectOutput()
        {
            string expected = "<6 next=\"7\" >" +
                "<_float cameraDistance=\"0\" />" +
				"<_bool reset=\"False\" cameraRotation=\"False\" showCameraDistance=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[CameraControlTargetStep]]></_type>" +
                "</_string>" +
                "<onObject type=\"0\" aID=\"1\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</onObject>" +
            "</6>";

            var step = new SetCameraTargetStep(new CutsceneEntity(1), Vector3.zero, 0f);

            Assert.AreEqual(expected, step.ToXML(6, false).ToString().ConvertValidXMLToORK());
        }

        [TestMethod]
        public void TestSetCameraTargetCanHaveRotation()
        {
            string expected =
            "<5 next=\"-1\" >" +
                "<_float cameraDistance=\"0\" />" +
                "<_bool reset=\"False\" cameraRotation=\"True\" showCameraDistance=\"False\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_floatarrays>" +
                    "<cameraRotationEuler 80 0 0 />" +
                "</_floatarrays>" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[CameraControlTargetStep]]></_type>" +
                "</_string>" +
                "<onObject type=\"0\" aID=\"0\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</onObject>" +
            "</5>";

            var step = new SetCameraTargetStep(new CutsceneEntity(0), new Vector3(80, 0, 0), 0f);

            Assert.AreEqual(expected, step.ToXML(5, true).ToString().ConvertValidXMLToORK());
        }

        [TestMethod]
        public void TestSetCameraTargetCanHaveDistance()
        {
            string expected =
            "<5 next=\"-1\" >" +
                "<_float cameraDistance=\"800\" />" +
                "<_bool reset=\"False\" cameraRotation=\"False\" showCameraDistance=\"True\" active=\"True\" overrideNodeName=\"False\" />" +
                "<_string>" +
                    "<nodeName><![CDATA[]]></nodeName>" +
                    "<_type><![CDATA[CameraControlTargetStep]]></_type>" +
                "</_string>" +
                "<onObject type=\"0\" aID=\"0\" wID=\"0\" pID=\"0\" pID2=\"-1\" >" +
                    "<_string>" +
                        "<childName><![CDATA[]]></childName>" +
                    "</_string>" +
                    "<objectKey type=\"0\" >" +
                        "<_string>" +
                            "<value><![CDATA[]]></value>" +
                        "</_string>" +
                    "</objectKey>" +
                "</onObject>" +
            "</5>";

            var step = new SetCameraTargetStep(new CutsceneEntity(0), new Vector3(0, 0, 0), 800f);

            Assert.AreEqual(expected, step.ToXML(5, true).ToString().ConvertValidXMLToORK());
        }

        [TestMethod]
        public void TestCreationFromInputStringDistance()
        {
            var step = SetCameraTargetStep.GetFromInputString("Joey camtarget --distance=800", new MockEntitySupplier());
            Assert.AreEqual(800f, step.Distance);
        }


    }
}

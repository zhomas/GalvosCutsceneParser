using System;
using System.Collections.Generic;
using GalvosCutsceneParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class AliasBuilderTest
    {
        [TestMethod]
        [ExpectedException(typeof(NoAliasException))]
        public void TestExceptionThrownIfNoAliasDefinition()
        {
            string input = TestHelpers.GetSampleGPL(0, 3);
            AliasBuilder aliasBuilder = new AliasBuilder(input);

            Assert.AreEqual(0, aliasBuilder.Entities.Count);
        }

        [TestMethod]
        public void TestEntityIsBuiltWithCorrectSyntax()
        {
            CutsceneEntity joey = AliasBuilder.BuildEntity("Joey = 0");
            CutsceneEntity lucy = AliasBuilder.BuildEntity("Lucy = 1");

            Assert.AreEqual("Joey", joey.Alias);
            Assert.AreEqual(0, joey.ID);

            Assert.AreEqual("Lucy", lucy.Alias);
            Assert.AreEqual(1, lucy.ID);
        }

        [TestMethod]
        [ExpectedException(typeof(BadAliasException))]
        public void TestExceptionThrownWithBadSyntax()
        {
            CutsceneEntity joey = AliasBuilder.BuildEntity("Joey 0");

            Assert.AreEqual("Joey", joey.Alias);
            Assert.AreEqual(0, joey.ID);
        }

        [TestMethod]
        [ExpectedException(typeof(BadAliasException))]
        public void TestExceptionThrownWhenWrongWayRound()
        {
            CutsceneEntity joey = AliasBuilder.BuildEntity("0 = Joey");

            Assert.AreEqual("Joey", joey.Alias);
            Assert.AreEqual(0, joey.ID);
        }

        [TestMethod]
        public void TestAliasListHasCorrectCount()
        {
            string gpl = TestHelpers.GetSampleGPL(2, 0);
            var aliasBuilder = new AliasBuilder(gpl);
            Assert.IsTrue(aliasBuilder.Entities.Count == 2);
        }
    }
}

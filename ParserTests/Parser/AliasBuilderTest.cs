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
        public void TestAliasBuilderFindsCorrectPortion()
        {
            string gpl = TestHelpers.GetSampleGPL(2);
            var aliasBuilder = new AliasBuilder();

            var s = aliasBuilder.GetAliasSectionOfInputText(gpl);
            Assert.AreEqual(2, s.Split('\n').Length);
        }

        [TestMethod]
        public void TestEntityIsBuilt()
        {
            var aliasBuilder = new AliasBuilder();
            BaseEntity entity = aliasBuilder.BuildEntity("Joey = 1");

            Assert.AreEqual("Joey", entity.Alias);
            Assert.AreEqual(1, entity.ID);
        }

        [TestMethod]
        public void TestAliasListHasCorrectCount()
        {
            string gpl = TestHelpers.GetSampleGPL(2);
            var aliasBuilder = new AliasBuilder();
            List<BaseEntity> list = aliasBuilder.GetEntitiesFromAliasText(gpl);
            Assert.IsTrue(list.Count == 2);
        }
    }
}

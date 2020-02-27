using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Data.Common
{
    [TestClass]
    public class DefinedEntityDataTests : AbstractClassTest<DefinedEntityData, NamedEntityData>
    {
        private class TestClass : DefinedEntityData
        {
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new TestClass();
        }

        [TestMethod]
        public void DefinitionTest()
        {
            var s = "AAAAA";
            Assert.AreNotEqual(s, obj.Definition);
            obj.Definition = s;
            Assert.AreEqual(s, obj.Definition);
        }
    }
} 
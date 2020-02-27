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
            IsNullableProperty(()=>obj.Definition, x =>obj.Definition = x, () =>"aaaaa");
        }
    }
} 
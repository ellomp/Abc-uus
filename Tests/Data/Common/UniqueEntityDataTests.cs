using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Data.Common
{
    [TestClass]
    public class UniqueEntityDataTests : AbstractClassTest<UniqueEntityData, PeriodData>
    {
        private class TestClass : UniqueEntityData
        {
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new TestClass();
        }

        [TestMethod]
        public void IdTest()
        {
            IsNullableProperty(() => obj.Id, x => obj.Id = x);
        }
    }
}
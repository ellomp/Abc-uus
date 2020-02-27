using System;
using System.Security.Cryptography.X509Certificates;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Data.Common
{
    [TestClass]
    public class PeriodDataTests : AbstractClassTest<PeriodData, object>     
    {
        private class TestClass : PeriodData
        {
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new TestClass();
        }
        [TestMethod]
        public void ValidFromTest()
        {
            //mul on funkt ja see f annab mulle kttte valid fromi ja ss on mu funt 
            //et kui ma selle argumendi x annan siis se omastatakse validfrom väärtusele.
            IsNullableProperty(() => obj.ValidFrom, x => obj.ValidFrom = x, () => DateTime.Now);
        }

        [TestMethod]
        public void ValidToTest()
        {
            IsNullableProperty(() => obj.ValidTo, x => obj.ValidTo = x, () => DateTime.Now);

        }

       
    }
}
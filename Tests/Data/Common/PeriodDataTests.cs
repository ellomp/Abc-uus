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
            isNullableProperty(() => obj.ValidFrom, x => obj.ValidFrom = x, () => DateTime.Now);
        }

        [TestMethod]
        public void ValidToTest()
        {
            isNullableProperty(() => obj.ValidTo, x => obj.ValidTo = x, () => DateTime.Now);

        }

        private static void isNullableProperty(Func<DateTime?> get, Action<DateTime?> set, Func<DateTime> rnd) //get set ja random on funktsioonid
        {
            var d = rnd();
            Assert.AreNotEqual(d, get());
            set(d);
            Assert.AreEqual(d, get());
            set(null);
            Assert.IsNull(get());
            }
    }
}
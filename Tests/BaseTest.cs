using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public abstract class BaseTest<TClass, TBaseClass>
    {
        protected TClass obj;
        protected Type type;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            type = typeof(TClass);
        }

        [TestMethod]
        public void InInheritedTest()
        {
            Assert.AreEqual(typeof(TBaseClass), type.BaseType);
        }
        protected static void IsNullableProperty<T>(Func<T> get, Action<T> set)
            //get set ja random on funktsioonid
        {
            var d = rnd();
            Assert.AreNotEqual(d, get());
            set(d);
            Assert.AreEqual(d, get());
            //set(null);
            //Assert.IsNull(get());
        }
    }
}
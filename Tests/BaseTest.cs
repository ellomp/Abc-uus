using System;
using Abc.Aids;
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
            IsProperty(get, set);
            var d = (T)GetRandom.Value(typeof(T));
            set(default);
            Assert.IsNull(get());
        }

        protected static void IsProperty<T>(Func<T> get, Action<T> set)
            //get set ja random on funktsioonid
        {
            var d = (T)GetRandom.Value(typeof(T));
            Assert.AreNotEqual(d, get());
            set(d);
            Assert.AreEqual(d, get());
        }
    }
}
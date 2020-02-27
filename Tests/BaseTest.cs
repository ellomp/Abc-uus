using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public abstract class BaseTest<TClass, TBaseClass> where TClass: new() //siin ütlen ka ära, et TClassil PEAB olema tühjade argumentidega constructor
    {
        [TestMethod]
        public void CanCreateTest() //tavaliselt esimene test on alati cancreate
        {
            Assert.IsNotNull(new TClass()); //pean saama teda luua
        }

        [TestMethod]
        public void InInheritedTest() //peab olema päritav
        {
            Assert.AreEqual(typeof(TBaseClass), new TClass().GetType().BaseType);
        }
    }
}
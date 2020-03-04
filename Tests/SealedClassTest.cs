using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests
{
    public abstract class SealedClassTest<TClass, TBaseClass> :ClassTest<TClass, TBaseClass> where TClass: new()
    //siin ütlen ka ära, et TClassil PEAB olema tühjade argumentidega constructor
    {
       [TestMethod]
        public void IsSealedTest()
        {
            Assert.IsTrue(type.IsSealed);
        }
    }

    //abstract klassi ei saa createda
    //abstract klasside puhul ei saa obj luua.
}
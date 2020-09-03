using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Spec.Constructors
{
    public partial class Constructors
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_NoDefaultConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(Invoke.Constructor());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_NoBogusConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(
                Invoke.Constructor(typeof(int), typeof(string)));
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_NoBogusValuesConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(
                Invoke.Constructor( 1, "test"));
        }



        [TestMethod]
        public void Injection_SelectByValueTypes()
        {
            Container.RegisterType<TypeWithMultipleCtors>(Invoke.Constructor(Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(string)),
                Inject.Parameter(typeof(int))));
            Assert.AreEqual(TypeWithMultipleCtors.Three, Container.Resolve<TypeWithMultipleCtors>().Signature);
        }
    }
}

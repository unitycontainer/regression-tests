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
        public void Injection_InjectRegistered()
        {
            // Arrange
            Container.RegisterInstance(_data);

            // Act
            var value = Container.Resolve<Foo>();

            // Verify
            Assert.AreSame(_data, value.Data);
        }

        [TestMethod]
        public void Injection_InjectionCtor()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterType<Foo>(new InjectionConstructor(_override));

            // Act
            var value = Container.Resolve<Foo>();

            // Verify
            Assert.AreSame(_override, value.Data);
        }
    }
}

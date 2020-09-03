using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Spec.Constructors
{
    public partial class Constructors
    {
        [TestMethod]
        public virtual void Override_CtorParameter()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionConstructor(_data));

            // Act
            var value = Container.Resolve<Service>(Override.Dependency<string>(_override));

            // Verify
            Assert.AreSame(_data, value.Data);
        }
    }


    public partial class Constructors_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void Override_CtorParameter()
            => base.Override_CtorParameter();
    }

}

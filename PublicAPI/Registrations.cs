using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Container.Interfaces
{
    public partial class UnityContainerAPI
    {
        [TestMethod]
        public void RegistrationsTest()
        {
            Assert.IsNotNull(Container.Registrations);
        }

        [TestMethod]
        public void Registrations_ToEnumerable()
        {
#if NET45
            Assert.IsNotNull(Container.Registrations as IEnumerable<ContainerRegistration>);
#else
            Assert.IsNotNull(Container.Registrations as IEnumerable<IContainerRegistration>);
#endif
        }

        [TestMethod]
        public void Registrations_ToArray()
        {
            var array = Container.Registrations.ToArray();

            Assert.IsNotNull(array);
            Assert.AreNotEqual(0, array.Length);
        }


        [TestMethod]
        public virtual void Registrations_Contains_IUnityContainer()
        {
            // Act
            var registration = Container.Registrations.FirstOrDefault(r => typeof(IUnityContainer) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
        }

    }
}

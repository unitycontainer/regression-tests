using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Breaking.Changes
{
    [TestClass]
    public class RegistrationsCollection
    {
        #region Scaffolding

        IUnityContainer Container;
        ContainerRegistrationComparer EqualityComparer = new ContainerRegistrationComparer();
        IService Instance = new Service();

        [TestInitialize]
        public void TestInitialize() => Container = new UnityContainer();

        #endregion


        /// <summary>
        /// ContainerRegistration <see cref="ContainerRegistration.LifetimeManager"/> 
        /// should never be null.
        /// </summary>
        [TestMethod]
        public void Registrations_Manager_Never_Null()
        {
            // Arrange
            Container.RegisterType<Service>();

            // Act
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration.LifetimeManager);
        }


        /// <summary>
        /// Property <see cref="ContainerRegistration.MappedToType"/> changes from 
        /// being null to <see cref="ContainerRegistration.RegisteredType"/> if no mapping 
        /// is provided
        /// </summary>
        [TestMethod]
        public virtual void Registrations_InstanceType_MappedTo()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), Instance);

            // Act
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);

            // Validate
            Assert.AreEqual(Instance.GetType(), registration.MappedToType);
        }


        /// <summary>
        /// Test case where collection is changed while being enumerated
        /// </summary>
        /// <remarks>
        /// Acceding to documentation it should throw if collection changes.
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Registrations_Enumerable_IsImmutable()
        {
            Container.RegisterType<IService, Service>()
                     .RegisterType<IService, Service>("second");

            var enumerable = Container.Registrations;

            var registrations1 = enumerable.ToArray();

            Container.RegisterType<Service>()
                     .RegisterType<Service>("second");

            var registrations2 = enumerable.ToArray();

            Assert.IsTrue(registrations1.SequenceEqual(registrations2, EqualityComparer));
            Assert.IsFalse(registrations1.SequenceEqual(Container.Registrations, EqualityComparer));
        }
    }

    #region Test Data

    public interface IService
    { }

    public class Service : IService
    { }

#if NET46
    public class ContainerRegistrationComparer : IEqualityComparer<IContainerRegistration>
    {
        public bool Equals(IContainerRegistration x, IContainerRegistration y)
        {
            return x.RegisteredType == y.RegisteredType && x.Name == y.Name;
        }

        public int GetHashCode(IContainerRegistration obj)
        {
            return obj.RegisteredType.GetHashCode() * 17 +
                    obj.Name?.GetHashCode() ?? 0;
        }
    }
#else
    public class ContainerRegistrationComparer : IEqualityComparer<ContainerRegistration>
    {
        public bool Equals(ContainerRegistration x, ContainerRegistration y)
        {
            return x.RegisteredType == y.RegisteredType && x.Name == y.Name;
        }

        public int GetHashCode(ContainerRegistration obj)
        {
            return obj.RegisteredType.GetHashCode() * 17 +
                   obj.Name?.GetHashCode() ?? 0;
        }
    }
#endif

    #endregion
}

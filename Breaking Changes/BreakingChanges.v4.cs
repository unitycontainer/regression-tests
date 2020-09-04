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
    public class BreakingChangesV4
    {
        #region Scaffolding

        IUnityContainer Container;
        readonly ContainerRegistrationComparer EqualityComparer = new ContainerRegistrationComparer();

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
            IService Instance = new Service();

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
        public void Registrations_Enumerable_Is_Immutable()
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

        /// <summary>
        /// This method demonstrates a case where <see cref="Type"/> mapping created
        /// with <see cref="InjectionConstructor"/> failed to build. 
        /// </summary>
        /// <remarks>
        /// When any injection members are present, the mapping should never redirect to 
        /// other registrations, instead it should create its own pipeline using 
        /// provided <see cref="InjectionMember"/> instances.
        /// </remarks>
        [TestMethod]
        public void Always_Build_If_Injected()
        {
            var instance = new Service<object>();

            // Arrange
            Container.RegisterType(typeof(IService<>), typeof(Service<>), new InjectionConstructor(typeof(object)))
                     .RegisterInstance(instance);
            // Act
            var value = Container.Resolve<IService<object>>();

            // Validate
            Assert.IsNotNull(value);
            // Should never be the same
            Assert.AreNotSame(instance, value);
        }

        /// <summary>
        /// Method demonstrating a case when dependency name is ignored
        /// </summary>
        /// <remarks>
        /// Annotation like this: CtorWithAttributedParams([Dependency(DependencyName)] string first)
        /// should resolve dependency with following contract: Type = string, Name = DependencyName
        /// </remarks>
        [TestMethod]
        public void DependencyNameIsNotIgnored()
        {
            // Setup
            Container.RegisterType(typeof(CtorWithAttributedParams), new InjectionConstructor(typeof(string)));

                   // Register two strings with right name and no name
            Container.RegisterInstance(typeof(string), null, "wrong_value", new ContainerControlledLifetimeManager())
                   // When resolving it should look for this name: CtorWithAttributedParams.DependencyName
                     .RegisterInstance(typeof(string), CtorWithAttributedParams.DependencyName, "right_value", new ContainerControlledLifetimeManager());

            // Act
            var result = Container.Resolve<CtorWithAttributedParams>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual("right_value", result.Signature);
        }

    }

    #region Test Data

    public interface IService
    { }

    public class Service : IService
    { }

    public interface IService<T>
    {
        string Id { get; }
    }

    public class Service<T> : IService<T>
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public Service()
        {
        }

        public Service(object inject)
        {
            Id = $"Ctor injected with: { inject.GetHashCode() }";
        }
    }

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

    public class CtorWithAttributedParams
    {
        public const string DependencyName = "dependency_name";

        public string Signature { get; }

        /// <summary>
        /// Constructor with NAMED dependency
        /// </summary>
        /// <param name="first">Parameter marked for injection with named (Name == DependencyName) dependency </param>
        /// <param name="second">not impertant</param>
        public CtorWithAttributedParams([Dependency(DependencyName)] string first)
        {
            Signature = first;
        }
    }

    #endregion
}

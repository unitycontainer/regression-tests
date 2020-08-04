using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Regression.Tests;
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

namespace Container.Registrations
{
    [TestClass]
    public class RegistrationsTests
    {
        IUnityContainer Container;
        ContainerRegistrationComparer EqualityComparer = new ContainerRegistrationComparer();

        [TestInitialize]
        public void TestInitialize() => Container = new UnityContainer();


        [TestMethod]
        public void IsNotNull()
        {
            Assert.IsNotNull(Container.Registrations);
        }

        [TestMethod]
        public void ContainerIncludesItselfUnderRegistrations()
        {
            Assert.IsNotNull(Container.Registrations.Where(r => r.RegisteredType == typeof(IUnityContainer)).FirstOrDefault());
        }

        [TestMethod]
        public void ProperContainerInHierarchies()
        {
            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            var root = Container.Registrations.Single(r => r.RegisteredType == typeof(IUnityContainer));
            var level1 = child1.Registrations.Single(r => r.RegisteredType == typeof(IUnityContainer));
            var level2 = child2.Registrations.Single(r => r.RegisteredType == typeof(IUnityContainer));
        }

        [TestMethod]
        public void RepeatableEnumerations()
        {
            Container.RegisterType<ILogger, MockLogger>()
                     .RegisterType<ILogger, MockLogger>("second");

            var registrations1 = (from r in Container.Registrations
                                  where r.RegisteredType == typeof(ILogger)
                                  select r).ToArray();

            var registrations2 = (from r in Container.Registrations
                                  where r.RegisteredType == typeof(ILogger)
                                  select r).ToArray();

            Assert.AreEqual(2, registrations1.Length);
            Assert.IsTrue(registrations1.Any(r => r.Name == null));
            Assert.IsTrue(registrations1.Any(r => r.Name == "second"));

            Assert.AreEqual(2, registrations2.Length);
            Assert.IsTrue(registrations2.Any(r => r.Name == null));
            Assert.IsTrue(registrations2.Any(r => r.Name == "second"));

            Assert.IsTrue(registrations1.SequenceEqual(registrations2, EqualityComparer));
        }

        [TestMethod]
        public void ReUseEnumerable()
        {
            Container.RegisterType<ILogger, MockLogger>()
                     .RegisterType<ILogger, MockLogger>("second");

            var child = Container.CreateChildContainer()
                                 .RegisterType<MockLogger>()
                                 .RegisterType<MockLogger>("second");

            var enumerable = child.Registrations;

            var registrations1 = enumerable.ToArray();
            var registrations2 = enumerable.ToArray();

            Assert.IsTrue(registrations1.SequenceEqual(registrations2, new ContainerRegistrationComparer()));

        }

        [TestMethod]
        public void NewRegistrationsShowUpInRegistrationsSequence()
        {
            Container.RegisterType<ILogger, MockLogger>()
                     .RegisterType<ILogger, MockLogger>("second");

            var registrations = (from r in Container.Registrations
                                 where r.RegisteredType == typeof(ILogger)
                                 select r).ToList();

            Assert.AreEqual(2, registrations.Count);

            Assert.IsTrue(registrations.Any(r => r.Name == null));
            Assert.IsTrue(registrations.Any(r => r.Name == "second"));
        }

        [TestMethod]
        public void TypeMappingShowsUpInRegistrationsCorrectly()
        {
            Container.RegisterType<ILogger, MockLogger>();

            var registration =
                (from r in Container.Registrations where r.RegisteredType == typeof(ILogger) select r).First();
            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void NonMappingRegistrationShowsUpInRegistrationsSequence()
        {
            Container.RegisterType<MockLogger>();
            var registration = (from r in Container.Registrations
                                where r.RegisteredType == typeof(MockLogger)
                                select r).First();

            Assert.AreSame(registration.RegisteredType, registration.MappedToType);
            Assert.IsNull(registration.Name);
        }

        [TestMethod]
        public void RegistrationsInParentContainerAppearInChild()
        {
            Container.RegisterType<ILogger, MockLogger>();
            var child = Container.CreateChildContainer();

            var registration =
                (from r in child.Registrations where r.RegisteredType == typeof(ILogger) select r).First();

            Assert.AreSame(typeof(MockLogger), registration.MappedToType);
        }

        [TestMethod]
        public void RegistrationsInChildContainerDoNotAppearInParent()
        {
            var child = Container.CreateChildContainer()
                .RegisterType<ILogger, MockLogger>("named");

            var childRegistration = child.Registrations.Where(r => r.RegisteredType == typeof(ILogger)).First();
            var parentRegistration = Container.Registrations
                                              .Where(r => r.RegisteredType == typeof(ILogger))
                                              .Cast<object>()
                                              .FirstOrDefault();

            Assert.IsNull(parentRegistration);
            Assert.IsNotNull(childRegistration);
        }

        [TestMethod]
        public void DuplicateRegistrationsOnlyShowUpOnceInChild()
        {
            Container.RegisterType<ILogger, MockLogger>("one");

            var child = Container.CreateChildContainer()
                .RegisterType<ILogger, SpecialLogger>("one");

            var registrations = from r in child.Registrations
                                where r.RegisteredType == typeof(ILogger)
                                select r;

            Assert.AreEqual(1, registrations.Count());

            var childRegistration = registrations.First();
            Assert.AreSame(typeof(SpecialLogger), childRegistration.MappedToType);
            Assert.AreEqual("one", childRegistration.Name);
        }

        [TestMethod]
        public void WhenRegistrationsAreRetrievedFromAContainer()
        {
            Container.RegisterType<ILogger, MockLoggerWithCtor>(new InjectionConstructor("default"));
            Container.RegisterType<ILogger, MockLoggerWithCtor>("foo", new InjectionConstructor("foo"));

            var registrations = Container.Registrations;


            var @default = registrations.SingleOrDefault(c => c.Name == null &&
                                                           c.RegisteredType == typeof(ILogger));

            Assert.IsNotNull(@default);
            Assert.AreEqual(typeof(MockLoggerWithCtor), @default.MappedToType);

            var foo = registrations.SingleOrDefault(c => c.Name == "foo");

            Assert.IsNotNull(foo);
            Assert.AreEqual(typeof(MockLoggerWithCtor), @default.MappedToType);
        }

        [TestMethod]
        public void WhenRegistrationsAreRetrievedFromANestedContainer()
        {
            Container.RegisterType<ILogger, MockLoggerWithCtor>(new InjectionConstructor("default"));
            Container.RegisterType<ILogger, MockLoggerWithCtor>("foo", new InjectionConstructor("foo"));

            var child = Container.CreateChildContainer();

            child.RegisterType<ISpecialLogger, SpecialLoggerWithCtor>(new InjectionConstructor("default"));
            child.RegisterType<ISpecialLogger, SpecialLoggerWithCtor>("another", new InjectionConstructor("another"));

            var registrations = Container.Registrations;

            var mappedCount = child.Registrations.Where(c => c.MappedToType == typeof(SpecialLoggerWithCtor)).Count();

            Assert.AreEqual(2, mappedCount);
        }

        [TestMethod]
        public void WhenRegistrationsAreRetrievedFromAContainerByLifeTimeManager()
        {
            Container.RegisterType<ILogger, MockLoggerWithCtor>(new PerResolveLifetimeManager(), new InjectionConstructor("default"));
            Container.RegisterType<ILogger, MockLoggerWithCtor>("foo", new PerResolveLifetimeManager(), new InjectionConstructor("foo"));

            var registrations = Container.Registrations;

            var count = registrations.Where(c => c.LifetimeManager?.GetType() == typeof(PerResolveLifetimeManager)).Count();

            Assert.AreEqual(2, count);
        }

#if !NET45
        [Ignore]
        [TestMethod] // http://unity.codeplex.com/WorkItem/View.aspx?WorkItemId=6053
        public void ResolveAllWithChildDoesNotRepeatOverriddenRegistrations()
        {
            //var expected = new HashSet<string>(new[] { "string1", "string20", "string30" });

            //Container
            //    .RegisterInstance("str1", "string1")
            //    .RegisterInstance("str2", "string2");

            //var child = Container.CreateChildContainer()
            //    .RegisterInstance("str2", "string20")
            //    .RegisterInstance("str3", "string30");

            //var array = child.Registrations.Where(r => typeof(string) == r.RegisteredType)
            //                               .Select(r => (string)r.Instance)
            //                               .ToArray();

            //var actual = new HashSet<string>(array);

            //Assert.IsTrue(actual.SetEquals(expected));
        }
#endif

        #region Test Data

#if NET46
        private class ContainerRegistrationComparer : IEqualityComparer<IContainerRegistration>
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
        private class ContainerRegistrationComparer : IEqualityComparer<ContainerRegistration>
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
}

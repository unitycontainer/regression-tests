using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Resolution;
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Breaking.Changes
{
    public partial class BreakingChangesV4
    {
        [TestMethod]
        // https://github.com/unitycontainer/abstractions/issues/83
        public void Issue_Abstractions_83()
        {
            // Arrange
            Container.RegisterInstance(Name);
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property(nameof(ObjectWithThreeProperties.Property), Name));

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Container);
            Assert.IsNotNull(result.Property);
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/129
        public void Issue_Container_129()
        {
            var config = "production.sqlite";

            // Setup
            Container.RegisterType<IProctRepository, ProctRepository>("DEBUG");
            Container.RegisterType<IProctRepository, ProctRepository>("PROD", new InjectionConstructor(config));

            // Act
            var ur = Container.Resolve<ProctRepository>();
            var qa = Container.Resolve<IProctRepository>("DEBUG");
            var prod = Container.Resolve<IProctRepository>("PROD");

            // Verify
            Assert.AreEqual(ur.Value, "default.sqlite");
            Assert.AreEqual(prod.Value, config);
            Assert.AreNotEqual(qa.Value, config);
        }

        [TestMethod]
        // https://github.com/unitycontainer/container/issues/140
        public void Issue_Container_140()
        {
            // Setup
            var noOverride = "default";
            var parOverride = "custom-via-parameteroverride";
            var depOverride = "custom-via-dependencyoverride";

            Container.RegisterType<Foo>(new InjectionConstructor(noOverride));
            // Act
            var defaultValue = Container.Resolve<Foo>().ToString();
            var depValue = Container.Resolve<Foo>(Override.Dependency<string>(depOverride)).ToString();
            var parValue = Container.Resolve<Foo>(Override.Parameter<string>("dependency", parOverride))
                                       .ToString();
            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(parOverride, parValue);
            Assert.AreSame(noOverride, depValue);
        }

        [TestMethod]
        public void Issue_Unity_164()
        {
            Container.RegisterType<ILogger, MockLogger>();
            var foo2 = new MockLogger();

            Container.RegisterFactory<ILogger>(x => foo2);
            var result = Container.Resolve<ILogger>();

            Assert.AreSame(result, foo2);
        }

        [TestMethod]
        public void Issue_Unity_88()
        {
            using (var unityContainer = new UnityContainer())
            {
                unityContainer.RegisterInstance(true);
                unityContainer.RegisterInstance("true", true);
                unityContainer.RegisterInstance("false", false);

                var resolveAll = unityContainer.ResolveAll(typeof(bool));
                Assert.IsNotNull(resolveAll.Select(o => o.ToString()).ToArray());
            }
        }

        [TestMethod]
        public void Issue_Unity_35()
        {
            var container = Container;

            container.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());
            IService logger = container.Resolve<IService>();

            Assert.IsNotNull(logger);
            Assert.AreSame(container.Resolve<IService>(), logger);

            container.RegisterType<Service>(new TransientLifetimeManager());

            Assert.AreSame(container.Resolve<IService>(), logger);
        }

        [TestMethod]
        public void Issue_Unity_154_Fail()
        {
            Container.RegisterType<OtherService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, OtherService>();
            Container.RegisterType<IOtherService, OtherService>(new InjectionConstructor(Container));

            Assert.AreNotSame(Container.Resolve<IService>(),
                              Container.Resolve<IOtherService>());

            Assert.AreSame(Container.Resolve<IService>(),
                           Container.Resolve<OtherService>());
        }
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        // https://github.com/unitycontainer/container/issues/149
        public void Issue_149()
        {
            Container.RegisterInstance("123");
            var instance = Container.Resolve<Test_Class>(new DependencyOverride<string>(new OptionalParameter<string>()));
            Assert.AreEqual("123", instance.Field);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        // https://github.com/unitycontainer/container/issues/149
        public void Issue_149_Negative()
        {
            // BUG: StackOverflow happens here since 5.9.0
            var instance = Container.Resolve<Test_Class>(new DependencyOverride<string>(new OptionalParameter<string>()));
            Assert.IsNull(instance.Field);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        // https://github.com/unitycontainer/container/issues/119
        public void Issue_119()
        {
            // Setup
            Container.RegisterType<IInterface, Class1>();
            Container.RegisterType<IInterface, Class1>(nameof(Class1));
            Container.RegisterType<IInterface, Class2>(nameof(Class2));
            Container.RegisterType<IEnumerable<IInterface>, IInterface[]>();
            Container.RegisterType<ATestClass>();

            // Act
            var instance = Container.Resolve<ATestClass>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(2, instance.Value.Count());
        }
    }
}

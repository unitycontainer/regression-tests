﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Resolution
{
    public partial class Enumerables
    {
        [TestMethod]
        public void EnumerableOfRegistered()
        {
            // Arrange
            Container.RegisterType(typeof(Service), (string)null);
            Container.RegisterType(typeof(Service), "1");
            Container.RegisterType(typeof(Service), "2");
            Container.RegisterType(typeof(Service), "3");
            Service.Instances = 0;


            // Act
            var enumerable = Container.Resolve<IEnumerable<Service>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(4, array.Length);
        }

        [TestMethod]
        public void EnumerableOfRegisterMapping()
        {
            // Arrange
            Container.RegisterType(typeof(IService), typeof(Service), (string)null);
            Container.RegisterType(typeof(IService), typeof(Service), "1");
            Container.RegisterType(typeof(IService), typeof(Service), "2");
            Container.RegisterType(typeof(IService), typeof(Service), "3");
            Service.Instances = 0;


            // Act
            var enumerable = Container.Resolve<IEnumerable<IService>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(4, array.Length);
            Assert.AreEqual(4, Service.Instances);
        }

        [TestMethod]
        public void Unregistered()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>();

            // Act
            var enumerable = Container.Resolve<IEnumerable<IDictionary<IService, Service>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void Lazy()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, Service>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var enumerable = Container.Resolve<IEnumerable<Lazy<IService>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
            Assert.IsNotNull(array[0].Value);
            Assert.AreEqual(1, Service.Instances);
        }

        [TestMethod]
        public void Func()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, Service>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var enumerable = Container.Resolve<IEnumerable<Func<IService>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
        }

        [TestMethod]
        public void Lazy_Func()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var enumerable = Container.Resolve<IEnumerable<Lazy<Func<IService>>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
            Assert.IsNotNull(array[0].Value);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0].Value());
            Assert.AreEqual(1, Service.Instances);
        }

        [TestMethod]
        public void Func_Lazy()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, Service>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var enumerable = Container.Resolve<IEnumerable<Func<Lazy<IService>>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0]().Value);
            Assert.AreEqual(1, Service.Instances);
        }

        [TestMethod]
        public void Func_Lazy_Instance()
        {
            // Arrange
            Container.RegisterInstance(new Lazy<IService>(() => new Service()));
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, Service>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var enumerable = Container.Resolve<IEnumerable<Func<Lazy<IService>>>>();

            // Verify
            var array = enumerable.ToArray();
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0]().Value);
            Assert.AreEqual(1, Service.Instances);
        }

        [TestMethod]
        public void SingleService()
        {
            // Arrange
            Service.Instances = 0;
            Container.RegisterType<IService, Service>();

            // Act
            var services = Container.Resolve<IEnumerable<IService>>();
            var service = services.Single();

            // Assert
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(Service));
        }

        [TestMethod]
        public void MixedServices()
        {
            // Arrange
            Container.RegisterType<IService, Service>(typeof(Service).FullName);
            Container.RegisterType<IService, Service>();
            Container.RegisterType<IService, OtherService>(typeof(OtherService).FullName);

            // Act
            var services = Container.Resolve<IEnumerable<IService>>();

            // Assert
            using (var enumerator = services.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsInstanceOfType(enumerator.Current, typeof(Service));
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsInstanceOfType(enumerator.Current, typeof(Service));
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsInstanceOfType(enumerator.Current, typeof(OtherService));
            }
        }

        [TestMethod]
        public void MultipleServices()
        {
            // Arrange
            Container.RegisterType<IService, Service>(typeof(Service).FullName);
            Container.RegisterType<IService, OtherService>(typeof(OtherService).FullName);

            // Act
            var services = Container.Resolve<IEnumerable<IService>>();

            // Assert
            using (var enumerator = services.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsInstanceOfType(enumerator.Current, typeof(Service));
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsInstanceOfType(enumerator.Current, typeof(OtherService));
            }
        }

        [TestMethod]
        public void RegistrationOrderIsPreserved()
        {
            // Arrange
            Container.RegisterType<IService, OtherService>(typeof(OtherService).FullName);
            Container.RegisterType<IService, Service>(typeof(Service).FullName);

            // Act
            var services = Container.Resolve<IEnumerable<IService>>();

            // Assert
            using (var enumerator = services.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsInstanceOfType(enumerator.Current, typeof(OtherService));
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsInstanceOfType(enumerator.Current, typeof(Service));
            }
        }

        [TestMethod]
        public void NonexistentService()
        {
            var services = Container.Resolve<IEnumerable<IService>>();

            // Assert
            Assert.AreEqual(0, services.Count());
        }

        [TestMethod]
        public void ResolvesMixedOpenClosedGenericsAsEnumerable()
        {
            // Arrange
            var instance = new Foo<IService>(new OtherService());

            Container.RegisterType<IService, Service>();
            Container.RegisterType<IFoo<IService>, Foo<IService>>("Instance");
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), "fa");
            Container.RegisterInstance<IFoo<IService>>(instance);

            // Act
            var enumerable = Container.Resolve<IEnumerable<IFoo<IService>>>().ToArray();

            // Assert
            Assert.AreEqual(3, enumerable.Length);
            Assert.IsNotNull(enumerable[0]);
            Assert.IsNotNull(enumerable[1]);
            Assert.IsNotNull(enumerable[2]);
        }

        [TestMethod]
        public void ResolvesDifferentInstances()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1", new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, Service>("2", new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, Service>("3", new ContainerControlledLifetimeManager());

            using (var scope = Container.CreateChildContainer())
            {
                var enumerable = scope.Resolve<IEnumerable<IService>>().ToArray();
                var service = scope.Resolve<IService>("3");

                // Assert
                Assert.AreEqual(3, enumerable.Length);
                Assert.IsNotNull(enumerable[0]);
                Assert.IsNotNull(enumerable[1]);
                Assert.IsNotNull(enumerable[2]);

                Assert.AreNotSame(enumerable[0], enumerable[1]);
                Assert.AreNotSame(enumerable[1], enumerable[2]);
                Assert.AreSame(service, enumerable[2]);
            }
        }

        [TestMethod]
        public void ResolvesDifferentInstancesForOpenGenerics()
        {
            // Arrange
            Container.RegisterType<IService, Service>();
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), "1", new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), "2", new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), "3", new ContainerControlledLifetimeManager());

            using (var scope = Container.CreateChildContainer())
            {
                var enumerable = scope.Resolve<IEnumerable<IFoo<IService>>>().ToArray();
                var service = scope.Resolve<IFoo<IService>>("3");

                // Assert
                Assert.AreEqual(3, enumerable.Length);
                Assert.IsNotNull(enumerable[0]);
                Assert.IsNotNull(enumerable[1]);
                Assert.IsNotNull(enumerable[2]);

                Assert.AreNotSame(enumerable[0], enumerable[1]);
                Assert.AreNotSame(enumerable[1], enumerable[2]);
                Assert.AreSame(service, enumerable[2]);
            }
        }

        [TestMethod]
        public void ClosedTrumpsOpenGeneric()
        {
            // Arrange
            var instance = new Foo<IService>(new OtherService());

            Container.RegisterInstance<IFoo<IService>>(Name, instance)
                     .RegisterType(typeof(IFoo<>), typeof(Foo<>), Name)
                     .RegisterType<IFoo<IService>, Foo<IService>>()
                     .RegisterType<IService, Service>();

            // Act
            var enumerable = Container.Resolve<IEnumerable<IFoo<IService>>>()
                                      .ToArray();
            // Assert
            Assert.AreEqual(2, enumerable.Length);
            Assert.IsNotNull(enumerable[0]);
            Assert.IsNotNull(enumerable[1]);
        }

        [TestMethod]
        public void ResolveAllof100()
        {
            IUnityContainer container = new UnityContainer();

            for (var i = 0; i < 50; i++)
            {
                container.RegisterInstance(i);
            }

            for (var i = 50; i < 100; i++)
            {
                container.RegisterInstance(i.ToString(), i);
            }

            var results = container.Resolve<IEnumerable<int>>();

            Assert.IsNotNull(results);
            Assert.AreEqual(100, results.Count());
            Assert.IsInstanceOfType(results, typeof(int[]));
        }

        [TestMethod]
        public void ResolveInChildAll100()
        {
            var container = Container;

            // Arrange
            for (var j = 0; j < 4; j++)
            {
                var start = j * 20;
                var end = start + 20;

                for (var i = start; i < end; i++)
                {
                    container.RegisterInstance(i);
                }
                container = container.CreateChildContainer();
            }

            // Act
            var instance = container.Resolve<IEnumerable<int>>();

            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(int[]));
            Assert.AreEqual(80, instance.Count());
        }

    }
}

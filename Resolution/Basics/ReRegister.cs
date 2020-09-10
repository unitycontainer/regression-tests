﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
#endif

namespace Resolution
{
    public partial class Basics
    {
        [TestMethod]
        public void RegisteredFactoryOverUnregisterd()
        {
            var instance = new Foo();

            // Act/Verify
            var instance1 = Container.Resolve(typeof(Foo));
                            Container.RegisterFactory<Foo>((c, t, n) => instance);
            var instance2 = Container.Resolve(typeof(Foo));

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);

            Assert.IsInstanceOfType(instance1, typeof(Foo));
            Assert.IsInstanceOfType(instance2, typeof(Foo));

            Assert.AreSame(instance, instance2);
            Assert.AreNotSame(instance1, instance);
            Assert.AreNotSame(instance1, instance2);
        }

        [TestMethod]
        public void RegisteredTypeOverUnregisterd()
        {
            // Act/Verify
            var instance1 = Container.Resolve(typeof(Foo));
            Container.RegisterType<Foo>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
            var instance2 = Container.Resolve(typeof(Foo));
            var instance3 = Container.Resolve(typeof(Foo));

            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.IsNotNull(instance3);

            Assert.IsInstanceOfType(instance1, typeof(Foo));
            Assert.IsInstanceOfType(instance2, typeof(Foo));
            Assert.IsInstanceOfType(instance3, typeof(Foo));

            Assert.AreNotSame(instance1, instance3);
            Assert.AreNotSame(instance1, instance2);
            Assert.AreSame(instance2, instance3);
        }
    }
}

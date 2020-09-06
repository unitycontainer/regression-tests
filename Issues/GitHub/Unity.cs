using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
#endif

namespace Issues
{
    public partial class GitHub
    {
        [TestMethod]
        public void Unity_211()
        {
            Container.RegisterType<IThing, Thing>();
            Container.RegisterType<IThing, Thing>("SecondConstructor",
                new InjectionConstructor(typeof(int)));

            Container.RegisterType<IGeneric<IThing>, Gen1>(nameof(Gen1));
            Container.RegisterType<IGeneric<IThing>, Gen2>(nameof(Gen2));

            var things = Container.ResolveAll(typeof(IGeneric<IThing>)); //Throws exception
            Assert.AreEqual(things.Count(), 2);
        }

        [TestMethod]
        public void unitycontainer_microsoft_dependency_injection_14()
        {
            var c1 = Container.CreateChildContainer();
            var c2 = Container.CreateChildContainer();

            c1.RegisterType(typeof(IList<>), typeof(List<>), new ContainerControlledLifetimeManager(),
                                                             new InjectionConstructor());
            var t1 = c1.Resolve<IList<int>>();
            Assert.IsNotNull(t1);

            c2.RegisterType(typeof(IList<>), typeof(List<>), new ContainerControlledLifetimeManager(),
                                                             new InjectionConstructor());
            var t2 = c2.Resolve<IList<int>>();
            Assert.IsNotNull(t2);

            Assert.AreNotSame(t2, t1);

        }

        [TestMethod]
        public void Unity_177()
        {
            Container.RegisterType<OtherService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, OtherService>();
            Container.RegisterType<IOtherService, OtherService>();


            Assert.AreSame(Container.Resolve<IService>(), Container.Resolve<IOtherService>());
        }

        [TestMethod]
        public void Unity_165()
        {
            Container.RegisterFactory<ILogger>(c => new MockLogger(), new HierarchicalLifetimeManager());

            Assert.AreSame(Container.Resolve<ILogger>(), Container.Resolve<ILogger>());
            Assert.AreNotSame(Container.Resolve<ILogger>(), Container.CreateChildContainer().Resolve<ILogger>());
        }

        [TestMethod]
        public void Unity_164()
        {
            Container.RegisterType<ILogger, MockLogger>();
            var foo2 = new MockLogger();

            Container.RegisterFactory<ILogger>(x => foo2);
            var result = Container.Resolve<ILogger>();

            Assert.AreSame(result, foo2);
        }
        
        [TestMethod]
        public void Unity_156()
        {
            using (var container = new UnityContainer())
            {
                var td = new Service();

                container.RegisterFactory<Service>(_ => td);
                container.RegisterType<IService, Service>();

                Assert.AreSame(td, container.Resolve<IService>());
                Assert.AreSame(td, container.Resolve<Service>());
            }
            using (var container = new UnityContainer())
            {
                var td = new Service();

                container.RegisterFactory<Service>(_ => td);
                container.RegisterType<IService, Service>();

                Assert.AreSame(td, container.Resolve<Service>());
                Assert.AreSame(td, container.Resolve<IService>());
            }
        }

        [TestMethod]
        public void Unity_154_2()
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
        public void Unity_154_1()
        {
            Container.RegisterType<OtherService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IService, OtherService>();
            Container.RegisterType<IOtherService, OtherService>();

            Assert.AreSame(Container.Resolve<IService>(),
                           Container.Resolve<IOtherService>());
        }


        [TestMethod]
        public void Unity_153()
        {
            IUnityContainer rootContainer = Container;
            rootContainer.RegisterType<IService, Service>(new HierarchicalLifetimeManager());

            using (IUnityContainer childContainer = rootContainer.CreateChildContainer())
            {
                var a = childContainer.Resolve<IService>();
                var b = childContainer.Resolve<IService>();

                Assert.AreSame(a, b);
            }
        }

        [TestMethod]
        public void Unity_88()
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
        public void Unity_35()
        {
            var container = Container;

            container.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());
            IService logger = container.Resolve<IService>();

            Assert.IsNotNull(logger);
            Assert.AreSame(container.Resolve<IService>(), logger);

            container.RegisterType<Service>(new TransientLifetimeManager());

            Assert.AreSame(container.Resolve<IService>(), logger);
        }

        // Test types 
        public interface ITestClass
        { }

        public class TestClass : ITestClass
        {
            public TestClass()
            { }

            [InjectionConstructor]
            public TestClass(TestClass _) //1
            {
            }
        }
    }
}

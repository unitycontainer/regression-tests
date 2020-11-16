﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Registrations
{
    public partial class Hierarchy
    {
        [TestMethod]
        public void ResolveSignletonType_Directly_InRootContainer_THEN_InstanceIsCreatedInRootContainer_AND_SammeInstanceResolved_InAllChildContainers()
        {
            var rootContainer = Container;
            rootContainer.RegisterType(typeof(ISingletonService), typeof(SingletonService), new ContainerControlledLifetimeManager());

            var rootContainerId = rootContainer.GetHashCode();
            var childContainer1 = rootContainer.CreateChildContainer();
            var childContainer2 = rootContainer.CreateChildContainer();

            var instanceFromRootContainer = rootContainer.Resolve<ISingletonService>();
            var instanceFromChildContainer1 = childContainer1.Resolve<ISingletonService>();
            var instanceFromChildContainer2 = childContainer2.Resolve<ISingletonService>();

            Assert.AreEqual(rootContainerId, instanceFromRootContainer.ContainerId, "instanceFromRootContainer should be created in root container");
            Assert.AreEqual(rootContainerId, instanceFromChildContainer1.ContainerId, "instanceFromChildContainer1 should be created in root container");
            Assert.AreEqual(rootContainerId, instanceFromChildContainer2.ContainerId, "instanceFromChildContainer2 should be created in root container");

            Assert.AreEqual(instanceFromRootContainer, instanceFromChildContainer1, "instanceFromRootContainer and instanceFromChildContainer1 must be same");
            Assert.AreEqual(instanceFromRootContainer, instanceFromChildContainer2, "instanceFromRootContainer and instanceFromChildContainer2 must be same");
        }

        [TestMethod]
        public void ResolveSingletonType_Directly_InChildContainer_THEN_InstanceIsCreatedInRootContainer_AND_SammeInstanceResolved_InAllChildContainers()
        {
            var rootContainer = Container;
            rootContainer.RegisterType(typeof(ISingletonService), typeof(SingletonService), new ContainerControlledLifetimeManager());

            var rootContainerId = rootContainer.GetHashCode();
            var childContainer1 = rootContainer.CreateChildContainer();
            var childContainer2 = rootContainer.CreateChildContainer();
            var childContainer11 = childContainer1.CreateChildContainer();

            var instanceFromChildContainer11 = childContainer11.Resolve<ISingletonService>();
            var instanceFromChildContainer1 = childContainer1.Resolve<ISingletonService>();
            var instanceFromChildContainer2 = childContainer2.Resolve<ISingletonService>();
            var instanceFromRootContainer = rootContainer.Resolve<ISingletonService>();

            Assert.AreEqual(rootContainerId, instanceFromChildContainer1.ContainerId, "instanceFromChildContainer1 should be created in root container");
            Assert.AreEqual(rootContainerId, instanceFromChildContainer2.ContainerId, "instanceFromChildContainer2 should be created in root container");
            Assert.AreEqual(rootContainerId, instanceFromChildContainer11.ContainerId, "instanceFromChildContainer11 should be created in root container");
            Assert.AreEqual(rootContainerId, instanceFromRootContainer.ContainerId, "instanceFromRootContainer should be created in root container");

            Assert.AreEqual(instanceFromRootContainer, instanceFromChildContainer1, "instanceFromRootContainer and instanceFromChildContainer1 must be same");
            Assert.AreEqual(instanceFromRootContainer, instanceFromChildContainer2, "instanceFromRootContainer and instanceFromChildContainer2 must be same");
            Assert.AreEqual(instanceFromRootContainer, instanceFromChildContainer11, "instanceFromRootContainer and instanceFromChildContainer11 must be same");
        }

        [TestMethod]
        public void ResolveSingletonType_AsDependency_InRootContainer_THEN_ConsumerInstance_AND_SignletonInstance_CreatedInRootContainer()
        {
            Container.RegisterType(typeof(ISingletonConsumer), typeof(SingletonConsumer), new TransientLifetimeManager());
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), new ContainerControlledLifetimeManager());

            var rootContainerId = Container.GetHashCode();
            var consumerInstanceFromRootContainter = Container.Resolve<ISingletonConsumer>();

            Assert.AreEqual(rootContainerId, consumerInstanceFromRootContainter.ContainerId, "consumerInstanceFromRootContainter should be created in root container");
            Assert.AreEqual(rootContainerId, consumerInstanceFromRootContainter.SingletonService.ContainerId, "SingletonService dependency of consumerInstanceFromRootContainter should be created in root container");
        }

        [TestMethod]
        public void ResolveSingletonType_AsDependency_InChildContainer_THEN_ConsumerInstance_CreatedInChildContiner_AND_SignletonInstance_CreatedInRootContainer()
        {
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), new ContainerControlledLifetimeManager());

            var childContainer1 = Container.CreateChildContainer()
                                           .RegisterType(typeof(ISingletonConsumer), typeof(SingletonConsumer), new TransientLifetimeManager());

            var childContainer2 = childContainer1.CreateChildContainer();

            var rootContainerId = Container.GetHashCode();
            var childContainer1id = childContainer1.GetHashCode();

            var consumerInstanceFromChildContainter2 = childContainer2.Resolve<ISingletonConsumer>();

            Assert.AreEqual(childContainer1id, consumerInstanceFromChildContainter2.ContainerId, "consumerInstanceFromChildContainter11 should be created in childContainer1");
            Assert.AreEqual(rootContainerId, consumerInstanceFromChildContainter2.SingletonService.ContainerId, "singletonService dependency of consumerInstanceFromRootContainter should be created in root container");
        }


        [TestMethod]
        public void ResolveSingletonType_AsDependency_InChildContainer_THEN_ConsumerInstance_CreatedInChildContiner_AND_SignletonInstance_CreatedInRootContainer_Unregistered()
        {
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), new ContainerControlledLifetimeManager());

            var childContainer1 = Container.CreateChildContainer();
            var childContainer2 = childContainer1.CreateChildContainer();

            var rootContainerId = Container.GetHashCode();
            var childContainer1id = childContainer2.GetHashCode();
            var childContainer2id = childContainer2.GetHashCode();

            var consumerInstanceFromChildContainter1 = childContainer2.Resolve<SingletonConsumer>();
            var consumerInstanceFromChildContainter2 = childContainer2.Resolve<SingletonConsumer>();

            Assert.AreEqual(rootContainerId,   consumerInstanceFromChildContainter2.SingletonService.ContainerId, "singletonService dependency of consumerInstanceFromRootContainter should be created in root container");
            Assert.AreEqual(childContainer1id, consumerInstanceFromChildContainter1.ContainerId, "consumerInstanceFromChildContainter11 should be created in childContainer1");
            Assert.AreEqual(childContainer2id, consumerInstanceFromChildContainter2.ContainerId, "consumerInstanceFromChildContainter11 should be created in childContainer2");
        }

        [TestMethod]
        public void ResolveSignletonType_WithDependency_InRootContainer_THEN_DependencyResolvedInRootContainer()
        {
            var rootContainer = Container;
            rootContainer.RegisterType(typeof(ITestElement), typeof(TestElement));
            rootContainer.RegisterType(typeof(ISingletonServiceWithDependency), typeof(SingletonServiceWithDependency), new ContainerControlledLifetimeManager());

            var rootContainerId = rootContainer.GetHashCode();

            var instanceFromRootContainer = rootContainer.Resolve<ISingletonServiceWithDependency>();

            Assert.AreEqual(rootContainerId, instanceFromRootContainer.Element.ContainerId, "instanceFromRootContainer.Element must be created in root container");
        }

        [TestMethod]
        public void ResolveSignletonType_WithDependency_InChildContainer_THEN_DependencyResolvedInRootContainer()
        {
            var rootContainer = Container;
            rootContainer.RegisterType(typeof(ITestElement), typeof(TestElement));
            rootContainer.RegisterType(typeof(ISingletonServiceWithDependency), typeof(SingletonServiceWithDependency), new ContainerControlledLifetimeManager());

            var rootContainerId = rootContainer.GetHashCode();

            var instanceFromChildContainer = rootContainer.CreateChildContainer().CreateChildContainer().Resolve<ISingletonServiceWithDependency>();

            Assert.AreEqual(rootContainerId, instanceFromChildContainer.Element.ContainerId, "instanceFromChildContainer.Element must be created in root container");
            Assert.AreEqual(rootContainerId, instanceFromChildContainer.ContainerId, "instanceFromChildContainer must be created in root container");
        }

        [TestMethod]
        public void ResolveSignletonType_WithFactoryDependency_InRootContainer_THEN_FactoryCreatesItemsInRootContainer()
        {
            var rootContainer = Container;
            rootContainer.RegisterType(typeof(ITestElement), typeof(TestElement));
            rootContainer.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));
            rootContainer.RegisterType(typeof(ISingletonServiceWithFactory), typeof(SingletonServiceWithFactory), new ContainerControlledLifetimeManager());

            var rootContainerId = rootContainer.GetHashCode();

            var instanceFromRootContainer = rootContainer.Resolve<ISingletonServiceWithFactory>();

            var itemsByInstanceFromRootContainer = instanceFromRootContainer.GetElements();

            Assert.IsTrue(itemsByInstanceFromRootContainer.All(i => i.ContainerId == rootContainerId), "all items from itemsByInstanceFromChildContainer must be created in root container");
        }

        [TestMethod]
        public void ResolveSignletonType_WithFactoryDependency_InChildContainer_THEN_FactoryCreatesItemsInRootContainer()
        {
            var rootContainer = Container;
            rootContainer.RegisterType(typeof(ITestElement), typeof(TestElement));
            rootContainer.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));
            rootContainer.RegisterType(typeof(ISingletonServiceWithFactory), typeof(SingletonServiceWithFactory), new ContainerControlledLifetimeManager());

            var rootContainerId = rootContainer.GetHashCode();

            var childContainer = rootContainer.CreateChildContainer().CreateChildContainer();
            var childContainerId = childContainer.GetHashCode();

            var instanceFromChildContainer = childContainer.Resolve<ISingletonServiceWithFactory>();

            var itemsByInstanceFromChildContainer = instanceFromChildContainer.GetElements();

            Assert.IsTrue(itemsByInstanceFromChildContainer.All(i => i.ContainerId == rootContainerId), "all items from itemsByInstanceFromChildContainer must be created in root container");
        }

        [TestMethod]
        public void DisposeRootContainer_WithSingleton_THEN_SingletonDisposed()
        {
            var rootContainer = Container;
            rootContainer.RegisterType(typeof(ISingletonService), typeof(SingletonService), new ContainerControlledLifetimeManager());

            var instanceFromRootContainer = rootContainer.Resolve<ISingletonService>();

            rootContainer.Dispose();

            Assert.IsTrue(instanceFromRootContainer.IsDisposed, "instanceFromRootContainer should be disposed when root container is disposed");
        }

        [TestMethod]
        public void DisposeChildContainer_WithSingleton_THEN_Singleton_NotDisposed()
        {
            var rootContainer = Container;

            rootContainer.RegisterType(typeof(ISingletonService), typeof(SingletonService), new ContainerControlledLifetimeManager());

            var childContainer = rootContainer.CreateChildContainer().CreateChildContainer();
            var instanceFromChildContainer = childContainer.Resolve<ISingletonService>();

            childContainer.Dispose();
            Assert.IsFalse(instanceFromChildContainer.IsDisposed, "instanceFromChildContainer should not be disposed when child container is disposed");
        }

#if !NET45

        [TestMethod]
        public void DisposeChildContainer_WithTransientConsumer_THEN_SingletonConsumer_IsDisposed_AND_Singleton_NotDisposed()
        {

            Container.RegisterType(typeof(ISingletonConsumer), typeof(SingletonConsumer), TypeLifetime.PerContainerTransient);
            Container.RegisterType(typeof(ISingletonService), typeof(SingletonService), new ContainerControlledLifetimeManager());

            var childContainer = Container.CreateChildContainer().CreateChildContainer();

            var consumerInstanceFromChildContainer = childContainer.Resolve<ISingletonConsumer>();
            var singletonInstanceFromChildContainer = childContainer.Resolve<ISingletonService>();

            childContainer.Dispose();

            Assert.IsTrue(consumerInstanceFromChildContainer.IsDisposed, "consumerInstanceFromChildContainer must be disposed when child container is disposed");
            Assert.IsFalse(singletonInstanceFromChildContainer.IsDisposed, "singletonInstanceFromChildContainer should not be disposed when child container is disposed");
        }

        [TestMethod]
        public void DisposeChildContainer_WithSingleton_WithDependency_THEN_Singleton_NotDisposed_AND_DependencyNotDisposed_StaysInRootContainer()
        {
            var rootContainer = Container;
            rootContainer.RegisterType(typeof(ITestElement), typeof(TestElement), TypeLifetime.PerContainerTransient);
            rootContainer.RegisterType(typeof(ISingletonServiceWithDependency), typeof(SingletonServiceWithDependency), new ContainerControlledLifetimeManager());

            var rootContainerId = rootContainer.GetHashCode();

            var childContainer = rootContainer.CreateChildContainer().CreateChildContainer();
            var instanceFromChildContainer = childContainer.Resolve<ISingletonServiceWithDependency>();

            childContainer.Dispose();

            Assert.IsFalse(instanceFromChildContainer.Element.IsDisposed, "instanceFromChildContainer.Element should not be disposed when child container is disposed");
            Assert.IsFalse(instanceFromChildContainer.IsDisposed, "instanceFromChildContainer should not be disposed when child container is disposed");
        }

#endif

        [TestMethod]
        public void DisposeChildContainer_WithSingleton_WithFactoryDependency_THEN_Singleton_NotDisposed_AND_FactoryCreatesItemsInRootContainer()
        {
            var rootContainer = Container;
            rootContainer.RegisterType(typeof(ITestElement), typeof(TestElement));
            rootContainer.RegisterType(typeof(ITestElementFactory), typeof(TestElementFactory));
            rootContainer.RegisterType(typeof(ISingletonServiceWithFactory), typeof(SingletonServiceWithFactory), new ContainerControlledLifetimeManager());

            var rootContainerId = rootContainer.GetHashCode();

            var childContainer = rootContainer.CreateChildContainer().CreateChildContainer();
            var singletonInstanceFromChildContainer = childContainer.Resolve<ISingletonServiceWithFactory>();

            childContainer.Dispose();

            var itemsByInstanceFromChildContainer = singletonInstanceFromChildContainer.GetElements();

            Assert.IsTrue(itemsByInstanceFromChildContainer.All(i => i.ContainerId == rootContainerId), "all items from itemsByInstanceFromChildContainer must be created in root container");
        }

        interface ISingletonService : IDisposable
        {
            long ContainerId { get; }

            bool IsDisposed { get; }
        }

        interface ISingletonServiceWithDependency : ISingletonService
        {
            ITestElement Element { get; }
        }

        interface ISingletonServiceWithFactory : ISingletonService
        {
            IEnumerable<ITestElement> GetElements();
        }

        interface ISingletonConsumer : IDisposable
        {
            long ContainerId { get; }

            bool IsDisposed { get; }

            ISingletonService SingletonService { get; }
        }

        interface ITestElement : IDisposable
        {
            long ContainerId { get; }

            bool IsDisposed { get; }
        }

        interface ITestElementFactory
        {
            ITestElement CreateElement();
        }

        class SingletonService : ISingletonService
        {
            public long ContainerId { get; }

            public bool IsDisposed { get; private set; }

            public SingletonService(IUnityContainer container)
            {
                ContainerId = container.GetHashCode();
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        class SingletonServiceWithFactory : ISingletonServiceWithFactory
        {
            private readonly ITestElementFactory _elementFactory;

            public long ContainerId { get; }

            public bool IsDisposed { get; private set; }

            public SingletonServiceWithFactory(IUnityContainer container, ITestElementFactory factory)
            {
                ContainerId = container.GetHashCode();
                _elementFactory = factory;
            }

            public IEnumerable<ITestElement> GetElements()
            {
                for (int i = 0; i < 10; i++)
                    yield return _elementFactory.CreateElement();
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        class SingletonServiceWithDependency : ISingletonServiceWithDependency
        {
            public long ContainerId { get; }

            public ITestElement Element { get; }

            public bool IsDisposed { get; private set; }

            public SingletonServiceWithDependency(IUnityContainer container, ITestElement element)
            {
                ContainerId = container.GetHashCode();
                Element = element;
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        class SingletonConsumer : ISingletonConsumer
        {
            public long ContainerId { get; }

            public bool IsDisposed { get; private set; }

            public ISingletonService SingletonService { get; }

            public SingletonConsumer(IUnityContainer container, ISingletonService singleton)
            {
                ContainerId = container.GetHashCode();
                SingletonService = singleton;
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        class TestElement : ITestElement
        {
            public long ContainerId { get; }
            public bool IsDisposed { get; private set; }

            public TestElement(IUnityContainer container)
            {
                ContainerId = container.GetHashCode();
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        class TestElementFactory : ITestElementFactory
        {
            private readonly IUnityContainer _container;

            public TestElementFactory(IUnityContainer container)
            {
                _container = container;
            }

            public ITestElement CreateElement()
            {
                return _container.Resolve<ITestElement>();
            }
        }
    }
}
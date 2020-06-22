﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
#endif

namespace Unity.Regression.Tests
{
    [TestClass]
    public partial class UnityContainerFixture : TestFixtureBase
    {
        [TestMethod]
        public void CanCreateObjectFromUnconfiguredContainer()
        {
            IUnityContainer container = GetContainer();

            object o = container.Resolve<object>();

            Assert.IsNotNull(o);
        }

        [TestMethod]
        public void ContainerResolvesRecursiveConstructorDependencies()
        {
            IUnityContainer container = GetContainer();
            ObjectWithOneDependency dep = container.Resolve<ObjectWithOneDependency>();

            Assert.IsNotNull(dep);
            Assert.IsNotNull(dep.InnerObject);
            Assert.AreNotSame(dep, dep.InnerObject);
        }

        [TestMethod]
        public void ContainerResolvesMultipleRecursiveConstructorDependencies()
        {
            IUnityContainer container = GetContainer();
            ObjectWithTwoConstructorDependencies dep = container.Resolve<ObjectWithTwoConstructorDependencies>();

            dep.Validate();
        }

        [TestMethod]
        public void CanResolveTypeMapping()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>();

            ILogger logger = container.Resolve<ILogger>();

            Assert.IsNotNull(logger);
            Assert.IsInstanceOfType(logger, typeof(MockLogger));
        }

        [TestMethod]
        public void CanRegisterTypeMappingsWithNames()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>()
                .RegisterType<ILogger, SpecialLogger>("special");

            ILogger defaultLogger = container.Resolve<ILogger>();
            ILogger specialLogger = container.Resolve<ILogger>("special");

            Assert.IsNotNull(defaultLogger);
            Assert.IsNotNull(specialLogger);

            Assert.IsInstanceOfType(defaultLogger, typeof(MockLogger));
            Assert.IsInstanceOfType(specialLogger, typeof(SpecialLogger));
        }

        [TestMethod]
        public void ShouldDoPropertyInjection()
        {
            IUnityContainer container = GetContainer();

            ObjectWithTwoProperties obj = container.Resolve<ObjectWithTwoProperties>();

            obj.Validate();
        }

        [TestMethod]
        public void ShouldSkipIndexers()
        {
            IUnityContainer container = GetContainer();

            ObjectWithIndexer obj = container.Resolve<ObjectWithIndexer>();

            obj.Validate();
        }

        [TestMethod]
        public void ShouldSkipStaticProperties()
        {
            IUnityContainer container = GetContainer();
            container.RegisterInstance<object>(this);

            var obj = container.Resolve<ObjectWithStaticAndInstanceProperties>();

            obj.Validate();
        }

        [TestMethod]
        public void ShouldDoAllInjections()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>();

            ObjectWithLotsOfDependencies obj = container.Resolve<ObjectWithLotsOfDependencies>();

            Assert.IsNotNull(obj);
            obj.Validate();
        }

        [TestMethod]
        public void CanGetObjectsUsingNongenericMethod()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(ILogger), typeof(MockLogger));

            object logger = container.Resolve(typeof(ILogger));

            Assert.IsNotNull(logger);
            Assert.IsInstanceOfType(logger, typeof(MockLogger));
        }

        [TestMethod]
        public void CanGetNamedObjectsUsingNongenericMethod()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(ILogger), typeof(MockLogger))
                .RegisterType(typeof(ILogger), typeof(SpecialLogger), "special");

            ILogger defaultLogger = container.Resolve(typeof(ILogger)) as ILogger;
            ILogger specialLogger = container.Resolve(typeof(ILogger), "special") as ILogger;

            Assert.IsNotNull(defaultLogger);
            Assert.IsNotNull(specialLogger);

            Assert.IsInstanceOfType(defaultLogger, typeof(MockLogger));
            Assert.IsInstanceOfType(specialLogger, typeof(SpecialLogger));
        }

        [TestMethod]
        public void AllInjectionsWorkFromNongenericMethods()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(ILogger), typeof(MockLogger));

            ObjectWithLotsOfDependencies obj = (ObjectWithLotsOfDependencies)container.Resolve(typeof(ObjectWithLotsOfDependencies));
            obj.Validate();
        }

        [TestMethod]
        public void ContainerSupportsSingletons()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>(new ContainerControlledLifetimeManager());

            ILogger logger1 = container.Resolve<ILogger>();
            ILogger logger2 = container.Resolve<ILogger>();

            Assert.IsInstanceOfType(logger1, typeof(MockLogger));
            Assert.AreSame(logger1, logger2);
        }

        [TestMethod]
        public void CanCreatedNamedSingletons()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>()
                .RegisterType<ILogger, SpecialLogger>("special", new ContainerControlledLifetimeManager());

            ILogger logger1 = container.Resolve<ILogger>();
            ILogger logger2 = container.Resolve<ILogger>();
            ILogger logger3 = container.Resolve<ILogger>("special");
            ILogger logger4 = container.Resolve<ILogger>("special");

            Assert.AreNotSame(logger1, logger2);
            Assert.AreSame(logger3, logger4);
        }

        [TestMethod]
        public void CanRegisterSingletonsWithNongenericMethods()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>(new ContainerControlledLifetimeManager())
                .RegisterType<ILogger, SpecialLogger>("special", new ContainerControlledLifetimeManager());

            ILogger logger1 = container.Resolve<ILogger>();
            ILogger logger2 = container.Resolve<ILogger>();
            ILogger logger3 = container.Resolve<ILogger>("special");
            ILogger logger4 = container.Resolve<ILogger>("special");

            Assert.AreSame(logger1, logger2);
            Assert.AreSame(logger3, logger4);
            Assert.AreNotSame(logger1, logger3);
        }

        [TestMethod]
        public void DisposingContainerDisposesSingletons()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<DisposableObject>(new ContainerControlledLifetimeManager());

            DisposableObject dobj = container.Resolve<DisposableObject>();

            Assert.IsFalse(dobj.WasDisposed);
            container.Dispose();

            Assert.IsTrue(dobj.WasDisposed);
        }

        [TestMethod]
        public void SingletonsRegisteredAsDefaultGetInjected()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ObjectWithOneDependency>(new ContainerControlledLifetimeManager());

            ObjectWithOneDependency dep = container.Resolve<ObjectWithOneDependency>();
            ObjectWithTwoConstructorDependencies dep2 =
                container.Resolve<ObjectWithTwoConstructorDependencies>();

            Assert.AreSame(dep, dep2.OneDep);
        }

        [TestMethod]
        public void CanDoInjectionOnExistingObjects()
        {
            IUnityContainer container = GetContainer();

            ObjectWithTwoProperties o = new ObjectWithTwoProperties();
            Assert.IsNull(o.Obj1);
            Assert.IsNull(o.Obj2);

            container.BuildUp(o);

            o.Validate();
        }

        [TestMethod]
        public void CanBuildUpExistingObjectWithNonGenericObject()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>();

            ObjectUsingLogger o = new ObjectUsingLogger();
            object result = container.BuildUp(o);

            Assert.IsInstanceOfType(result, typeof(ObjectUsingLogger));
            Assert.AreSame(o, result);
            Assert.IsNotNull(o.Logger);
            Assert.IsInstanceOfType(o.Logger, typeof(MockLogger));
        }

        [TestMethod]
        public void CanBuildupObjectWithExplicitInterface()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>();

            ObjectWithExplicitInterface o = new ObjectWithExplicitInterface();
            container.BuildUp<ISomeCommonProperties>(o);

            o.ValidateInterface();
        }

        [TestMethod]
        public void CanBuildupObjectWithExplicitInterfaceUsingNongenericMethod()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>();

            ObjectWithExplicitInterface o = new ObjectWithExplicitInterface();
            container.BuildUp(typeof(ISomeCommonProperties), o);

            o.ValidateInterface();
        }

        [Ignore]
        [TestMethod]
        public void CanUseInstanceAsSingleton()
        {
            //MockLogger logger = new MockLogger();

            //IUnityContainer container = GetContainer()
            //    .RegisterInstance(typeof(ILogger), "logger", logger, new ContainerControlledLifetimeManager());

            //ILogger o = container.Resolve<ILogger>("logger");
            //Assert.AreSame(logger, o);
        }

        [TestMethod]
        public void CanUseInstanceAsSingletonViaGenericMethod()
        {
            MockLogger logger = new MockLogger();

            IUnityContainer container = GetContainer()
                .RegisterInstance<ILogger>("logger", logger);

            ILogger o = container.Resolve<ILogger>("logger");
            Assert.AreSame(logger, o);
        }

        [TestMethod]
        public void DisposingContainerDisposesOwnedInstances()
        {
            DisposableObject o = new DisposableObject();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(object), o);

            container.Dispose();
            Assert.IsTrue(o.WasDisposed);
        }

        [TestMethod]
        public void DisposingContainerDoesNotDisposeUnownedInstances()
        {
            DisposableObject o = new DisposableObject();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(object), o, new ExternallyControlledLifetimeManager());

            container.Dispose();
            Assert.IsFalse(o.WasDisposed);
            GC.KeepAlive(o);
        }

        [TestMethod]
        public void ContainerDefaultsToInstanceOwnership()
        {
            DisposableObject o = new DisposableObject();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(object), o);
            container.Dispose();
            Assert.IsTrue(o.WasDisposed);
        }

        [TestMethod]
        public void ContainerDefaultsToInstanceOwnershipViaGenericMethod()
        {
            DisposableObject o = new DisposableObject();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(DisposableObject), o);
            container.Dispose();
            Assert.IsTrue(o.WasDisposed);
        }

        [TestMethod]
        public void InstanceRegistrationWithoutNameRegistersDefault()
        {
            MockLogger l = new MockLogger();
            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(ILogger), l);

            ILogger o = container.Resolve<ILogger>();
            Assert.AreSame(l, o);
        }

        [TestMethod]
        public void InstanceRegistrationWithoutNameRegistersDefaultViaGenericMethod()
        {
            MockLogger l = new MockLogger();
            IUnityContainer container = GetContainer()
                .RegisterInstance<ILogger>(l);

            ILogger o = container.Resolve<ILogger>();
            Assert.AreSame(l, o);
        }

        [TestMethod]
        public void CanRegisterDefaultInstanceWithoutLifetime()
        {
            DisposableObject o = new DisposableObject();

            IUnityContainer container = GetContainer()
                .RegisterInstance(typeof(object), o, new ExternallyControlledLifetimeManager());

            object result = container.Resolve<object>();
            Assert.IsNotNull(result);
            Assert.AreSame(o, result);

            container.Dispose();
            Assert.IsFalse(o.WasDisposed);
            GC.KeepAlive(o);
        }

        [TestMethod]
        public void CanRegisterDefaultInstanceWithoutLifetimeViaGenericMethod()
        {
            DisposableObject o = new DisposableObject();

            IUnityContainer container = GetContainer()
                .RegisterInstance<object>(o, new ExternallyControlledLifetimeManager());

            object result = container.Resolve<object>();
            Assert.IsNotNull(result);
            Assert.AreSame(o, result);

            container.Dispose();
            Assert.IsFalse(o.WasDisposed);
            GC.KeepAlive(o);
        }

        [TestMethod]
        public void CanSpecifyInjectionConstructorWithDefaultDependencies()
        {
            string sampleString = "Hi there";
            IUnityContainer container = GetContainer()
                .RegisterInstance(sampleString);

            ObjectWithInjectionConstructor o = container.Resolve<ObjectWithInjectionConstructor>();

            Assert.IsNotNull(o.ConstructorDependency);
            Assert.AreSame(sampleString, o.ConstructorDependency);
        }

        [TestMethod]
        public void CanGetInstancesOfAllRegisteredTypes()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>("mock")
                .RegisterType<ILogger, SpecialLogger>("special")
                .RegisterType<ILogger, MockLogger>("another");

            List<ILogger> loggers = new List<ILogger>(
                container.ResolveAll<ILogger>());

            Assert.AreEqual(3, loggers.Count);
            Assert.IsInstanceOfType(loggers[0], typeof(MockLogger));
            Assert.IsInstanceOfType(loggers[1], typeof(SpecialLogger));
            Assert.IsInstanceOfType(loggers[2], typeof(MockLogger));
        }

        [TestMethod]
        public void GetAllDoesNotReturnTheDefault()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, SpecialLogger>("special")
                .RegisterType<ILogger, MockLogger>();

            List<ILogger> loggers = new List<ILogger>(
                container.ResolveAll<ILogger>());
            Assert.AreEqual(1, loggers.Count);
            Assert.IsInstanceOfType(loggers[0], typeof(SpecialLogger));
        }

        [TestMethod]
        public void CanGetAllWithNonGenericMethod()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>("mock")
                .RegisterType<ILogger, SpecialLogger>("special")
                .RegisterType<ILogger, MockLogger>("another");

            List<object> loggers = new List<object>(
                container.ResolveAll(typeof(ILogger)));

            Assert.AreEqual(3, loggers.Count);
            Assert.IsInstanceOfType(loggers[0], typeof(MockLogger));
            Assert.IsInstanceOfType(loggers[1], typeof(SpecialLogger));
            Assert.IsInstanceOfType(loggers[2], typeof(MockLogger));
        }

        [TestMethod]
        public void GetAllReturnsRegisteredInstances()
        {
            MockLogger l = new MockLogger();

            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>("normal")
                .RegisterType<ILogger, SpecialLogger>("special")
                .RegisterInstance<ILogger>("instance", l);

            List<ILogger> loggers = new List<ILogger>(
                container.ResolveAll<ILogger>());

            Assert.AreEqual(3, loggers.Count);
            Assert.IsInstanceOfType(loggers[0], typeof(MockLogger));
            Assert.IsInstanceOfType(loggers[1], typeof(SpecialLogger));
            Assert.AreSame(l, loggers[2]);
        }

        [TestMethod]
        public void CanRegisterLifetimeAsSingleton()
        {
            IUnityContainer container = GetContainer()
                .RegisterType<ILogger, MockLogger>()
                .RegisterType<ILogger, SpecialLogger>("special", new ContainerControlledLifetimeManager());

            ILogger logger1 = container.Resolve<ILogger>();
            ILogger logger2 = container.Resolve<ILogger>();
            ILogger logger3 = container.Resolve<ILogger>("special");
            ILogger logger4 = container.Resolve<ILogger>("special");

            Assert.AreNotSame(logger1, logger2);
            Assert.AreSame(logger3, logger4);
        }

        [TestMethod]
        public void ShouldThrowIfAttemptsToResolveUnregisteredInterface()
        {
            IUnityContainer container = GetContainer();

            Assert.ThrowsException<ResolutionFailedException>(() =>
                {
                    container.Resolve<ILogger>();
                });
        }

        [TestMethod]
        public void CanBuildSameTypeTwice()
        {
            IUnityContainer container = GetContainer();

            container.Resolve<ObjectWithTwoConstructorDependencies>();
            container.Resolve<ObjectWithTwoConstructorDependencies>();
        }

        [TestMethod]
        public void CanRegisterMultipleStringInstances()
        {
            IUnityContainer container = GetContainer();
            string first = "first";
            string second = "second";

            container.RegisterInstance<string>(first)
                .RegisterInstance<string>(second);

            string result = container.Resolve<string>();

            Assert.AreEqual(second, result);
        }

        [Ignore]
        [TestMethod]
        public void RegisteringTheSameLifetimeManagerTwiceThrows()
        {
            LifetimeManager singleton = new ContainerControlledLifetimeManager();

            //Assert.ThrowsException<InvalidOperationException>(() =>
            //    {
            //        new UnityContainer()
            //            .RegisterType<ILogger, MockLogger>(singleton)
            //            .RegisterType<ILogger, SpecialLogger>("special", singleton);
            //    });
        }

        [TestMethod]
        public void CanRegisterGenericTypesAndResolveThem()
        {
            Dictionary<string, string> myDict = new Dictionary<string, string>();
            myDict.Add("One", "two");
            myDict.Add("Two", "three");

            IUnityContainer container = GetContainer()
                .RegisterInstance(myDict)
                .RegisterType(typeof(IDictionary<,>), typeof(Dictionary<,>));

            IDictionary<string, string> result = container.Resolve<IDictionary<string, string>>();
            Assert.AreSame(myDict, result);
        }

        [TestMethod]
        public void CanSpecializeGenericsViaTypeMappings()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(IRepository<>), typeof(MockRespository<>))
                .RegisterType<IRepository<SomeType>, SomeTypRepository>();

            IRepository<string> generalResult = container.Resolve<IRepository<string>>();
            IRepository<SomeType> specializedResult = container.Resolve<IRepository<SomeType>>();

            Assert.IsInstanceOfType(generalResult, typeof(MockRespository<string>));
            Assert.IsInstanceOfType(specializedResult, typeof(SomeTypRepository));
        }

        [TestMethod]
        public void ContainerResolvesItself()
        {
            IUnityContainer container = GetContainer();

            Assert.AreSame(container, container.Resolve<IUnityContainer>());
        }

        //[Ignore]
        //[TestMethod]
        //public void ContainerResolvesItselfEvenAfterGarbageCollect()
        //{
        //    IUnityContainer container = GetContainer();
        //    container.AddNewExtension<GarbageCollectingExtension>();

        //    Assert.IsNotNull(container.Resolve<IUnityContainer>());
        //}

        //public class GarbageCollectingExtension : UnityContainerExtension
        //{
        //    //protected override void Initialize()
        //    //{
        //    //    this.Context.Strategies.AddNew<GarbageCollectingStrategy>(UnityBuildStage.Setup);
        //    //}

        //    //public class GarbageCollectingStrategy : BuilderStrategy
        //    //{
        //    //    public override void PreBuildUp(IBuilderContext context)
        //    //    {
        //    //        GC.Collect();
        //    //    }
        //    //}
        //}

        [TestMethod]
        public void ChildContainerResolvesChildNotParent()
        {
            IUnityContainer parent = new UnityContainer();
            IUnityContainer child = parent.CreateChildContainer();

            Assert.AreSame(child, child.Resolve<IUnityContainer>());
        }

        [TestMethod]
        public void ParentContainerResolvesParentNotChild()
        {
            IUnityContainer parent = new UnityContainer();
            IUnityContainer child = parent.CreateChildContainer();

            Assert.AreSame(parent, parent.Resolve<IUnityContainer>());
        }

        [Ignore]
        [TestMethod]
        public void ResolvingOpenGenericGivesInnerInvalidOperationException()
        {
            IUnityContainer container = GetContainer()
                .RegisterType(typeof(List<>), new InjectionConstructor(10));

            //Assert.ThrowsException<ResolutionFailedException>(
            //    () => { container.Resolve(typeof(List<>)); },
            //    (e) => { AssertExtensions.IsInstanceOfType(e.InnerException, typeof(ArgumentException)); });
        }

        [Ignore]
        [TestMethod]
        public void ResovingObjectWithPrivateSetterGivesUsefulException()
        {
            //IUnityContainer container = GetContainer();

            //Assert.ThrowsException<ResolutionFailedException>(
            //    () => { container.Resolve<ObjectWithPrivateSetter>(); },
            //    (e) => { AssertExtensions.IsInstanceOfType(e.InnerException, typeof(InvalidOperationException)); });
        }

        [TestMethod]
        public void ResolvingUnconfiguredPrimitiveDependencyGivesReasonableException()
        {
            ResolvingUnconfiguredPrimitiveGivesResonableException<string>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<bool>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<char>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<float>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<double>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<byte>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<short>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<int>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<long>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<IntPtr>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<UIntPtr>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<ushort>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<uint>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<ulong>();
            ResolvingUnconfiguredPrimitiveGivesResonableException<sbyte>();
        }

        private void ResolvingUnconfiguredPrimitiveGivesResonableException<T>()
        {
            IUnityContainer container = GetContainer();
            try
            {
                container.Resolve<TypeWithPrimitiveDependency<T>>();
            }
            catch (ResolutionFailedException e)
            {
                Assert.IsInstanceOfType(e.InnerException, typeof(InvalidOperationException));
                return;
            }
            Assert.Fail("Expected exception did not occur");
        }

        internal class SomeType
        {
        }

        public interface IRepository<TEntity>
        {
        }

        public class MockRespository<TEntity> : IRepository<TEntity>
        {
        }

        public class SomeTypRepository : IRepository<SomeType>
        {
        }

        public class ObjectWithPrivateSetter
        {
            [Dependency]
            public object Obj1 { get; private set; }
        }

        public class TypeWithPrimitiveDependency<T>
        {
            public TypeWithPrimitiveDependency(T dependency)
            {
            }
        }
    }
}

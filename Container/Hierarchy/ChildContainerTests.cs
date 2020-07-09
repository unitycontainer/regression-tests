using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Regression.Tests;
using System.Linq;
using System.Collections.Generic;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Lifetime;
using Unity;
#endif


namespace Container.Hierarchy
{
    [TestClass]
    public class ChildContainerTests
    {
        IUnityContainer Container;

        [TestInitialize]
        public void TestInitialize() => Container = new UnityContainer();


        [TestMethod]
        public void WhenResolvingAnIUnityContainerItResolvesItself()
        {
            IUnityContainer resolvedContainer = Container.Resolve<IUnityContainer>();

            Assert.AreSame(Container, resolvedContainer);
        }

        [TestMethod]
        public void WhenResolveingAnIUnityContainerForAChildContainerItResolvesTheChildContainer()
        {
            IUnityContainer childContainer = Container.CreateChildContainer();

            IUnityContainer resolvedContainer = childContainer.Resolve<IUnityContainer>();

            Assert.AreSame(childContainer, resolvedContainer);
        }

        [TestMethod]
        public void AClassThatHasADependencyOnTheContainerGetsItInjected()
        {
            IUnityContainerInjectionClass obj;

            obj = Container.Resolve<IUnityContainerInjectionClass>();

            Assert.AreSame(Container, obj.Container);
        }

        [TestMethod]
        public void AClassThatHasADependencyOnTheChildContainerGetsItInjected()
        {
            IUnityContainerInjectionClass obj;
            IUnityContainer childContainer = Container.CreateChildContainer();

            obj = childContainer.Resolve<IUnityContainerInjectionClass>();

            Assert.AreSame(childContainer, obj.Container);
        }

        [TestMethod]
        public void ChildContainersAreAllowedToBeCollectedWhenDisposed()
        {
            var child = Container.CreateChildContainer();
            var wr = new WeakReference(child);
            child.Dispose();
            child = null;

#if NET45
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
#else
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);
#endif
            Assert.IsFalse(wr.IsAlive);
        }

        [TestMethod]
        public void CanResolveItselfInScopes()
        {
            var child0 = Container.CreateChildContainer();
            var child1 = child0.CreateChildContainer();

            Assert.AreSame(Container, Container.Resolve<IUnityContainer>());
            Assert.AreSame(child0, child0.Resolve<IUnityContainer>());
            Assert.AreSame(child1, child1.Resolve<IUnityContainer>());

            Assert.AreNotSame(Container, child0);
            Assert.AreNotSame(Container, child1);
            Assert.AreNotSame(child0, child1);
        }

        [TestMethod]
        public void CreateChildUsingParentsConfiguration()
        {
            Container.RegisterType<ITemporary, Temporary>();
            var child = Container.CreateChildContainer();

            ITemporary temp = child.Resolve<ITemporary>();

            Assert.IsNotNull(temp);
            Assert.IsInstanceOfType(temp, typeof(Temporary));
        }

        [TestMethod]
        public void NamesRegisteredInParentAppearInChild()
        {
            Container.RegisterType<ITemporary, SpecialTemp>("test");
            var child = Container.CreateChildContainer();

            ITemporary temp = child.Resolve<ITemporary>("test");

            Assert.IsInstanceOfType(temp, typeof(SpecialTemp));
        }

        [TestMethod]
        public void NamesRegisteredInParentAppearInChildGetAll()
        {
            string[] numbers = { "first", "second", "third" };

            Container.RegisterInstance(numbers[0], "first")
                .RegisterInstance(numbers[1], "second");

            var child = Container.CreateChildContainer()
                .RegisterInstance(numbers[2], "third");

            List<string> nums = new List<string>(child.ResolveAll<string>());
            CollectionAssert.AreEquivalent(numbers, nums);
        }

        [TestMethod]
        public void ChildConfigurationOverridesParentConfiguration()
        {
            Container.RegisterType<ITemporary, Temporary>();

            var child = Container.CreateChildContainer()
                .RegisterType<ITemporary, SpecialTemp>();

            ITemporary parentTemp = Container.Resolve<ITemporary>();
            ITemporary childTemp = child.Resolve<ITemporary>();

            Assert.IsInstanceOfType(parentTemp, typeof(Temporary));
            Assert.IsInstanceOfType(childTemp, typeof(SpecialTemp));
        }

        [TestMethod]
        public void DisposingParentDisposesChild()
        {
            var child = Container.CreateChildContainer();

            MyDisposableObject spy = new MyDisposableObject();
            child.RegisterInstance(spy);
            Container.Dispose();

            Assert.IsTrue(spy.WasDisposed);
        }

        [TestMethod]
        public void CanDisposeChildWithoutDisposingParent()
        {
            MyDisposableObject parentSpy = new MyDisposableObject();
            MyDisposableObject childSpy = new MyDisposableObject();

            Container.RegisterInstance(parentSpy);
            var child = Container.CreateChildContainer()
                              .RegisterInstance(childSpy);
            child.Dispose();

            Assert.IsFalse(parentSpy.WasDisposed);
            Assert.IsTrue(childSpy.WasDisposed);

            childSpy.WasDisposed = false;
            Container.Dispose();

            Assert.IsTrue(parentSpy.WasDisposed);
            Assert.IsFalse(childSpy.WasDisposed);
        }

        [TestMethod]
        public void VerifyToList()
        {
            string[] numbers = { "first", "second", "third" };

            Container.RegisterInstance(numbers[0], "first")
                .RegisterInstance(numbers[1], "second");
            var child = Container.CreateChildContainer()
                .RegisterInstance(numbers[2], "third");

            List<string> nums = new List<string>(child.ResolveAll<string>());
            CollectionAssert.AreEquivalent(numbers, nums);
        }

        [TestMethod]
        public void ChangesInParentReflectsInChild()
        {
            string[] numbers = { "first", "second", "third", "fourth" };

            Container.RegisterInstance(numbers[0], "1")
                  .RegisterInstance(numbers[1], "2");
            var child = Container.CreateChildContainer();

            List<string> childnums = new List<string>(child.ResolveAll<string>());
            List<string> parentnums = new List<string>(Container.ResolveAll<string>());

            CollectionAssert.AreEquivalent(childnums, parentnums);

            Container.RegisterInstance(numbers[3], "4"); //Register an instance in Parent but not in child

            List<string> childnums2 = new List<string>(child.ResolveAll<string>());
            List<string> parentnums2 = new List<string>(Container.ResolveAll<string>());

            CollectionAssert.AreEquivalent(childnums2, parentnums2); //Both parent child should have same instances
        }

        [TestMethod]
        public void DuplicateRegInParentAndChild()
        {
            string[] numbers = { "first", "second", "third", "fourth" };

            Container.RegisterInstance(numbers[0], "1")
                .RegisterInstance(numbers[1], "2");

            var child = Container.CreateChildContainer();

            List<string> childnums = new List<string>(child.ResolveAll<string>());
            List<string> parentnums = new List<string>(Container.ResolveAll<string>());

            CollectionAssert.AreEquivalent(childnums, parentnums);

            Container.RegisterInstance(numbers[3], "4");
            child.RegisterInstance(numbers[3], "4");

            List<string> childnums2 = new List<string>(child.ResolveAll<string>());
            List<string> parentnums2 = new List<string>(Container.ResolveAll<string>());

            CollectionAssert.AreEquivalent(childnums2, parentnums2); //Both parent child should have same instances
        }

        [TestMethod]
        public void VerifyArgumentNullException()
        {
            string[] numbers = { "first", "second", "third" };

            Container.RegisterInstance("1", numbers[0])
                .RegisterInstance("2", numbers[1]);
            var child = Container.CreateChildContainer()
                .RegisterInstance("3", numbers[2]);

            List<string> nums = new List<string>(child.ResolveAll<string>());

            CollectionAssert.AreEquivalent(numbers, nums);
        }

        [TestMethod]
        public void CreateParentChildContainersWithSameName()
        {
            Container.RegisterType<ITemporary, Temp>("First");
            Container = Container.CreateChildContainer();
            Container.RegisterType<ITemporary, Temp>("First");

            List<ITemporary> count = new List<ITemporary>(Container.ResolveAll<ITemporary>());

            Assert.AreEqual(1, count.Count);
        }

        [TestMethod]
        public void MoreChildContainers1()
        {
            Container.RegisterType<ITemporary, Temp>("First");
            Container.RegisterType<ITemporary, Temp>("First");

            var child1 = Container.CreateChildContainer();
            child1.RegisterType<ITemporary, Temp>("First");
            child1.RegisterType<ITemporary, Temp>("First");

            var child2 = child1.CreateChildContainer();
            child2.RegisterType<ITemporary, Temp>("First");
            child2.RegisterType<ITemporary, Temp>("First");

            var child3 = child2.CreateChildContainer();
            child3.RegisterType<ITemporary, Temp>("First");
            child3.RegisterType<ITemporary, Temp>("First");

            var child4 = child3.CreateChildContainer();
            child4.RegisterType<ITemporary, Temp>("First");

            ITemporary first = child4.Resolve<ITemporary>("First");

            child4.RegisterType<ITemporary, Temp>("First", new ContainerControlledLifetimeManager());
            var count = new List<ITemporary>(child4.ResolveAll<ITemporary>());

            Assert.AreEqual(1, count.Count);
        }

        [TestMethod]
        public void MoreChildContainers2()
        {
            Container.RegisterType<ITemporary, Temp>("First", new HierarchicalLifetimeManager());
            var result = Container.Resolve<ITemporary>("First");

            var child1 = Container.CreateChildContainer();
            child1.RegisterType<ITemporary, Temp>("First", new HierarchicalLifetimeManager());
            var result1 = child1.Resolve<ITemporary>("First");

            var child2 = child1.CreateChildContainer();
            child2.RegisterType<ITemporary, Temp>("First", new HierarchicalLifetimeManager());
            var result2 = child2.Resolve<ITemporary>("First");

            Assert.AreNotEqual(result.GetHashCode(), result1.GetHashCode());
            Assert.AreNotEqual(result.GetHashCode(), result2.GetHashCode());
            Assert.AreNotEqual(result1.GetHashCode(), result2.GetHashCode());

            List<ITemporary> count = new List<ITemporary>(child2.ResolveAll<ITemporary>());

            Assert.AreEqual(1, count.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void GetObjectAfterDispose()
        {
            Container.RegisterType<Temp>("First", new ContainerControlledLifetimeManager());

            var child = Container.CreateChildContainer();
            child.RegisterType<ITemporary>("First", new ContainerControlledLifetimeManager());
            Container.Dispose();
            child.Resolve<ITemporary>("First");
        }

        [TestMethod]
        public void VerifyArgumentNotNullOrEmpty()
        {
            string[] numbers = { "first", "second", "third" };

            Container.RegisterInstance("1", numbers[0])
                .RegisterInstance("2", numbers[1]);
            var child = Container.CreateChildContainer()
                .RegisterInstance("3", numbers[2]);
            List<string> nums = new List<string>(child.ResolveAll<string>());

            CollectionAssert.AreEquivalent(numbers, nums);
        }

        [TestMethod]
        public void VerifyArgumentNotNullOrEmpty1()
        {
            string[] numbers = { "first", "second", "third" };

            Container.RegisterInstance("1", numbers[0])
                .RegisterInstance("2", numbers[1]);
            var child = Container.CreateChildContainer()
                .RegisterInstance("3", numbers[2]);
            List<string> nums = new List<string>(child.ResolveAll<string>());

            CollectionAssert.AreEquivalent(numbers, nums);
        }

        [TestMethod]
        public void VerifyArgumentNotNullOrEmpty2()
        {
            string[] numbers = { "first", "second", "third" };

            Container.RegisterInstance("1", numbers[0])
                .RegisterInstance("2", numbers[1]);
            var child = Container.CreateChildContainer()
                .RegisterInstance("3", numbers[2]);
            List<string> nums = new List<string>(child.ResolveAll<string>());

            CollectionAssert.AreEquivalent(numbers, nums);
        }

        //bug # 3978 http://unity.codeplex.com/WorkItem/View.aspx?WorkItemId=6053
        [TestMethod]
        public void ChildParentRegisrationOverlapTest()
        {
            Container.RegisterInstance("str1", "string1");
            Container.RegisterInstance("str2", "string2");

            var child = Container.CreateChildContainer();

            child.RegisterInstance("str2", "string20");
            child.RegisterInstance("str3", "string30");

            var childStrSet = new HashSet<string>(child.ResolveAll<string>());
            var parentStrSet = new HashSet<string>(Container.ResolveAll<string>());

            Assert.IsTrue(childStrSet.SetEquals(new[] { "string1", "string20", "string30" }));
            Assert.IsTrue(parentStrSet.SetEquals(new[] { "string1", "string2" }));
        }
    }
    #region Test Data

    public class IUnityContainerInjectionClass
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
    }

    public interface ITemporary
    {
    }

    public class Temp : ITemporary
    {
    }

    public class Temporary : ITemporary
    {
    }

    public class SpecialTemp : ITemporary //Second level
    {
    }

    #pragma warning disable CA1063 // Implement IDisposable Correctly

    public class MyDisposableObject : IDisposable
    {
        private bool wasDisposed = false;

        public bool WasDisposed
        {
            get { return wasDisposed; }
            set { wasDisposed = value; }
        }

        public void Dispose()
        {
            wasDisposed = true;
        }
    }
    
    #pragma warning restore CA1063 

    #endregion

}

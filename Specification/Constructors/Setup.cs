using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Spec.Constructors
{
    [TestClass]
    public partial class Constructors
    {
        private string _data = "data";
        private string _override = "override";
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
        }

        #region Test Data

        public class TypeWithAmbiguousCtors
        {
            public const string One = "1";
            public const string Two = "2";
            public const string Three = "3";
            public const string Four = "4";
            public const string Five = "5";

            public string Signature { get; }

            public TypeWithAmbiguousCtors()
            {
                Signature = One;
            }

            public TypeWithAmbiguousCtors(object first, object second)
            {
                Signature = Two;
            }

            public TypeWithAmbiguousCtors(IUnityContainer first, object second)
            {
                Signature = Three;
            }

            public TypeWithAmbiguousCtors(object first, IUnityContainer second)
            {
                Signature = Four;
            }

            [Dependency]
            public IUnityContainer Container { get; set; }
        }

        public class WithDefaultCtor
        {
            public WithDefaultCtor()
            {
                Data = this;
            }

            public object Data { get; }
        }

        public interface IService
        {
        }

        public class Service : IService
        {
            public Service() => Ctor = 1;

            [InjectionConstructor]
            public Service(object data)
            {
                Ctor = 2;
                Data = data?.ToString();
            }

            public Service(IUnityContainer container) => Ctor = 3;

            public Service(object[] data) => Ctor = 4;

            public int Ctor { get; }    // Constructor called 

            public Service(string data)
            {
                Data = data;
            }

            public string Data { get; }
        }

        public class CtorWithDependency
        {
            public CtorWithDependency([Dependency] string data)
            {
                Data = data;
            }

            public string Data { get; }
        }

        public class CtorWithNamedDependency
        {
            public CtorWithNamedDependency()
            {
                Data = null;
            }

            public CtorWithNamedDependency([Dependency("name")] string data)
            {
                Data = data;
            }

            public string Data { get; }
        }

        public class CtorWithOptionalDependency
        {
            public CtorWithOptionalDependency([OptionalDependency] string data = null)
            {
                Data = data;
            }

            public string Data { get; }
        }
        public class CtorWithOptionalNamedDependency
        {
            public CtorWithOptionalNamedDependency([OptionalDependency("name")] string data = null)
            {
                Data = data;
            }

            public string Data { get; }
        }

        public class TypeWithAmbuguousAnnotations
        {
            public TypeWithAmbuguousAnnotations() => Ctor = 1;

            [InjectionConstructor]
            public TypeWithAmbuguousAnnotations(object arg) => Ctor = 2;

            public TypeWithAmbuguousAnnotations(IUnityContainer container) => Ctor = 3;

            [InjectionConstructor]
            public TypeWithAmbuguousAnnotations(object[] data) => Ctor = 4;

            public int Ctor { get; }    // Constructor called 

            [Dependency]
            public IUnityContainer Container { get; set; }
        }

        #endregion
    }

    [TestClass]
    public partial class Constructors_Diagnostic : Constructors
    {
        [TestInitialize]
#if NET45
        public override void TestInitialize() => Container = new UnityContainer();
#else
        public override void TestInitialize() => Container = new UnityContainer()
            .AddExtension(new Unity.Diagnostic());
#endif

        [TestMethod]
        public void Baseline_Diagnostic()
        { }
    }
}

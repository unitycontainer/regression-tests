using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Specification
{
    [TestClass]
    public partial class Properties
    {
        protected const string Name = "name";
        protected const string Other = "other";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
            Container.RegisterInstance(Name);
            Container.RegisterInstance(Name, Name);
            Container.RegisterInstance(Other, Other);
            Container.RegisterType<ObjectWithProperty>(
                    new InjectionConstructor(),
                    Resolve.Property(nameof(ObjectWithProperty.MyProperty)))
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>(Name)
                .RegisterInstance(Name);
        }
    }

    [TestClass]
    public partial class Properties_Diagnostic : Properties
    {
        [TestInitialize]
#if NET45
        public override void TestInitialize() => base.TestInitialize();
#else
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new Unity.Diagnostic());
        }
#endif
    }
}

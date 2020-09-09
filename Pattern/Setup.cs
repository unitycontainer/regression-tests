using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification
{
    [TestClass]
    public abstract partial class VerificationPattern
    {
        #region Fields

        protected const int NamedInt = 123;
        protected const int DefaultInt = 345;
        protected const int RegisteredInt = 678;
        protected const string Name = "name";
        protected const string NamedString = "named";
        protected const string DefaultString = "default";
        protected const string Registeredtring = "registered";
        public static Unresolvable Singleton = Unresolvable.Create();
        public static Unresolvable NamedSingleton = Unresolvable.Create();

        protected IUnityContainer Container;

        #endregion


        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();

        
        #region Implementation

        protected Type TargetType(string name) => Type.GetType($"{GetType().FullName}+{name}");

        protected abstract InjectionMember GetInjectionMember(object argument);

        #endregion
    }
}

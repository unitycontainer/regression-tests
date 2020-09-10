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
        protected const int InjectedInt = 678;
        protected const int RegisteredInt = 890;
        protected const string Name = "name";
        protected const string NamedString = "named";
        protected const string DefaultString = "default";
        protected const string InjectedString = "injected";
        protected const string RegisteredString = "registered";
        public readonly static Unresolvable Singleton = Unresolvable.Create("singleton");
        public readonly static Unresolvable NamedSingleton = Unresolvable.Create("named");
        public readonly static Unresolvable InjectedSingleton = SubUnresolvable.Create("injected");

        protected IUnityContainer Container;

        #endregion


        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();

        
        #region Implementation

        protected Type TargetType(string name) => Type.GetType($"{GetType().FullName}+{name}");

        protected virtual void RegisterTypes()
        {
            Container.RegisterInstance(RegisteredInt)
                     .RegisterInstance(RegisteredString)
                     .RegisterInstance(Singleton)
                     .RegisterInstance(Name, NamedInt)
                     .RegisterInstance(Name, NamedSingleton);
        }

        protected abstract InjectionMember GetInjectionMethodBase(object argument);

        protected abstract InjectionMember GetInjectionMember(object argument);

        #endregion
    }
}

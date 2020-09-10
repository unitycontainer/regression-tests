using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
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
        protected const string Null = "null";
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
                     .RegisterInstance(typeof(TestStruct), new TestStruct(55, "struct"))
#if !V4
                     .RegisterInstance(typeof(string),       Null, (object)null)
                     .RegisterInstance(typeof(Unresolvable), Null, (object)null)
#endif
                     .RegisterInstance(Name, NamedInt)
                     .RegisterInstance(Name, NamedSingleton);
        }

        protected abstract InjectionMember GetMemberByName();

        protected abstract InjectionMember GetInjectionMethodBase(object argument);

        protected abstract InjectionMember GetResolvedMember(Type argument);

        protected abstract InjectionMember GetResolvedMember(Type argument, string name);

        protected abstract InjectionMember GetInjectionMember(object argument);

        #endregion
    }
}

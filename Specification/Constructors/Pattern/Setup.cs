using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    [TestClass]
    public partial class Constructors : VerificationPattern
    {
        protected override InjectionMember GetInjectionMethodBase(object argument) 
            => new InjectionConstructor(argument);

        protected override InjectionMember GetResolvedMember(Type argument) 
            => new InjectionConstructor(new ResolvedParameter(argument));

        protected override InjectionMember GetResolvedMember(Type argument, string name) 
            => new InjectionConstructor(new ResolvedParameter(argument, name));

        protected override InjectionMember GetInjectionMember(Type type)
            => new InjectionConstructor(type);

        protected override InjectionMember GetInjectionMember(object argument) 
            => new InjectionConstructor(new InjectionParameter(argument));
    }
}

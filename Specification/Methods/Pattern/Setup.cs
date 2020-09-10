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
    public partial class Methods : VerificationPattern
    {
        protected override InjectionMember GetInjectionMethodBase(object argument) 
            => new InjectionMethod("Method", argument);

        protected override InjectionMember GetResolvedMember(Type argument) 
            => new InjectionMethod("Method", new ResolvedParameter(argument));

        protected override InjectionMember GetResolvedMember(Type argument, string name) 
            => new InjectionMethod("Method", new ResolvedParameter(argument, name));

        protected override InjectionMember GetInjectionMember(object argument) 
            => new InjectionMethod("Method", new InjectionParameter(argument));
    }
}

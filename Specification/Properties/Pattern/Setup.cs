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
    public partial class Properties : VerificationPattern
    {
        protected override InjectionMember GetMemberByName()
            => new InjectionProperty("Property");

        protected override InjectionMember GetInjectionMethodBase(object argument) 
            => throw new NotSupportedException();

        protected override InjectionMember GetResolvedMember(Type argument) 
            => new InjectionProperty("Property", new ResolvedParameter(argument));

        protected override InjectionMember GetResolvedMember(Type argument, string name) 
            => new InjectionProperty("Property", new ResolvedParameter(argument, name));

        protected override InjectionMember GetInjectionMember(object argument) 
            => new InjectionProperty("Property", argument);
    }
}

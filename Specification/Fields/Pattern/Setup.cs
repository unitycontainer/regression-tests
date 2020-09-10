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
    public partial class Fields : VerificationPattern
    {
        protected override InjectionMember GetInjectionMethodBase(object argument) 
            => throw new NotSupportedException();

        protected override InjectionMember GetResolvedMember(Type argument) 
            => new InjectionField("Field", new ResolvedParameter(argument));

        protected override InjectionMember GetResolvedMember(Type argument, string name) 
            => new InjectionField("Field", new ResolvedParameter(argument, name));

        protected override InjectionMember GetInjectionMember(Type type)
            => new InjectionField("Field", type);

        protected override InjectionMember GetInjectionMember(object argument) 
            => new InjectionField("Field", argument);
    }
}

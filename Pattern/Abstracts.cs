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
    public abstract partial class VerificationPattern
    {
        protected abstract InjectionMember Get_ByName_Member(Type type, string name);

        protected abstract InjectionMember Get_Resolved_Member(Type type, string name);

        protected abstract InjectionMember Get_Optional_Member(Type type, string name);

        protected abstract InjectionMember Get_Generic_Member(Type type, string name);

        protected abstract InjectionMember Get_GenericOptional_Member(Type type, string name);
        

        protected abstract InjectionMember GetInjectionMember(object argument);
    }
}

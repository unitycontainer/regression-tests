﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            PocoType = typeof(Implicit_Dependency_Generic<>);
            Required = typeof(Required_Dependency_Generic<>);
            Optional = typeof(Optional_Dependency_Generic<>);

            Required_Named = typeof(Required_Dependency_Named<>);
            Optional_Named = typeof(Optional_Dependency_Named<>);

            PocoType_Default_Value = typeof(Implicit_WithDefault_Value);
            Required_Default_Value = typeof(Required_WithDefault_Value);
            Optional_Default_Value = typeof(Optional_WithDefault_Value);

            PocoType_Default_Class = typeof(Implicit_WithDefault_Class);
            Required_Default_String = typeof(Required_WithDefault_Class);
            Optional_Default_Class = typeof(Optional_WithDefault_Class);
        }


        protected override InjectionMember GetMemberByName()
            => null;

        protected override InjectionMember GetInjectionMethodBase(object argument) 
            => new InjectionConstructor(argument);

        protected override InjectionMember GetResolvedMember(Type argument) 
            => new InjectionConstructor(new ResolvedParameter(argument));

        protected override InjectionMember GetResolvedMember(Type argument, string name) 
            => new InjectionConstructor(new ResolvedParameter(argument, name));

        protected override InjectionMember GetInjectionMember(object argument) 
            => new InjectionConstructor(argument);
    }
}

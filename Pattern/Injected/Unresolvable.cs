using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    /// <summary>
    /// Testing unsupported parapeter types
    /// </summary>
    /// <example>
    /// 
    /// public class PocoType 
    /// {
    ///     [InjectionConstructor]
    ///     public PocoType(ref int value) { }
    ///     
    ///     [InjectionMethod]
    ///     public void Method(out int value) { }
    /// }
    /// 
    /// </example>
    public abstract partial class VerificationPattern
    {
        [DataTestMethod]
        [DynamicData(nameof(Parameter_Data))]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public virtual void Injected_ByType_Parameters(string target, Type dependency)
        {
            var type = TargetType(target);

            // Arrange
            Container.RegisterType(type, GetInjectionMember(dependency));

            RegisterTypes();

            // Act
            _ = Container.Resolve(type);
        }

        [DataTestMethod]
        [DynamicData(nameof(Parameter_Data))]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public virtual void Injected_ByName_Parameters(string target, Type dependency)
        {
            var type = TargetType(target);

            // Arrange
            Container.RegisterType(type, GetMemberByName());

            RegisterTypes();

            // Act
            _ = Container.Resolve(type);
        }

        [DataTestMethod]
        [DynamicData(nameof(Parameter_Data))]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public virtual void Injected_ByResolving_Parameters(string target, Type dependency)
        {
            var type = TargetType(target);

            // Arrange
            Container.RegisterType(type, GetResolvedMember(dependency, null));

            RegisterTypes();

            // Act
            _ = Container.Resolve(type);
        }


        #region Test Data

        public static IEnumerable<object[]> Parameter_Data
        {
            get
            {
                /////////////////////////////////////////////////////////////////////////////
                //                       //  Type                      Dependency          

                yield return new object[] { "Implicit_Dependency_Ref", typeof(Unresolvable) };
                yield return new object[] { "Implicit_Dependency_Out", typeof(Unresolvable) };
                yield return new object[] { "Required_Dependency_Ref", typeof(Unresolvable) };
                yield return new object[] { "Required_Dependency_Out", typeof(Unresolvable) };
                yield return new object[] { "Optional_Dependency_Ref", typeof(Unresolvable) };
                yield return new object[] { "Optional_Dependency_Out", typeof(Unresolvable) };
            }
        }

        #endregion
    }
}

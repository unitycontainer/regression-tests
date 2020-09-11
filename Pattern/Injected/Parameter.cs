using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        /// <summary>
        /// Test resolving from empty container.
        /// </summary>
        /// <param name="target">Name of the <see cref="Type"/> to resolve</param>
        [DataTestMethod]

        [DataRow("Implicit_Dependency_Ref",                 typeof(Unresolvable)) ]
        [DataRow("Implicit_Dependency_Out",                 typeof(Unresolvable)) ]
        [DataRow("Implicit_Dependency_RefStruct",           typeof(TestRefStruct))]

        [DataRow("Required_Dependency_Ref",       typeof(Unresolvable)) ]
        [DataRow("Required_Dependency_Out",       typeof(Unresolvable)) ]
        [DataRow("Required_Dependency_RefStruct", typeof(TestRefStruct))]
#if !V5 
        [DataRow("Optional_Dependency_Ref",       typeof(Unresolvable)) ]
        [DataRow("Optional_Dependency_Out",       typeof(Unresolvable)) ]
        [DataRow("Optional_Dependency_RefStruct", typeof(TestRefStruct))]
#endif
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public virtual void Unregistered_Injected_Unsupported(string target, Type dependency)
        {
            var type = TargetType(target);

            // Arrange
            Container.RegisterType(type, GetInjectionMember(dependency));

            // Act
            _ = Container.Resolve(type);
        }

        /// <summary>
        /// Test resolving from initialized container.
        /// </summary>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        [DataTestMethod]

        [DataRow("Implicit_Dependency_Ref",                 typeof(Unresolvable)) ]
        [DataRow("Implicit_Dependency_Out",                 typeof(Unresolvable)) ]
        [DataRow("Implicit_Dependency_RefStruct",           typeof(TestRefStruct))]

        [DataRow("Required_Dependency_Ref",       typeof(Unresolvable)) ]
        [DataRow("Required_Dependency_Out",       typeof(Unresolvable)) ]
        [DataRow("Required_Dependency_RefStruct", typeof(TestRefStruct))]
#if !V5 
        [DataRow("Optional_Dependency_Ref",       typeof(Unresolvable)) ]
        [DataRow("Optional_Dependency_Out",       typeof(Unresolvable)) ]
        [DataRow("Optional_Dependency_RefStruct", typeof(TestRefStruct))]
#endif
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public virtual void Registered_Injected_Unsupported(string name, Type dependency)
        {
            var type = TargetType(name);

            // Arrange
            RegisterTypes();
            Container.RegisterType(type, GetInjectionMember(dependency));

            // Act
            _ = Container.Resolve(type);
        }
    }
}

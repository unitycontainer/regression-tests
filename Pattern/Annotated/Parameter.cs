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
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        [DataTestMethod]
        [DataRow("Required_Dependency_Ref")]
        [DataRow("Required_Dependency_Out")]
        [DataRow("Required_Dependency_RefStruct")]
#if !V5 
        [DataRow("Optional_Dependency_Ref")]
        [DataRow("Optional_Dependency_Out")]
        [DataRow("Optional_Dependency_RefStruct")]
#endif
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unregistered_Annotated_Unsupported(string name)
        {
            var type = TargetType(name);

            // Act
            //_ = Container.Resolve(type);
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test resolving from initialized container.
        /// </summary>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        [DataTestMethod]
        [DataRow("Required_Dependency_Ref")]
        [DataRow("Required_Dependency_Out")]
        [DataRow("Required_Dependency_RefStruct")]
#if !V5 
        [DataRow("Optional_Dependency_Ref")]
        [DataRow("Optional_Dependency_Out")]
        [DataRow("Optional_Dependency_RefStruct")]
#endif
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Registered_Annotated_Unsupported(string name)
        {
            var type = TargetType(name);

            // Arrange
            RegisterTypes();

            // Act
            //_ = Container.Resolve(type);
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
        }
    }
}

﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Spec.Constructors
{
    public partial class Constructors
    {
        [TestMethod]
        public void Override_OptionalDependencyLegacy()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalDependency>(new DependencyOverride(typeof(string), _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }


        [TestMethod]
        public void Override_OptionalNamedDependencyLegacy()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalNamedDependency>(new DependencyOverride(typeof(string), Name, _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }

        [TestMethod]
        public void Override_OptionalDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalDependency>(Override.Dependency<string>(_override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }


        [TestMethod]
        public void Override_OptionalNamedDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalNamedDependency>(Override.Dependency<string>(Name, _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }
    }
}

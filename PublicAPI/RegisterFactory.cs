﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Regression.Tests;
using System.Linq;
using System.Collections.Generic;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Container.Interfaces
{
    public partial class UnityContainerAPI
    {
        protected Func<IUnityContainer, object> ShortFactory = (c) => c;
        protected Func<IUnityContainer, Type, string, object> FullFactory = (c, t, n) => c;

        [TestMethod]
        public void RegisterFactroryTest()
        {
            // Arrange
            IFactoryLifetimeManager manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), Name, FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #region RegisterFactory overloads


        #region Generics Short Factory overloads

        [TestMethod]
        public void RegisterFactrory_Short_T()
        {
            // Act
            Container.RegisterFactory<IService>(ShortFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_Short_T_Manager()
        {
            // Arrange
            IFactoryLifetimeManager manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory<IService>(ShortFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public void RegisterFactrory_Short_T_Name()
        {
            // Act
            Container.RegisterFactory<IService>(Name, ShortFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_Short_T_Name_Manager()
        {
            // Arrange
            IFactoryLifetimeManager manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory<IService>(Name, ShortFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion


        #region Generics Full Factory overloads

        [TestMethod]
        public void RegisterFactrory_Full_T()
        {
            // Act
            Container.RegisterFactory<IService>(FullFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_Full_T_Manager()
        {
            // Arrange
            IFactoryLifetimeManager manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory<IService>(FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public void RegisterFactrory_Full_T_Name()
        {
            // Act
            Container.RegisterFactory<IService>(Name, FullFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_Full_T_Name_Manager()
        {
            // Arrange
            IFactoryLifetimeManager manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory<IService>(Name, FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion


        #region Non-generic overloads


        #region Short Factory overloads

        [TestMethod]
        public void RegisterFactrory_Short_Type()
        {
            // Act
            Container.RegisterFactory(typeof(IService), ShortFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_Short_Type_Manager()
        {
            // Arrange
            IFactoryLifetimeManager manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), ShortFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public void RegisterFactrory_Short_Type_Name()
        {
            // Act
            Container.RegisterFactory(typeof(IService), Name, ShortFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_Short_Type_Name_Manager()
        {
            // Arrange
            IFactoryLifetimeManager manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), Name, ShortFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion


        #region Full Factory overloads

        [TestMethod]
        public void RegisterFactrory_Full_Type()
        {
            // Act
            Container.RegisterFactory(typeof(IService), FullFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_Full_Type_Manager()
        {
            // Arrange
            IFactoryLifetimeManager manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public void RegisterFactrory_Full_Type_Name()
        {
            // Act
            Container.RegisterFactory(typeof(IService), Name, FullFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_Full_Type_Name_Manager()
        {
            // Arrange
            IFactoryLifetimeManager manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), Name, FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion


        #endregion

        #endregion
    }
}
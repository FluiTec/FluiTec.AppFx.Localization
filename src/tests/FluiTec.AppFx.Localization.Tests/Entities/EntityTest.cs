using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Tests.Entities
{
    /// <summary>
    /// An entity test.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public abstract class EntityTest<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Creates the entity.
        /// </summary>
        ///
        /// <returns>
        /// The new entity.
        /// </returns>
        protected abstract T CreateEntity();

        /// <summary>
        /// Creates other entity.
        /// </summary>
        ///
        /// <returns>
        /// The new other entity.
        /// </returns>
        protected abstract T CreateOtherEntity();

        /// <summary>
        /// (Unit Test Method) equals same instance.
        /// </summary>
        [TestMethod]
        public void EqualsSameInstance()
        {
            var entity = CreateEntity();
            Assert.IsTrue(entity.Equals(entity));
        }

        /// <summary>
        /// Equals copy.
        /// </summary>
        [TestMethod]
        public void EqualsCopy()
        {
            var entity1 = CreateEntity();
            var entity2 = CreateEntity();
            Assert.IsTrue(entity1.Equals(entity2));
        }

        /// <summary>
        /// Not equals different entity.
        /// </summary>
        [TestMethod]
        public void NotEqualsDifferentEntity()
        {
            var entity1 = CreateEntity();
            var entity2 = CreateOtherEntity();
            Assert.IsFalse(entity1.Equals(entity2));
        }
    }
}
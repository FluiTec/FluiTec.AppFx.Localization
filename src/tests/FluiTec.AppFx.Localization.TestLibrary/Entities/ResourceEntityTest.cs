using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.TestLibrary.Entities
{
    /// <summary>
    ///     A resource entity test.
    /// </summary>
    public class ResourceEntityTest : DependencyLocalizationDataTest<ResourceEntity>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        ///
        /// <value>
        /// The author.
        /// </value>
        protected AuthorEntity Author { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="dataServiceProvider">  The data service provider. </param>
        public ResourceEntityTest(
            DataServiceProvider<ILocalizationDataService, ILocalizationUnitOfWork> dataServiceProvider) : base(
            dataServiceProvider)
        {
        }

        #endregion

        #region AbstractTestMethods

        /// <summary>
        ///     Creates an entity.
        /// </summary>
        /// <returns>
        ///     The new entity.
        /// </returns>
        protected override ResourceEntity CreateEntity()
        {
            return new ResourceEntity
            {
                AuthorId = Author.Id,
                ResourceKey = "FluiTec.AppFx.ClassName.ClassProperty1",
                ModificationDate = DateTimeOffset.Now
            };
        }

        /// <summary>
        ///     Creates non updateable entity.
        /// </summary>
        /// <returns>
        ///     The new non updateable entity.
        /// </returns>
        protected override ResourceEntity CreateNonUpdateableEntity()
        {
            // set not-existent id to prevent successful update
            return new ResourceEntity
            {
                Id = 100,
                AuthorId = Author.Id,
                ResourceKey = "NO-UPDATE",
                ModificationDate = DateTimeOffset.Now
            };
        }

        /// <summary>
        ///     Enumerates create entities in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process create entities in this collection.
        /// </returns>
        protected override IEnumerable<ResourceEntity> CreateEntities()
        {
            return new[]
            {
                new ResourceEntity
                {
                    Id = 1,
                    AuthorId = Author.Id,
                    ResourceKey = "FluiTec.AppFx.ClassName.ClassProperty1",
                    ModificationDate = DateTimeOffset.Now
                },
                new ResourceEntity
                {
                    Id = 2,
                    AuthorId = Author.Id,
                    ResourceKey = "FluiTec.AppFx.ClassName.ClassProperty2",
                    ModificationDate = DateTimeOffset.Now
                }
            };
        }

        /// <summary>
        ///     Change entity.
        /// </summary>
        /// <param name="entity">   The entity. </param>
        /// <returns>
        ///     A TEntity.
        /// </returns>
        protected override ResourceEntity ChangeEntity(ResourceEntity entity)
        {
            entity.ResourceKey = "Changed.Key";
            return entity;
        }

        /// <summary>
        /// Creates the dependencies.
        /// </summary>
        ///
        /// <param name="uow">  The uow. </param>
        protected override void CreateDependencies(ILocalizationUnitOfWork uow)
        {
            var author = new AuthorEntity {Name = "Max Mustermann"};
            Author = uow.AuthorRepository.Add(author);
        }

        #endregion

        #region TestMethods

        /// <summary>
        ///     (Unit Test Method) can read entity by key.
        /// </summary>
        [TestMethod]
        public virtual void CanReadEntityByKey()
        {
            using var uow = BeginUnitOfWork();

            var resource = uow.GetRepository<IResourceRepository>().Add(CreateEntity());
            
            var dbEntity = uow.GetRepository<IResourceRepository>().Get(resource.ResourceKey);

            Assert.IsTrue(EntityEquals(resource, dbEntity));
        }

        /// <summary>
        ///     (Unit Test Method) can read entity by key asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanReadEntityByKeyAsync()
        {
            using var uow = BeginUnitOfWork();
            var resource = uow.GetRepository<IResourceRepository>().Add(CreateEntity());
            
            var dbEntity = uow.GetRepository<IResourceRepository>().GetAsync(resource.ResourceKey).Result;

            Assert.IsTrue(EntityEquals(resource, dbEntity));
        }

        /// <summary>
        ///     (Unit Test Method) can read entities by key prefix.
        /// </summary>
        [TestMethod]
        public virtual void CanReadEntitiesByKeyPrefix()
        {
            var prefix = "FluiTec.AppFx.ClassName";
            using var uow = BeginUnitOfWork();

            var entities = CreateEntities().ToList();
            var entity = CreateEntity();
            entity.ResourceKey = "Trash";

            uow.GetRepository<IResourceRepository>().AddRange(entities);
            uow.GetRepository<IResourceRepository>().Add(entity); // ensure we have something we don't want back
            var dbEntities = uow.GetRepository<IResourceRepository>().GetByKeyPrefix(prefix);

            Assert.AreEqual(entities.Count, dbEntities.Count());
        }

        /// <summary>
        ///     (Unit Test Method) can read entities by key prefix asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanReadEntitiesByKeyPrefixAsync()
        {
            var prefix = "FluiTec.AppFx.ClassName";
            using var uow = BeginUnitOfWork();

            var entities = CreateEntities().ToList();

            uow.GetRepository<IResourceRepository>().AddRange(entities);
            var dbEntities = uow.GetRepository<IResourceRepository>().GetByKeyPrefixAsync(prefix).Result;

            Assert.AreEqual(entities.Count, dbEntities.Count());
        }

        /// <summary>
        ///     (Unit Test Method) can get by author.
        /// </summary>
        [TestMethod]
        public virtual void CanGetByAuthor()
        {
            using var uow = BeginUnitOfWork();

            var resource = uow.GetRepository<IResourceRepository>().Add(CreateEntity());

            var dbResource = uow.GetRepository<IResourceRepository>().GetByAuthor(Author).Single();
            Assert.IsTrue(resource.Equals(dbResource));
        }

        /// <summary>
        ///     (Unit Test Method) can get by author asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanGetByAuthorAsync()
        {
            using var uow = BeginUnitOfWork();

            var resource = uow.GetRepository<IResourceRepository>().Add(CreateEntity());

            var dbResource = uow.GetRepository<IResourceRepository>().GetByAuthorAsync(Author).Result.Single();
            Assert.IsTrue(resource.Equals(dbResource));
        }

        /// <summary>
        ///     (Unit Test Method) can get by author identifier.
        /// </summary>
        [TestMethod]
        public virtual void CanGetByAuthorId()
        {
            using var uow = BeginUnitOfWork();
            var resource = uow.GetRepository<IResourceRepository>().Add(CreateEntity());

            var dbResource = uow.GetRepository<IResourceRepository>().GetByAuthor(Author.Id).Single();
            Assert.IsTrue(resource.Equals(dbResource));
        }

        /// <summary>
        ///     (Unit Test Method) can get by author identifier asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanGetByAuthorIdAsync()
        {
            using var uow = BeginUnitOfWork();

            var resource = uow.GetRepository<IResourceRepository>().Add(CreateEntity());

            var dbResource = uow.GetRepository<IResourceRepository>().GetByAuthorAsync(Author.Id).Result.Single();
            Assert.IsTrue(resource.Equals(dbResource));
        }

        #endregion
    }
}
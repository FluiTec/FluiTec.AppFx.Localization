using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.TestLibrary.Entities
{
    /// <summary>
    ///     A language entity test.
    /// </summary>
    public class LanguageEntityTest :  LocalizationDataTest<LanguageEntity>
    {
        #region Constructors

        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="dataServiceProvider">  The data service provider. </param>
        protected LanguageEntityTest(
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
        protected override LanguageEntity CreateEntity()
        {
            return new LanguageEntity {IsoName = "de-DE", Name = "Deutsch"};
        }

        /// <summary>
        ///     Creates non updateable entity.
        /// </summary>
        /// <returns>
        ///     The new non updateable entity.
        /// </returns>
        protected override LanguageEntity CreateNonUpdateableEntity()
        {
            // set not-existent id to prevent successful update
            return new LanguageEntity {Id = 100, IsoName = "no-UP", Name = "NO-UPDATE"};
        }

        /// <summary>
        ///     Enumerates create entities in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process create entities in this collection.
        /// </returns>
        protected override IEnumerable<LanguageEntity> CreateEntities()
        {
            return new[]
            {
                new LanguageEntity {IsoName = "de-DE", Name = "Deutsch"},
                new LanguageEntity {IsoName = "en-US", Name = "English"}
            };
        }
        
        /// <summary>
        ///     Change entity.
        /// </summary>
        /// <param name="entity">   The entity. </param>
        /// <returns>
        ///     A TEntity.
        /// </returns>
        protected override LanguageEntity ChangeEntity(LanguageEntity entity)
        {
            entity.Name = "Changed";
            return entity;
        }

        #endregion

        #region TestMethods

        /// <summary>
        ///     (Unit Test Method) can read entity by name.
        /// </summary>
        [TestMethod]
        public void CanReadEntityByName()
        {
            using var uow = BeginUnitOfWork();
            var entity = uow.GetRepository<ILanguageRepository>().Add(CreateEntity());
            var dbEntity = uow.GetRepository<ILanguageRepository>().Get(entity.IsoName);

            Assert.IsTrue(EntityEquals(entity, dbEntity));
        }

        /// <summary>
        ///     (Unit Test Method) can read entity by name asynchronous.
        /// </summary>
        [TestMethod]
        public void CanReadEntityByNameAsync()
        {
            using var uow = BeginUnitOfWork();
            var entity = uow.GetRepository<ILanguageRepository>().Add(CreateEntity());
            var dbEntity = uow.GetRepository<ILanguageRepository>().GetAsync(entity.IsoName).Result;

            Assert.IsTrue(EntityEquals(entity, dbEntity));
        }

        /// <summary>
        /// (Unit Test Method) can read entity by two letter ISO.
        /// </summary>
        [TestMethod]
        public void CanReadEntityByTwoLetterIso()
        {
            using var uow = BeginUnitOfWork();

            var entities = CreateEntities().ToList();
            uow.GetRepository<ILanguageRepository>().AddRange(entities);

            var filteredEntitiesCode = entities.Where(e => e.IsoName.StartsWith("de"));
            var filteredEntitiesDb = uow.GetRepository<ILanguageRepository>().GetByTwoLetterIso("de");

            Assert.AreEqual(filteredEntitiesCode.Count(), filteredEntitiesDb.Count());
        }

        /// <summary>
        /// (Unit Test Method) can read entity by two letter ISO asynchronous.
        /// </summary>
        [TestMethod]
        public void CanReadEntityByTwoLetterIsoAsync()
        {
            using var uow = BeginUnitOfWork();

            var entities = CreateEntities().ToList();
            uow.GetRepository<ILanguageRepository>().AddRange(entities);

            var filteredEntitiesCode = entities.Where(e => e.IsoName.StartsWith("de"));
            var filteredEntitiesDb = uow.GetRepository<ILanguageRepository>().GetByTwoLetterIsoAsync("de").Result;

            Assert.AreEqual(filteredEntitiesCode.Count(), filteredEntitiesDb.Count());
        }

        #endregion
    }
}
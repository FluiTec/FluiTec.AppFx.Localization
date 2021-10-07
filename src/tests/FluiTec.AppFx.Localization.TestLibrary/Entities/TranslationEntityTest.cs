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
    ///     A translation entity test.
    /// </summary>
    public class TranslationEntityTest : DependencyLocalizationDataTest<TranslationEntity>
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

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        ///
        /// <value>
        /// The resource.
        /// </value>
        protected ResourceEntity Resource { get; private set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        ///
        /// <value>
        /// The language.
        /// </value>
        protected LanguageEntity Language1 { get; private set; }

        /// <summary>
        /// Gets or sets the language 2.
        /// </summary>
        ///
        /// <value>
        /// The language 2.
        /// </value>
        protected LanguageEntity Language2 { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="dataServiceProvider">  The data service provider. </param>
        public TranslationEntityTest(
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
        protected override TranslationEntity CreateEntity()
        {
            return new TranslationEntity {LanguageId = Language1.Id, ResourceId = Resource.Id, Value = "deusch"};
        }

        /// <summary>
        ///     Creates non updateable entity.
        /// </summary>
        /// <returns>
        ///     The new non updateable entity.
        /// </returns>
        protected override TranslationEntity CreateNonUpdateableEntity()
        {
            // set not-existent id to prevent successful update
            return new TranslationEntity {Id = 100, LanguageId = Language1.Id, ResourceId = Resource.Id, Value = "NO-UPDATE"};
        }

        /// <summary>
        ///     Enumerates create entities in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process create entities in this collection.
        /// </returns>
        protected override IEnumerable<TranslationEntity> CreateEntities()
        {
            return new[]
            {
                new TranslationEntity {LanguageId = Language1.Id, ResourceId = Resource.Id, Value = "deutsch"},
                new TranslationEntity {LanguageId = Language2.Id, ResourceId = Resource.Id, Value = "english"}
            };
        }

        /// <summary>
        ///     Change entity.
        /// </summary>
        /// <param name="entity">   The entity. </param>
        /// <returns>
        ///     A TEntity.
        /// </returns>
        protected override TranslationEntity ChangeEntity(TranslationEntity entity)
        {
            entity.Value = "Changed";
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

            var resource = new ResourceEntity {AuthorId = Author.Id, Key = "MyKey", ModificationDate = DateTimeOffset.Now};
            Resource = uow.ResourceRepository.Add(resource);

            var language1 = new LanguageEntity {IsoName = "de-DE", Name = "Deutsch"};
            Language1 = uow.LanguageRepository.Add(language1);

            var language2 = new LanguageEntity {IsoName = "en-US", Name = "English (US)"};
            Language2 = uow.LanguageRepository.Add(language2);
        }

        #endregion

        #region TestMethods

        /// <summary>
        ///     (Unit Test Method) can get by resource.
        /// </summary>
        [TestMethod]
        public void CanGetByResource()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByResource(Resource).Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by resource asynchronous.
        /// </summary>
        [TestMethod]
        public void CanGetByResourceAsync()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByResourceAsync(Resource).Result
                .Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by resource identifier.
        /// </summary>
        [TestMethod]
        public void CanGetByResourceId()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByResource(Resource.Id).Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by resource identifier asynchronous.
        /// </summary>
        [TestMethod]
        public void CanGetByResourceIdAsync()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByResourceAsync(Resource.Id).Result
                .Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by resource key.
        /// </summary>
        [TestMethod]
        public void CanGetByResourceKey()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByResource(Resource.Key).Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by resource key asynchronous.
        /// </summary>
        [TestMethod]
        public void CanGetByResourceKeyAsync()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByResourceAsync(Resource.Key).Result
                .Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by language.
        /// </summary>
        [TestMethod]
        public void CanGetByLanguage()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByLanguage(Language1).Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by language asynchronous.
        /// </summary>
        [TestMethod]
        public void CanGetByLanguageAsync()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByLanguageAsync(Language1).Result
                .Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by language identifier.
        /// </summary>
        [TestMethod]
        public void CanGetByLanguageId()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByLanguage(Language1.Id).Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by language identifier asynchronous.
        /// </summary>
        [TestMethod]
        public void CanGetByLanguageIdAsync()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByLanguageAsync(Language1.Id).Result
                .Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by language name.
        /// </summary>
        [TestMethod]
        public void CanGetByLanguageName()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByLanguage(Language1.IsoName).Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        /// <summary>
        ///     (Unit Test Method) can get by language name asynchronous.
        /// </summary>
        [TestMethod]
        public void CanGetByLanguageNameAsync()
        {
            using var uow = BeginUnitOfWork();

            var translation = uow.GetRepository<ITranslationRepository>().Add(CreateEntity());

            var dbTranslation = uow.GetRepository<ITranslationRepository>().GetByLanguageAsync(Language1.IsoName).Result
                .Single();
            Assert.IsTrue(translation.Equals(dbTranslation));
        }

        #endregion
    }
}
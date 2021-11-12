using System.Collections.Generic;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.TestLibrary.Entities;

/// <summary>
///     An author entity test.
/// </summary>
public abstract class AuthorEntityTest : LocalizationDataTest<AuthorEntity>
{
    #region Constructors

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="dataServiceProvider">  The data service provider. </param>
    protected AuthorEntityTest(
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
    protected override AuthorEntity CreateEntity()
    {
        return new AuthorEntity {Name = "Name 1"};
    }

    /// <summary>
    ///     Creates non updateable entity.
    /// </summary>
    /// <returns>
    ///     The new non updateable entity.
    /// </returns>
    protected override AuthorEntity CreateNonUpdateableEntity()
    {
        // set not-existent id to prevent successful update
        return new AuthorEntity {Id = 100, Name = "NO-UPDATE"};
    }

    /// <summary>
    ///     Enumerates create entities in this collection.
    /// </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process create entities in this collection.
    /// </returns>
    protected override IEnumerable<AuthorEntity> CreateEntities()
    {
        return new[] {new AuthorEntity {Name = "Name 1"}, new AuthorEntity {Name = "Name 2"}};
    }

    /// <summary>
    ///     Change entity.
    /// </summary>
    /// <param name="entity">   The entity. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    protected override AuthorEntity ChangeEntity(AuthorEntity entity)
    {
        entity.Name = "Changed Name";
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
        var entity = uow.GetRepository<IAuthorRepository>().Add(CreateEntity());
        var dbEntity = uow.GetRepository<IAuthorRepository>().Get(entity.Name);

        Assert.IsTrue(EntityEquals(entity, dbEntity));
    }

    /// <summary>
    ///     (Unit Test Method) can read entity by name asynchronous.
    /// </summary>
    [TestMethod]
    public void CanReadEntityByNameAsync()
    {
        using var uow = BeginUnitOfWork();
        var entity = uow.GetRepository<IAuthorRepository>().Add(CreateEntity());
        var dbEntity = uow.GetRepository<IAuthorRepository>().GetAsync(entity.Name).Result;

        Assert.IsTrue(EntityEquals(entity, dbEntity));
    }

    #endregion
}
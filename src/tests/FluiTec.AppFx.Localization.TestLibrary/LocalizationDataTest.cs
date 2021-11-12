using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataTests;

namespace FluiTec.AppFx.Localization.TestLibrary;

/// <summary>
///     A localization data test.
/// </summary>
public abstract class
    LocalizationDataTest<TEntity> : EntityDataTest<ILocalizationDataService, ILocalizationUnitOfWork, TEntity, int>
    where TEntity : class, IKeyEntity<int>, IEquatable<TEntity>, new()
{
    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="dataServiceProvider">  The data service provider. </param>
    protected LocalizationDataTest(
        DataServiceProvider<ILocalizationDataService, ILocalizationUnitOfWork> dataServiceProvider) : base(
        dataServiceProvider)
    {
    }

    /// <summary>
    ///     Query if 'entity' has valid key.
    /// </summary>
    /// <param name="entity">   The entity. </param>
    /// <returns>
    ///     True if valid key, false if not.
    /// </returns>
    protected override bool HasValidKey(TEntity entity)
    {
        return entity.Id > 0;
    }

    /// <summary>
    ///     Entity equals.
    /// </summary>
    /// <param name="code"> The code. </param>
    /// <param name="db">   The database. </param>
    /// <returns>
    ///     True if it succeeds, false if it fails.
    /// </returns>
    protected override bool EntityEquals(TEntity code, TEntity db)
    {
        return code.Equals(db);
    }
}
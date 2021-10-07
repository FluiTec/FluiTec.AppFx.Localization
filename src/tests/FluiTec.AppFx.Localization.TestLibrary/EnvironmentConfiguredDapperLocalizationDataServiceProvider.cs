using System;

namespace FluiTec.AppFx.Localization.TestLibrary
{
    /// <summary>
    ///     An environment configured dapper localization data service provider.
    /// </summary>
    public abstract class
        EnvironmentConfiguredDapperLocalizationDataServiceProvider : DapperLocalizationDataServiceProvider
    {
        /// <summary>
        ///     Gets the name of the variable.
        /// </summary>
        /// <value>
        ///     The name of the variable.
        /// </value>
        protected abstract string VariableName { get; }

        /// <summary>
        ///     Gets a value indicating whether the environment configured.
        /// </summary>
        /// <value>
        ///     True if environment configured, false if not.
        /// </value>
        public bool EnvironmentConfigured =>
            !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(VariableName));
    }
}
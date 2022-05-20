using System.CommandLine;
using FluiTec.AppFx.Console.ConsoleItems;

namespace FluiTec.AppFx.Localization.Dynamic.Console
{
    /// <summary>
    /// A localization console module.
    /// </summary>
    public class LocalizationConsoleModule : ModuleConsoleItem
    {
        /// <summary>
        ///     Values that represent exit codes.
        /// </summary>
        public enum ExitCode
        {
            /// <summary>
            ///     An enum constant representing the success option.
            /// </summary>
            Success = 0,

            /// <summary>
            ///     An enum constant representing the error option.
            /// </summary>
            Error = 1
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalizationConsoleModule() : base("Localization")
        {
        }

        protected override void Initialize()
        {

        }

        /// <summary>
        /// Configure command.
        /// </summary>
        ///
        /// <returns>
        /// A Command.
        /// </returns>
        public override Command ConfigureCommand()
        {
            var cmd = new Command("--localization", "Localization of the software.");
            return cmd;
        }
    }
}
using System.Linq;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Reflection.AssemblyScanner;
using FluiTec.AppFx.Localization.Reflection.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Tests.Reflection
{
    /// <summary>
    ///     (Unit Test Class) an assembly scanner test.
    /// </summary>
    [TestClass]
    public class AssemblyScannerTest
    {
        /// <summary>
        ///     Can get current assembly.
        /// </summary>
        [TestMethod]
        public void CanGetCurrentAssembly()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);

            Assert.IsNotNull(aScanner.GetAssemblies()
                .SingleOrDefault(a => a.FullName == typeof(AssemblyScannerTest).Assembly.FullName));
        }
    }
}
using System.Linq;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Reflection.AssemblyScanner;
using FluiTec.AppFx.Localization.Reflection.Helpers;
using FluiTec.AppFx.Localization.Reflection.TypeScanner;
using FluiTec.AppFx.Localization.Tests.Reflection.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Tests.Reflection
{
    /// <summary>
    ///     (Unit Test Class) a type scanner test.
    /// </summary>
    [TestClass]
    public class TypeScannerTest
    {
        /// <summary>
        ///     Can find localized class.
        /// </summary>
        [TestMethod]
        public void CanFindLocalizedClass()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);

            Assert.IsTrue(tScanner.GetTypes(aScanner.GetAssemblies()).Contains(typeof(LocalizedModel)));
        }

        /// <summary>
        ///     Can omit unlocalized class.
        /// </summary>
        [TestMethod]
        public void CanOmitUnlocalizedClass()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);

            Assert.IsFalse(tScanner.GetTypes(aScanner.GetAssemblies()).Contains(typeof(UnlocalizedModel)));
        }

        /// <summary>
        ///     Can find localized enum.
        /// </summary>
        [TestMethod]
        public void CanFindLocalizedEnum()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);

            Assert.IsTrue(tScanner.GetTypes(aScanner.GetAssemblies()).Contains(typeof(LocalizedEnum)));
        }

        /// <summary>
        ///     Can omit unlocalized enum.
        /// </summary>
        [TestMethod]
        public void CanOmitUnlocalizedEnum()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);

            Assert.IsFalse(tScanner.GetTypes(aScanner.GetAssemblies()).Contains(typeof(UnlocalizedEnum)));
        }
    }
}
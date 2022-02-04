using System.Linq;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Reflection.AssemblyScanner;
using FluiTec.AppFx.Localization.Reflection.Helpers;
using FluiTec.AppFx.Localization.Reflection.MemberScanner;
using FluiTec.AppFx.Localization.Reflection.TypeScanner;
using FluiTec.AppFx.Localization.Tests.Reflection.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Tests.Reflection
{
    /// <summary>
    /// (Unit Test Class) a member scanner test.
    /// </summary>
    [TestClass]
    public class MemberScannerTest
    {
        /// <summary>
        /// Can find property.
        /// </summary>
        [TestMethod]
        public void CanFindProperty()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(LocalizedModel)))
                .Any(m => m.Name == nameof(LocalizedModel.Name)));
        }

        /// <summary>
        /// Can find inherited propery.
        /// </summary>
        [TestMethod]
        public void CanFindProperyInInheritingType()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(InheritingLocalizedModel)))
                .Any(m => m.Name == nameof(InheritingLocalizedModel.Name)));

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(InheritingLocalizedModel)))
                .Any(m => m.Name == nameof(InheritingLocalizedModel.Name2)));
        }

        /// <summary>
        /// Can find inherited propery.
        /// </summary>
        [TestMethod]
        public void CanFindProperyInNonInheritingType()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            Assert.IsFalse(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(NonInheritingLocalizedModel)))
                .Any(m => m.Name == nameof(NonInheritingLocalizedModel.Name)));

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(NonInheritingLocalizedModel)))
                .Any(m => m.Name == nameof(NonInheritingLocalizedModel.Name2)));
        }

        /// <summary>
        /// Can find only included property.
        /// </summary>
        [TestMethod]
        public void CanFindOnlyIncludedProperty()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            Assert.IsFalse(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(OnlyIncludedLocalizedModel)))
                .Any(m => m.Name == nameof(OnlyIncludedLocalizedModel.Name)));

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(OnlyIncludedLocalizedModel)))
                .Any(m => m.Name == nameof(OnlyIncludedLocalizedModel.Name2)));
        }

        /// <summary>
        /// Can find only included property in inherinting class.
        /// </summary>
        [TestMethod]
        public void CanFindOnlyIncludedPropertyInInherintingClass()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            Assert.IsFalse(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(InheritingOnlyIncludedLocalizedModel)))
                .Any(m => m.Name == nameof(InheritingOnlyIncludedLocalizedModel.Name)));

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(InheritingOnlyIncludedLocalizedModel)))
                .Any(m => m.Name == nameof(InheritingOnlyIncludedLocalizedModel.Name2)));

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(InheritingOnlyIncludedLocalizedModel)))
                .Any(m => m.Name == nameof(InheritingOnlyIncludedLocalizedModel.Name3)));
        }

        /// <summary>
        /// Can find only included property in non inherinting class.
        /// </summary>
        [TestMethod]
        public void CanFindOnlyIncludedPropertyInNonInherintingClass()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            Assert.IsFalse(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(NonInheritingOnlyIncludedLocalizedModel)))
                .Any(m => m.Name == nameof(NonInheritingOnlyIncludedLocalizedModel.Name2)));

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(NonInheritingOnlyIncludedLocalizedModel)))
                .Any(m => m.Name == nameof(NonInheritingOnlyIncludedLocalizedModel.Name3)));
        }

        /// <summary>
        /// Can find properties.
        /// </summary>
        [TestMethod]
        public void CanFindProperties()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(LocalizedModelMulti)))
                .Any(m => m.Name == nameof(LocalizedModelMulti.Name1)));
            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(LocalizedModelMulti)))
                .Any(m => m.Name == nameof(LocalizedModelMulti.Name2)));
        }

        /// <summary>
        /// Can find enum value.
        /// </summary>
        [TestMethod]
        public void CanFindEnumValue()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(LocalizedEnum)))
                .Any(m => m.Name == nameof(LocalizedEnum.Value1)));
        }

        /// <summary>
        /// Can find enum values.
        /// </summary>
        [TestMethod]
        public void CanFindEnumValues()
        {
            var options = new ServiceLocalizationOptions();
            var helper = new ReflectionHelper();
            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            Assert.IsTrue(mScanner.GetMembers(tScanner
                    .GetTypes(aScanner.GetAssemblies())
                    .Single(t => t == typeof(LocalizedEnumSingle)))
                .Any(m => m.Name == nameof(LocalizedEnumSingle.Value1)));
        }
    }
}
using System;
using System.Collections.Generic;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Dynamic;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Import;
using FluiTec.AppFx.Localization.Localizers;
using FluiTec.AppFx.Localization.Reflection.AssemblyScanner;
using FluiTec.AppFx.Localization.Reflection.Attributes;
using FluiTec.AppFx.Localization.Reflection.Helpers;
using FluiTec.AppFx.Localization.Reflection.MemberScanner;
using FluiTec.AppFx.Localization.Reflection.TypeScanner;
using FluiTec.AppFx.Localization.Services;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization.CliTest
{
    /// <summary>
    /// A program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry-point for this application.
        /// </summary>
        ///
        /// <param name="args"> An array of command-line argument strings. </param>
        private static void Main(string[] args)
        {
            //TestScanner();
            //TestStringLocalizer();
            TestStringImporter();
            System.Console.ReadLine();
        }

        /// <summary>
        /// Tests scanner.
        /// </summary>
        private static void TestScanner()
        {
            var options = new ServiceLocalizationOptions();

            var helper = new ReflectionHelper();

            var aScanner = new ExclusionFilteringAssemblyScanner(options, helper);
            var tScanner = new LocalizedAttributeFilteringTypeScanner(helper);
            var mScanner = new DefaultFilteringMemberScanner(helper);

            foreach (var a in aScanner.GetAssemblies())
            {
                System.Console.WriteLine(a.FullName);
                foreach (var t in tScanner.GetTypes(a))
                {
                    System.Console.WriteLine($"-> {t.FullName}");
                    foreach (var m in mScanner.GetMembers(t))
                    {
                        System.Console.WriteLine($">-> {m.Name}");
                    }
                }
            }
        }

        /// <summary>
        /// Tests string localizer.
        /// </summary>
        private static void TestStringLocalizer()
        {
            var sp = GetServiceProvider();
            AddSampleData(sp);

            var factory = sp.GetRequiredService<IStringLocalizerFactory>();

            ReportContents(factory.Create(typeof(Program)), "localizer");
        }

        /// <summary>
        /// Tests string importer.
        /// </summary>
        private static void TestStringImporter()
        {
            var sp = GetServiceProvider();

            var importService = sp.GetRequiredService<ILocalizationImportService>();
            var count = importService.Import();
            var added = sp.GetRequiredService<ILocalizationDataService>().BeginUnitOfWork().TranslationRepository.GetAll();
        }

        /// <summary>
        /// Reports the contents.
        /// </summary>
        ///
        /// <param name="localizer">    The localizer. </param>
        /// <param name="name">         The name. </param>
        private static void ReportContents(IStringLocalizer localizer, string name)
        {
            System.Console.WriteLine($"Localized strings in <{name}>:");
            foreach(var s in localizer.GetAllStrings())
                System.Console.WriteLine($"-> {s}");
        }

        /// <summary>
        /// Gets service provider.
        /// </summary>
        ///
        /// <returns>
        /// The service provider.
        /// </returns>
        private static IServiceProvider GetServiceProvider()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new []
                {
                    new KeyValuePair<string, string>("DynamicDataOptions:Provider", "NMemory"),
                    new KeyValuePair<string, string>("DynamicDataOptions:AutoMigrate", "false"),
                    new KeyValuePair<string, string>("ServiceLocalizationOptions:MemoryCacheEntryOptions:SlidingExpiration", "00:15:00"),
                    new KeyValuePair<string, string>("ServiceLocalizationOptions:AssemblyFilterExlusions:0", "test1"),
                    new KeyValuePair<string, string>("ServiceLocalizationOptions:AssemblyFilterExlusions:1", "test2"),
                    new KeyValuePair<string, string>("ServiceLocalizationImportOptions:DefaultLanguageIsoName", "en"),
                    new KeyValuePair<string, string>("ServiceLocalizationImportOptions:UpdateableAuthors:0", "Import-Code"),
                    new KeyValuePair<string, string>("ServiceLocalizationImportOptions:UpdateableAuthors:1", "Import-File (json)")
                })
                .Build();

            var manager = new ValidatingConfigurationManager(config);
            var services = new ServiceCollection();
            
            services.ConfigureDynamicLocalizationDataProvider(manager);
            services.AddLogging();

            services.Configure<ServiceLocalizationOptions>(manager, true);
            services.Configure<ServiceLocalizationImportOptions>(manager, true);
            services.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache));
            services.AddSingleton(typeof(ILocalizationService), typeof(MemoryBackedDataLocalizationService));
            services.AddSingleton(typeof(IStringLocalizerFactory), typeof(ServiceStringLocalizerFactory));

            services.AddSingleton<ILocalizationImportService, LocalizationImportService>();
            services.AddSingleton<ReflectionHelper>();
            services.AddSingleton<IAssemblyScanner, ExclusionFilteringAssemblyScanner>();
            services.AddSingleton<ITypeScanner, LocalizedAttributeFilteringTypeScanner>();
            services.AddSingleton<IMemberScanner, DefaultFilteringMemberScanner>();
            services.AddSingleton<ILocalizationSource, CodeLocalizationSource>();

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Adds a sample data.
        /// </summary>
        ///
        /// <param name="serviceProvider">  The service provider. </param>
        private static void AddSampleData(IServiceProvider serviceProvider)
        {
            using var uow = serviceProvider.GetRequiredService<ILocalizationDataService>().BeginUnitOfWork();

            var author1 = uow.AuthorRepository.Add(new AuthorEntity {Name = "Max Mustermann"});
            var language1 = uow.LanguageRepository.Add(new LanguageEntity {IsoName = "de", Name = "Deutsch"});
            var language2 = uow.LanguageRepository.Add(new LanguageEntity {IsoName = "de-DE", Name = "Deutsch (Deutschland)"});
            var resource1 = uow.ResourceRepository.Add(new ResourceEntity
                {ResourceKey = "FluiTec.AppFx.Localization.CliTest.Program.Key", ModificationDate = DateTimeOffset.UtcNow, AuthorId = author1.Id});
            var translation1 = uow.TranslationRepository.Add(new TranslationEntity
                {ResourceId = resource1.Id, LanguageId = language1.Id, Value = "key-de"});
            var translation2 = uow.TranslationRepository.Add(new TranslationEntity
                {ResourceId = resource1.Id, LanguageId = language2.Id, Value = "key-de-DE"});
            var resource2 = uow.ResourceRepository.Add(new ResourceEntity
                {ResourceKey = "FluiTec.AppFx.Localization.CliTest.Program.Key2", ModificationDate = DateTimeOffset.UtcNow, AuthorId = author1.Id});
            var translation3 = uow.TranslationRepository.Add(new TranslationEntity
                {ResourceId = resource2.Id, LanguageId = language1.Id, Value = "key2-de"});

            uow.Commit();
        }
    }

    [Localized]
    public class Simple
    {
        [Translation("iv", "Invariant")]
        [Translation("de", "Deutsch")]
        [Translation("de-DE", "Deutsch (Deutschland)")]
        public string Name { get; set; }
    }

    [Localized]
    public class Inherited : Simple
    {
        [Translation("iv", "Invariant")]
        [Translation("de", "Deutsch")]
        [Translation("de-DE", "Deutsch (Deutschland)")]
        public string Name2 { get; set; }
    }

    [Localized(Inherited = false)]
    public class InheritedFalse : Simple
    {
        [Translation("iv", "Invariant")]
        [Translation("de", "Deutsch")]
        [Translation("de-DE", "Deutsch (Deutschland)")]
        public string Name2 { get; set; }
    }

    [Localized(OnlyIncluded = true)]
    public class OnlyIncluded
    {
        [Translation("iv", "Invariant")]
        [Translation("de", "Deutsch")]
        [Translation("de-DE", "Deutsch (Deutschland)")]
        public string Name { get; set; }

        [Include]
        [Translation("iv", "Invariant")]
        [Translation("de", "Deutsch")]
        [Translation("de-DE", "Deutsch (Deutschland)")]
        public string Name2 { get; set; }
    }

    [Localized(OnlyIncluded = true)]
    public class OnlyIncludedInheritedTrue : OnlyIncluded
    {
        [Include]
        [Translation("iv", "Invariant")]
        [Translation("de", "Deutsch")]
        [Translation("de-DE", "Deutsch (Deutschland)")]
        public string Name3 { get; set; }
    }

    [Localized(Inherited = false, OnlyIncluded = true)]
    public class OnlyIncludedInheritedFalse : OnlyIncluded
    {
        [Include]
        [Translation("iv", "Invariant")]
        [Translation("de", "Deutsch")]
        [Translation("de-DE", "Deutsch (Deutschland)")]
        public string Name4 { get; set; }
    }

    [Localized]
    public enum Enum
    {
        [Translation("iv", "Invariant")]
        [Translation("de", "Deutsch")]
        [Translation("de-DE", "Deutsch (Deutschland)")]
        Var1,
        [Translation("iv", "Invariant")]
        [Translation("de", "Deutsch")]
        [Translation("de-DE", "Deutsch (Deutschland)")]
        Var2
    }
}
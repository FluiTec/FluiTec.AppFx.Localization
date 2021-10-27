using System;
using System.Collections.Generic;
using System.Globalization;
using FluiTec.AppFx.Localization.Dynamic;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Services;
using FluiTec.AppFx.Options.Managers;
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
            var sp = GetServiceProvider();
            AddSampleData(sp);

            var factory = sp.GetRequiredService<IStringLocalizerFactory>();
            var localizer = factory.Create(typeof(Program));
            var strings = localizer.GetAllStrings();
        }

        static IServiceProvider GetServiceProvider()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new []
                {
                    new KeyValuePair<string, string>("DynamicDataOptions:Provider", "NMemory"),
                    new KeyValuePair<string, string>("DynamicDataOptions:AutoMigrate", "false")
                })
                .Build();
            var manager = new ConsoleReportingConfigurationManager(config);
            var services = new ServiceCollection();
            
            services.ConfigureDynamicLocalizationDataProvider(manager);
            services.AddLogging();

            //services.AddSingleton(typeof(IStringLocalizerFactory), typeof(DbStringLocalizerFactory));

            services.AddSingleton(typeof(ILocalizationService), typeof(DataLocalizationService));
            services.AddSingleton(typeof(IStringLocalizerFactory), typeof(ServiceStringLocalizerFactory));

            return services.BuildServiceProvider();
        }

        static void AddSampleData(IServiceProvider serviceProvider)
        {
            using var uow = serviceProvider.GetRequiredService<ILocalizationDataService>().BeginUnitOfWork();

            var author1 = uow.AuthorRepository.Add(new AuthorEntity {Name = "Max Mustermann"});
            var language1 = uow.LanguageRepository.Add(new LanguageEntity {IsoName = "de", Name = "Deutsch"});
            var language2 = uow.LanguageRepository.Add(new LanguageEntity {IsoName = "de-DE", Name = "Deutsch (Deutschland)"});
            var resource1 = uow.ResourceRepository.Add(new ResourceEntity
                {ResourceKey = "Namespace.Class.Key", ModificationDate = DateTimeOffset.Now, AuthorId = author1.Id});
            var translation1 = uow.TranslationRepository.Add(new TranslationEntity
                {ResourceId = resource1.Id, LanguageId = language1.Id, Value = "de"});
            var translation2 = uow.TranslationRepository.Add(new TranslationEntity
                {ResourceId = resource1.Id, LanguageId = language2.Id, Value = "de-DE"});
            var resource2 = uow.ResourceRepository.Add(new ResourceEntity
                {ResourceKey = "Namespace.Class2.Key", ModificationDate = DateTimeOffset.Now, AuthorId = author1.Id});
            var translation3 = uow.TranslationRepository.Add(new TranslationEntity
                {ResourceId = resource2.Id, LanguageId = language1.Id, Value = "de"});

            uow.Commit();
        }
    }
}

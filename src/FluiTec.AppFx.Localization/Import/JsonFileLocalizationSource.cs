using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluiTec.AppFx.Localization.Configuration;
using Newtonsoft.Json;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    ///     A JSON file localization source.
    /// </summary>
    public class JsonFileLocalizationSource : FileLocalizationSource
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="importOptions">    Options for controlling the import. </param>
        public JsonFileLocalizationSource(ServiceLocalizationImportOptions importOptions) : base(importOptions)
        {
        }

        /// <summary>
        ///     Gets the file extension.
        /// </summary>
        /// <value>
        ///     The file extension.
        /// </value>
        public override string FileExtension => ".json";

        /// <summary>
        ///     Finds the resources in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        public override IEnumerable<ILocalizationResource> FindResources()
        {
            var resources = new List<ILocalizationResource>();

            resources.AddRange(
                ImportFiles
                    .Select(f =>
                        JsonConvert.DeserializeObject<IEnumerable<FileLocalizationResource>>(File.ReadAllText(f)))
                    .SelectMany(f => f)
                    .SelectMany(r => r.Translations.Select(t => new LocalizationResource
                    {
                        Author = FileAuthor,
                        ResourceKey = r.ResourceKey,
                        Language = t.Language,
                        Translation = t.Translation
                    }))
            );

            return resources;
        }
    }
}
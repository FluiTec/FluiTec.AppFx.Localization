using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluiTec.AppFx.Localization.Configuration;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    ///     A file localization source.
    /// </summary>
    public abstract class FileLocalizationSource : BaseLocalizationSource
    {
        /// <summary>
        ///     (Immutable) the file author.
        /// </summary>
        public const string FileAuthor = "Import-File";

        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="importOptions">    Options for controlling the import. </param>
        protected FileLocalizationSource(ServiceLocalizationImportOptions importOptions) : base(importOptions)
        {
            ImportFiles = new List<string>();

            AddImportFiles();
        }

        /// <summary>
        ///     Gets the file extension.
        /// </summary>
        /// <value>
        ///     The file extension.
        /// </value>
        public abstract string FileExtension { get; }

        /// <summary>
        ///     Gets the import files.
        /// </summary>
        /// <value>
        ///     The import files.
        /// </value>
        protected List<string> ImportFiles { get; }

        /// <summary>
        ///     Adds import files.
        /// </summary>
        protected void AddImportFiles()
        {
            if (ImportOptions.ImportFiles == null) return;

            foreach (var file in ImportOptions.ImportFiles.Where(CanHandle))
                AddFile(file);
        }

        /// <summary>
        ///     Determine if we can handle.
        /// </summary>
        /// <param name="fileName"> Filename of the file. </param>
        /// <returns>
        ///     True if we can handle, false if not.
        /// </returns>
        public virtual bool CanHandle(string fileName)
        {
            return File.Exists(fileName) && string.Equals(new FileInfo(fileName).Extension, FileExtension,
                StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        ///     Adds a file.
        /// </summary>
        /// <param name="fileName"> Filename of the file. </param>
        public virtual void AddFile(string fileName)
        {
            if (File.Exists(fileName))
                ImportFiles.Add(fileName);
        }
    }
}
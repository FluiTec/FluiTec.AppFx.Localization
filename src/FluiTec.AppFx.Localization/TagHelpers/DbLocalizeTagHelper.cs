using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

// ReSharper disable VirtualMemberCallInConstructor

namespace FluiTec.AppFx.Localization.TagHelpers
{
    [HtmlTargetElement(LocalizeTagName)]
    public class DbLocalizeTagHelper : DbGenericLocalizeTagHelper
    {
        /// <summary>Name of the localize tag.</summary>
        private const string LocalizeTagName = "dblocalize";

        /// <summary>Constructor.</summary>
        /// <param name="localizerFactory">     The localizer factory. </param>
        /// <param name="hostingEnvironment">   The hosting environment. </param>
        /// <param name="options">              Options for controlling the operation. </param>
        public DbLocalizeTagHelper(IHtmlLocalizerFactory localizerFactory, IHostingEnvironment hostingEnvironment,
            IOptions<LocalizeTagHelperOptions> options)
            : base(localizerFactory, hostingEnvironment, options)
        {
        }

        /// <summary>
        ///     Asynchronously executes the
        ///     <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given
        ///     <paramref name="context" /> and.
        /// </summary>
        /// <remarks>
        ///     By default this calls into
        ///     <see
        ///         cref="M:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper.Process(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext,Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput)" />
        ///     .
        /// </remarks>
        /// <param name="context">  Contains information associated with the current HTML tag. </param>
        /// <param name="output">   The Html-Output.</param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task" /> that on completion updates the
        ///     <paramref name="output" />.
        /// </returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.TagName = null;
        }
    }
}
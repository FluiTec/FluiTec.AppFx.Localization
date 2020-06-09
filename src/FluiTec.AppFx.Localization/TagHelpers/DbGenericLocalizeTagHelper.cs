using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Localization.TagHelpers
{
    /// <summary>A database generic localize tag helper.</summary>
    [HtmlTargetElement(Attributes = "dblocalize")]
    public class DbGenericLocalizeTagHelper : TagHelper
    {
        private const string LocalizeHtml = "html";
        private const string LocalizeName = "resource-name";
        private const string LocalizeNewline = "newline";
        private const string LocalizeTrim = "trim";
        private const string LocalizeTrimLines = "trimlines";
        private const string LocalizeType = "resource-type";
        private readonly string _applicationName;
        private readonly IHtmlLocalizerFactory _localizerFactory;

        public DbGenericLocalizeTagHelper(IHtmlLocalizerFactory localizerFactory,
            IHostingEnvironment hostingEnvironment, IOptions<LocalizeTagHelperOptions> options)
        {
            _localizerFactory = localizerFactory ?? throw new ArgumentNullException(nameof(localizerFactory));
            if (hostingEnvironment == null) throw new ArgumentNullException(nameof(hostingEnvironment));

            _localizerFactory = localizerFactory;
            _applicationName = hostingEnvironment.ApplicationName;

            if (options != null)
            {
                NewLineHandling = options.Value.NewLineHandling;
                TrimWhitespace = options.Value.TrimWhitespace;
                IsHtml = !options.Value.HtmlEncodeByDefault;
            }
            else
            {
                NewLineHandling = NewLineHandling.Auto;
                TrimWhitespace = true;
                IsHtml = false;
            }
        }

        [HtmlAttributeName(LocalizeHtml)] public virtual bool IsHtml { get; set; }

        [HtmlAttributeName("dblocalizer")] public IHtmlLocalizer Localizer { get; set; }

        [HtmlAttributeName(LocalizeName)] public virtual string Name { get; set; } = string.Empty;

        [HtmlAttributeName(LocalizeNewline)] public virtual NewLineHandling NewLineHandling { get; set; }

        [HtmlAttributeName(LocalizeTrimLines)] public virtual bool TrimEachLine { get; set; }

        [HtmlAttributeName(LocalizeTrim)] public virtual bool TrimWhitespace { get; set; }

        [HtmlAttributeName(LocalizeType)] public virtual Type Type { get; set; }

        [HtmlAttributeNotBound] [ViewContext] public ViewContext ViewContext { get; set; }

        protected virtual bool SupportsParameters => true;

        public override void Init(TagHelperContext context)
        {
            Localizer = Localizer ??
                        throw new ArgumentNullException(
                            nameof(Localizer)); //localizerFactory.ResolveLocalizer(ViewContext, applicationName, Type, Name);

            if (!SupportsParameters)
            {
                return;
            }

            Stack<List<object>> currentStack;

            if (!context.Items.ContainsKey(typeof(DbGenericLocalizeTagHelper)))
            {
                currentStack = new Stack<List<object>>();
                context.Items.Add(typeof(DbGenericLocalizeTagHelper), currentStack);
            }
            else
            {
                currentStack = (Stack<List<object>>) context.Items[typeof(DbGenericLocalizeTagHelper)];
            }

            currentStack.Push(new List<object>());
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes.ContainsName("localize"))
            {
                int index = output.Attributes.IndexOfName("localize");
                output.Attributes.RemoveAt(index);
            }

            var content = await GetContentAsync(context, output);

            if (TrimWhitespace || TrimEachLine)
            {
                content = content?.Trim();
            }

            if (NewLineHandling != NewLineHandling.None || TrimEachLine)
            {
                content = HandleNormalization(content, NewLineHandling, TrimEachLine);
            }

            var parameters = GetParameters(context).ToList();
            if (IsHtml)
            {
                LocalizedHtmlString locString;
                if (parameters.Any())
                {
                    locString = Localizer[content, parameters.ToArray()];
                }
                else
                {
                    locString = Localizer[content];
                }

                SetHtmlContent(context, output.Content, locString);
            }
            else
            {
                LocalizedString locString;
                if (parameters.Any())
                {
                    locString = Localizer.GetString(content, parameters.ToArray());
                }
                else
                {
                    locString = Localizer.GetString(content);
                }

                SetContent(context, output.Content, locString);
            }
        }

        protected virtual async Task<string> GetContentAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync(true);
            if (output.IsContentModified)
            {
                return output.Content.GetContent(NullHtmlEncoder.Default);
            }

            return content.GetContent(NullHtmlEncoder.Default);
        }

        protected virtual IEnumerable<object> GetParameters(TagHelperContext context)
        {
            if (!context.Items.ContainsKey(typeof(DbGenericLocalizeTagHelper)))
            {
                return new object[0];
            }

            var stack = (Stack<List<object>>) context.Items[typeof(DbGenericLocalizeTagHelper)];

            return stack.Pop();
        }

        protected virtual void SetContent(TagHelperContext context, TagHelperContent outputContent, string content)
        {
            outputContent.SetContent(content);
        }

        protected virtual void SetHtmlContent(TagHelperContext context, TagHelperContent outputContent,
            IHtmlContent htmlContent)
        {
            outputContent.SetHtmlContent(htmlContent);
        }

        private static void AppendContent(string content, bool trimEachLine, StringBuilder newContent, int lastIndex,
            int index)
        {
            var substring = content.Substring(lastIndex, index - lastIndex);
            if (trimEachLine)
            {
                newContent.Append(substring.Trim());
            }
            else
            {
                newContent.Append(substring.TrimEnd('\r'));
            }
        }

        private static void AppendNewLine(string content, bool trimEachLine, StringBuilder newContent, int index,
            string newLine)
        {
            if (newLine == null)
            {
                if (trimEachLine && index > 0 && content[index - 1] == '\r')
                {
                    newContent.Append("\r\n");
                }
                else
                {
                    newContent.Append('\n');
                }
            }
            else
            {
                newContent.Append(newLine);
            }
        }

        private static string GetDefaultNewLine(NewLineHandling newLineHandling)
        {
            string newLine = null;
            switch (newLineHandling)
            {
                case NewLineHandling.Auto:
                    newLine = Environment.NewLine;
                    break;

                case NewLineHandling.Windows:
                    newLine = "\r\n";
                    break;

                case NewLineHandling.Unix:
                    newLine = "\n";
                    break;
            }

            return newLine;
        }

        private static string HandleNormalization(string content, NewLineHandling newLineHandling, bool trimEachLine)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }

            if (trimEachLine)
            {
                content = content.Trim();
            }

            StringBuilder newContent = new StringBuilder();
            int lastIndex = 0;
            int index;
            string newLine = GetDefaultNewLine(newLineHandling);

            while ((index = content.IndexOf('\n', lastIndex)) >= 0)
            {
                AppendContent(content, trimEachLine, newContent, lastIndex, index);
                AppendNewLine(content, trimEachLine, newContent, index, newLine);

                lastIndex = index + 1;
            }

            AppendContent(content, trimEachLine, newContent, lastIndex, content.Length);

            return newContent.ToString();
        }
    }
}
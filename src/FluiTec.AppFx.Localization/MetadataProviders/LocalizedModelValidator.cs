using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluiTec.DbLocalizationProvider.DataAnnotations;
using FluiTec.DbLocalizationProvider.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FluiTec.AppFx.Localization.MetadataProviders
{
    /// <summary>
    ///     Credits:
    ///     https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.DataAnnotations/Internal/DataAnnotationsModelValidator.cs.
    /// </summary>
    public class LocalizedModelValidator : IModelValidator
    {
        /// <summary>   The empty validation context instance. </summary>
        private static readonly object EmptyValidationContextInstance = new object();

        /// <summary>   The attribute. </summary>
        private readonly ValidationAttribute _attribute;

        /// <summary>   Constructor. </summary>
        /// <param name="attribute">    The attribute. </param>
        public LocalizedModelValidator(ValidationAttribute attribute)
        {
            _attribute = attribute;
        }

        /// <summary>   Validates the model value. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have
        ///     unsupported or illegal values.
        /// </exception>
        /// <param name="validationContext">
        ///     The
        ///     <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ModelValidationContext" />.
        /// </param>
        /// <returns>
        ///     A list of
        ///     <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ModelValidationResult" />
        ///     indicating the results of validating the model value.
        /// </returns>
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext validationContext)
        {
            if (validationContext == null)
                throw new ArgumentNullException(nameof(validationContext));

            if (validationContext.ModelMetadata == null)
                throw new ArgumentException($"{nameof(validationContext.ModelMetadata)} is null",
                    nameof(validationContext));

            if (validationContext.MetadataProvider == null)
                throw new ArgumentException($"{nameof(validationContext.MetadataProvider)} in null",
                    nameof(validationContext));

            var metadata = validationContext.ModelMetadata;
            var memberName = metadata.PropertyName;
            var container = validationContext.Container;

            var context = new ValidationContext(
                container ?? validationContext.Model ?? EmptyValidationContextInstance,
                validationContext.ActionContext?.HttpContext?.RequestServices,
                null)
            {
                DisplayName = metadata.GetDisplayName(),
                MemberName = memberName
            };

            var result = _attribute.GetValidationResult(validationContext.Model, context);
            if (result == ValidationResult.Success) return Enumerable.Empty<ModelValidationResult>();
            var resourceKey =
                ResourceKeyBuilder.BuildResourceKey(metadata.ContainerType, metadata.PropertyName, _attribute);
            var translation = ModelMetadataLocalizationHelper.GetTranslation(resourceKey);
            var errorMessage = !string.IsNullOrEmpty(translation) ? translation : result?.ErrorMessage;

            var validationResults = new List<ModelValidationResult>();
            if (result?.MemberNames != null)
                validationResults.AddRange(result.MemberNames.Select(resultMemberName =>
                        string.Equals(resultMemberName, memberName, StringComparison.Ordinal)
                            ? null
                            : resultMemberName)
                    .Select(newMemberName => new ModelValidationResult(newMemberName, errorMessage)));

            if (validationResults.Count == 0)
                validationResults.Add(new ModelValidationResult(null, errorMessage));

            return validationResults;
        }
    }
}
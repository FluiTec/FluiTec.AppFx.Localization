using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FluiTec.AppFx.Localization.MetadataProviders
{
    /// <summary>
    ///     Credits:
    ///     https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.DataAnnotations/Internal/DataAnnotationsMetadataProvider.cs.
    /// </summary>
    public class LocalizedValidationMetadataProvider : IModelValidatorProvider
    {
        /// <summary>
        ///     Creates the validators for
        ///     <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ModelValidatorProviderContext.ModelMetadata" />.
        /// </summary>
        /// <remarks>
        ///     Implementations should add the
        ///     <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.Validation.IModelValidator" />
        ///     instances to the appropriate
        ///     <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidatorItem" /> instance
        ///     which should be added to
        ///     <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ModelValidatorProviderContext.Results" />.
        /// </remarks>
        /// <param name="context">
        ///     The
        ///     <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ModelValidatorProviderContext" />.
        /// </param>
        public void CreateValidators(ModelValidatorProviderContext context)
        {
            for (var i = 0; i < context.Results.Count; i++)
            {
                var validatorItem = context.Results[i];
                if (validatorItem.Validator != null && !(validatorItem.Validator is DataAnnotationsModelValidator))
                    continue;

                if (!(validatorItem.ValidatorMetadata is ValidationAttribute attribute))
                    continue;

                validatorItem.Validator = new LocalizedModelValidator(attribute);
                validatorItem.IsReusable = true;

                // Inserts validators based on whether or not they are 'required'. We want to run
                // 'required' validators first so that we get the best possible error message.
                if (!(attribute is RequiredAttribute)) continue;
                context.Results.Remove(validatorItem);
                context.Results.Insert(0, validatorItem);
            }

            // Produce a validator if the type supports IValidatableObject
            if (typeof(IValidatableObject).IsAssignableFrom(context.ModelMetadata.ModelType))
                context.Results.Add(new ValidatorItem
                {
                    Validator = new ValidatableObjectAdapter(),
                    IsReusable = true
                });
        }
    }
}
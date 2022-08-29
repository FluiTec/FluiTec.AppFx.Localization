using FluiTec.AppFx.Localization.Reflection.Attributes;

namespace FluiTec.AppFx.Localization.WebSample.Resources;

[Localized]
public class DataAnnotationsResource
{
    [Translation("iv", "The field '{0}' is required.")]
    [Translation("de", "Das Feld '{0}' ist ein Pflichtfeld.")]
    public string Required { get; set; }
}
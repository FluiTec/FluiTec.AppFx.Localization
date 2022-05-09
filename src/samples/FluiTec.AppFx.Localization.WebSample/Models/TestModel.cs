using System.ComponentModel.DataAnnotations;
using FluiTec.AppFx.Localization.Reflection.Attributes;

namespace FluiTec.AppFx.Localization.WebSample.Models;

/// <summary>
/// A data Model for the test.
/// </summary>
[Localized]
public class TestModel
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    ///
    /// <value>
    /// The name.
    /// </value>
    [Display(Name=nameof(Name))]
    [Translation("iv", "Invariant")]
    [Translation("de", "Deutsch")]
    [Translation("de-DE", "Deutsch (Deutschland)")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the test.
    /// </summary>
    ///
    /// <value>
    /// The test.
    /// </value>
    [Display(Name=nameof(Test))]
    [Translation("iv", "Invariant")]
    [Translation("de", "Deutsch")]
    [Translation("de-DE", "Deutsch (Deutschland)")]
    public string Test { get; set; }
}
using System.ComponentModel.DataAnnotations;
using FluiTec.DbLocalizationProvider.Abstractions;

namespace WebSample.Models
{
    [LocalizedModel]
    public class TestModel
    {
        [Display(Description = "Test-ENG")]
        [DisplayTranslationForCulture("Test", "Test-DE", "de")]
        public string Test { get; set; }
    }
}

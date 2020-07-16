using System.ComponentModel.DataAnnotations;
using FluiTec.DbLocalizationProvider.Abstractions;

namespace WebSample.Models
{
    [LocalizedModel]
    public class TestModel
    {
        [Display(Description = "Test-ENG")]
        [DisplayTranslationForCulture("Test-DE", "de")]
        public string Test { get; set; }

        [Display(Description = "Test2-ENG")]
        [DisplayTranslationForCulture("Test2-DE", "de")]
        public string Test2 {get;set;}
    }
}
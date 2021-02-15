using System.ComponentModel.DataAnnotations;

namespace MyTerraformStack.models
{
    public class ProviderOptions
    {
        public const string SectionName = "Config:Provider";
        [Required(ErrorMessage = "DigitalOceanToken is required for ProviderOptions")]
        public string DigitalOceanToken { get; set; }
        [Required(ErrorMessage = "DigitalOceanSpacesAccessId is required for ProviderOptions")]
        public string DigitalOceanSpacesAccessId { get; set; }
        [Required(ErrorMessage = "DigitalOceanSpacesSecretKey is required for ProviderOptions")]
        public string DigitalOceanSpacesSecretKey { get; set; }
    }
}

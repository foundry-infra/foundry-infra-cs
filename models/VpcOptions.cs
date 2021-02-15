using System.ComponentModel.DataAnnotations;

namespace MyTerraformStack.models
{
    public class VpcOptions
    {
        public const string SectionName = "Config:Vpc";
        [Required(ErrorMessage = "Name is required for VpcOptions")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Region is required for VpcOptions")]
        public string Region { get; set; }
    }
}

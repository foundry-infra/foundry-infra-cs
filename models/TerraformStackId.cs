using System.ComponentModel.DataAnnotations;

namespace FoundryInfra.models
{
    public class TerraformStackId
    {
        [Required(ErrorMessage = "Id is required for TerraformStackId")]
        public string Id { get; set; } = default!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace MyTerraformStack.models
{
    public class TerraformStackId
    {
        [Required(ErrorMessage = "Id is required for TerraformStackId")]
        public string Id { get; set; } = default!;
    }
}

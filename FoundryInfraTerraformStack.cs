using Constructs;
using Hashicorp.Cdktf;
using Microsoft.Extensions.Options;

namespace FoundryInfra
{
    internal class FoundryInfraTerraformStack : TerraformStack
    {
        public FoundryInfraTerraformStack(Construct scope, 
            models.TerraformStackId stackId, 
            IOptions<models.ProviderOptions> providerOptions, 
            IOptions<models.VpcOptions> vpcOptions
        ) : base(scope, stackId.Id)
        {
            // define resources here
            var digitaloceanProvider = new digitalocean.DigitaloceanProvider(this, "digitalocean", 
                new digitalocean.DigitaloceanProviderConfig
            {
                Token = providerOptions.Value.DigitalOceanToken,
                SpacesAccessId = providerOptions.Value.DigitalOceanSpacesAccessId,
                SpacesSecretKey = providerOptions.Value.DigitalOceanSpacesSecretKey,
            });
            
            var vpc = new digitalocean.Vpc(this, "vpc", new digitalocean.VpcConfig
            {
                Name = vpcOptions.Value.Name,
                Region = vpcOptions.Value.Region,
            });
        }
    }
}

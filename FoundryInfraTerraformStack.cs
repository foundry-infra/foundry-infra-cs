using Constructs;
using Hashicorp.Cdktf;

namespace MyTerraformStack
{
    internal class FoundryInfraTerraformStack : TerraformStack
    {
        public FoundryInfraTerraformStack(Construct scope, models.TerraformStackId stackId, models.ProviderConfig providerConfig, models.VpcConfig vpcConfig) : base(scope, stackId.Id)
        {
            // define resources here
            var digitaloceanProvider = new digitalocean.DigitaloceanProvider(this, "digitalocean", new digitalocean.DigitaloceanProviderConfig
            {
                Token = providerConfig.DigitalOceanToken,
                SpacesAccessId = providerConfig.DigitalOceanSpacesAccessId,
                SpacesSecretKey = providerConfig.DigitalOceanSpacesSecretKey,
            });
            
            var vpc = new digitalocean.Vpc(this, "vpc", new digitalocean.VpcConfig
            {
                Name = vpcConfig.Name,
                Region = vpcConfig.Region,
            });
        }
    }
}

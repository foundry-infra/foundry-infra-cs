using System;
using Microsoft.Extensions.Options;

namespace MyTerraformStack.models
{
    public class ProviderConfig
    {
        public string DigitalOceanToken { get; private set; }
        public string DigitalOceanSpacesAccessId { get; private set; }
        public string DigitalOceanSpacesSecretKey { get; private set; }

        public ProviderConfig(IOptions<FoundryInfraOptions> options)
        {
            if (options.Value == null)
            {
                throw new ArgumentException($"{nameof(ProviderConfig)} received invalid options argument ${nameof(options)}.");
            }

            DigitalOceanToken = options.Value.DigitalOceanToken;
            DigitalOceanSpacesAccessId = options.Value.DigitalOceanSpacesAccessId;
            DigitalOceanSpacesSecretKey = options.Value.DigitalOceanSpacesSecretKey;
        }
    }
}

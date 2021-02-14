using System;
using Microsoft.Extensions.Options;

namespace MyTerraformStack.models
{
    public class VpcConfig
    {
        public string Name { get; private set; }
        public string Region { get; private set; }
        
        public VpcConfig(IOptions<FoundryInfraOptions> options)
        {
            if (options.Value == null)
            {
                throw new ArgumentException($"{nameof(VpcConfig)} received invalid options argument ${nameof(options)}.");
            }

            Name = options.Value.VpcName;
            Region = options.Value.VpcRegion;
        }
    }
}

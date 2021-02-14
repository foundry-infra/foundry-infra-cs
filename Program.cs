using System;
using System.Threading.Tasks;
using Constructs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hashicorp.Cdktf;

namespace MyTerraformStack
{ 
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            // build on the defaults provide by CreateDefaultBuilder (application json, logging, etc)
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // The root configuration to be consumed by models
                    var configuration = hostContext.Configuration; 
                    services.Configure<FoundryInfraOptions>(configuration.GetSection(FoundryInfraOptions.Name));
                    // The configuration models  will consume the foundry infra options
                    services.AddSingleton(new models.TerraformStackId { Id = "my-terraform-stack-with-config"});
                    services.AddSingleton<models.ProviderConfig>();
                    services.AddSingleton<models.VpcConfig>();
                    // The stack will consume all models
                    services.AddSingleton<TerraformStack, FoundryInfraTerraformStack>();
                    services.AddSingleton(new App());
                    services.AddSingleton<Construct>((s) => s.GetService<App>() ?? throw new Exception($"Unable to find service {nameof(App)}."));
                    // the console service that executes app.synth
                    services.AddHostedService<ConsoleHostedService>();

                })
                .RunConsoleAsync();
        }
    }
}

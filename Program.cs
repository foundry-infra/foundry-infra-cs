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
                    var he = hostContext.HostingEnvironment;
                    var isdev = hostContext.HostingEnvironment.IsDevelopment();
                    
                    // The root configuration to be consumed by models
                    var configuration = hostContext.Configuration;
                    
                    // Add the option configuration and exercise validation on each. Not very DRY.
                    services.AddOptions<models.ProviderOptions>()
                        .Bind(configuration.GetSection(models.ProviderOptions.SectionName))
                        .ValidateDataAnnotations();
                    services.AddOptions<models.VpcOptions>()
                        .Bind(configuration.GetSection(models.VpcOptions.SectionName))
                        .ValidateDataAnnotations();
                    
                    // This configuration is for cdktf itself and set in the TerraformStack object.
                    services.AddSingleton(new models.TerraformStackId { Id = "my-terraform-stack-with-config" });
                    
                    // Use a concrete implementation for TerraformStack with all the terraform goodness
                    services.AddSingleton<TerraformStack, FoundryInfraTerraformStack>();
                    
                    // HACK: I need the App and App as the base class, Construct, so I register it twice.
                    var app = new App();
                    services.AddSingleton(app);
                    services.AddSingleton<Construct>(app);
                    
                    // the console service that executes app.synth
                    services.AddHostedService<ConsoleHostedService>();

                })
                .RunConsoleAsync();
        }
    }
}

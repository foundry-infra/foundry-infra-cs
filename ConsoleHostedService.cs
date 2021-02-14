using System;
using System.Threading;
using System.Threading.Tasks;
using Hashicorp.Cdktf;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyTerraformStack
{
    internal sealed class ConsoleHostedService : IHostedService
    {
        private readonly App _app;
        private readonly IHostApplicationLifetime _appLifetime;

        public ConsoleHostedService(ILogger<ConsoleHostedService> logger, IHostApplicationLifetime appLifetime, App app, TerraformStack stack)
        {
            Logger = logger;
            _appLifetime = appLifetime;
            _app = app;
        }

        private ILogger<ConsoleHostedService> Logger { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                var proxy = Task.Run( (/*cancellationToken*/) =>
                {
                    try
                    {
                        // the main piece of work
                        _app.Synth();
                        Logger.LogInformation("App synth complete");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, "Unhandled exception!");
                    }
                    finally
                    {
                        // Stop the application once the work is done
                        _appLifetime.StopApplication();
                    }
                }, cancellationToken);
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

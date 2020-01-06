using Microsoft.Extensions.DependencyInjection;
using ServiceStack;

namespace MyApp
{
    public class ConfigureGrpc : IConfigureServices, IConfigureAppHost
    {
        public void Configure(IServiceCollection services)
        {
            services.AddServiceStackGrpc();
        }

        public void Configure(IAppHost appHost)
        {
            appHost.Plugins.Add(new GrpcFeature(appHost.GetApp()));
        }
    }
}
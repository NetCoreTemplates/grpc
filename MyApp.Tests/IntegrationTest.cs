using System.Threading.Tasks;
using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using NUnit.Framework;
using MyApp.ServiceInterface;
using MyApp.ServiceModel;
using ProtoBuf.Grpc.Client;

namespace MyApp.Tests
{
    public class IntegrationTest
    {
        private const int Port = 2000;
        static readonly string BaseUri = $"http://localhost:{Port}/";
        private readonly ServiceStackHost appHost;

        class AppHost : AppSelfHostBase
        {
            public AppHost() : base(nameof(IntegrationTest), typeof(MyServices).Assembly) { }

            public override void Configure(Container container)
            {
                Plugins.Add(new GrpcFeature(App));
            }

            public override void ConfigureKestrel(KestrelServerOptions options)
            {
                options.ListenLocalhost(2000, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            }

            public override void Configure(IServiceCollection services)
            {
                services.AddServiceStackGrpc();
            }

            public override void Configure(IApplicationBuilder app)
            {
                app.UseRouting();
            }
        }

        public IntegrationTest()
        {
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);

            GrpcClientFactory.AllowUnencryptedHttp2 = true;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test] // Requires Host project running on https://localhost:5001 (default)
        public async Task Can_call_Hello_Service_WebHost()
        {
            var client = new GrpcServiceClient("https://localhost:5001");

            var response = await client.GetAsync(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }

        public IServiceClientAsync CreateClient() => new GrpcServiceClient(BaseUri);

        [Test]
        public async Task Can_call_Hello_Service()
        {
            var client = CreateClient();

            var response = await client.GetAsync(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }
    }
}
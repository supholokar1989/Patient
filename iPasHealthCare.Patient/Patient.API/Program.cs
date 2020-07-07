using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Patient.API;
using Serilog;
using System;
using System.IO;
using System.Net;

namespace Patient.API
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                var host = BuildWebHost(configuration, args);

                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
                  WebHost.CreateDefaultBuilder(args)
                      .CaptureStartupErrors(false)
                      .ConfigureKestrel(options =>
                      {
                          IPAddress httpAddress = IPAddress.Parse("127.0.0.1");
                          var ports = GetDefinedPorts(configuration);
                          options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
                          {
                              listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                          });

                          options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
                          {
                              listenOptions.Protocols = HttpProtocols.Http2;
                          });

                      })
                      .UseStartup<Startup>()
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .UseConfiguration(configuration)
                      .UseSerilog()
                      .Build();

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            var logstashUrl = configuration["Serilog:LogstashgUrl"];
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", AppName)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            return builder.Build();
        }
        private static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
        {
            var grpcPort = config.GetValue("GRPC_PORT", 30001);
            var port = config.GetValue("PORT", 30000);
            return (port, grpcPort);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;
using Patient.API.Infrastructure;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Patient.Domain;
using Patient.API.Infrastructure.Models;
using Patient.Infrastructure.Repository.Interfaces;
using Patient.Infrastructure.Repository;
using Patient.API.Grpc;
using Patient.Infrastructure.Grpc.Interfaces;

namespace Patient.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Polly
            //IAsyncPolicy<HttpResponseMessage> retryPolicy =
            //Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            //.RetryAsync(3);
            //services.AddSingleton<IAsyncPolicy<HttpResponseMessage>>(retryPolicy);
            // Health Check
            services.AddHealthChecks();
            services.AddAutoMapper(typeof(Startup));
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Patient Service", Version = "v1" });
            });
            services
                .AddCustomMvc()
                .AddCustomConfiguration(Configuration)
                .AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings
                    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var settings = new CosmosDbSettings();
            Configuration.GetSection("DocumentDatabase").Bind(settings);
            //services.AddHealthChecksUI();


            services.AddSingleton<IDocumentRepository, DocumentRepository>(_ =>
            {
                return new DocumentRepository(settings.EndpointUri,
                  settings.PrimaryKey,
                  settings.ApplicationName,
                   settings.DatabaseName,
                   settings.CurrentContainerName,
                   settings.HistoryContainerName,
                   settings.PartitionKey);
            });           
            //services.AddTransient<IClientGRPCClientService, gRPCClientService>(_ =>
            //{
            //    return new gRPCClientService(Configuration.GetValue<string>("GrpcClientConnection:ClientServiceURL"));
            //});

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatRModule());
            container.RegisterModule(new ApplicationModule(Configuration.GetValue<string>("GrpcClientConnection:ClientServiceURL")));

            return new AutofacServiceProvider(container.Build());

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Health Check
            app.UseHealthChecks("/api/HealthCheck", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            // Health Check UI
            app.UseHealthChecksUI(delegate (Options options)
            {
                options.UIPath = "/api/HealthCheck";
            });
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient Service");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

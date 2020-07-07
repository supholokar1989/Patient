using Autofac;
using Patient.API.Grpc;
using Patient.Infrastructure.Grpc.Interfaces;
using Patient.Infrastructure.Repository;
using Patient.Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.API.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        //public string QueriesConnectionString { get; }
         public string _grpcClientUrl { get; }
        //public string ModuleName { get; }
        //public ApplicationModule(string qconstr, string clientURL, string moduleName)
        //{
        //    QueriesConnectionString = qconstr;
        //    GRPCClientURL = clientURL;
        //    ModuleName = moduleName;
        //}
        public ApplicationModule(string clientUrl)
        {
            _grpcClientUrl = clientUrl;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new gRPCClientService(_grpcClientUrl))
                    .As<IClientGRPCClientService>()
                    .InstancePerLifetimeScope();

            //builder.RegisterType<RegistrationIntegrationEventService>()
            //    .As<IRegistrationIntegrationEventService>()
            //    .InstancePerLifetimeScope();
        }
    }
}

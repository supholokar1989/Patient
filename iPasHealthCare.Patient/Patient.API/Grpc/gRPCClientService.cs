using Grpc.Net.Client;
using GrpcTenant;
using Patient.Domain.DTOs;
using Patient.Infrastructure.Grpc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.API.Grpc
{
    public class gRPCClientService : IClientGRPCClientService
    {
        private string _grpcClientAddress;
        public gRPCClientService(string address)
        {
            _grpcClientAddress = address;
        }

        public Task<string> ClientFacilitySubscribesToModule(string tenantId, string facilityCode)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var clientChannel = GrpcChannel.ForAddress(_grpcClientAddress);
            var clientClient = new TenantApiProvider.TenantApiProviderClient(clientChannel);
            var modulesRequest = new ModulesRequest { TenantId = tenantId, FacilityCode = facilityCode };
            var clientServieReply = clientClient.GetFailityIDByFacilityCodeAndTenantId(modulesRequest);

            if (clientServieReply != null)
            {
                return Task.FromResult(clientServieReply.FacilityId);
            }
            return null;
        }
    }
}

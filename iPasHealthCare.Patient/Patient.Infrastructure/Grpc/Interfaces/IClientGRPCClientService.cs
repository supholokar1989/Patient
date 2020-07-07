using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Infrastructure.Grpc.Interfaces
{
    public interface IClientGRPCClientService
    {
        Task<string> ClientFacilitySubscribesToModule(string tenantId, string facilityCode);
    }
}

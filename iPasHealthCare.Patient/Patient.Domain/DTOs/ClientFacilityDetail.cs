using System;
using System.Collections.Generic;
using System.Text;

namespace Patient.Domain.DTOs
{
    public class ClientFacilityDetail
    {
        public Int64 TenantId { get; set; }



        public string ClientName { get; set; }
        public Int64 FacilityId { get; set; }
        public string FacilityCode { get; set; }
    }
}

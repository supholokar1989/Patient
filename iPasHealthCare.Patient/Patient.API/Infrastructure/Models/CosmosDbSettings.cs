using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.API.Infrastructure.Models
{
    public class CosmosDbSettings
    {
        public string EndpointUri { get; set; }
        public string PrimaryKey { get; set; }

        public string ApplicationName { get; set; }

        public string DatabaseName { get; set; }

        public string CurrentContainerName { get; set; }
        public string HistoryContainerName { get; set; }

        public string PartitionKey { get; set; }
    }
}

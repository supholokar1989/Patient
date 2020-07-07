using Microsoft.Azure.Cosmos;
using Patient.Domain.Models;
using Patient.Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Patient.Infrastructure.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private CosmosClient _cosmostClient;
        private Container _currentContainer;
        private Container _historyContainer;

        public Container CurrentContainer { get { return _currentContainer; } }
        public Container HistoryContainer { get { return _historyContainer; } }

        public DocumentRepository(string endPointURL, string primaryKey, string applicationName,
            string dbName, string CurrentContainerName, string HistoryContainerName, string partitionKey)
        {
            _cosmostClient = new CosmosClient(endPointURL, primaryKey, new CosmosClientOptions() { ApplicationName = applicationName });
            _currentContainer = _cosmostClient.GetContainer(dbName, CurrentContainerName);
            _historyContainer = _cosmostClient.GetContainer(dbName, HistoryContainerName);
        }

        public Task<bool> AppointmentReschedulingAsync(PatientVisitModel PatientVisit)
        {
            throw new NotImplementedException();
        }
        public async Task<PatientVisitModel> GetPatientVisitByIdAsync(string id, string tenantId)
        {
            return await _currentContainer.ReadItemAsync<PatientVisitModel>(id, new PartitionKey(tenantId));
        }
        public async Task<dynamic> GetPatientVisitByPatientId_AppointmentIdAsync(string patientId, string appointmentId, string tenantId)
        {
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c where  c.PatientVisit.AppointmentId = @appointmentId and c.PatientVisit.PatientId =@patientId")
            .WithParameter("@patientId", patientId).WithParameter("@appointmentId", appointmentId);
            FeedIterator<dynamic> feedIterator = _currentContainer.GetItemQueryIterator<dynamic>(queryDefinition, null, new QueryRequestOptions() { PartitionKey = new PartitionKey(tenantId), MaxItemCount = 1 });
            List<PatientVisitModel> patientVisitList = new List<PatientVisitModel>();
            FeedResponse<dynamic> response = null;
            while (feedIterator.HasMoreResults)
            {
                response = await feedIterator.ReadNextAsync();
                if (response.Count > 0)
                {
                    Newtonsoft.Json.Linq.JObject result = response.First();
                    return result;
                }
               
                //var etagDoc = r.ToObject<ETagCosmosDocument>();
                //var document = r.ToObject<PatientVisitModel>();
                //patientVisitList.Add(document);
                ////foreach (var item in await feedIterator.ReadNextAsync())
                ////{
                ////    patientVisitList.Add(item);
                ////}
            }
            return null;
        }

        public async Task<bool> AddPatientVisitAsync(PatientVisitModel patientVisit, Container container)
        {
            var result = await container.CreateItemAsync<PatientVisitModel>(patientVisit, new PartitionKey(patientVisit.TenantId));
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdatePatientVisitAsync(PatientVisitModel patientVisit, string ETag)
        {
            var result = await _currentContainer.UpsertItemAsync<PatientVisitModel>(patientVisit, new PartitionKey(patientVisit.TenantId), new ItemRequestOptions { IfMatchEtag = ETag });

            if (result != null)
            {
                return true;
            }
            return false;
        }

    }
}
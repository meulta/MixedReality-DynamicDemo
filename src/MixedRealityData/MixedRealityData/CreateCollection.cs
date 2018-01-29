using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MixedRealityData.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VirtualTourApi.Repositories;

namespace MixedRealityData
{
    public static class CreateCollection
    {
        [FunctionName("CreateCollection")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var dataRepository = new DataRepository<UnityPrimitive>(Constants.HostName, Constants.AuthKey, "AzureMixedReality", "Objects");
            await dataRepository.InitializeDatabaseAsync("AzureMixedReality", "Objects");
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}

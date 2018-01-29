using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MixedRealityData.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VirtualTourApi.Repositories;

namespace MixedRealityData
{
    public static class CreateObject
    {
        [FunctionName("CreateObject")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var dataRepository = new DataRepository<UnityPrimitive>(Constants.HostName, Constants.AuthKey, "AzureMixedReality", "Objects");
            var unityPrimitive = await req.Content.ReadAsAsync<UnityPrimitive>();
            var result = await dataRepository.CreateItemAsync(unityPrimitive);
            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}

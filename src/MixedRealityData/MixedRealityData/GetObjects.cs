using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using VirtualTourApi.Repositories;
using MixedRealityData.Models;
using System.Threading.Tasks;

namespace MixedRealityData
{
    public static class GetObjects
    {
        [FunctionName("GetObjects")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var dataRepository = new DataRepository<UnityPrimitive>(Constants.HostName, Constants.AuthKey, "AzureMixedReality", "Objects");
            var unityPrimitve = await dataRepository.FetchItemsAsync();
            return req.CreateResponse(HttpStatusCode.OK, unityPrimitve);
        }
    }
}

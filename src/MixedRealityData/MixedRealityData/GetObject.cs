using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MixedRealityData.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VirtualTourApi.Repositories;

namespace MixedRealityData
{
    public static class GetObject
    {
        [FunctionName("GetObject")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var dataRepository = new DataRepository<UnityPrimitive>(Constants.HostName, Constants.AuthKey, "AzureMixedReality", "Objects");
            var queryNameValuePair = req.GetQueryNameValuePairs()
                .Where(kvp => kvp.Key == "id")
                .SingleOrDefault();

            var id = queryNameValuePair.Value;
            var unityPrimitve = await dataRepository.FetchItemAsync(id);
            return req.CreateResponse(HttpStatusCode.OK, unityPrimitve);
        }
    }
}

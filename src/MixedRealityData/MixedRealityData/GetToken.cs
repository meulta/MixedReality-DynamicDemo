using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using MixedRealityData.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MixedRealityData
{
    public static class GetToken
    {
        [FunctionName("GetToken")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            // To create the account SAS, you need to use your shared key credentials. Modify for your account.
            var storageAccount = CloudStorageAccount.Parse(Constants.StorageConnectionString);

            // Create a new access policy for the account.
            var policy = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write | SharedAccessAccountPermissions.Create | SharedAccessAccountPermissions.List | SharedAccessAccountPermissions.Update | SharedAccessAccountPermissions.Delete,
                Services = SharedAccessAccountServices.Blob | SharedAccessAccountServices.File,
                ResourceTypes = SharedAccessAccountResourceTypes.Service | SharedAccessAccountResourceTypes.Object | SharedAccessAccountResourceTypes.Container,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Protocols = SharedAccessProtocol.HttpsOnly
            };

            // Return the SAS token.
            return await Task.FromResult(req.CreateResponse(HttpStatusCode.OK, storageAccount.GetSharedAccessSignature(policy)));
        }
    }
}

using MixedRealityData.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectsAPI: MonoBehaviour {

    public static async Task<IEnumerable<UnityPrimitive>> FetchSceneData()
    {
        var url = $"http://localhost:7071/api/GetObjects";
        var uri = new Uri(url);
#if UNITY_EDITOR
        var webClient = new WebClient();
        var json = await webClient.DownloadStringTaskAsync(uri);
        Debug.LogWarning(json);
        return JsonConvert.DeserializeObject<IEnumerable<UnityPrimitive>>(json);
#else
        var client = new Windows.Web.Http.HttpClient();
        Windows.Web.Http.HttpResponseMessage response = await client.GetAsync(uri);
        SceneData result = null;
        if (response.IsSuccessStatusCode)
        {
            var stringres = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<SceneData>(stringres);
        }

        return result;
#endif
    }

    public static void UpdateObject(UnityPrimitive updatedObject)
    {
        var url = $"http://localhost:7071/api/UpdateObject";
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        string data =  JsonConvert.SerializeObject(updatedObject);
        byte[] pData = System.Text.Encoding.ASCII.GetBytes(data.ToCharArray());
        WWW api = new WWW(url, pData, headers);
    }

    public static UnityPrimitive CreateObject(UnityPrimitive updatedObject)
    {
        var url = $"http://localhost:7071/api/CreateObject";
#if UNITY_EDITOR
        var webClient = new WebClient();
        var data = JsonConvert.SerializeObject(updatedObject, Formatting.Indented);
        webClient.Headers["content-type"] = "application/json";
        /* Always returns a byte[] array data as a response. */
        var response_data = webClient.UploadString(url, "POST", data);

        // Parse the returned data (if any) if needed.
        var responseString = response_data;

        Debug.LogWarning(responseString);
        return JsonConvert.DeserializeObject<UnityPrimitive>(response_data);
#else
        
#endif
    }
}

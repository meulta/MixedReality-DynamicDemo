using MixedRealityData.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public static class ObjectsAPI {

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
}

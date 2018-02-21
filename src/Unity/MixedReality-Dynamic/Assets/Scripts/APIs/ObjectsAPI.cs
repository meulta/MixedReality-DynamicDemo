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

    private static string UrlTemplate = "http://dynamicmr.azurewebsites.net/api/{0}?code=1ikEUkA26LG9KPsOAczkZ6tzDxYMeMMAAoM2t2pbKVPI0l7FrJoUrA==";

    public static async Task<IEnumerable<UnityPrimitive>> FetchSceneData()
    {
        var url = string.Format(UrlTemplate, "GetObjects");
        var uri = new Uri(url);
#if UNITY_EDITOR
        var webClient = new WebClient();
        var json = await webClient.DownloadStringTaskAsync(uri);
        return JsonConvert.DeserializeObject<IEnumerable<UnityPrimitive>>(json);
#else
        var client = new Windows.Web.Http.HttpClient();
        Windows.Web.Http.HttpResponseMessage response = await client.GetAsync(uri);
        IEnumerable<UnityPrimitive> result = null;
        if (response.IsSuccessStatusCode)
        {
            var stringres = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<IEnumerable<UnityPrimitive>>(stringres);
        }

        return result;
#endif
    }

    public static void UpdateObject(UnityPrimitive updatedObject)
    {
        //Here is an example of how to use WWW class
        var url = string.Format(UrlTemplate, "UpdateObject");
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        string data =  JsonConvert.SerializeObject(updatedObject);
        byte[] pData = System.Text.Encoding.ASCII.GetBytes(data.ToCharArray());
        WWW api = new WWW(url, pData, headers);
    }

#if UNITY_EDITOR
    public static UnityPrimitive CreateObject(UnityPrimitive updatedObject)
 #else
    public static async Task<UnityPrimitive> CreateObject(UnityPrimitive updatedObject)
#endif
    {

        //Here is an example of how to use WebClient class and HttpClient class in UWP

        var url = string.Format(UrlTemplate, "CreateObject");
        var data = JsonConvert.SerializeObject(updatedObject, Formatting.Indented);

#if UNITY_EDITOR
        var webClient = new WebClient();
        webClient.Headers["content-type"] = "application/json";
        /* Always returns a byte[] array data as a response. */
        var response_data = webClient.UploadString(url, "POST", data);

        // Parse the returned data (if any) if needed.
        var responseString = response_data;

        Debug.LogWarning(responseString);
        return JsonConvert.DeserializeObject<UnityPrimitive>(response_data);
#else
        var client = new Windows.Web.Http.HttpClient();
        var content = new Windows.Web.Http.HttpStringContent(data, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
        Windows.Web.Http.HttpResponseMessage response = await client.PostAsync(new Uri(url), content);
        UnityPrimitive result = null;
        if (response.IsSuccessStatusCode)
        {
            var stringres = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<UnityPrimitive>(stringres);
        }

        return result;
#endif
    }
}

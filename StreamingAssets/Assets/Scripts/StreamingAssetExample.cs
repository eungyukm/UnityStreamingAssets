using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class StreamingAssetExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        string url =  Path.Combine(Application.streamingAssetsPath , "image01.png");
        //audioSource.clip = url;
        Debug.Log("url : " + url);


        var data = BetterStreamingAssets.FileExists(url);
        Debug.Log("data " + data);

        if(data)
        {
            byte[] byteContents = BetterStreamingAssets.ReadAllBytes(url);
            Texture2D texture = new Texture2D(2,2);
            texture.LoadImage(byteContents);
            GetComponent<MeshRenderer>().material.mainTexture = texture;
        }
        GetUrl(url);
    }

    void GetUrl(string path)
    {
        var load = UnityWebRequest.Get(path);
        load.SendWebRequest();
        while(!load.isDone)
        {
            if (load.isNetworkError || load.isHttpError)
            {
                Debug.Log("Error");
                break;
            }
        }
        if (load.isNetworkError || load.isHttpError)
        {
            Debug.Log("Error!!");
        }
        else
        {
            Debug.Log("Success!!");
            //File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "your.bytes"), loadingRequest.downloadHandler.data);
        }
        //using (UnityWebRequest webRequest = new UnityWebRequest(path))
        //{
        //    yield return webRequest.SendWebRequest();

        //    if(webRequest.isNetworkError)
        //    {
        //        Debug.Log("Error");
        //    }
        //    else
        //    {
        //        Debug.Log("Received : " + webRequest.downloadHandler.text);
        //    }
        //}
    }
}

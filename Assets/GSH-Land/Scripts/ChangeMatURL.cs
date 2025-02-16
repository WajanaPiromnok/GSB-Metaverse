using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChangeMatURL : MonoBehaviour
{
    public string url;
    public Material img;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadImage(url));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            img.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}

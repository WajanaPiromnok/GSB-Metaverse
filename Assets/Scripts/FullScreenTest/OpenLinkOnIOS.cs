using System.Runtime.InteropServices;
using UnityEngine;

public class OpenLinkOnIOS : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenLinkOnDevice(string url);

    public void OpenLink(string url)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            OpenLinkOnDevice(url);
        }
        else
        {
            Debug.Log("This feature is only available in WebGL builds.");
        }
    }
}

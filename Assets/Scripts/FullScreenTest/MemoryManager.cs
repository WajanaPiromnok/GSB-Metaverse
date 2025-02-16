using System.Runtime.InteropServices;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SetMemorySize(int sizeInMB);

    public DeviceDetection detection;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (detection.isIPhone)
            { 
                SetMemorySize(256); // Set to 256 MB
            }
        }

        Debug.Log("is iPhone : " + detection.isIPhone);
    }
}

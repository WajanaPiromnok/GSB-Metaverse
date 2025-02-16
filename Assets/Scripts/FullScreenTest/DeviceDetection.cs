using System.Runtime.InteropServices;
using UnityEngine;

public class DeviceDetection : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern int IsIPhone();

    [DllImport("__Internal")]
    private static extern int IsIPad();

    public bool isIPhone;
    public bool isIPad;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            isIPhone = IsIPhone() == 1;
            isIPad = IsIPad() == 1;
        }
    }
}

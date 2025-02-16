using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CaptureScreen : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CaptureWeb();

    void Start() 
    {
        
    }

    public void captureImage()
    {
        CaptureWeb();
    }
}

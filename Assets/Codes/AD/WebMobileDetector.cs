using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CheckIfMobileForUnityWebGL.Samples;
#if !UNITY_EDITOR && UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class WebMobileDetector : MonoBehaviour
{
    public bool isMobile; 
    // Start is called before the first frame update
    void Awake()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            isMobile = IsMobile();
#endif

        Debug.Log("is this browser run on mobile? : " + isMobile);

        DontDestroyOnLoad(this.gameObject);
    }


#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern bool IsMobile();
#endif

    // Update is called once per frame
    void Update()
    {
        
    }
}

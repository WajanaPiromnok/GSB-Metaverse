using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class iPhoneCheck : MonoBehaviour
{
    public DeviceDetection deviceDetect;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "is iPhone : " + deviceDetect.isIPhone;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

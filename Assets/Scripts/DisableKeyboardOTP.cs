using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NeoludicGames.PopInput;

public class DisableKeyboardOTP : MonoBehaviour
{
    [SerializeField] WebGLInputFieldHelper _helper;
    [SerializeField] WebMobileDetector _webMobile;

    // Start is called before the first frame update
    void Start()
    {
        _webMobile = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<WebMobileDetector>();
        if (_webMobile.isMobile == false)
        {
            _helper.enabled = false;
        }

        if (_webMobile.isMobile == true)
        {
            _helper.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

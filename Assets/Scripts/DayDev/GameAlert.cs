using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameAlert : MonoBehaviour
{
    public TextMeshProUGUI TxtAlert;
    public TextMeshProUGUI TxtBt;

    public GameObject ObjTitle;

    void Start()
    {
        
    }

    public void vSetAlert(string strAlert, bool showTxtTitle = true)
    {
        ObjTitle.SetActive(showTxtTitle);
        TxtAlert.text = strAlert;

        Vector3 pos = TxtAlert.transform.localPosition;
        pos.y = showTxtTitle ? 3f : 70f;
        TxtAlert.transform.localPosition = pos;

        TxtBt.text = showTxtTitle ? "Try Again" : "OK";


        GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutin);
    }

    void Update()
    {
        
    }
}

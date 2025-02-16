using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMic : MonoBehaviour
{
    public bool toggleStage = false;

    public Image baseImage;
    public Sprite talkOff;
    public Sprite talkOn;

    public void MicToggle(bool stage) 
    {
        toggleStage = stage;
    }

    public void Start()
    {
        toggleStage = false;
    }

    private void Update()
    {
        if (toggleStage == false)
        {
            baseImage.sprite = talkOff;
        }

        if (toggleStage == true)
        {
            baseImage.sprite = talkOn;
        }

        if (Input.GetKey(KeyCode.T))
        {
            toggleStage = true;            
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            toggleStage = false;            
        }
    }

}

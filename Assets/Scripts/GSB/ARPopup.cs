using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ARPopup : MonoBehaviour
{
    public GameObject AR;
    public GameObject CanvasAR;
    public GameObject GlobalVol;
    public bool ischeck = false;

    public bool tutorialFirst = true;

    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject tutorialUI;
    //[SerializeField] private GameObject panelAction;
    [SerializeField] private GameObject micToggle;

    private void Start()
    {
        soundOn = GameObject.Find("SoundEnable");
        tutorialUI = GameObject.Find("TutorialUI");
        //panelAction = GameObject.Find("PanelAction_Off");
        micToggle = GameObject.Find("Mic Toggle");
    }

    private void OnTriggerEnter(Collider enter)
    {
        PhotonView phView = enter.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            return;
        }

        else
        {
            if (enter.gameObject.tag == "Player")
            {
                ischeck = true;
            }
        }
    }

    private void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.tag == "Player")
        {
            ischeck = false;
            soundOn.GetComponent<Button>().interactable = true;
            tutorialUI.GetComponent<Button>().interactable = true;
            //panelAction.GetComponent<Button>().interactable = true;
            micToggle.GetComponent<Button>().interactable = true;

            CanvasAR.SetActive(false);
            AR.SetActive(false);
            GlobalVol.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        if (ischeck == true)
        {
            CanvasAR.SetActive(true);
            AR.SetActive(true);
            GlobalVol.SetActive(true);
            soundOn.GetComponent<Button>().interactable = false;
            tutorialUI.GetComponent<Button>().interactable = false;
            //panelAction.GetComponent<Button>().interactable = false;
            micToggle.GetComponent<Button>().interactable = false;
        }

        if (ischeck == false)
        {
            CanvasAR.SetActive(false);
            AR.SetActive(false);
            GlobalVol.SetActive(false);
            soundOn.GetComponent<Button>().interactable = true;
            tutorialUI.GetComponent<Button>().interactable = true;
            //panelAction.GetComponent<Button>().interactable = true;
            micToggle.GetComponent<Button>().interactable = true;
        }
    }

    public void CloseButton()
    {
        soundOn.GetComponent<Button>().interactable = true;
        tutorialUI.GetComponent<Button>().interactable = true;
        //panelAction.GetComponent<Button>().interactable = true;
        micToggle.GetComponent<Button>().interactable = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CheckPopup : MonoBehaviour
{
    public static CheckPopup popupmain;

    public GetDataInfoObject getdata;
    public GameObject popup;
    public GameObject popupcanvasgachapon;
    public GameObject[] popupui;
    public int numroom;
    public int numobject;
    public int num360;
    public bool isgachapon;

    public bool isCheck;
    public bool isImg;
    int numpopup;

    public GameObject Event1, Event2, Event3, Event4, Event5, Event6;
    public GameObject Zone1, Zone2, Zone3;

    public GameObject Events, Gachapon, Activity;

    public GameObject GlobalVolume;

    public GameObject Tutorial;

    public PhotonView phView;

    public bool close = false;

    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject panelAction;
    [SerializeField] private GameObject micToggle;

    void Start()
    {
        if (isgachapon)
        {
            popup.SetActive(false);
        }
    }

    void Update()
    {
        if (soundOn == null)
        {
            soundOn = GameObject.Find("SoundEnable");
        }

        if (tutorialUI == null)
        {
            tutorialUI = GameObject.Find("TutorialUI");
        }

        if (panelAction == null)
        {
            panelAction = GameObject.Find("PanelAction_Off");
        }

        if (micToggle == null)
        {
            micToggle = GameObject.Find("Mic Toggle");
        }
    }

    public void OnTriggerEnter(Collider enter)
    {
        phView = enter.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            return;
        }

        else
        {
            if (enter.gameObject.tag == "Player")
            {

                close = false;

                /*if (numroom == 0)
                {
                    Debug.Log(numroom);
                    getdata.buyButton.SetActive(false);
                    getdata.infoDetail.SetActive(false);
                }*/

                /*if (numroom == 1)
                {
                    Debug.Log(numroom);
                    getdata.buyButton.SetActive(false);
                    getdata.infoDetail.SetActive(false);
                }*/

                /*if (numroom == 2)
                {
                    Debug.Log(numroom);
                    getdata.buyButton.SetActive(true);
                    getdata.infoDetail.SetActive(true);
                }*/

                GlobalVolume.SetActive(true);
                if (isgachapon)
                {
                    popup.SetActive(true);
                    popupcanvasgachapon.SetActive(true);                    
                }
                else
                {
                    isCheck = true;

                    if (popup.activeInHierarchy == false)
                    {
                        if (isImg)
                        {
                            popup.SetActive(true);
                            getdata.GetDataInfoPopup(numroom, numobject);

                        }
                        else
                        {
                            popup.SetActive(true);
                            getdata.GetDataInfoPopupObject(numroom, numobject);
                        }
                    }
                }

                Zone1.SetActive(true);
                Zone2.SetActive(false);
                Zone3.SetActive(false);

                Event1.SetActive(true);
                Event2.SetActive(false);
                Event3.SetActive(false);
                Event4.SetActive(false);
                Event5.SetActive(false);
                Event6.SetActive(false);
            }
        }
    }

    /*public void OnTriggerStay(Collider stay)
    {
        phView = stay.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            return;
        }

        else
        {
            if (stay.gameObject.tag == "Player")
            {
                if (close == false)
                {
                    GlobalVolume.SetActive(true);
                    if (isgachapon)
                    {
                        popup.SetActive(true);
                        popupcanvasgachapon.SetActive(true);
                    }
                    else
                    {
                        isCheck = true;

                        if (popup.activeInHierarchy == false)
                        {
                            if (isImg)
                            {
                                popup.SetActive(true);
                                getdata.GetDataInfoPopup(numroom, numobject);

                            }
                            else
                            {
                                popup.SetActive(true);
                                getdata.GetDataInfoPopupObject(numroom, numobject);
                            }
                        }
                    }
                }
                else if (close == true)
                {
                    GlobalVolume.SetActive(false);
                    Tutorial.SetActive(true);
                    popup.SetActive(false);
                }
            }
        }
    }*/

    private void OnTriggerExit(Collider exit)
    {
        phView = exit.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            return;
        }
        else {
            if (exit.gameObject.tag == "Player")
            {
                GlobalVolume.SetActive(false);
                Tutorial.SetActive(true);
                popup.SetActive(false);

                soundOn.GetComponent<Button>().interactable = true;
                tutorialUI.GetComponent<Button>().interactable = true;
                panelAction.GetComponent<Button>().interactable = true;
                micToggle.GetComponent<Button>().interactable = true;

                if (isgachapon)
                {
                    popup.SetActive(false);
                    popupui[numpopup].SetActive(false);
                    popupcanvasgachapon.SetActive(false);
                }
                else
                {
                    isCheck = false;
                }

                //popup.SetActive(false);

                Events.SetActive(false);
                Gachapon.SetActive(false);
                Activity.SetActive(false);
            }
        }
        
    }

    public void closeUI()
    {
        close = true;
    }

    /*private void OnMouseDown()
    {
        if (isCheck && popup.activeInHierarchy == false)
        {
            if (isImg)
            {
                popup.SetActive(true);
                getdata.GetDataInfoPopup(numroom, numobject);
            }
            else
            {
                popup.SetActive(true);
                getdata.GetDataInfoPopupObject(numroom, numobject);
            }
        }
    }*/

    public void PopupUI(int num)
    {
        if(popupui[0].activeInHierarchy == false && popupui[1].activeInHierarchy == false && popupui[2].activeInHierarchy == false)
        {
            popupui[num].SetActive(true);
            numpopup = num;
        }
    }

    public void PopupUIClose()
    {
        popupui[numpopup].SetActive(false);

        soundOn.GetComponent<Button>().interactable = true;
        tutorialUI.GetComponent<Button>().interactable = true;
        panelAction.GetComponent<Button>().interactable = true;
        micToggle.GetComponent<Button>().interactable = true;
    }

    public void PopupUIOpen()
    {
        soundOn.GetComponent<Button>().interactable = false;
        tutorialUI.GetComponent<Button>().interactable = false;
        panelAction.GetComponent<Button>().interactable = false;
        micToggle.GetComponent<Button>().interactable = false;
    }
}

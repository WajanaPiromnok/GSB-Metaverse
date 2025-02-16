using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using AD;

public class CheckNPC : MonoBehaviour
{
    public PhotonView phView;
    [Header("Popup")]
    public GameObject Popup;

    public GameObject ZonePopup;

    public int npc;

    public CheckUIZone checkUIZone;

    public GameObject mainUI_PC;
    public GameObject mainUI_Mobile;

    [SerializeField] WebMobileDetector _webMobile;
    [SerializeField] GameObject _uiLook;

    [SerializeField] GameplaySystem _gameplaySystem;

    // Start is called before the first frame update
    void Start()
    {
        _webMobile = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<WebMobileDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider enter)
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
                Debug.Log("Enter" + npc);
                Popup.SetActive(true);
                checkUIZone.checkNPC = npc;

                switch (npc)
                {
                    case 0:
                        ZonePopup.SetActive(false);
                        if (_webMobile.isMobile == true)
                        {
                            mainUI_PC.SetActive(false);
                            mainUI_Mobile.SetActive(true);
                        }

                        if (_webMobile.isMobile == false)
                        {
                            mainUI_PC.SetActive(true);
                            mainUI_Mobile.SetActive(false);
                        }
                        break;

                    case 1:
                        ZonePopup.SetActive(false);
                        if (_webMobile.isMobile == true)
                        {
                            mainUI_PC.SetActive(false);
                            mainUI_Mobile.SetActive(true);
                        }

                        if (_webMobile.isMobile == false)
                        {
                            mainUI_PC.SetActive(true);
                            mainUI_Mobile.SetActive(false);
                        }
                        break;

                    case 2:
                        ZonePopup.SetActive(false);
                        if (_webMobile.isMobile == true)
                        {
                            mainUI_PC.SetActive(false);
                            mainUI_Mobile.SetActive(true);
                        }

                        if (_webMobile.isMobile == false)
                        {
                            mainUI_PC.SetActive(true);
                            mainUI_Mobile.SetActive(false);
                        }
                        break;

                    case 3:
                        ZonePopup.SetActive(false);
                        if (_webMobile.isMobile == true)
                        {
                            mainUI_PC.SetActive(false);
                            mainUI_Mobile.SetActive(true);
                        }

                        if (_webMobile.isMobile == false)
                        {
                            mainUI_PC.SetActive(true);
                            mainUI_Mobile.SetActive(false);
                        }
                        break;

                    case 4:
                        ZonePopup.SetActive(false);
                        if (_webMobile.isMobile == true)
                        {
                            mainUI_PC.SetActive(false);
                            mainUI_Mobile.SetActive(true);
                        }

                        if (_webMobile.isMobile == false)
                        {
                            mainUI_PC.SetActive(true);
                            mainUI_Mobile.SetActive(false);
                        }
                        break;

                    case 5:
                        ZonePopup.SetActive(false);
                        if (_webMobile.isMobile == true)
                        {
                            mainUI_PC.SetActive(false);
                            mainUI_Mobile.SetActive(true);
                        }

                        if (_webMobile.isMobile == false)
                        {
                            mainUI_PC.SetActive(true);
                            mainUI_Mobile.SetActive(false);
                        }
                        break;

                    case 6:
                        ZonePopup.SetActive(false);
                        if (_webMobile.isMobile == true)
                        {
                            mainUI_PC.SetActive(false);
                            mainUI_Mobile.SetActive(true);
                        }

                        if (_webMobile.isMobile == false)
                        {
                            mainUI_PC.SetActive(true);
                            mainUI_Mobile.SetActive(false);
                        }
                        break;
                }

                if (_webMobile.isMobile == true)
                {
                    _uiLook.SetActive(false);
                }

                if (_webMobile.isMobile == false)
                {
                    _uiLook.SetActive(false);
                }
            }
        }
    }

    void OnTriggerStay(Collider stay)
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
                //Debug.Log("Stay" + npc);
                Popup.SetActive(true);
                checkUIZone.checkNPC = npc;
                //checkUIZone.onClickPopup();

                if (_webMobile.isMobile == true)
                {
                    _uiLook.SetActive(false);
                }

                if (_webMobile.isMobile == false)
                {
                    _uiLook.SetActive(false);
                }
            }
        }
    }

    void OnTriggerExit(Collider exit)
    {
        phView = exit.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            return;
        }
        else
        {
            if (exit.gameObject.tag == "Player")
            {
                //Debug.Log("Exit" + npc);
                Popup.SetActive(false);
                ZonePopup.SetActive(false);
                checkUIZone.checkNPC = npc;
                switch (npc)
                {
                    case 0:
                        ZonePopup.SetActive(false);
                        _gameplaySystem.onUIClose();
                        break;

                    case 1:
                        ZonePopup.SetActive(false);
                        _gameplaySystem.onUIClose();
                        break;

                    case 2:
                        ZonePopup.SetActive(false);
                        _gameplaySystem.onUIClose();
                        break;

                    case 3:
                        ZonePopup.SetActive(false);
                        _gameplaySystem.onUIClose();
                        break;

                    case 4:
                        ZonePopup.SetActive(false);
                        _gameplaySystem.onUIClose();
                        break;

                    case 5:
                        ZonePopup.SetActive(false);
                        _gameplaySystem.onUIClose();
                        break;

                    case 6:
                        ZonePopup.SetActive(false);
                        _gameplaySystem.onUIClose();
                        break;
                }

                if (_webMobile.isMobile == true)
                {
                    _uiLook.SetActive(true);
                }

                if (_webMobile.isMobile == false)
                {
                    _uiLook.SetActive(false);
                }
            }
        }
    }
}

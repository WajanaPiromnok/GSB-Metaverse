using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class CheckNPCPhase2 : MonoBehaviour
{
    public PhotonView phView;
    [Header("Popup")]
    public GameObject Popup;

    public GameObject ZonePopup;

    public int npc;

    public CheckUIZonePhase2 checkUIZone;

    public GameObject tutorial;

    // Start is called before the first frame update
    void Start()
    {

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
                        ZonePopup.SetActive(true);
                        checkUIZone.onClickPopup(npc);
                        tutorial.SetActive(false);
                        Debug.Log(npc);
                        break;

                    case 1:
                        ZonePopup.SetActive(true);
                        checkUIZone.onClickPopup(npc);
                        tutorial.SetActive(false);
                        Debug.Log(npc);
                        break;

                    case 2:
                        ZonePopup.SetActive(true);
                        checkUIZone.onClickPopup(npc);
                        tutorial.SetActive(false);
                        Debug.Log(npc);
                        break;

                    case 3:
                        ZonePopup.SetActive(true);
                        checkUIZone.onClickPopup(npc);
                        tutorial.SetActive(false);
                        Debug.Log(npc);
                        break;

                    case 4:
                        ZonePopup.SetActive(true);
                        checkUIZone.onClickPopup(npc);
                        tutorial.SetActive(false);
                        Debug.Log(npc);
                        break;

                    case 5:
                        ZonePopup.SetActive(true);
                        checkUIZone.onClickPopup(npc);
                        tutorial.SetActive(false);
                        Debug.Log(npc);
                        break;

                    case 6:
                        ZonePopup.SetActive(true);
                        checkUIZone.onClickPopup(npc);
                        tutorial.SetActive(false);
                        Debug.Log(npc);
                        break;
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
                Debug.Log("Stay" + npc);
                Popup.SetActive(true);
                checkUIZone.checkNPC = npc;
                checkUIZone.onClickPopup(npc);
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
                Debug.Log("Exit" + npc);
                Popup.SetActive(false);
                ZonePopup.SetActive(false);
                checkUIZone.checkNPC = npc;


                switch (npc)
                {
                    case 0:
                        ZonePopup.SetActive(false);
                        tutorial.SetActive(true);
                        break;

                    case 1:
                        ZonePopup.SetActive(false);
                        tutorial.SetActive(true);
                        break;
                    
                    case 2:
                        ZonePopup.SetActive(false);
                        tutorial.SetActive(true);
                        break;

                    case 3:
                        ZonePopup.SetActive(false);
                        tutorial.SetActive(true);
                        break;

                    case 4:
                        ZonePopup.SetActive(false);
                        tutorial.SetActive(true);
                        break;
                    case 5:
                        ZonePopup.SetActive(false);
                        tutorial.SetActive(true);
                        break;
                        
                    case 6:
                        ZonePopup.SetActive(false);
                        tutorial.SetActive(true);
                        break;
                }
            }
        }
    }
}

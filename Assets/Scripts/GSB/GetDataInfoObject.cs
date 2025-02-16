using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Photon.Pun;

public class GetDataInfoObject : MonoBehaviour
{

    [Serializable]
    public struct KeepData
    {
        public string nameroom;
        public Sprite[] pic;
        public List<RotateImg360> keep360;
        public string[] namehead;
        public string[] nameartist;
        public string[] infoartist;

        public string[] urlReserve;
    }
    [Serializable]
    public struct RotateImg360
    {
        public Sprite[] pic;
    }

    public List<KeepData> keepdata;

    public Image picobject;
    public TextMeshProUGUI nameHead;
    public TextMeshProUGUI nameArtist;
    public TextMeshProUGUI infoArtist;
    public RectTransform panelText;
    public GameObject canvas;
    public Rotate360 rot360;
    public TextMeshProUGUI textExplain360;
    public GameObject buyButton;

    public Button buttonBuy;

    public GameObject infoDetail;

    public int NumRoom;
    public int NumObject;

    string urlOpen, url;

    List<string> arrayURLreserve;

    public TimeServer timeServer;

    public CheckPopup[] checkpop;

    int time;
    int date;


    void Start()
    {
        //int.TryParse(timeServer.words[1], out time);
    }

    void Update()
    {
        //buyButton.SetActive(false);

        /*if (NumRoom == 2)
        {
            buyButton.SetActive(true);
            int.TryParse(timeServer.words, out time);
            //Debug.Log(time);
            int.TryParse(timeServer.wordDate, out date);
            //Debug.Log(date);

            if (NumObject == 0 || NumObject == 2)
            {
                //Visitor
                if (date <= 2610)
                {
                    Debug.Log("Day is not meet our requirements");
                    buttonBuy.interactable = false;
                }
                else if (date == 2710)
                {
                    if (time >= 0830)
                    {
                        buttonBuy.interactable = true;
                    }
                    else
                    {
                        buttonBuy.interactable = false;
                    }
                }
                else if ((date >= 2810) && (date <= 1411))
                {
                    buttonBuy.interactable = true;
                }
                else
                {
                    buttonBuy.interactable = false;
                }
            }

            if(NumObject == 1 || NumObject == 3)
            { 
                //Student
                Debug.Log("Student");
                if (date <= 2610)
                {
                    Debug.Log("Day is not meet our requirements");
                    buttonBuy.interactable = false;
                }
                else if (date == 2710)
                {
                    if (time >= 0830)
                    {
                        buttonBuy.interactable = true;
                    }
                    else
                    {
                    buttonBuy.interactable = false;
                    }
                }
                else if ((date >= 2810) && (date <= 3011))
                {
                    buttonBuy.interactable = true;
                }
                else
                {
                buttonBuy.interactable = false;
                }
            }
        }*/

        if (NumRoom != 2)
        {
            buyButton.SetActive(false);
        }

        /*
        if (timeServer.words[1] != "12:23")
        {
            buttonBuy.interactable = false;
        }
        else
        {
            buttonBuy.interactable = true;
        }*/

    }

    public void GetDataInfoPopup(int numroom, int numob)
    {
        NumRoom = numroom;

        NumObject = numob;

        picobject.sprite = keepdata[numroom].pic[numob];
        List<string> arraytextnamehead = new List<string>(keepdata[numroom].namehead[numob].Split('|'));
        List<string> arraytextname = new List<string>(keepdata[numroom].nameartist[numob].Split('|'));
        List<string> arraytextinfo = new List<string>(keepdata[numroom].infoartist[numob].Split('|'));
        if (numroom == 2)
        {
            arrayURLreserve = new List<string>(keepdata[numroom].urlReserve[numob].Split('|'));
        }
        else
        {
            Debug.Log("No URL");
        }
        nameHead.text = "";
        nameArtist.text = "";
        infoArtist.text = "";
        textExplain360.text = "";
        rot360.enabled = false;

        if (numroom == 2)
        {
            infoDetail.SetActive(true);
            //buyButton.SetActive(true);
        }
        else if (numroom != 2)
        {
            infoDetail.SetActive(false);
            //buyButton.SetActive(false);
        }

        for (int i = 0; i < arraytextnamehead.Count; i++)
        {
            nameHead.text += arraytextnamehead[i];
        }

        for (int i = 0; i < arraytextname.Count; i++)
        {
            nameArtist.text += arraytextname[i];
        }

        for (int i = 0; i < arraytextinfo.Count; i++)
        {
            infoArtist.text += arraytextinfo[i];

            //if (arraytextinfo.Count <= 8)
            //{
            //    panelText.sizeDelta = new Vector2(717.4639f, 450);
            //}
            //else if (arraytextinfo.Count > 8)
            //{
            //    panelText.sizeDelta = new Vector2(717.4639f, 54 * arraytextinfo.Count);
            //}
        }

        /*for (int i = 0; i < arrayURLreserve.Count; i++)
        {
            url = " ";
            urlOpen = String.Join(url, arrayURLreserve);
            Application.OpenURL(urlOpen);
            Debug.Log(urlOpen);
        }*/

        if (rot360.enabled == false)
        {
            //อย่าลืมเอา comment ออก
            buyButton.SetActive(true);
        }
    }

    public void GetDataInfoPopupObject(int numroom, int numob)
    {
        NumRoom = numroom;

        NumObject = numob;

        picobject.sprite = keepdata[numroom].keep360[numob].pic[0];
        List<string> arraytextnamehead = new List<string>(keepdata[numroom].namehead[numob].Split('|'));
        List<string> arraytextname = new List<string>(keepdata[numroom].nameartist[numob].Split('|'));
        List<string> arraytextinfo = new List<string>(keepdata[numroom].infoartist[numob].Split('|'));
        if (numroom == 2)
        {
            arrayURLreserve = new List<string>(keepdata[numroom].urlReserve[numob].Split('|'));            
        }
        else
        {
            Debug.Log("No URL");
        }        
        nameHead.text = "";
        nameArtist.text = "";
        infoArtist.text = "";
        textExplain360.text = "คลิก และลากรูปภาพเพื่อดูแบบ 360 องศา";
        rot360.enabled = true;
        rot360.sprites.Clear();

        if (numroom == 2)
        {
            infoDetail.SetActive(true);
            //buyButton.SetActive(true);
        }
        else if (numroom != 2)
        {
            infoDetail.SetActive(false);
            //buyButton.SetActive(false);
        }

        if (rot360.enabled == true)
        {
            buyButton.SetActive(false);
        }

        for (int i = 0; i < arraytextnamehead.Count; i++)
        {
            nameHead.text += arraytextnamehead[i];
        }

        for (int i = 0; i < arraytextname.Count; i++)
        {
            nameArtist.text += arraytextname[i];
        }

        for (int i = 0; i < arraytextinfo.Count; i++)
        {
            infoArtist.text += arraytextinfo[i];
        }

        /*for (int i = 0; i < arrayURLreserve.Count; i++)
        {
            url = " ";
            urlOpen = String.Join(url, arrayURLreserve);
            Application.OpenURL(urlOpen);
            Debug.Log(urlOpen);
        }*/

        for (int i = 0; i < keepdata[numroom].keep360[numob].pic.Length; i++)
        {
            rot360.sprites.Add(keepdata[numroom].keep360[numob].pic[i]);
        }
    }


    public void Reserve()
    {
        for (int i = 0; i < arrayURLreserve.Count; i++)
        {
            url = " ";
            urlOpen = String.Join(url, arrayURLreserve);
            Application.OpenURL(urlOpen);
            Debug.Log(urlOpen);
        }

        int serverTime = System.Environment.TickCount;

        print(serverTime);

        print("Time.time: " + Time.time + " PhotonNetwork.time: " + PhotonNetwork.ServerTimestamp);

    }
    public void ClosePopup()
    {
        canvas.SetActive(false);
    }
}

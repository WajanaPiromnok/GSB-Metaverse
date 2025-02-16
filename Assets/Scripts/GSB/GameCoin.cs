using AD;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoin : MonoBehaviour
{
    public CoinManager ObjCoinManager;
    public AudioSource coinSourceSound;
    public AudioClip coinSound;
    [SerializeField] GameplaySystem gameplaySystem;
    // Start is called before the first frame update
    void Start()
    {
        coinSourceSound.clip = coinSound;
    }

    // Update is called once per frame
    void Update()
    {
        TimeAddCoin();
    }

    public void TimeAddCoin()
    {
        if (ObjCoinManager.s_TimeCount > 150)
        {
            ObjCoinManager.vRespUpdateCoin();

            ObjCoinManager.s_TimeCount = 0;

            reSpawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameplaySystem.phView = other.gameObject.GetComponent<PhotonView>();

            coinSourceSound.Play();

            if (!gameplaySystem.phView)
            {
                return;
            }

            if (ObjCoinManager.isGuest == true)
            {
                Debug.Log("Get Coin");
                GetCoin(false);
                this.gameObject.SetActive(false);
            }

            else
            {
                if (ObjCoinManager.isGuest == false)
                {
                    this.gameObject.SetActive(false);
                    GetComponent<PhotonView>().RPC("RPC_GetCoin", RpcTarget.All, false);
                }

                for (int i = 0; i < ObjCoinManager.ObjCoin.Length; i++)
                {
                    if (this.gameObject.activeSelf == false)
                    {
                        //ObjCoinManager.bGetData(i);
                        ObjCoinManager.vSetData(i, false);
                        ObjCoinManager.ObjCoin[i].SetActive(false);
                    }
                }
            }
        }
    }

    public void GetCoin(bool active)
    {
        /*gameObject.GetComponent<AniImage>().vSetColor(Color.white);
        gameObject.GetComponent<AniImage>().vSetCutScene(active ? CutInOut.Cutin : CutInOut.Cutout);
        float target = active ? 2.25f : 0f;
        gameObject.GetComponent<AniImage>().vSetScale(transform.localScale.x, target, 20f);
        gameObject.GetComponent<Collider>().enabled = active;*/

        if (gameplaySystem.phView.IsMine)
        {
            ObjCoinManager.addCoinGuest();
        }
    }

    public void reSpawn()
    {
        /*this.gameObject.GetComponent<AniImage>().vSetColor(Color.white);
        this.gameObject.GetComponent<AniImage>().vSetCutScene(CutInOut.Cutin);
        float target = 2.25f;
        this.gameObject.GetComponent<AniImage>().vSetScale(transform.localScale.x, target, 20f);
        this.gameObject.GetComponent<Collider>().enabled = true;*/
    }

    [PunRPC]
    public void RPC_GetCoin(bool active)
    {
        Debug.Log("RPC_GetCoin " + this.name + "," + active);
        /*gameObject.GetComponent<AniImage>().vSetColor(Color.white);
        gameObject.GetComponent<AniImage>().vSetCutScene(active ? CutInOut.Cutin : CutInOut.Cutout);
        float target = active ? 2.25f : 0f;
        gameObject.GetComponent<AniImage>().vSetScale(transform.localScale.x, target, 20f);
        gameObject.GetComponent<Collider>().enabled = active;*/

        if (gameplaySystem.phView.IsMine)
        {
            ObjCoinManager.vAddUserCoin();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePopup : MonoBehaviour
{
    public AniImage AniGachapon;
    public AniImage AniBox;
    public AniImage AniNFT;
    public AniImage AniReword;
    public AniImage AniSorry;

    public Image ImBgPink;

    public CoinManager ObjCoinManager;

    public Material MatDefault;
    public Material MatMono;

    public TextMeshProUGUI TxtCoin;
    public TextMeshProUGUI TxtCodeShopee;
    public TextMeshProUGUI TxtCodeNFT;

    public AudioSource soundcoin;

    // Start is called before the first frame update

    private void Awake()
    {
        ObjCoinManager = FindObjectOfType<CoinManager>();

        AniGachapon.gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*อย่าลืมเอาคอมเม้นท์ออก*/

        /*
        if (SceneManager.GetActiveScene().name == "AomsinLand(DevelopScene)-Edit")
        {
            TxtCoin.text = ObjCoinManager.TxtScore.text;

            ImBgPink.material = (CoinManager.s_NumCoin >= 10) ? MatDefault : MatMono;
        }
        */
    }

    public void vPressOpenGachapon()
    {
        AniBox.gameObject.SetActive(true);
        AniNFT.gameObject.SetActive(false);
        AniReword.gameObject.SetActive(false);
        AniSorry.gameObject.SetActive(false);

        AniGachapon.vSetCutSceneAutoCheck(CutInOut.Cutin);
    }

    public void vPressRandom()
    {
        ObjCoinManager.vGetGachapon();

        if(ObjCoinManager.m_coinsound >= 10)
        {
            soundcoin.Play();
        }
    }

    public void vSetCopyShopeeText()
    {
        CopyBridge.CopyText(TxtCodeShopee.text);
        GUIUtility.systemCopyBuffer = TxtCodeShopee.text;
    }

    public void vSetCopyNFTText()
    {
        CopyBridge.CopyText(TxtCodeNFT.text);
        GUIUtility.systemCopyBuffer = TxtCodeNFT.text;
    }
}

using Photon.Pun;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using AD;

public class CoinManager : MonoBehaviour
{
    public const string BASE_PATH = "https://gsb-api.apptr1.com/";//"https://meta.tr1.co.th/api/";
    public const string PATH_GETCOIN = BASE_PATH + "coin/";
    public const string PATH_ADDCOIN = BASE_PATH + "coin/add/";
    public const string PATH_RESETCOIN = BASE_PATH + "resetcoin.php";
    public const string PATH_NFT = BASE_PATH + "nft.php";
    public const string PATH_REWARD = BASE_PATH + "reward.php";

    public const string PATH_SPENT_COIN = BASE_PATH + "coin/spent/";

    public const int MAX_COIN = 25;
    public const int MAX_COIN_MAP = 10;
    public const float NUM_DISTANCE = 1f;

    public static int[] s_IntArray = new int[2];
    public static bool s_UpdateArray = false;
    public static bool s_AddCoins = false;

    public static int s_NumCoin;
    public int numCoins;
    public int s_TimeCount;
    public static int s_NumLimitUse;

    public GameObject[] ObjCoin;

    public GameObject ObjPlayer;

    public TextMeshProUGUI TxtScore;
    public TextMeshProUGUI TxtScoreMobile;

    public GetCoinClass ObjGetCoinClass;
    public AddCoinClass ObjAddCoinClass;
    public ResetCoinClass ObjResetCoinClass;
    public NFTClass ObjNFTClass;
    public RewordClass ObjRewordClass;

    public GameObject ObjBox;
    public GameObject ObjNFT;
    public GameObject ObjReword;
    public GameObject ObjNotGet;

    public TextMeshProUGUI TxtReword;
    public TextMeshProUGUI TxtNFT;

    public int m_coinsound;

    //fix coin spawn
    int m_SumTimeMove = 6;

    int m_NumSecond;

    public bool isGuest = false;

    [SerializeField] int numRan;
    [SerializeField] int numCheck;

    [SerializeField] int tagno;
    [SerializeField] int tagbit;
    [SerializeField] int _tag1;
    [SerializeField] int _index1;

    [SerializeField] private WebMobileDetector _webMobile;

    [SerializeField] private GameplaySystem _gameplaySystem;

    [SerializeField] private CheckUIZone _checkUIZone;

    [SerializeField] private GameObject _zoneUI;

    private void Awake()
    {
        s_IntArray[0] = 0;
        s_IntArray[1] = 0;
        s_UpdateArray = false;
        s_TimeCount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        vStart();
        /*s_NumCoin = PlayerPrefs.GetInt("coin");
        Debug.Log("Coin = " + s_NumCoin);*/

        _webMobile = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<WebMobileDetector>();

        _gameplaySystem = GameObject.FindGameObjectWithTag("GameplaySystem").GetComponent<GameplaySystem>();

        s_NumCoin = 0;

        vResetUserCoin(s_NumCoin);
    }

    public void vStart()
    {
        //int numRan = Random.Range(0, 4);
        for (int i = 0; i < ObjCoin.Length; i++)
        {
            //if (i % MAX_COIN_MAP == 0)
            //numRan = Random.Range(0, MAX_COIN_MAP);
            ObjCoin[i].name = ""+i;
            ObjCoin[i].SetActive(false);// !(numRan == i % MAX_COIN_MAP));
            //ObjCoin[i].GetComponent<GameCoin>().ObjCoinManager = this;
        }
            
        m_NumSecond = -1;

        PhotonNetwork.FetchServerTimestamp();

        vGetUserCoin();
    }

    public void vGetUserCoin()
    {
        string path = PATH_GETCOIN + GameController.s_StrAuthenCode;

        //path += "api_key=" + GameController.API_KEY;
        //path += "&authen_code=" + GameController.s_StrAuthenCode;

        StartCoroutine(GetDataUser(path));
    }

    public void vAddUserCoin()
    {
        string path = PATH_ADDCOIN + GameController.s_StrAuthenCode;

        //path += "api_key=" + GameController.API_KEY;
        //path += "&authen_code=" + GameController.s_StrAuthenCode;
        //path += "&coin=" + addCoin;

        WWWForm form = new WWWForm();

        StartCoroutine(PostDataUser(path, form));
    }

    public void addCoinGuest()
    {
        s_NumCoin += 1;
    }

    public void vResetUserCoin(int resetCoin)
    {
        string path = PATH_SPENT_COIN + GameController.s_StrAuthenCode;

        /*path += "api_key=" + GameController.API_KEY;
        path += "&authen_code=" + GameController.s_StrAuthenCode;
        path += "&coin=" + resetCoin;*/

        WWWForm form = new WWWForm();

        StartCoroutine(PostDataUser(path, form));
    }

    public void vGetGachapon()
    {
        if (s_NumCoin < 10)
            return;

        vGetReword();

        s_NumCoin -= 10;
        vResetUserCoin(s_NumCoin);
    }

    public void clearCoin()
    {
        s_NumCoin -= 25;

        s_NumCoin = 0;

        vResetUserCoin(s_NumCoin);

        s_NumCoin = ObjResetCoinClass.user.coin;
    }

    public void vGetNFT()
    {
        string path = PATH_SPENT_COIN;

        /*path += "api_key=" + GameController.API_KEY;
        path += "&authen_code=" + GameController.s_StrAuthenCode;*/

        StartCoroutine(GetDataUser(path));
    }

    public void vGetReword()
    {
        string path = PATH_REWARD + "?";

        path += "api_key=" + GameController.API_KEY;
        path += "&authen_code=" + GameController.s_StrAuthenCode;

        StartCoroutine(GetDataUser(path));
    }

    IEnumerator GetDataUser(string URL)
    {
        Debug.Log("URL = " + URL);
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.isNetworkError || www.isHttpError)
        {
            Debug.Log(URL + "\n = " + www.error + "," + www.downloadHandler.text);

            isGuest = true;
        }

        else
        {
            isGuest = false;
            //if(www.error == )
            Debug.Log(URL + " jsonData = " + www.downloadHandler.text);
            string jsonData = www.downloadHandler.text;

            //JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
            //string status_code;
            //status_code = jsonNode["status_code"];
            //if (status_code == "200")
            {
                if (URL.Contains(PATH_GETCOIN))
                {
                    ObjGetCoinClass = GetCoinClass.CreateFromJSON(jsonData);
                    s_NumCoin = ObjGetCoinClass.user.coin;
                    //s_NumLimitUse = ObjGetCoinClass.limit_use;
                    if (s_NumLimitUse >= MAX_COIN || s_NumCoin >= MAX_COIN)
                        vDisAllCoin();
                }
                else if (URL.Contains(PATH_ADDCOIN))
                {
                    ObjAddCoinClass = AddCoinClass.CreateFromJSON(jsonData);
                    s_NumCoin = ObjAddCoinClass.user.coin;
                    if (s_NumLimitUse >= MAX_COIN || s_NumCoin >= MAX_COIN)
                        vDisAllCoin();
                }
                else if (URL.Contains(PATH_RESETCOIN))
                {
                    ObjResetCoinClass = ResetCoinClass.CreateFromJSON(jsonData);
                    s_NumCoin = ObjResetCoinClass.user.coin;
                }
                else if (URL.Contains(PATH_NFT))
                {
                    if (SceneManager.GetActiveScene().name == "AomsinLand(DevelopScene)-Edit")
                    {
                        ObjNFTClass = NFTClass.CreateFromJSON(jsonData);
                        ObjNFT.GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutin);
                        ObjBox.GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutout);
                        TxtNFT.text = ObjNFTClass.message;
                        //get nft
                    }
                }
                else if (URL.Contains(PATH_REWARD))
                {
                    if (SceneManager.GetActiveScene().name == "AomsinLand(DevelopScene)-Edit")
                    {
                        ObjRewordClass = RewordClass.CreateFromJSON(jsonData);
                        ObjReword.GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutin);
                        ObjBox.GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutout);
                        TxtReword.text = ObjRewordClass.message.Replace("is claim", "");
                        //get reword
                    }
                }
            }
            ////else
            //{
            //    if (URL.Contains(PATH_NFT))
            //    {
            //        ObjNFTClass = NFTClass.CreateFromJSON(jsonData);
            //        ObjNotGet.GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutin);
            //        ObjBox.GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutout);
            //        //not get nft
            //    }
            //    else if (URL.Contains(PATH_REWARD))
            //    {
            //        ObjRewordClass = RewordClass.CreateFromJSON(jsonData);
            //        ObjNotGet.GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutin);
            //        ObjBox.GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutout);
            //        //not get reword
            //    }
            //    Debug.Log("Failed");
            //}
        }
    }

    IEnumerator PostDataUser(string URL, WWWForm form)
    {
        Debug.Log("URL = " + URL);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.isNetworkError || www.isHttpError)
        {
            Debug.Log(URL + "\n = " + www.error + "," + www.downloadHandler.text);
        }
        else
        {
            //if(www.error == )
            Debug.Log(URL + " jsonData = " + www.downloadHandler.text);
            string jsonData = www.downloadHandler.text;

            if (URL.Contains(PATH_ADDCOIN))
            {
                ObjAddCoinClass = AddCoinClass.CreateFromJSON(jsonData);
                s_NumCoin = ObjAddCoinClass.user.coin;
                //s_NumLimitUse = ObjAddCoinClass.limit_use;
                Debug.Log("get coin");
                if (s_NumLimitUse >= MAX_COIN || s_NumCoin >= MAX_COIN)
                    vDisAllCoin();
            }
            if (URL.Contains(PATH_SPENT_COIN))
            {
                Debug.Log("reset coin");
                ObjResetCoinClass = ResetCoinClass.CreateFromJSON(jsonData);
                s_NumCoin = ObjResetCoinClass.user.coin;
            }
        }
    }

    //fix to set limit_use to disable.
    void vDisAllCoin()
    {
        for (int i = 0; i < ObjCoin.Length; i++)
        {
            ObjCoin[i].SetActive(false);
        }
    }
        // Update is called once per frame
    void Update()
    {
        vUpdateTextCoin();
        vCaleTimeAddCoin();

        numCoins = s_NumCoin;

        if (s_NumCoin >= MAX_COIN)
        {
            s_NumCoin = 25;

            _zoneUI.SetActive(true);

            _checkUIZone.onShopeeZone();

            _gameplaySystem.DisableThirdPerson();
            _gameplaySystem.onUIOpen();
        }

        //vRespUpdateAddCoins();
        //vRespUpdateCoin();
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            //s_NumCoin = 50;
            vResetUserCoin(5);
            //vGetReword();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //s_NumCoin = 25;
            //s_NumCoin = 50;
            //vGetReword();
        }

       if (Input.GetKeyDown(KeyCode.O))
        {
            s_NumCoin = 25;
        }
#endif
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            s_NumCoin = 20;
        }*/
    }

    public void getCoinSyncScene()
    {
        PlayerPrefs.SetInt("coin", s_NumCoin);
    }

    public void onGetAllCoin()
    {
        s_NumCoin -= 25;
        vResetUserCoin(s_NumCoin);
        vDisAllCoin();
    }

    public void vUpdateTextCoin()
    {
        if (_webMobile.isMobile == true)
        { 
            TxtScoreMobile.text = s_NumCoin.ToString("00") + " / " + MAX_COIN;
            m_coinsound = s_NumCoin;
        }

        if (_webMobile.isMobile == false)
        {
            TxtScore.text = s_NumCoin.ToString("00") + " / " + MAX_COIN;
            m_coinsound = s_NumCoin;
        }

        /*if (isGuest == true)
        {
            TxtScore.text = "กรุณา Login เพื่อสะสมเหรียญ";

            for (int i = 0; i < ObjCoin.Length; i++)
            {
                ObjCoin[i].SetActive(false);
            }
        }*/
    }

    public void vRespUpdateCoin()
    {
        for (int i = 0; i < ObjCoin.Length; i++)
        {
            if (/*!GameController.s_IsGuest &&*/ s_NumLimitUse < MAX_COIN)
                ObjCoin[i].SetActive(bGetData(i));
            //if (i == 11)
            //Debug.Log("bGetData(i) = "+ bGetData(i));
        }
    }

    public void vRemoveAllCoins()
    {
        s_IntArray[0] = 0;
        s_IntArray[1] = 0;
        for (int i = 0; i < ObjCoin.Length; i++)
        {
            ObjCoin[i].SetActive(false);
        }
    }

    public void vRespUpdateAddCoins()
    {
        for (int i = 0; i < 5; i++)
        {
            numRan = Random.Range(1, ObjCoin.Length * 2);
            numCheck = numRan;
            while (numRan > 0)
            {
                for (int j = 0; j < ObjCoin.Length; j++)
                {
                    if (/*!ObjCoin[j].activeSelf*/!bGetData(j) && numRan > 0 && j >= (i * 10) && j <= (i * 10) + 10)
                    {
                        numRan--;
                        if (numRan <= 0)
                        {
                            if (/*!GameController.s_IsGuest &&*/ s_NumLimitUse < MAX_COIN)
                            {
                                ObjCoin[j].SetActive(true);
                            }
                            
                            vSetData(j, true);
                            break;
                        }
                    }
                }

                if (numCheck == numRan)
                    break;
            }
        }
    }

    public void vCaleTimeAddCoin()
    {
        int second = ((int)PhotonNetwork.Time) % 120;
        //int min = ((int)PhotonNetwork.Time / 60) % 60;
        //int hour = ((int)PhotonNetwork.Time / 3600) % 24;
        //Debug.Log("PhotonNetwork.Time = "+ PhotonNetwork.Time+","+ s_TimeCount);
        if (m_NumSecond != second)//System.DateTime.Now.Second)
        {
            //fix coin spawn
            if (second % m_SumTimeMove == 0 && s_TimeCount < 600)//300)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    vRespUpdateAddCoins();
                    s_UpdateArray = true;
                    //Debug.Log("1 s_AddCoins = " + s_TimeCount);

                }
            }
            if (PhotonNetwork.IsMasterClient)
            {
                if (s_TimeCount < 600)
                    s_TimeCount++;
                else
                {
                    s_TimeCount = 0;
                    vRemoveAllCoins();
                    s_UpdateArray = true;

                    for (int i = 0; i < ObjCoin.Length; i++)
                    {
                        if (bGetData(i) == true)
                        {
                            _tag1 = 1;
                        }

                        if (bGetData(i) == false)
                        {
                            _tag1 = 0;
                        }
                    }
                    //Debug.Log();
                }
            }
            //Debug.Log("2 s_AddCoins = " + s_TimeCount);
            //Debug.Log("IsMasterClient = " + PhotonNetwork.IsMasterClient);
        }
        
        m_NumSecond = second;
        //s_AddCoins

        //System.DateTime time = new System.DateTime(PhotonNetwork.ServerTimestamp);
        //Debug.Log("ServerTimestamp = " + PhotonNetwork.ServerTimestamp+","+ PhotonNetwork.Time);
        //PhotonNetwork.FetchServerTimestamp();
    }

    public bool bGetData(int index)
    {
        tagno = index / 32;
        tagbit = index % 32;

        _tag1 = s_IntArray[tagno];

        _tag1 = (int)((_tag1 >> tagbit) & 1);

        return (_tag1 == 1);
    }

    public void vSetData(int index, bool isActive)
    {
        //
        int _tagno = index / 32;
        int _tagbit = index % 32;

        int _tag = s_IntArray[tagno];

        int member = _tag;

        if (isActive)
            _tag |= (int)(1 << _tagbit);
        else
        {
            //value & ~(1 << position);
            _tag &= (~(1 << _tagbit));
        }

        s_IntArray[_tagno] = _tag;

    }
}

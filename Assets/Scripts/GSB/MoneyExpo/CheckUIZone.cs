using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AD;

public class CheckUIZone : MonoBehaviour
{
    [SerializeField] private GameObject _zone;
    [Header("UI Popup Controller")]
    [SerializeField] TextMeshProUGUI Headline;
    [Space(10)]
    [SerializeField] string[] HeadlineString;
    [Space(10)]
    [SerializeField] TextMeshProUGUI detailInformation;
    [Space(10)]
    [SerializeField] string[] detailString;
    [Space(10)]
    [SerializeField] GameObject Zone1;
    [Space(10)]
    [SerializeField] GameObject Zone3;
    [Space(10)]
    [SerializeField] GameObject Zone5;
    [Space(10)]
    [SerializeField] Image[] imageNPCZone1;
    [Space(10)]
    [SerializeField] Image[] imageNPCZone3;
    [Space(10)]
    [SerializeField] Image[] imageNPCZone5;
    [Space(10)]
    [SerializeField] Sprite[] imagePopup;

    [Space(10)]
    [SerializeField] GameObject _shopee;
    [SerializeField] GameObject _Zone;

    [Space(10)]
    [SerializeField] TextMeshProUGUI[] buttonInfoText;
    [Space(10)]
    [SerializeField] string[] ColorEachButton;
    [Space(10)]
    [SerializeField] Color[] newColor;
    [Space(10)]
    [SerializeField] string[] websiteString;
    [Space(10)]
    [SerializeField] GameplaySystem _gameplaySystem;

    [Space(20)]
    public int checkNPC;

    [SerializeField] WebMobileDetector _webMobile;
    [SerializeField] GameObject _uiPC;
    [SerializeField] GameObject _uiMobile;
    [SerializeField] CoinManager _coinManager;

    [SerializeField] OpenLinkOnIOS _openLinkOnIOS;
    [SerializeField] DeviceDetection _deviceDetection;


    // Start is called before the first frame update
    void Awake()
    {
        _coinManager = GameObject.FindObjectOfType<CoinManager>().GetComponent<CoinManager>();
        _webMobile = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<WebMobileDetector>();
        _openLinkOnIOS = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<OpenLinkOnIOS>();
        _deviceDetection = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<DeviceDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_coinManager.numCoins == 25)
        {
            _uiPC.SetActive(false);
            _uiMobile.SetActive(false);

            _zone.SetActive(true);
        }
    }

    public void onClickPopup()
    {
        if (_webMobile.isMobile == true)
        {
            _uiPC.SetActive(false);
            _uiMobile.SetActive(false);
        }

        if (_webMobile.isMobile == false)
        {
            _uiPC.SetActive(false);
            _uiMobile.SetActive(false);
        }

        if (checkNPC == 0)
        {
            Headline.gameObject.SetActive(true);
            Headline.text = HeadlineString[0];
            
            detailInformation.text = detailString[0];

            detailInformation.gameObject.SetActive(true);
            
            if (ColorUtility.TryParseHtmlString(ColorEachButton[0], out newColor[0]))
            {
                buttonInfoText[0].color = newColor[0];
            }

            if (ColorUtility.TryParseHtmlString(ColorEachButton[1], out newColor[1]))
            {
                buttonInfoText[1].color = newColor[1];
            }

            Zone1.SetActive(true);
            Zone3.SetActive(false);
            Zone5.SetActive(false);

            imageNPCZone1[0].sprite = imagePopup[0];
            imageNPCZone1[1].sprite = imagePopup[1];

            _shopee.SetActive(false);
        }

        if (checkNPC == 1)
        {
            Headline.gameObject.SetActive(true);
            Headline.text = HeadlineString[1];

            detailInformation.text = detailString[1];

            detailInformation.gameObject.SetActive(true);

            if (ColorUtility.TryParseHtmlString(ColorEachButton[5], out newColor[0]))
            {
                buttonInfoText[0].color = newColor[0];
            }

            if (ColorUtility.TryParseHtmlString(ColorEachButton[6], out newColor[1]))
            {
                buttonInfoText[1].color = newColor[1];
            }

            Zone1.SetActive(true);
            Zone3.SetActive(false);
            Zone5.SetActive(false);

            imageNPCZone1[0].sprite = imagePopup[2];
            imageNPCZone1[1].sprite = imagePopup[3];

            _shopee.SetActive(false);
        }

        if (checkNPC == 2)
        {
            Headline.gameObject.SetActive(true);
            Headline.text = HeadlineString[2];

            detailInformation.text = detailString[2];

            detailInformation.gameObject.SetActive(true);

            if (ColorUtility.TryParseHtmlString(ColorEachButton[10], out newColor[0]))
            {
                buttonInfoText[0].color = newColor[0];
            }

            if (ColorUtility.TryParseHtmlString(ColorEachButton[11], out newColor[1]))
            {
                buttonInfoText[1].color = newColor[1];
            }

            if (ColorUtility.TryParseHtmlString(ColorEachButton[12], out newColor[2]))
            {
                buttonInfoText[2].color = newColor[2];
            }

            if (ColorUtility.TryParseHtmlString(ColorEachButton[13], out newColor[3]))
            {
                buttonInfoText[3].color = newColor[3];
            }

            Zone1.SetActive(false);
            Zone3.SetActive(true);
            Zone5.SetActive(false);
            
            imageNPCZone3[0].sprite = imagePopup[4];
            imageNPCZone3[1].sprite = imagePopup[5];
            imageNPCZone3[2].sprite = imagePopup[6];
            imageNPCZone3[3].sprite = imagePopup[7];

            _shopee.SetActive(false);
        }

        if (checkNPC == 3)
        {
            Headline.gameObject.SetActive(true);
            Headline.text = HeadlineString[3];

            detailInformation.text = detailString[3];

            detailInformation.gameObject.SetActive(true);

            if (ColorUtility.TryParseHtmlString(ColorEachButton[15], out newColor[0]))
            {
                buttonInfoText[0].color = newColor[0];
            }

            if (ColorUtility.TryParseHtmlString(ColorEachButton[16], out newColor[1]))
            {
                buttonInfoText[1].color = newColor[1];
            }

            if (ColorUtility.TryParseHtmlString(ColorEachButton[17], out newColor[2]))
            {
                buttonInfoText[2].color = newColor[2];
            }

            if (ColorUtility.TryParseHtmlString(ColorEachButton[18], out newColor[3]))
            {
                buttonInfoText[3].color = newColor[3];
            }

            Zone1.SetActive(false);
            Zone3.SetActive(true);
            Zone5.SetActive(false);

            imageNPCZone3[0].sprite = imagePopup[8];
            imageNPCZone3[1].sprite = imagePopup[9];
            imageNPCZone3[2].sprite = imagePopup[10];
            imageNPCZone3[3].sprite = imagePopup[11];

            _shopee.SetActive(false);
        }

        if (checkNPC == 4)
        {
            Headline.gameObject.SetActive(true);
            Headline.text = HeadlineString[4];

            detailInformation.text = detailString[4];

            detailInformation.gameObject.SetActive(true);

            if (ColorUtility.TryParseHtmlString(ColorEachButton[20], out newColor[0]))
            {
                buttonInfoText[0].color = newColor[0];
            }

            if (ColorUtility.TryParseHtmlString(ColorEachButton[21], out newColor[1]))
            {
                buttonInfoText[1].color = newColor[1];
            }

            Zone1.SetActive(false);
            Zone3.SetActive(false);
            Zone5.SetActive(true);


            imageNPCZone5[0].sprite = imagePopup[12];
            imageNPCZone5[1].sprite = imagePopup[13];
            imageNPCZone5[2].sprite = imagePopup[14];

            _shopee.SetActive(false);
        }

        if (checkNPC == 5)
        {
            Headline.gameObject.SetActive(true);
            Headline.text = HeadlineString[5];

            detailInformation.gameObject.SetActive(false);


            Zone1.SetActive(true);
            Zone3.SetActive(false);
            Zone5.SetActive(false);

            imageNPCZone1[0].sprite = imagePopup[15];
            imageNPCZone1[1].sprite = imagePopup[16];

            _shopee.SetActive(false);
        }

        if (checkNPC == 6)
        {
            Headline.gameObject.SetActive(true);
            Headline.text = HeadlineString[6];

            detailInformation.gameObject.SetActive(false);


            Zone1.SetActive(false);
            Zone3.SetActive(false);
            Zone5.SetActive(true);

            imageNPCZone5[0].sprite = imagePopup[17];
            imageNPCZone5[1].sprite = imagePopup[18];
            imageNPCZone5[2].sprite = imagePopup[19];

            _shopee.SetActive(false);
        }

    }

    public void onShopeeZone()
    {
        Headline.gameObject.SetActive(false);

        detailInformation.gameObject.SetActive(false);

        Zone1.SetActive(false);
        Zone3.SetActive(false);

        _shopee.SetActive(true);
    }

    public void onClickShopeeLink()
    {
        Debug.Log("Click Shopee");

        if (_webMobile.isMobile == false)
        { 
            Application.OpenURL(websiteString[20]);
        }
        if (_webMobile.isMobile == true)
        {
            if (_deviceDetection.isIPhone)
            {
                _openLinkOnIOS.OpenLink(websiteString[20]);
            }
            else if (_deviceDetection.isIPad)
            {
                _openLinkOnIOS.OpenLink(websiteString[20]);
            }
            else
            { 
                Application.OpenURL(websiteString[20]);
            }
        }

        _coinManager.onGetAllCoin();

        if (_webMobile.isMobile == true)
        {
            _uiPC.SetActive(false);
            _uiMobile.SetActive(true);
        }

        if (_webMobile.isMobile == false)
        {
            _uiPC.SetActive(true);
            _uiMobile.SetActive(false);
        }

        _shopee.SetActive(false);
        _Zone.SetActive(false);


    }

    public void OpenLinkZone0()
    {
        if (checkNPC == 0)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[0]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[0]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[0]);
                }
                else
                {
                    Application.OpenURL(websiteString[0]);
                }
            }
        }

        if (checkNPC == 1)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[2]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[2]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[2]);
                }
                else
                {
                    Application.OpenURL(websiteString[2]);
                }
            }
        }

        if (checkNPC == 2)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[4]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[4]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[4]);
                }
                else
                {
                    Application.OpenURL(websiteString[4]);
                }
            }
        }

        if (checkNPC == 3)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[8]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[8]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[8]);
                }
                else
                {
                    Application.OpenURL(websiteString[8]);
                }
            }
        }

        if (checkNPC == 4)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[12]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[12]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[12]);
                }
                else
                {
                    Application.OpenURL(websiteString[12]);
                }
            }
        }

        if (checkNPC == 5)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[15]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[15]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[15]);
                }
                else
                {
                    Application.OpenURL(websiteString[15]);
                }
            }
        }

        if (checkNPC == 6)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[17]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[17]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[17]);
                }
                else
                {
                    Application.OpenURL(websiteString[17]);
                }
            }
        }
    }

    public void OpenLinkZone1()
    {
        if (checkNPC == 0)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[1]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[1]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[1]);
                }
                else
                {
                    Application.OpenURL(websiteString[1]);
                }
            }
        }

        if (checkNPC == 1)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[3]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[3]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[3]);
                }
                else
                {
                    Application.OpenURL(websiteString[3]);
                }
            }
        }

        if (checkNPC == 2)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[5]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[5]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[5]);
                }
                else
                {
                    Application.OpenURL(websiteString[5]);
                }
            }
        }

        if (checkNPC == 3)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[9]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[9]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[9]);
                }
                else
                {
                    Application.OpenURL(websiteString[9]);
                }
            }
        }

        if (checkNPC == 4)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[13]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[13]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[13]);
                }
                else
                {
                    Application.OpenURL(websiteString[13]);
                }
            }
        }

        if (checkNPC == 5)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[16]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[16]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[16]);
                }
                else
                {
                    Application.OpenURL(websiteString[16]);
                }
            }
        }

        if (checkNPC == 6)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[18]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[18]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[18]);
                }
                else
                {
                    Application.OpenURL(websiteString[18]);
                }
            }
        }
    }

    public void OpenLinkZone2()
    {
        if (checkNPC == 2)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[6]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[6]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[6]);
                }
                else
                {
                    Application.OpenURL(websiteString[6]);
                }
            }
        }

        if (checkNPC == 3)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[10]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[10]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[10]);
                }
                else
                {
                    Application.OpenURL(websiteString[10]);
                }
            }
        }

        if (checkNPC == 4)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[14]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[14]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[14]);
                }
                else
                {
                    Application.OpenURL(websiteString[14]);
                }
            }
        }

        if (checkNPC == 6)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[19]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[19]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[19]);
                }
                else
                {
                    Application.OpenURL(websiteString[19]);
                }
            }
        }
    }
    public void OpenLinkZone3()
    {
        if (checkNPC == 2)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[7]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[7]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[7]);
                }
                else
                {
                    Application.OpenURL(websiteString[7]);
                }
            }
        }

        if (checkNPC == 3)
        {
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL(websiteString[11]);
            }
            if (_webMobile.isMobile == true)
            {
                if (_deviceDetection.isIPhone)
                {
                    _openLinkOnIOS.OpenLink(websiteString[11]);
                }
                else if (_deviceDetection.isIPad)
                {
                    _openLinkOnIOS.OpenLink(websiteString[11]);
                }
                else
                {
                    Application.OpenURL(websiteString[11]);
                }
            }
        }
    }
}

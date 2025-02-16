// Gameplay System V1.4 By VF
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using AD.Network;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using MarksAssets.LaunchURLWebGL;
using AD;


    public class GameplaySystem : MonoBehaviour
    {
        public ThirdPersonController thirdController;
        public CharacterControl charControl;
        public static GameplaySystem main;

        [SerializeField] CoinManager _coinManager;

        public ARPopup arpopup;
        //public RectTransform menu;
        //public GameObject popup;
        //public TextMeshProUGUI popupOld;
        //public Text popupTMP;
        //public Text NamepopupTMP;
        //private string privateMap = "VIPRoom";
        public Chair[] chairs;

        //Profile Select
        //public Text targetName;
        //public Text targetFull;
        //public Text targetBio;

        public GameObject /*panelWarpOff, panelWarpOn,*/ panelEmoOff, panelEmoOn, panelEmoMobileOn, panelEmoMobileoff;//, panelInfo;

        public CharacterControl characterControl;
        [HideInInspector] CharacterControl menuCharacterControl;
        [HideInInspector] CharacterControl contactCharacterControl;
        [HideInInspector] CharacterControl.Command contactCommand;
        [SerializeField] int contactData;

        public GameObject tutorialPage;
        public GameObject tutorialPopup;
        public GameObject mapPage;

        public GameObject zonePage;

        public PhotonView phView;

        [SerializeField] bool tutorialOpen = false;

        [SerializeField] bool zoneOpen = false;

        [SerializeField] bool mapOpen = false;

        [SerializeField] GameObject obj;

        public ToggleMic mic;

        public AudioSource audioListen;
        public AudioSource coin;
        public AudioSource Video;

        public AudioSource[] audioAll;

        [SerializeField] GameObject _tutorialStep;

        public bool isOverUI => EventSystem.current != null ? EventSystem.current.IsPointerOverGameObject(-1) : false;

        public GameObject[] UIZone;
        public GameObject UiCanvas;

        [SerializeField] GameObject _uiMobile;
        [SerializeField] GameObject _uiPC;

        [SerializeField] GameObject _uiTutorialPC;
        [SerializeField] GameObject _uiTutorialMobile;
        [SerializeField] GameObject _uiLookMobile;

        [SerializeField] WebMobileDetector _webMobile;

        [SerializeField] LaunchURLWebGL _launchURLWebGL;

        [SerializeField] GameObject _shopeeUI;

        public DeviceDetection deviceDetection;

        [SerializeField] GameObject[] _allSound;

    string sceneName;

        private void Awake()
        {
            if (main == null) main = this;
            else Destroy(gameObject);
            tutorialOpen = true;

            _webMobile = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<WebMobileDetector>();

            deviceDetection = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<DeviceDetection>();
        }

        public void deleteNetwork()
        {
            GameObject obj = GameObject.FindWithTag("NetworkManager");
            if (obj != null)
            {
                Destroy(obj);
            }

            Destroy(obj);

            SceneManager.LoadScene("ChooseRoom");

            _coinManager = GameObject.FindGameObjectWithTag("CoinManager").GetComponent<CoinManager>();
        }

        /*public void Teleport(string mapname) 
        {
            NetworkManager.main.spawnPoint = Vector3.zero;
            NetworkManager.main.Join(mapname);
        }*/

        public void EnableThirdPerson()
        {
           /* if (SceneManager.GetActiveScene().name == "AomsinLand(DevelopScene)-Edit")
            {
                arpopup.GetComponent<ARPopup>().enabled = true;
            }*/

            thirdController.enabled = true;

            charControl.enabled = true;
        }

        public void DisableThirdPerson()
        {
            thirdController.GetComponent<ThirdPersonController>().enabled = false;
        }

        public void onClickCloseStepTutorial()
        {
            _tutorialStep.SetActive(false);

            mapPage.SetActive(true);
        }

        private void SetupChairs()
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Chair");
            chairs = new Chair[gameObjects.Length];
            for (int i = 0; i < gameObjects.Length; i++) { chairs[i] = gameObjects[i].GetComponent<Chair>(); }
        }

        private void Update()
        {
            //if (Input.GetButtonDown("Fire1")) if (!isOverUI) HideMenu(); 

            if (SceneManager.GetActiveScene().name == "AomsinLand(DevelopScene)-Edit")
            {
                /*if (tutorialOpen == true)
                {
                    mic.enabled = false;

                    arpopup.tutorialFirst = true;
                    arpopup.ischeck = false;
                }*/

                /*if (tutorialOpen == false)
                {
                    arpopup.tutorialFirst = false;
                    arpopup.ischeck = true;
                }*/

            Debug.Log("Is Mobile : " + _webMobile.isMobile);
            }

            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {
                if (mapOpen == true)
                {
                    mic.enabled = false;
                }
            }

            if (SceneManager.GetActiveScene().name == "MoneyExpo Phase 2")
            {
                if (mapOpen == true)
                {
                    mic.enabled = false;
                }
            }

            if (SceneManager.GetActiveScene().name == "MoneyExpo2024")
            {
                if (_coinManager.numCoins == 20)
                {
                    onUIOpen();
                }
            }

            if (SceneManager.GetActiveScene().name == "ChristmasScene")
            {
                if (_coinManager.numCoins == 25)
                {
                    onUIOpen();
                }
            }
        }

        public void Start()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (deviceDetection.isIPhone)
            {
                for (int i = 0; i < _allSound.Length; i++)
                {
                    _allSound[i].SetActive(false);
                }
            }

        sceneName = currentScene.name;

            //panelWarpOff.SetActive(true);
            //panelWarpOn.SetActive(false);
            if(_webMobile.isMobile == true) 
            {
                panelEmoOff.SetActive(false);
                panelEmoOn.SetActive(false);

                panelEmoMobileoff.SetActive(true);
                panelEmoMobileOn.SetActive(false);
            }

            if (_webMobile.isMobile == false)
            {
                panelEmoOff.SetActive(true);
                panelEmoOn.SetActive(false);

                panelEmoMobileoff.SetActive(false);
                panelEmoMobileOn.SetActive(false);
            }
            //panelInfo.SetActive(false);

            mic = GameObject.FindObjectOfType<ToggleMic>();
            phView = GameObject.FindObjectOfType<PhotonView>();

            thirdController = GameObject.FindObjectOfType<ThirdPersonController>();
            thirdController.enabled = false;

            charControl = GameObject.FindObjectOfType<CharacterControl>();
            charControl.enabled = false;

            SetupChairs();

            if (sceneName == "AomsinLand(DevelopScene)-Edit")
            {
                Destroy(GameObject.Find("MusicDress"));
            /*arpopup = GameObject.FindObjectOfType<ARPopup>();
            arpopup.enabled = false;*/

            if (_webMobile.isMobile == false)
            {
                _uiPC.SetActive(true);
                _uiMobile.SetActive(false);
            }

            if (_webMobile.isMobile == true)
            {
                _uiPC.SetActive(false);
                _uiMobile.SetActive(true);
            }

            EnableThirdPerson();
        }
        }        

        public void ToggleEmotion(bool stage) 
        {
            if (stage)
            {
                if (_webMobile.isMobile == true)
                {
                    panelEmoOff.SetActive(false);
                    panelEmoOn.SetActive(false);

                    panelEmoMobileoff.SetActive(false);
                    panelEmoMobileOn.SetActive(true);
                }

                if (_webMobile.isMobile == false)
                {
                    panelEmoOff.SetActive(false);
                    panelEmoOn.SetActive(true);

                    panelEmoMobileoff.SetActive(false);
                    panelEmoMobileOn.SetActive(false);
                }
            }

            else 
            {
                if (_webMobile.isMobile == true)
                {
                    panelEmoOff.SetActive(false);
                    panelEmoOn.SetActive(false);

                    panelEmoMobileoff.SetActive(true);
                    panelEmoMobileOn.SetActive(false);
                }

                if (_webMobile.isMobile == false)
                {
                    panelEmoOff.SetActive(true);
                    panelEmoOn.SetActive(false);

                    panelEmoMobileoff.SetActive(false);
                    panelEmoMobileOn.SetActive(false);
                }
            }
        }

        /*public void ToggleWarp(bool stage)
        {
            if (stage)
            {
                panelWarpOff.SetActive(false);
                panelWarpOn.SetActive(true);
            }

            else
            {
                panelWarpOff.SetActive(true);
                panelWarpOn.SetActive(false);
            }
        }*/

        /*public void ToggleInfo(bool stage)
        {
            if (stage)
            {
                panelInfo.SetActive(true);
            }

            else
            {
                panelInfo.SetActive(false);
            }
            HideMenu();
        }*/

        public void ZonePageOpen()
        {
            zoneOpen = true;

            DisableThirdPerson();

            zonePage.SetActive(true);

            if (_webMobile.isMobile == false)
            {
                _uiPC.SetActive(false);
                _uiMobile.SetActive(false);
            }

            if (_webMobile.isMobile == true)
            {
                _uiPC.SetActive(false);
                _uiMobile.SetActive(false);
            }
        }

        public void onClickWebsite(string web)
        {
            _launchURLWebGL.launchURLBlank(web);
        }

        public void closeShopee()
        {
            Debug.Log("Close Shopee");
            _coinManager.onGetAllCoin();
            _shopeeUI.SetActive(false);
            onUIClose();
            if (_webMobile.isMobile == false)
            {
                Application.OpenURL("https://forms.office.com/r/fPF2VdVcxv?origin=lprLink");
            }
            else
            {
                Application.OpenURL("https://forms.office.com/r/fPF2VdVcxv?origin=lprLink");
            }

        }

        public void ZonePageClose()
        {
            if (_shopeeUI.activeSelf == true)
            {
                if (_webMobile.isMobile == false)
                {
                    Application.OpenURL("https://forms.office.com/r/fPF2VdVcxv?origin=lprLink");
                }
                else
                {
                    onClickWebsite("https://forms.office.com/r/fPF2VdVcxv?origin=lprLink");
                }

            Debug.Log("Close Shopee");
            _coinManager.onGetAllCoin();
            _shopeeUI.SetActive(false);
            onUIClose();
        }

            else
            {
                zoneOpen = false;

                zonePage.SetActive(false);

                if (_webMobile.isMobile == false)
                {
                    _uiTutorialPC.SetActive(true);
                    _uiTutorialMobile.SetActive(false);
                    _uiLookMobile.SetActive(false);
                }

                if (_webMobile.isMobile == true)
                {
                    _uiTutorialPC.SetActive(false);
                    _uiTutorialMobile.SetActive(true);
                    _uiLookMobile.SetActive(true);
                }
            }
        }

        public void TutorialPageOpen()
        {
            /*phView = gameObject.GetComponent<PhotonView>();
            if (!phView.IsMine)
            {
                // Not our local player - ignore
                return;
            }*/

            tutorialOpen = true;

            DisableThirdPerson();

            tutorialPage.SetActive(true);

            if (_webMobile.isMobile == false)
            {
                _uiPC.SetActive(false);
                _uiMobile.SetActive(false);
            }

            if (_webMobile.isMobile == true)
            {
                _uiPC.SetActive(false);
                _uiMobile.SetActive(false);
            }
        }

        public void TutorialPageClose()
        {
            tutorialOpen = false;
            tutorialPage.SetActive(false);

        if (_webMobile.isMobile == false)
        {
            _uiPC.SetActive(true);
            _uiMobile.SetActive(false);
        }

        if (_webMobile.isMobile == true)
        {
            _uiPC.SetActive(false);
            _uiMobile.SetActive(true);
        }

        EnableThirdPerson();

    }

    public void TutorialPopupClose()
    {
        tutorialOpen = false;
        tutorialPopup.SetActive(false);

        if (_webMobile.isMobile == false)
        {
            _uiPC.SetActive(true);
            _uiMobile.SetActive(false);
        }

        if (_webMobile.isMobile == true)
        {
            _uiPC.SetActive(false);
            _uiMobile.SetActive(true);
        }

        EnableThirdPerson();
    }


        public void MapPageOpen()
        {
        mapOpen = true;

        DisableThirdPerson();

        mapPage.SetActive(true);

        if (_webMobile.isMobile == false)
        {
            _uiPC.SetActive(false);
            _uiMobile.SetActive(false);
        }

        if (_webMobile.isMobile == true)
        {
            _uiPC.SetActive(false);
            _uiMobile.SetActive(false);
        }
    }

        public void MapPageClose()
        {
            /*phView = gameObject.GetComponent<PhotonView>();

            if (!phView.IsMine)
            {
                // Not our local player - ignore
                return;
            }*/

            mapOpen = false;
            mapPage.SetActive(false);

            if (_webMobile.isMobile == false)
            {
                _uiPC.SetActive(true);
                _uiMobile.SetActive(false); 
        }

            if (_webMobile.isMobile == true)
            {
                _uiPC.SetActive(false);
                _uiMobile.SetActive(true);
            }

            EnableThirdPerson();
        }

        public void onUIOpen()
        {
            if (_webMobile.isMobile == false)
            {
                _uiTutorialPC.SetActive(false);
                _uiTutorialMobile.SetActive(false);
                _uiLookMobile.SetActive(false);
            }

            if (_webMobile.isMobile == true)
            {
                _uiTutorialPC.SetActive(false);
                _uiTutorialMobile.SetActive(false);
                _uiLookMobile.SetActive(false);
            }
        }

        public void onUIClose()
        {
            if (_webMobile.isMobile == false)
            {
                _uiTutorialPC.SetActive(true);
                _uiTutorialMobile.SetActive(false);
                _uiLookMobile.SetActive(false);
                _uiLookMobile.SetActive(false);
            }

            if (_webMobile.isMobile == true)
            {
                _uiTutorialPC.SetActive(false);
                _uiTutorialMobile.SetActive(true);
                _uiLookMobile.SetActive(true);
            }
        }


        public void SoundOpen()
        {
            audioListen.GetComponent<AudioSource>().mute = true;
            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {

            }
            else
            {
                Video.GetComponent<AudioSource>().mute = true;
            }
            
            coin.GetComponent<AudioSource>().mute = true;
            foreach (AudioSource audio in audioAll)
            {
                audio.mute = true;
            }
        }

        public void SoundMute()
        {
            audioListen.GetComponent<AudioSource>().mute = false;
            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {

            }
            else
            {
                Video.GetComponent<AudioSource>().mute = true;
            }
            coin.GetComponent<AudioSource>().mute = false;
            foreach (AudioSource audio in audioAll)
            {
                audio.mute = false;
            }
        }

        public void ShowMenu(int viewID)
        {
            menuCharacterControl = FindCharacterControl(viewID);
            //menu.position = Input.mousePosition;
            //menu.gameObject.SetActive(true);
        }

        //public void ClickPrivateRoom() => ClickCommand(CharacterControl.Command.PrivateRoom, Random.Range(10000, 99999));
        //public void ClickMarry() => ClickCommand(CharacterControl.Command.Marry);
        //public void ClickHug() => ClickCommand(CharacterControl.Command.Hug);
        /*public void ClickCommand(CharacterControl.Command command, int data = 0)
        { 
            menuCharacterControl.photonView.RPC("Request", RpcTarget.All, characterControl.photonView.ViewID, command, data);
            HideMenu();
        }*/

        /*public void ClickProfile()
        {
            targetName.text = menuCharacterControl.nickname;

            HideMenu();
        }*/
        public void ClickClosePhotobooth()
        {
            characterControl.photonView.RPC("PlayerOutroom", RpcTarget.All, 0);
        }
        /*public void WarpToPhotobooth()
        {
            Vector3 myPos = new Vector3(1.39f, 0, -500.57f);
            Quaternion myEulerAngles = new Quaternion(0, 3.723f, 0, 0);
            characterControl.photonView.RPC("CharacterTelepot", RpcTarget.All, myPos, myEulerAngles);
        }*/

        /*public void HideMenu()
        {
            if (menu.gameObject.activeSelf)
            {
                menu.gameObject.SetActive(false);
            }
        }*/

        /*public void RequestReceive(int fromViewID, CharacterControl.Command command, int data)
        {
            if (contactCharacterControl != null) return;
            contactCharacterControl = FindCharacterControl(fromViewID);
            contactCommand = command;
            contactData = data;
            NamepopupTMP.text = contactCharacterControl.nameTMP.text;
            switch (command)
            {
                case CharacterControl.Command.PrivateRoom:                    
                    popupTMP.text = "ต้องการชวนคุณเข้าห้องส่วนตัว";
                    break;
                case CharacterControl.Command.Marry:
                    popupTMP.text = "ต้องการขอแต่งงานกับคุณ";
                    break;
                case CharacterControl.Command.Hug:
                    popupTMP.text = "ต้องการขอกอดกับคุณ";
                    break;
            }
            popup.SetActive(true);
        }*/
       /* public void RequestAccept()
        {
            if (contactCharacterControl == null) return;
            if (characterControl.animationNow == CharacterControl.AnimationName.Sit) return;
            if (contactCharacterControl.animationNow == CharacterControl.AnimationName.Sit) return;
            switch (contactCommand)
            {
                case CharacterControl.Command.PrivateRoom:
                    contactCharacterControl.photonView.RPC("CharacterTelepotToMap", RpcTarget.Others, privateMap, contactData);
                    Network.NetworkManager.main.Join(privateMap, contactData);
                    break;
                case CharacterControl.Command.Marry:
                    PlayCoupleAnimation(CharacterControl.AnimationName.Marry, 1.5f);
                    break;
                case CharacterControl.Command.Hug:
                    //PhotoBooth(CharacterControl.AnimationName.Hug, 0.2f);
                    PlayCoupleAnimation(CharacterControl.AnimationName.Hug, 0.4f);
                    break;
            }
            RequestCancel();
        }*/
        /*public void RequestCancel()
        {
            contactCharacterControl = null;
            contactCommand = CharacterControl.Command.None;
            contactData = 0;
            //popup.SetActive(false);
        }*/

        public void PlayAnimationClapping() => characterControl.PlayAnimationClapping();
        //public void PlayAnimationHug() => characterControl.PlayAnimationHug();
        //public void PlayAnimationSit() => characterControl.PlayAnimationSit();
        public void PlayAnimationPraying() => characterControl.PlayAnimationPraying();
        public void PlayAnimationPointing() => characterControl.PlayAnimationPointing();
        public void PlayAnimationWaving() => characterControl.PlayAnimationWaving();
        //public void PlayAnimationMarry() => characterControl.PlayAnimationMarry();
        /*private void PlayCoupleAnimation(CharacterControl.AnimationName animationName, float distance = 1)
        {
            Vector3 myEulerAngles = 
        (characterControl.transform.position, contactCharacterControl.transform.position);
            Vector3 contactEulerAngles = LookAt(contactCharacterControl.transform.position, characterControl.transform.position);
            contactCharacterControl.photonView.RPC("CharacterTelepot", RpcTarget.All, contactCharacterControl.transform.position, contactEulerAngles);
            characterControl.photonView.RPC("CharacterTelepot", RpcTarget.All, contactCharacterControl.transform.position + (contactCharacterControl.transform.forward * distance), myEulerAngles);
            contactCharacterControl.photonView.RPC("PlayAnimation", RpcTarget.All, animationName);
            characterControl.photonView.RPC("PlayAnimation", RpcTarget.All, animationName);
            if (animationName == CharacterControl.AnimationName.Marry) 
            {
                //Don't forget to remove comments

                //StartCoroutine(DefaultCloth(characterControl, characterControl.characterEditor.GetBottomIndex()));
                //StartCoroutine(DefaultCloth(contactCharacterControl, contactCharacterControl.characterEditor.GetBottomIndex()));

                //End Here

                contactCharacterControl.photonView.RPC("ChangeClothSet", RpcTarget.All, 10);
                characterControl.photonView.RPC("ChangeClothSet", RpcTarget.All, 10);
                
            }
        }*/

        /*IEnumerator DefaultCloth(CharacterControl characterControl, int groupIndex)
        {
            yield return new WaitForSeconds(13);
            characterControl.photonView.RPC("ChangeClothSet", RpcTarget.All, groupIndex);

        }
        private void PhotoBooth(int number, float distance = 1)
        {
            Debug.Log("work");
            Vector3 myPos = new Vector3(1.39f, 0, -500.57f);
            Vector3 partnerPos = new Vector3(0.46f, 0, -500.6f);

            //Vector3 myEulerAngles = LookAt(partnerPos, myPos);
            //Vector3 contactEulerAngles = LookAt(myPos, partnerPos);
            Quaternion myEulerAngles = new Quaternion(0, 3.723f, 0, 0);
            Quaternion contactEulerAngles = new Quaternion(0, 20.976f, 0, 0);

            characterControl.photonView.RPC("CharacterTelepot", RpcTarget.All, myPos, contactEulerAngles);
            contactCharacterControl.photonView.RPC("CheckPlayerInside", RpcTarget.All, 0);

            if (number == 0)
            {
            }
            else
            {
                contactCharacterControl.photonView.RPC("CharacterTelepot", RpcTarget.All, partnerPos, myEulerAngles);
            }

            //contactCharacterControl.photonView.RPC("PlayAnimation", RpcTarget.All, animationName);
            //characterControl.photonView.RPC("PlayAnimation", RpcTarget.All, animationName);
        }*/

        public void onSelectZone1()
        {
            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {
                UiCanvas.SetActive(true);
                UIZone[0].SetActive(true);
                UIZone[1].SetActive(false);
                UIZone[2].SetActive(false);
                UIZone[3].SetActive(false);
                UIZone[4].SetActive(false);
                UIZone[5].SetActive(false);
                UIZone[6].SetActive(false);
            }
        }

        public void onSelectZone2()
        {
            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {
                UiCanvas.SetActive(true);
                UIZone[0].SetActive(false);
                UIZone[1].SetActive(true);
                UIZone[2].SetActive(false);
                UIZone[3].SetActive(false);
                UIZone[4].SetActive(false);
                UIZone[5].SetActive(false);
                UIZone[6].SetActive(false);
            }
        }

        public void onSelectZone3()
        {
            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {
                UiCanvas.SetActive(true);
                UIZone[0].SetActive(false);
                UIZone[1].SetActive(false);
                UIZone[2].SetActive(true);
                UIZone[3].SetActive(false);
                UIZone[4].SetActive(false);
                UIZone[5].SetActive(false);
                UIZone[6].SetActive(false);
            }
        }

        public void onSelectZone4()
        {
            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {
                UiCanvas.SetActive(true);
                UIZone[0].SetActive(false);
                UIZone[1].SetActive(false);
                UIZone[2].SetActive(false);
                UIZone[3].SetActive(true);
                UIZone[4].SetActive(false);
                UIZone[5].SetActive(false);
                UIZone[6].SetActive(false);
            }
        }

        public void onSelectZone5()
        {
            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {
                UiCanvas.SetActive(true);
                UIZone[0].SetActive(false);
                UIZone[1].SetActive(false);
                UIZone[2].SetActive(false);
                UIZone[3].SetActive(false);
                UIZone[4].SetActive(true);
                UIZone[5].SetActive(false);
                UIZone[6].SetActive(false);
            }
        }

        public void onSelectZone6()
        {
            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {
                UiCanvas.SetActive(true);
                UIZone[0].SetActive(false);
                UIZone[1].SetActive(false);
                UIZone[2].SetActive(false);
                UIZone[3].SetActive(false);
                UIZone[4].SetActive(false);
                UIZone[5].SetActive(true);
                UIZone[6].SetActive(false);
            }
        }

        public void onSelectZone7()
        {
            if (SceneManager.GetActiveScene().name == "MoneyExpo")
            {
                UiCanvas.SetActive(true);
                UIZone[0].SetActive(false);
                UIZone[1].SetActive(false);
                UIZone[2].SetActive(false);
                UIZone[3].SetActive(false);
                UIZone[4].SetActive(false);
                UIZone[5].SetActive(false);
                UIZone[6].SetActive(true);
            }
        }


        public Chair FindChair(int index) => chairs[index];
        public int FindChairIndex(Chair chair)
        {
            for (int i = 0; i < chairs.Length; i++)
            {
                if (chair == chairs[i]) return i;
            }
            return -1;
        }
        public CharacterControl FindCharacterControl(int viewID)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject go in gameObjects)
            {
                CharacterControl characterControl = go.GetComponent<CharacterControl>();
                if (characterControl.photonView.ViewID == viewID) return characterControl;
            }
            return null;
        }

        private Vector3 LookAt(Vector3 from, Vector3 to)
        {
            Vector3 lookPosition = to - from;
            lookPosition.y = 0;
            return Quaternion.LookRotation(lookPosition).eulerAngles;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GameplaySystem), true)]
    public class GameplaySystemEditor : Editor
    {
        protected GameplaySystem gameplaySystem;
        private void OnEnable() => gameplaySystem = (GameplaySystem)target;

        /*public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("SetupChairs")) { SetupChairs(); }
        }*/
        /*
        private void SetupChairs()
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Chair");
            gameplaySystem.chairs = new Chair[gameObjects.Length];
            for (int i = 0; i < gameObjects.Length; i++) { gameplaySystem.chairs[i] = gameObjects[i].GetComponent<Chair>(); }
        }*/
    }
#endif

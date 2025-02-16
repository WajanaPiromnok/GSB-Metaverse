// Character Control V1.6 By VF
// Character Control Plus Sound By ST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
using FrostweepGames.VoicePro;
using FrostweepGames.Plugins.Native;
using StarterAssets;
using UnityEngine.SceneManagement;

namespace AD
{
    public class CharacterControl : MonoBehaviourPunCallbacks, IPunObservable
    {
        public TextMeshPro nameTMP;
        public Animator animator;
        //public NavMeshAgent agent;
        public new PhotonView photonView;

        //Don't forget to remove Comments

        public CharacterEditor characterEditor;

        [SerializeField] bool micCheck = false;

        //End here
        //[HideInInspector] public GameObject flower;
        public string micName;
        public ToggleMic playerMicToggle;
        public GameObject mic;
        public new Camera camera;
        public new UICanvasControllerInput uiCanvasController;
        public new MobileDisableAutoSwitchControls mobileDisableAuto;
        public AnimationName animationNow = AnimationName.IdleWalkRunBlend;
        private int animationCount;
        private Chair chair;
        private Vector2 mouseDownPosition;
        private bool holdingFlower;
        public Recorder recorder;
        public Listener listener;
        public string nickname, gender;

        public enum AnimationName { IdleWalkRunBlend, Breathing, Clapping, Praying, Pointing, Waving, Sit }

        public GameObject PlayerFollowCamera;

        public CoinManager ObjCoinManager;

        public PlayerAllManager ObjPlayerAllManager;

        public GameObject ObjPlayerArmature;

        public bool IsEnterScene = true;

        public ThirdPersonController thirdperson;

        public int NumIndexChair;

        [SerializeField] private WebMobileDetector _webMobile;

        //public int hatIndex, faceIndex, glassesIndex, bodyIndex, topIndex, bottomIndex, footwearIndex;

        void Awake()
        {
            _webMobile = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<WebMobileDetector>();
            if (!PhotonNetwork.InRoom) { this.enabled = false; return; }
            camera = Camera.main;
            if (camera == null) camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Start()
        {
            ObjCoinManager = FindObjectOfType<CoinManager>();
            ObjPlayerAllManager = FindObjectOfType<PlayerAllManager>();

            if (photonView.IsMine)
            {
                //camera.GetComponent<CameraControl>().Setup(transform);
                recorder = GameObject.FindObjectOfType<Recorder>();
                listener = GameObject.FindObjectOfType<Listener>();
                RefreshMicrophonesButtonOnClickHandler();
                recorder.StopRecord();

                if (playerMicToggle == null)
                {
                    mic = GameObject.FindGameObjectWithTag("Microphone");
                    playerMicToggle = GameObject.FindObjectOfType<ToggleMic>();
                }

                //nameTMP.text = "" + photonView.ViewID;
                ObjCoinManager = FindObjectOfType<CoinManager>();
                LoadPlayerData();
                characterEditor.Load();
                photonView.RPC("CharacterData", RpcTarget.OthersBuffered, nickname, gender, characterEditor.GetHatIndex(), characterEditor.GetFaceIndex(), characterEditor.GetGlassesIndex(), characterEditor.GetBodyIndex(), characterEditor.GetTopIndex(), characterEditor.GetBottomIndex(), characterEditor.GetFootwearIndex());

                /*อย่าลืมปิดคอมเม้นท์ เหรียญ*/
                //ObjCoinManager.ObjPlayer = gameObject;

                //Debug.Log("ObjCoinManager = " + (ObjCoinManager == null));
            }

            PhotonAnimatorView photonAnimatorView = GetComponent<PhotonAnimatorView>();
            photonAnimatorView.enabled = false;
            photonAnimatorView.enabled = true;
            thirdperson = GetComponent<ThirdPersonController>();

            if (!photonView.IsMine)
            {
                characterEditor.Load();
            }

            if (SceneManager.GetActiveScene().name != "ChooseRoom")
            {
                if (_webMobile.isMobile == true)
                {
                    uiCanvasController = UICanvasControllerInput.main;
                    if (uiCanvasController == null) uiCanvasController = GameObject.FindGameObjectWithTag("UICanvasController").GetComponent<UICanvasControllerInput>();

                    mobileDisableAuto = MobileDisableAutoSwitchControls.main;
                    if (mobileDisableAuto == null) mobileDisableAuto = GameObject.FindGameObjectWithTag("UICanvasController").GetComponent<MobileDisableAutoSwitchControls>();
                }
            }

        }

        public void LoadPlayerData()
        {
            nickname = PlayerPrefs.GetString("nickname");
            gender = PlayerPrefs.GetString("gender");
            nameTMP.text = nickname;
        }

        void Update()
        {
            //if (photonView.IsMine)
            GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = photonView.IsMine;
            PlayerFollowCamera.SetActive(photonView.IsMine);
            

            //set input mice
            if (photonView.IsMine)
            {
                CharactorInput();
                MicInput();
                
            }

            //Debug.Log("GetPlayerNumber = " + PhotonNetwork.CountOfPlayersOnMaster + "," + PhotonNetwork.CountOfPlayers);
            //CharactorInput();
            //CharactorAnimation();
        }

        private void LateUpdate() => nameTMP.transform.rotation = camera.transform.rotation;
        private void GetMicStage()
        {
            ToggleMic(playerMicToggle.toggleStage);
        }

        private void MicInput()
        {
            if (Input.GetKey(KeyCode.T))
            {
                ToggleMic(true);
            }
            if (Input.GetKeyUp(KeyCode.T))
            {
                ToggleMic(false);
            }
        }
        private void CharactorInput()
        {
            if (playerMicToggle != null) GetMicStage();
            if (Input.GetButtonDown("Fire1")) mouseDownPosition = Input.mousePosition;
            if (Input.GetButtonUp("Fire1"))
            {
                /*
                if (GameplaySystem.main.isOverUI) return;
                if (Vector2.Distance(mouseDownPosition, Input.mousePosition) > 70) return;
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Player")
                    {
                        if (Vector3.Distance(transform.position, hit.transform.position) < 1.5f)
                        {
                            PhotonView view = hit.transform.GetComponent<PhotonView>();
                            if (view.ViewID != photonView.ViewID)
                            {
                                GameplaySystem.main.ShowMenu(view.ViewID);
                            }
                        }
                    }
                    else if (hit.transform.tag == "Chair")
                    {
                        if (Vector3.Distance(transform.position, hit.transform.position) < 1.5f)
                        {
                            if (animationNow != AnimationName.Sit)
                            {
                                Chair chair = hit.transform.GetComponent<Chair>();
                                if (chair.characterControl == null)
                                {
                                    photonView.RPC("SitAt", RpcTarget.All, GameplaySystem.main.FindChairIndex(chair));
                                }
                            }

                        }
                        else
                        {
                            MoveTo(hit.point);
                            photonView.RPC("MoveTo", RpcTarget.Others, hit.point);
                        }
                    }
                    else
                    {
                        MoveTo(hit.point);
                        photonView.RPC("MoveTo", RpcTarget.Others, hit.point);
                    }

                }*/
            }
            /*if (Input.GetKeyDown(KeyCode.P))
            {
                //Don't forget to remove comments

                characterEditor.Default();
                characterEditor.Save();
                characterEditor.SetGroup(10);
                photonView.RPC("CharacterData", RpcTarget.OthersBuffered, nameTMP.text, characterEditor.GetHatIndex(), characterEditor.GetFaceIndex(), characterEditor.GetGlassesIndex(), characterEditor.GetBodyIndex(), characterEditor.GetTopIndex(), characterEditor.GetBottomIndex(), characterEditor.GetFootwearIndex());

                //End Here
            }*/

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                //Debug.Log("You Click ");
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform.tag == "Chair")
                    {
                        vFindChair(true, hit.transform.gameObject);
                        Debug.Log("You selected the " + hit.transform.name);
                    }
                    else
                        vFindChair(false, null);
                }
            }
            else if (thirdperson.PlayerIsSit &&
                (Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.UpArrow) ||
                Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D)))
            {
                vFindChair(false, null);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) PlayAnimationClapping();
            if (Input.GetKeyDown(KeyCode.Alpha2)) PlayAnimationPraying();
            //if (Input.GetKeyDown(KeyCode.Alpha3)) PlayAnimationMarry();
            if (Input.GetKeyDown(KeyCode.Alpha3)) PlayAnimationPointing();
            if (Input.GetKeyDown(KeyCode.Alpha4)) PlayAnimationWaving();
            //if (Input.GetKeyDown(KeyCode.Alpha6)) PlayAnimationHug();
            //if (Input.GetKeyDown(KeyCode.F)) { holdingFlower = !holdingFlower; flower.SetActive(holdingFlower); }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Walk Run Blend") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            {
                if (animationNow != AnimationName.IdleWalkRunBlend)
                {
                    animationNow = AnimationName.IdleWalkRunBlend;
                    thirdperson.enabled = true;
                }
            }
        }

        public void vFindChair(bool isSit, GameObject objChair)
        {
            for (int i = 0; i < GameplaySystem.main.chairs.Length; i++)
            {
                if (!thirdperson.PlayerIsSit)
                {
                    if (isSit && objChair && objChair == GameplaySystem.main.chairs[i].gameObject && GameplaySystem.main.chairs[i].gameObject.activeInHierarchy && GameplaySystem.main.chairs[i].characterControl == null && Vector3.Distance(transform.position, GameplaySystem.main.chairs[i].transform.position) < 1f)
                    {
                        //Debug.Log("1 i =" + i);
                        NumIndexChair = i;
                        thirdperson.PlayerIsSit = true;
                        thirdperson.vSetMoveZero();
                        GameplaySystem.main.chairs[i].vSetChar(transform.GetComponent<CharacterControl>());
                        animator.Play("Sit");
                        break;
                    }
                }
                else if(!isSit)
                {
                    if (NumIndexChair == i)//transform == GameplaySystem.main.chairs[i].characterControl)
                    {
                        //Debug.Log("2 i =" + i);
                        //thirdperson.vSetMoveZero();
                        thirdperson.PlayerIsSit = false;
                        //thirdperson.enabled = false;
                        GameplaySystem.main.chairs[i].vRemoveChar();
                        animator.Play("Idle Walk Run Blend");
                        //thirdperson.enabled = true;
                        break;
                    }
                }
            }

            /*
            thirdperson.PlayerIsSit = true;
            GameplaySystem.main.chairs[0].vSetChar(transform.GetComponent<CharacterControl>());
            animator.Play("Sit");*/
        }

        //transform.localRotation = Quaternion.Lerp(transform.localRotation,
        //        Quaternion.Euler(new Vector3(0, transform.localRotation.y + 360, 0)), 5f * Time.deltaTime);

        public void ToggleMic(bool mute)
        {
            if (mute && NetworkRouter.Instance.ReadyToTransmit && micName != "No Devices")
            {
                print("Ready To Call");
                print("Mic Name :" + micName);
                //checkStatus.isRecord.text = "Record With : " + micName;
                if (playerMicToggle.toggleStage == true)
                {
                    nameTMP.text = nickname + " <sprite index=0>";
                }                
                recorder.StartRecord();
            }
            else
            {
                //print("Failed to Call");
                nameTMP.text = nickname;
                RefreshMicrophonesButtonOnClickHandler();
                //checkStatus.isRecord.text = "Mute With : " + micName;
                recorder.StopRecord();
            }

        }

        public void PlayAnimation(int index) => PlayAnimation((AnimationName)index);
        public void PlayAnimation(AnimationName animationName) => photonView.RPC("PlayAnimation", RpcTarget.All, animationName);

        //{ Idle, Breathing, Clapping, Left, Right, Pointing, Waving }
        public void PlayAnimationClapping() => PlayAnimation(AnimationName.Clapping);
        public void PlayAnimationPraying() => PlayAnimation(AnimationName.Praying);
        public void PlayAnimationPointing() => PlayAnimation(AnimationName.Pointing);
        public void PlayAnimationWaving() => PlayAnimation(AnimationName.Waving);
        public void PlayAnimationIdle() => PlayAnimation(AnimationName.IdleWalkRunBlend);
        public void PlayAnimationSit() => PlayAnimation(AnimationName.Sit);

        /*public void PlayAnimationMarry() 
        {
             PlayAnimation(AnimationName.Marry);
        }*/

        /*private void CharactorAnimation()
        {
            if (animationNow != AnimationName.Idle && animationNow != AnimationName.Walk) return;
            if (Vector3.Distance(Vector3.zero, agent.velocity) > 0.3f) ChangeAnimation(AnimationName.Walk);
            else ChangeAnimation(AnimationName.Idle);
        }*/

        private void ChangeAnimation(AnimationName animationName)
        {
            if (animationNow == animationName && animationNow  ==AnimationName.IdleWalkRunBlend) return;
            animationNow = animationName;
            animationCount++;
            thirdperson.enabled = false;
            switch (animationNow)
            {
                case AnimationName.IdleWalkRunBlend: animator.CrossFadeInFixedTime(animationNow.ToString(), 0f); break;
                case AnimationName.Clapping: animator.CrossFadeInFixedTime(animationNow.ToString(), 0f); break;
                case AnimationName.Praying: animator.CrossFadeInFixedTime(animationNow.ToString(), 0f); break;
                case AnimationName.Pointing: animator.CrossFadeInFixedTime(animationNow.ToString(), 0f); break;
                case AnimationName.Waving: animator.CrossFadeInFixedTime(animationNow.ToString(), 0f); break;
                default: animator.CrossFadeInFixedTime(animationNow.ToString(), 0.2f); StartCoroutine(PlayAnimationIdle(animationCount)); break;
            }
        }

        IEnumerator PlayAnimationIdle(int count)
        {
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
            if (count == animationCount)
            {
                ChangeAnimation(AnimationName.IdleWalkRunBlend);
            }
        }

        public void MoveTo(Vector3 position)
        {
            if (this.chair != null)
            {
                this.chair.characterControl = null;
                this.chair = null;
            }
            //agent.enabled = true;
            //agent.isStopped = false;
            //agent.SetDestination(position);
            //ChangeAnimation(AnimationName.Walk);
        }

        private void RefreshMicrophonesButtonOnClickHandler()
        {
            recorder.RefreshMicrophones();
            if (CustomMicrophone.HasConnectedMicrophoneDevices())
            {
                micName = CustomMicrophone.devices[0];
                recorder.SetMicrophone(CustomMicrophone.devices[0]);
            }
            else micName = "No Devices";
        }


        [PunRPC]
        public void MoveTo(Vector3 position, PhotonMessageInfo info) { if (!photonView.IsMine) MoveTo(position); }

        [PunRPC]
        public void PlayAnimation(AnimationName animationName, PhotonMessageInfo info)
        {
            //if (agent.enabled == true) if (animationName != AnimationName.Walk) agent.isStopped = true;
            ChangeAnimation(animationName);
        }

        //[PunRPC]
        //public void RPC_GetCoin(bool active)
        //{
        //    Debug.Log("RPC_GetCoin " + this.name + "," + active);
        //    gameObject.GetComponent<AniImage>().vSetColor(Color.white);
        //    gameObject.GetComponent<AniImage>().vSetCutScene(active ? CutInOut.Cutin : CutInOut.Cutout);
        //    float target = active ? 1f : 0f;
        //    gameObject.GetComponent<AniImage>().vSetScale(transform.localScale.x, target, 20f);
        //    gameObject.GetComponent<Collider>().enabled = active;
        //}

        /*[PunRPC]
        public void SitAt(int index, PhotonMessageInfo info)
        {
            //agent.enabled = false;
            Chair chair = GameplaySystem.main.FindChair(index);
            transform.position = chair.sitPoint.transform.position;
            transform.eulerAngles = chair.sitPoint.transform.eulerAngles;
            ChangeAnimation(AnimationName.Sit);
            if (this.chair != null)
            {
                if (this.chair.characterControl != this) this.chair.characterControl.MoveTo(chair.transform.position);
                else
                {
                    this.chair.characterControl = null;
                    this.chair = null;
                }
            }
            chair.characterControl = this;
            this.chair = chair;
        }*/

        /*[PunRPC]
        public void CharacterTelepot(Vector3 position, Vector3 rotation, PhotonMessageInfo info)
        {
            //agent.enabled = false;
            transform.position = position;
            transform.eulerAngles = rotation;
        }
        //PhotoBooth
        [PunRPC]
        public void CheckPlayerInside(int players, PhotonMessageInfo info)
        {
            PhotoboothController.playersInPhotobooth++;
        }
        [PunRPC]
        public void PlayerOutroom(int players, PhotonMessageInfo info)
        {
            PhotoboothController.playersInPhotobooth = 0;
        }*/

        /*[PunRPC]
        public void CharacterStage(Vector3 position, Vector3 rotation, Vector3 moveTo, AnimationName animationName, PhotonMessageInfo info)
        {
            if (photonView.IsMine) return;
            //agent.enabled = false;
            if (Vector3.Distance(transform.position, position) > 1f) transform.position = position;
            if (animationName != AnimationName.Sit)
            {
                //agent.enabled = true;
                //if (Vector3.Distance(transform.position, moveTo) > 1f) agent.SetDestination(moveTo);
            }
            transform.eulerAngles = rotation;
            ChangeAnimation(animationName);
        }*/

        [PunRPC]
        public void CharacterData(string rnickname, string rgender,  int hatIndex, int faceIndex, int glassesIndex, int bodyIndex, int topIndex, int bottomIndex, int footwearIndex, PhotonMessageInfo info)
        {
            if (photonView.IsMine)
            {
                return;
            }

            else 
            {
                nameTMP.text = rnickname;
                nickname = rnickname;
                gender = rgender;
                characterEditor.Load(hatIndex, faceIndex, glassesIndex, bodyIndex, topIndex, bottomIndex, footwearIndex);
            }
        }

        [PunRPC]
        public void ChangeClothSet(int groupIndex, PhotonMessageInfo info)
        {
            //Don't forget to remove comment

            characterEditor.SetGroup(groupIndex);
            characterEditor.Show();            

            //End Here
        }

        [PunRPC]
        public void Request(int fromViewID, Command command, int data, PhotonMessageInfo info)
        {
            if (!photonView.IsMine) return;
            print(fromViewID);

            print(command);
            print(data);
           // GameplaySystem.main.RequestReceive(fromViewID, command, data);
        }
        public enum Command { None, Clapping, Right, Pointing, Waving, Sit };

        /*[PunRPC]
        public void CharacterTelepotToMap(string map, int data, PhotonMessageInfo info)
        {
            if (!photonView.IsMine) return;
            Network.NetworkManager.main.Join(map, data);
        }*/

        public override void OnJoinedRoom()
        {
            if (!photonView.IsMine) return;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (!photonView.IsMine) return;

            //if (chair != null) photonView.RPC("SitAt", RpcTarget.All, GameplaySystem.main.FindChairIndex(chair));
            //else photonView.RPC("CharacterStage", RpcTarget.Others, transform.position, transform.eulerAngles, animationNow);
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            Player player = info.photonView.Controller;
            if (stream.IsWriting)
            {
                //Debug.Log("1 OnPhotonSerializeView = " + player.UserId+","+ player.IsLocal+","+player.IsMasterClient+","+player.NickName);
                //We own this player: send the others our data
                //stream.SendNext(transform.position);
                //stream.SendNext(transform.rotation);

                stream.SendNext(characterEditor.GetHatIndex());
                stream.SendNext(characterEditor.GetFaceIndex());
                stream.SendNext(characterEditor.GetGlassesIndex());
                stream.SendNext(characterEditor.GetBodyIndex());
                stream.SendNext(characterEditor.GetTopIndex());
                stream.SendNext(characterEditor.GetBottomIndex());
                stream.SendNext(characterEditor.GetFootwearIndex());
                stream.SendNext(CoinManager.s_UpdateArray ? 1 : 0);
                stream.SendNext(CoinManager.s_IntArray[0]);
                stream.SendNext(CoinManager.s_IntArray[1]);
                if (CoinManager.s_UpdateArray)
                {
                    CoinManager.s_UpdateArray = false;
                }
            }
            else
            {
                //Debug.Log("2 OnPhotonSerializeView = " + player.UserId + "," + player.IsLocal + "," + player.IsMasterClient + "," + player.NickName);
                //Network player, receive data
                //transform.position = (Vector3)stream.ReceiveNext();
                //transform.rotation = (Quaternion)stream.ReceiveNext();

                characterEditor.hatIndex = (int)stream.ReceiveNext();
                characterEditor.faceIndex = (int)stream.ReceiveNext();
                characterEditor.glassesIndex = (int)stream.ReceiveNext();
                characterEditor.bodyIndex = (int)stream.ReceiveNext();
                characterEditor.topIndex = (int)stream.ReceiveNext();
                characterEditor.bottomIndex = (int)stream.ReceiveNext();
                characterEditor.footwearIndex = (int)stream.ReceiveNext();

                characterEditor.CheckAndShow();

                bool update = (int)stream.ReceiveNext() == 1;
                int num1 = (int)stream.ReceiveNext();
                int num2 = (int)stream.ReceiveNext();

                /*if (!ObjCoinManager)
                {
                    ObjCoinManager = FindObjectOfType<CoinManager>();
                    ObjCoinManager.ObjPlayer = gameObject;
                    //update = true;
                }*/

                if (IsEnterScene)
                {
                    update = true;
                    IsEnterScene = false;
                }

                if (update)
                {
                    CoinManager.s_IntArray[0] = num1;
                    CoinManager.s_IntArray[1] = num2;
                    //CoinManager.s_UpdateArray = false;

                    if (ObjCoinManager)
                        ObjCoinManager.vRespUpdateCoin();
                }
            }

            //GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = false;

            //Debug.Log("OnPhotonSerializeView");
        }
    }
}



// Character Control V1.2 By VF
// Character Control Plus Sound By ST
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using FrostweepGames.VoicePro;
using FrostweepGames.Plugins.Native;
using UnityEngine.EventSystems;

namespace AD
{
    public class CharacterControl : MonoBehaviourPunCallbacks
    {
        private int fingerID = -1;
        public TextMeshPro nameTMP;
        public Animator animator;
        public NavMeshAgent agent;
        public new PhotonView photonView;
        public CharacterEditor characterEditor;
        //Check Status by Activate Network Protocol
        //public CheckStatus checkStatus;
        public string micName;
        public ToggleMic playerMicToggle;

        public Recorder recorder;
        public Listener listener;

        private new Camera camera;
        private AnimationName animationNow = AnimationName.Idle;
        private int animationCount;
        private Chair chair;
        

        public enum AnimationName { Idle, Walk, Excited, GiveItem, Hug, Sit, Marry }

        void Awake()
        {
#if !UNITY_EDITOR
     fingerID = 0; 
#endif
            if (!PhotonNetwork.InRoom) { this.enabled = false; return; }
            camera = Camera.main;
            if (camera == null) camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void RefreshMicrophonesButtonOnClickHandler()
        {
            recorder.RefreshMicrophones();
            if (CustomMicrophone.HasConnectedMicrophoneDevices())
            {
                micName = CustomMicrophone.devices[0];
                recorder.SetMicrophone(CustomMicrophone.devices[0]);
            }
            else micName = "No Devices";
        }

        private void Start()
        {
            if (photonView.IsMine)
            {
                recorder = GameObject.FindObjectOfType<Recorder>();
                listener = GameObject.FindObjectOfType<Listener>();
                RefreshMicrophonesButtonOnClickHandler();
                recorder.StopRecord();
                camera.GetComponent<CameraControl>().Setup(transform);

                playerMicToggle = GameObject.FindObjectOfType<ToggleMic>();

                //nameTMP.text = "N" + Random.Range(1000, 9999);
                nameTMP.text = PlayerPrefs.GetString("nickname");
                characterEditor.Load();
                photonView.RPC("CharacterData", RpcTarget.OthersBuffered, nameTMP.text, characterEditor.GetHatIndex(), characterEditor.GetFaceIndex(), characterEditor.GetGlassesIndex(), characterEditor.GetTopIndex(), characterEditor.GetBottomIndex(), characterEditor.GetFootwearIndex());
            }
        }

        void Update()
        {
            if (photonView.IsMine) CharactorInput();
            CharactorAnimation();
        }

        private void LateUpdate() => nameTMP.transform.rotation = camera.transform.rotation;

        private void GetMicStage() 
        {
            ToggleMic(playerMicToggle.toggleStage);
        }

        private void CharactorInput()
        {
            if (playerMicToggle != null) GetMicStage();
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                //Mod UI
                if (EventSystem.current.IsPointerOverGameObject(fingerID))
                {
                    return;
                }
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Player")
                    {
                        PhotonView view = hit.transform.GetComponent<PhotonView>();
                        print("View ID: " + view.ViewID);
                    }
                    else if(hit.transform.tag == "Chair")
                    {
                        Chair chair = hit.transform.GetComponent<Chair>();
                        if (Vector3.Distance(transform.position, hit.point) < 1.5f)
                        {
                            if (chair.characterControl == null)
                            {
                                transform.position = chair.sitPoint.position;
                                photonView.RPC("CharacterTelepot", RpcTarget.All, chair.sitPoint.position, chair.sitPoint.eulerAngles);
                                PlaySit();
                            }
                        }
                        else
                        {
                            MoveTo(hit.point);
                            photonView.RPC("MoveTo", RpcTarget.Others, hit.point);
                        }
                    }
                    else
                    {
                        MoveTo(hit.point);
                        photonView.RPC("MoveTo", RpcTarget.Others, hit.point);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                characterEditor.Default();
                characterEditor.Save();
                photonView.RPC("CharacterData", RpcTarget.OthersBuffered, nameTMP.text, characterEditor.GetHatIndex(), characterEditor.GetFaceIndex(), characterEditor.GetGlassesIndex(), characterEditor.GetTopIndex(), characterEditor.GetBottomIndex(), characterEditor.GetFootwearIndex());
            }
            if (animationNow != AnimationName.Idle && animationNow != AnimationName.Walk) return;
            if (Input.GetKeyDown(KeyCode.Alpha1)) PlayExcited();
            if (Input.GetKeyDown(KeyCode.Alpha2)) PlaySit();
            if (Input.GetKeyDown(KeyCode.Alpha3)) PlayMarry(); //Play Hug
            if (Input.GetKeyDown(KeyCode.Space)) ToggleMic(true);
            if (Input.GetKeyUp(KeyCode.Space)) ToggleMic(false);
        }

        //RefreshMicrophonesButtonOnClickHandler()
        public void ToggleMic(bool mute)
        {
            if (mute && NetworkRouter.Instance.ReadyToTransmit && micName != "No Devices")
            {
                print("Ready To Call");
                print("Mic Name :" + micName);
                //checkStatus.isRecord.text = "Record With : " + micName;
                recorder.StartRecord();
            }
            else
            {
                RefreshMicrophonesButtonOnClickHandler();
                //checkStatus.isRecord.text = "Mute With : " + micName;
                recorder.StopRecord();
            }

        }
        public void PlayExcited() => photonView.RPC("PlayAnimation", RpcTarget.All, AnimationName.Excited);
        public void PlaySit() => photonView.RPC("PlayAnimation", RpcTarget.All, AnimationName.Sit);

        public void PlayHug() => photonView.RPC("PlayAnimation", RpcTarget.All, AnimationName.Hug);

        public void PlayMarry() => photonView.RPC("PlayAnimation", RpcTarget.All, AnimationName.Marry);
        //public void PlaySitGround() => photonView.RPC("PlayAnimation", RpcTarget.All, AnimationName.Sit, true);

        private void CharactorAnimation()
        {
            //bool isWalk = animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName.Walk.ToString());
            //if (animator.GetCurrentAnimatorStateInfo(0).)
            //print(animator.GetCurrentAnimatorStateInfo(0).length + " | " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (animationNow != AnimationName.Idle && animationNow != AnimationName.Walk) return;
            if (Vector3.Distance(Vector3.zero, agent.velocity) > 0.3f) ChangeAnimation(AnimationName.Walk);
            else ChangeAnimation(AnimationName.Idle);
        }

        private void ChangeAnimation(AnimationName animationName)
        {
            if (animationNow == animationName) return;
            animationNow = animationName;
            animationCount++;
            switch (animationNow)
            {
                case AnimationName.Walk: animator.CrossFadeInFixedTime(animationNow.ToString(), 0.3f); break;
                case AnimationName.Sit: animator.CrossFadeInFixedTime(animationNow.ToString(), 0.4f); break;
                case AnimationName.Excited: animator.CrossFadeInFixedTime(animationNow.ToString(), 0.2f); StartCoroutine(PlayAnimationIdle(animationCount)); break;
                case AnimationName.Hug: animator.CrossFadeInFixedTime(animationNow.ToString(), 0.2f); StartCoroutine(PlayAnimationIdle(animationCount)); break;
                case AnimationName.Marry: animator.CrossFadeInFixedTime(animationNow.ToString(), 0.2f); StartCoroutine(PlayAnimationIdle(animationCount)); break;
                //case AnimationName.SitGround: animator.CrossFadeInFixedTime(animationNow.ToString(), 0.4f); break;
                default: animator.CrossFadeInFixedTime(animationNow.ToString(), 0.2f); break;
            }
        }

        IEnumerator PlayAnimationIdle(int count)
        {
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
            if (count == animationCount) ChangeAnimation(AnimationName.Idle);
        }

        public void MoveTo(Vector3 position)
        {
            if (this.chair != null)
            {
                this.chair.characterControl.MoveTo(chair.transform.forward);
                this.chair.characterControl = null;
                this.chair = null;
            }
            agent.enabled = true;
            agent.isStopped = false;
            agent.SetDestination(position);
            ChangeAnimation(AnimationName.Walk);
        }
        [PunRPC]
        public void MoveTo(Vector3 position, PhotonMessageInfo info) { if (!photonView.IsMine) MoveTo(position); }

        [PunRPC]
        public void PlayAnimation(AnimationName animationName, PhotonMessageInfo info)
        {
            if (agent.enabled == true) if (animationName != AnimationName.Walk) agent.isStopped = true;
            ChangeAnimation(animationName);
        }

        [PunRPC]
        public void SitAt(Chair chair, PhotonMessageInfo info)
        {
            agent.enabled = false;
            transform.position = chair.transform.position;
            transform.eulerAngles = chair.transform.eulerAngles;
            ChangeAnimation(AnimationName.Sit);
            if (this.chair != null)
            {
                this.chair.characterControl.MoveTo(chair.transform.forward);
                this.chair.characterControl = null;
                this.chair = null;
            }
            chair.characterControl = this;
            this.chair = chair;
        }

        [PunRPC]
        public void CharacterTelepot(Vector3 position, Vector3 rotation, PhotonMessageInfo info)
        {
            agent.enabled = false;
            transform.position = position;
            transform.eulerAngles = rotation;
        }

        [PunRPC]
        public void CharacterStage(Vector3 position, Vector3 rotation, Vector3 moveTo, AnimationName animationName, PhotonMessageInfo info)
        {
            if (photonView.IsMine) return;
            agent.enabled = false;
            if (Vector3.Distance(transform.position, position) > 1f) transform.position = position;
            if (animationName != AnimationName.Sit)
            {
                agent.enabled = true;
                if (Vector3.Distance(transform.position, moveTo) > 1f) agent.SetDestination(moveTo);
            }
            transform.eulerAngles = rotation;
            ChangeAnimation(animationName);
        }

        [PunRPC]
        public void CharacterData(string name, int hatIndex, int faceIndex, int glassesIndex, int topIndex, int bottomIndex, int footwearIndex, PhotonMessageInfo info)
        {
            if (photonView.IsMine) return;
            nameTMP.text = name;
            characterEditor.Load(hatIndex, faceIndex, glassesIndex, topIndex, bottomIndex, footwearIndex);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (!photonView.IsMine) return;
            photonView.RPC("CharacterStage", RpcTarget.Others, transform.position, transform.eulerAngles, agent.destination, animationNow);
        }
    }
}*/

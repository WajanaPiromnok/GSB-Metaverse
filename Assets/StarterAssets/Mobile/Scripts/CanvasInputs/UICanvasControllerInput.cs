using Photon.Pun;
using UnityEngine;
using AD;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        public static UICanvasControllerInput main;

        [SerializeField] private new CharacterControl _charControl;

        [SerializeField] WebMobileDetector _webMobile;

        private void Awake()
        {

        }

    

        private void Start()
        {
            _webMobile = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<WebMobileDetector>();

            if (_webMobile.isMobile == false)
            {
                this.gameObject.SetActive(false);
            }

            if (_webMobile.isMobile == true)
            {
                this.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (_charControl == null)
            {
                _charControl = FindLocalCharacterControl();
            }

            else
            {
                if (_charControl.photonView.IsMine == true)
                {
                    if (starterAssetsInputs == null)
                    {
                        FindAndSetLocalPlayerInput();
                    }
                }
            }
        }

        CharacterControl FindLocalCharacterControl()
        {
            CharacterControl[] allControls = GameObject.FindObjectsOfType<CharacterControl>();
            foreach (var control in allControls)
            {
                if (control.photonView.IsMine)
                {
                    return control;
                }
            }
            return null;
        }

        void FindAndSetLocalPlayerInput()
        {
            StarterAssetsInputs[] allInputs = GameObject.FindObjectsOfType<StarterAssetsInputs>();
            foreach (var input in allInputs)
            {
                if (input.GetComponent<PhotonView>().IsMine)
                {
                    starterAssetsInputs = input;
                    break;
                }
            }
        }

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }
        
    }

}

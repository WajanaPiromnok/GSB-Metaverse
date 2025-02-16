/*
The PlayerInput component has an auto-switch control scheme action that allows automatic changing of connected devices.
IE: Switching from Keyboard to Gamepad in-game.
When built to a mobile phone; in most cases, there is no concept of switching connected devices as controls are typically driven through what is on the device's hardware (Screen, Tilt, etc)
In Input System 1.0.2, if the PlayerInput component has Auto Switch enabled, it will search the mobile device for connected devices; which is very costly and results in bad performance.
This is fixed in Input System 1.1.
For the time-being; this script will disable a PlayerInput's auto switch control schemes; when project is built to mobile.
*/

using AD;
using Photon.Pun;
using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;


public class MobileDisableAutoSwitchControls : MonoBehaviour
{
   
    [Header("Target")]
    public PlayerInput playerInput;

    [SerializeField] private new CharacterControl _charControl;

    public static MobileDisableAutoSwitchControls main;

    [SerializeField] WebMobileDetector _webMobile;

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

    void DisableAutoSwitchControls()
    {
        playerInput.neverAutoSwitchControlSchemes = true;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        DisableAutoSwitchControls();
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
                if (playerInput == null)
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
        PlayerInput[] allInputs = GameObject.FindObjectsOfType<PlayerInput>();
        foreach (var input in allInputs)
        {
            if (input.GetComponent<PhotonView>().IsMine)
            {
                playerInput = input;
                StartCoroutine(wait());  // Now start the coroutine to disable auto switch controls
                break;
            }
        }
    }
}
#endif

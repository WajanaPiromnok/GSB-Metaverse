using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CloseThirdPersonOnScene : MonoBehaviour
{
    public ThirdPersonController thirdMan, thirdWoman;
    public StarterAssetsInputs starterMan, starterWoman;
    public CursorController cursorMan, cursorWoman;
    public PlayerInput playerMan, playerWoman;
    public FallDetection fallMan, fallWoman;
    // Start is called before the first frame update
    void Start()
    {
        thirdMan.enabled = false;
        thirdWoman.enabled = false;
        starterMan.enabled = false;
        starterWoman.enabled = false;
        cursorMan.enabled = false;
        cursorWoman.enabled = false;
        playerMan.enabled = false;
        playerWoman.enabled = false;
        fallMan.enabled = false;
        fallWoman.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        thirdMan.enabled = false;
        thirdWoman.enabled = false;
        starterMan.enabled = false;
        starterWoman.enabled = false;
        cursorMan.enabled = false;
        cursorWoman.enabled = false;
        playerMan.enabled = false;
        playerWoman.enabled = false;
        fallMan.enabled = false;
        fallWoman.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CloseThirdPersonOnSceneDress : MonoBehaviour
{
    public ThirdPersonController third;
    public StarterAssetsInputs starter;
    public CursorController cursor;
    public PlayerInput player;
    public FallDetection fall;

    // Start is called before the first frame update
    void Start()
    {
        third.enabled = false;
        starter.enabled = false;
        cursor.enabled = false;
        player.enabled = false;
        fall.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        third.enabled = false;
        starter.enabled = false;
        cursor.enabled = false;
        player.enabled = false;
        fall.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

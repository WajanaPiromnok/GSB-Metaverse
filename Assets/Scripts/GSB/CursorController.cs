using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class CursorController : MonoBehaviour
{
    [SerializeField] private bool _escCheck = false;
    public ThirdPersonController thirdPerson;
    public CharacterController character;
    public StarterAssetsInputs starter;
    public FirstPersonController firstPerson;

    //StarterAssets.StarterAssetsInputs starterInput;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        starter.cursorLocked = false;
        starter.cursorInputForLook = false;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;

        if (Input.GetMouseButtonDown(1))
        {
            starter.cursorLocked = true;
            starter.cursorInputForLook = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            starter.cursorLocked = false;
            starter.cursorInputForLook = false;
            starter.look = new Vector2(0, 0);
        }
    }

    void MouseVisible()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        thirdPerson.enabled = false;
        character.enabled = false;
        starter.enabled = false;
        firstPerson.enabled = false;
    }

    void MouseInvisible()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        thirdPerson.enabled = true;
        character.enabled = true;
        starter.enabled = true;
        firstPerson.enabled = true;
    }
}

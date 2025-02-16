using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public AD.CharacterControl characterControl;
    public Transform sitPoint;
    public Vector3 VeLastPos;

    public void vSetChar(AD.CharacterControl addChar)
    {
        characterControl = addChar;
        VeLastPos = characterControl.transform.position;
        characterControl.transform.position = transform.position;
        characterControl.transform.eulerAngles = transform.eulerAngles;
    }

    public void vRemoveChar()
    {
        characterControl.transform.position = VeLastPos;
        characterControl = null;
    }
}

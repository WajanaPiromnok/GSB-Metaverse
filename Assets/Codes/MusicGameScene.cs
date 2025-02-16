using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGameScene : MonoBehaviour
{
    public AudioSource walkBG;
    public AudioClip jumpBG;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            //Debug.Log(walkBG);
            walkBG.Play();
        }
        
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            walkBG.Pause();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            walkBG.PlayOneShot(jumpBG, 1.0f);
        }
    }
}

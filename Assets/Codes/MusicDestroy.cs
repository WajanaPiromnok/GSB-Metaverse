using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicDestroy : MonoBehaviour
{
    public bool isLoad = true;

    private void Update()
    {
        //if(SceneManager.LoadScene("Loading"))
    }
    public void DestroyThisMusic()
    {
        Destroy(GameObject.Find("MusicDress"));
    }
}

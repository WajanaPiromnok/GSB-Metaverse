using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PauseGame : MonoBehaviour
{
    [SerializeField] bool isGamePause = false;

    [SerializeField] GameObject VideoWall;
    [SerializeField] GameObject VideoNFT1;

    // Start is called before the first frame update
    void Start()
    {
        VideoWall = GameObject.Find("VideoWall");
        VideoNFT1 = GameObject.Find("VideoNFT-1");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePause = !isGamePause;
        }

        if (isGamePause)
        {
            Time.timeScale = 0f;
            VideoWall.GetComponent<VideoPlayer>().Pause();
            VideoNFT1.GetComponent<VideoPlayer>().Pause();
        }
        else
        {
            Time.timeScale = 1f;
            VideoWall.GetComponent<VideoPlayer>().Play();
            VideoNFT1.GetComponent<VideoPlayer>().Play();
        }

    }

    /*void PausePlay()
    {

    }*/
}

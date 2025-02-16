using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
public class VideoController : MonoBehaviour
{
    public string videoURL;

    public VideoPlayer videoPlayer;

    //Audio
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private RenderTexture texture;
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());

        videoPlayer.url = Path.Combine(Application.streamingAssetsPath , videoURL);
    }

    IEnumerator playVideo()
    {
        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = true;
        audioSource.playOnAwake = true;

        //Set video To Play then prepare Audio to prevent Buffering
        //videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        WaitForSeconds waitTime = new WaitForSeconds(5);
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            //Prepare/Wait for 5 sceonds only
            yield return waitTime;
            //Break out of the while loop after 5 seconds wait
            break;
        }

        Debug.Log("Done Preparing Video");

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            //Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        Debug.Log("Done Playing Video");
    }
}

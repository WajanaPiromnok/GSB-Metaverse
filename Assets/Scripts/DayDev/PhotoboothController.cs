using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PhotoboothController : MonoBehaviour
{
    public Image BG;
    private GameObject player;
    [SerializeField] private GameObject[] _camera;
    [SerializeField] public Transform[] pos;
    [SerializeField] public Sprite[] bg_image;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject allPanel;
    private int currentBG = 0;
    public static int playersInPhotobooth = 0;


    public static WebGLUpload _webGLUpload = null;
    public static WebGLDownload _webGLDownload = null;
    private void Awake()
    {
        _webGLUpload = GetComponent<WebGLUpload>();
        _webGLDownload = GetComponent<WebGLDownload>();
    }
    private void Update()
    {
        /*if (playersInPhotobooth == 1)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
        if (playersInPhotobooth == 0)
        {
            gameObject.GetComponent<Collider>().enabled = true;
        }*/
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("work");
            player = other.gameObject;
            panel.SetActive(true);
            _camera[0].SetActive(false);
            _camera[1].SetActive(true);
            other.GetComponent<NavMeshAgent>().enabled = false;
            //AD.GameplaySystem.main.WarpToPhotobooth();
        }
    }
    public void Clicked_PreBG()
    {
        currentBG -= 1;
        if (currentBG < 0) { currentBG = 0; }
        BG.sprite = bg_image[currentBG];
    }
    public void Clicked_NextBG()
    {
        currentBG += 1;
        if (currentBG >= bg_image.Length) { currentBG = bg_image.Length - 1; }
        BG.sprite = bg_image[currentBG];
    }
    public void Clicked_closePhoto()
    {
        panel.SetActive(false);
        _camera[1].SetActive(false);
        _camera[0].SetActive(true);
        player.GetComponent<NavMeshAgent>().enabled = true;
    }
    public void Clicked_screenShot()
    {
        allPanel.SetActive(false);
        _webGLDownload.GetScreenshot(WebGLDownload.ImageFormat.jpg, 1, "");
        StartCoroutine(waitsec());
    }
    public void OpenAR()
    {
        Application.OpenURL("https://www.facebook.com/fbcameraeffects/tryit/443925063944157/");
    }
    IEnumerator waitsec()
    {
        yield return new WaitForSeconds(2.0f);
        allPanel.SetActive(true);
    }
}

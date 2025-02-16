using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TriggerSound : MonoBehaviour
{
    [SerializeField] private float fadeTime = 2f;

    public AudioSource m_MyAudioSource;

    public AudioSource soundBG;
    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView phView = other.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            return;
        }
        else 
        {
            if (other.tag == "Player")
            {
                StartCoroutine(AudioHelper.FadeIn(m_MyAudioSource, fadeTime));
                StartCoroutine(AudioHelper.FadeOut(soundBG, fadeTime));
            }
        }

    }

    /*private void OnTriggerStay(Collider other)
    {
        PhotonView phView = other.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            return;
        }
        else
        {
            if (other.tag == "Player")
            {
                StartCoroutine(AudioHelper.FadeIn(m_MyAudioSource, fadeTime));
                StartCoroutine(AudioHelper.FadeOut(soundBG, fadeTime));
            }
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        PhotonView phView = other.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            return;
        }
        else
        {
            if (other.tag == "Player")
            {
                StartCoroutine(AudioHelper.FadeOut(m_MyAudioSource, fadeTime));
                StartCoroutine(AudioHelper.FadeIn(soundBG, fadeTime));
                //m_MyAudioSource.volume -= maxVolume * Time.deltaTime;
                //soundBG.volume += maxVolume * Time.deltaTime;
            }
        }
    }
}

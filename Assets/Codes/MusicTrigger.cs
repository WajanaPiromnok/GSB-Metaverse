using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource[] musicAudios;
    private AudioSource maintheme;
    private AudioSource changesong;
    // Start is called before the first frame update
    void Start()
    {
        musicAudios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource song in musicAudios) 
        {
            if (song.name == "SongA") maintheme = song;
        }
        changesong = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            StartCoroutine(SoundControl.FadeIn(changesong, 5f));
            StartCoroutine(SoundControl.FadeOut(maintheme, 5f));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") 
        {
            StartCoroutine(SoundControl.FadeIn(maintheme, 5f));
            StartCoroutine(SoundControl.FadeOut(changesong, 5f));
        }
    }
}

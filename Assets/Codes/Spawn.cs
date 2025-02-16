using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using AD;
using AD.Network;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    public float min = -5, max = 5;

    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            if (SceneManager.GetActiveScene().name == "AomsinLand(DevelopScene)-Edit")
            {
                Vector3 position = NetworkManager.main.spawnPoint;
                if (position == Vector3.zero) position = transform.position + new Vector3(Random.Range(min, max), 0.0f, Random.Range(min, max));
                GameObject go;
                if (PlayerPrefs.GetString("nickname") == "Dev GSB")
                {
                    go = PhotonNetwork.Instantiate("PlayerDev", position, Quaternion.identity);
                }
                else
                {
                    if (PlayerPrefs.GetString("gender") == "1")
                    {
                        go = PhotonNetwork.Instantiate("PlayerMP", position, Quaternion.identity);
                        go.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        go = PhotonNetwork.Instantiate("PlayerFP", position, Quaternion.identity);
                        go.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                GameplaySystem.main.characterControl = go.GetComponent<CharacterControl>();

                //LookAtCamera.lookat.transforms = go.transform;
                //LookAtCamera2.lookat2.transforms = go.transform;
            }

            if (SceneManager.GetActiveScene().name == "MoneyExpo2024")
            {
                Vector3 position = NetworkManager.main.spawnPoint;
                if (position == Vector3.zero) position = transform.position + new Vector3(Random.Range(min, max), 0.0f, Random.Range(min, max));
                GameObject go;
                if (PlayerPrefs.GetString("nickname") == "Dev GSB")
                {
                    go = PhotonNetwork.Instantiate("PlayerDev", position, Quaternion.identity);
                }
                else
                {
                    if (PlayerPrefs.GetString("gender") == "1")
                    {
                        go = PhotonNetwork.Instantiate("PlayerMP", position, Quaternion.Euler(0f, 0f, 0f));
                        go.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        go = PhotonNetwork.Instantiate("PlayerFP", position, Quaternion.Euler(0f, 0f, 0f));
                        go.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                GameplaySystem.main.characterControl = go.GetComponent<CharacterControl>();

                //LookAtCamera.lookat.transforms = go.transform;
                //LookAtCamera2.lookat2.transforms = go.transform;
            }

            if (SceneManager.GetActiveScene().name == "ChristmasScene")
            {
                Vector3 position = NetworkManager.main.spawnPoint;
                if (position == Vector3.zero) position = transform.position + new Vector3(Random.Range(min, max), 0.0f, Random.Range(min, max));
                GameObject go;
                if (PlayerPrefs.GetString("nickname") == "Dev GSB")
                {
                    go = PhotonNetwork.Instantiate("PlayerDev", position, Quaternion.identity);
                }
                else
                {
                    if (PlayerPrefs.GetString("gender") == "1")
                    {
                        go = PhotonNetwork.Instantiate("PlayerMP", position, Quaternion.Euler(0f, 0f, 0f));
                        go.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        go = PhotonNetwork.Instantiate("PlayerFP", position, Quaternion.Euler(0f, 0f, 0f));
                        go.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                GameplaySystem.main.characterControl = go.GetComponent<CharacterControl>();

                //LookAtCamera.lookat.transforms = go.transform;
                //LookAtCamera2.lookat2.transforms = go.transform;
            }
        }
    }

}

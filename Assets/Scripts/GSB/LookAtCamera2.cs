using System.Collections;
using System.Collections.Generic;
using AD;
using Photon.Pun;
using UnityEngine;

public class LookAtCamera2 : MonoBehaviour
{
    public static LookAtCamera lookat;

    public Transform transforms;
    public bool isCamera;

    public GameplaySystem gameplaySystem;

    public Quaternion keeptransform;

    public void Start()
    {
        gameplaySystem = FindObjectOfType<GameplaySystem>();
    }


    public void OnTriggerEnter(Collider enter)
    {
        gameplaySystem.phView = enter.gameObject.GetComponent<PhotonView>();
        if (!gameplaySystem.phView)
        {
            return;
        }

        else
        {
            if (enter.gameObject.tag == "Player")
            {
                Debug.Log("Lookat");
            }
        }
    }

    public void OnTriggerExit(Collider exit)
    {
        gameplaySystem.phView = exit.gameObject.GetComponent<PhotonView>();
        if (!gameplaySystem.phView)
        {
            return;
        }

        else
        {
            if (exit.gameObject.tag == "Player")
            {
                Debug.Log("false");
            }
        }
    }

    void Update()
    {
        if (isCamera)
        {
            Vector3 target_point = Camera.main.transform.position;
            target_point.y = 0;
            transform.LookAt(target_point);
        }
        else
        {
            if (transforms != null)
            {
                Vector3 target_point = transforms.transform.position;
                target_point.y = transform.position.y;
                transform.LookAt(target_point);
            }
            else
            {
                if (gameplaySystem.characterControl)
                    transforms = gameplaySystem.characterControl.transform;
                //transform.localRotation = Quaternion.Lerp(transform.localRotation, keeptransform, Time.deltaTime * 10);
            }
        }
    }
}

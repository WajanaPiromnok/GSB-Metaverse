using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FallDetection : MonoBehaviour
{
    public GameObject go;

    //public Vector3 fallVector;
    // Start is called before the first frame update
    void Start()
    {
        CheckPointFall.checkpoint = new Vector3(0f, 0f, 0f);

        //CheckPointFall.checkpoint = fallVector;
        go = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PhotonView phView = gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }

        //Debug.Log(CheckPointFall.checkpoint);

        if (go.transform.position.y <= -3)
        {
            //Debug.Log("fall");
            go.transform.position = CheckPointFall.checkpoint;
        }

        /*if (go.transform.position.y == -22)
        {
            go.transform.position = CheckPointFall.checkpoint;
        }*/
    }

    private void LateUpdate()
    {
        PhotonView phView = gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }

        //Debug.Log(CheckPointFall.checkpoint);

        if (go.transform.position.y <= -3.5)
        {
            //Debug.Log("fall");
            go.transform.position = CheckPointFall.checkpoint;
        }
    }

    private void FixedUpdate()
    {
        PhotonView phView = gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }

        //Debug.Log(CheckPointFall.checkpoint);

        if (go.transform.position.y <= -3.7)
        {
            //Debug.Log("fall");
            go.transform.position = CheckPointFall.checkpoint;
        }
    }
}

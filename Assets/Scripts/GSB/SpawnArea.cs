using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnArea : MonoBehaviour
{
    //public Vector3 checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        //checkpoint = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*PhotonView phView = gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }

        if (transform.position.y < -3)
        {
            transform.position = checkpoint;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView phView = other.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }
        
        Debug.Log("Hit");

    }

    void CreateController()
    {
        PhotonView photonV = GetComponent<PhotonView>();

        //PhotonNetwork.Instantiate("PlayerMP", spawn, Quaternion.identity, 0, new object[] { photonV.ViewID });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PhotonView phView = other.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }

        Debug.Log("Hit");
        CheckPointFall.checkpoint = transform.position;
    }
}

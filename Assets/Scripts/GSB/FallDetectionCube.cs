using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FallDetectionCube : MonoBehaviour
{
    [SerializeField] private GameObject _checkFall;
    // Start is called before the first frame update
    void Start()
    {
        CheckPointFall.checkpoint = new Vector3(0f, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView phView = other.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }

        if (other.tag == "Player")
        {
            _checkFall = GameObject.FindGameObjectWithTag("Player");

            if (_checkFall != null)
            {
                _checkFall.transform.position = CheckPointFall.checkpoint;
                Debug.Log("Hit");
            }            
            //transform.position = CheckPointFall.checkpoint;
        }
    }
}

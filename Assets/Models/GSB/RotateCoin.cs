using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateCoin : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotation;

    [SerializeField] 
    private float _speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotation * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView phView = other.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }


        /*if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }*/
    }
}

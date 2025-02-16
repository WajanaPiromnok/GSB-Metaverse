using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlatformMove : MonoBehaviour
{
    public GameObject platform;
    [SerializeField] private GameObject _stopController;

    Vector3 m_LastPos;
    Vector3 m_AddPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("1 = " + transform.position + "," + m_LastPos);
        /*Vector3 nowPos = transform.position;
        m_AddPos = new Vector3(nowPos.x - m_LastPos.x, nowPos.y - m_LastPos.y, nowPos.z - m_LastPos.z);
        m_LastPos = transform.position;
        if(m_AddPos != Vector3.zero)
            Debug.Log("2 = "+ (double)m_AddPos.z+","+ m_LastPos+","+ nowPos);*/
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
            Debug.Log("Hi");
            phView.transform.parent = platform.transform;
            
            _stopController = GameObject.FindGameObjectWithTag("Player");

            //_stopController.GetComponent<CharacterController>().enabled = false;

            //_stopController.GetComponent<ThirdPersonController>().enabled = false;
            //_stopController.GetComponent<CharacterController>().enabled = false;
            //_stopController.GetComponent<Animator>().Play("Idle");
            //_stopController.GetComponent<FallDetection>().enabled = false;
            //m_LastPos = other.transform.localPosition;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        PhotonView phView = other.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }

        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetMouseButtonDown(1))
            {
                //phView.transform.parent = null;
                /*_stopController.GetComponent<ThirdPersonController>().enabled = true;
                _stopController.GetComponent<Animator>().Play("Idle Walk Run Blend");
                _stopController.GetComponent<FallDetection>().enabled = true;*/

                //_stopController.GetComponent<CharacterController>().enabled = true;
            }
            //Vector3 pos = other.transform.position;
           // other.transform.localPosition = m_LastPos;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PhotonView phView = other.gameObject.GetComponent<PhotonView>();

        if (!phView.IsMine)
        {
            // Not our local player - ignore
            return;
        }

        if (other.tag == "Player")
        {
            phView.transform.parent = null;
            //_stopController.GetComponent<CharacterController>().enabled = true;
            /*_stopController.GetComponent<ThirdPersonController>().enabled = true;
            _stopController.GetComponent<Animator>().Play("Idle Walk Run Blend");
            _stopController.GetComponent<FallDetection>().enabled = true;*/
        }
    }
}

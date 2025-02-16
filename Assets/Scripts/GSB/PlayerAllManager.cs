using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAllManager : MonoBehaviour
{
    public const int MAX_PLAYER = 50;
    public const int MAX_PLAYER_MOBILE = 3;

    [SerializeField]
    List<GameObject> m_Player;

    [SerializeField]
    float[] distance;

    [SerializeField]
    GameObject objMine;

    [SerializeField] WebMobileDetector _webMobile;

    private void Awake()
    {
        m_Player = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _webMobile = GameObject.FindGameObjectWithTag("CheckMobile").GetComponent<WebMobileDetector>();
    }

// Update is called once per frame
void Update()
    {
        vCheckPlayerNull();
        vCheckDistance();
    }

    public void vCheckPlayerNull()
    {
        bool finish = false;
        while (!finish)
        {
            finish = true;
            for (int i = 0; i < m_Player.Count; i++)
            {
                if (m_Player[i] == null)
                {
                    m_Player.RemoveAt(i);
                    finish = false;
                    break;
                }
            }
        }
    }

    public void vCheckDistance()
    {
        objMine = null;
        for (int i = 0; i < m_Player.Count; i++)
        {
            if (m_Player[i].GetComponent<AD.CharacterControl>().photonView.IsMine)
            {
                objMine = m_Player[i];
                break;
            }
        }

        if (!objMine)
            return;

        distance = new float[m_Player.Count];

        for (int i = 0; i < m_Player.Count; i++)
        {
            if (m_Player[i] == objMine)
                continue;

            distance[i] = Vector3.Distance(m_Player[i].transform.position, objMine.transform.position);

            if (_webMobile.isMobile == false)
            {
                if (distance[i] <= 15.0f)
                {
                    m_Player[i].GetComponent<AD.CharacterControl>().nameTMP.gameObject.SetActive(true);
                    m_Player[i].GetComponent<AD.CharacterControl>().ObjPlayerArmature.SetActive(true);

                    if (m_Player.Count >= 15)
                    {
                        m_Player[i].GetComponent<AD.CharacterControl>().nameTMP.gameObject.SetActive(false);
                        m_Player[i].GetComponent<AD.CharacterControl>().ObjPlayerArmature.SetActive(false);
                    }
                }

                if (distance[i] > 15.0f)
                {
                    m_Player[i].GetComponent<AD.CharacterControl>().nameTMP.gameObject.SetActive(false);
                    m_Player[i].GetComponent<AD.CharacterControl>().ObjPlayerArmature.SetActive(false);
                }
            }

            if (_webMobile.isMobile == true)
            {
                if (distance[i] <= 5.0f)
                {
                    m_Player[i].GetComponent<AD.CharacterControl>().nameTMP.gameObject.SetActive(true);
                    m_Player[i].GetComponent<AD.CharacterControl>().ObjPlayerArmature.SetActive(true);

                    if (m_Player.Count >= 3)
                    {
                        m_Player[i].GetComponent<AD.CharacterControl>().nameTMP.gameObject.SetActive(false);
                        m_Player[i].GetComponent<AD.CharacterControl>().ObjPlayerArmature.SetActive(false);
                    }
                }

                if (distance[i] > 5.0f)
                {
                    m_Player[i].GetComponent<AD.CharacterControl>().nameTMP.gameObject.SetActive(false);
                    m_Player[i].GetComponent<AD.CharacterControl>().ObjPlayerArmature.SetActive(false);
                }
            }

        }

        for (int i = 0; i < m_Player.Count; i++)
        {
            if (m_Player[i] == objMine)
                continue;

            int count = 0;

            for (int j = 0; j < m_Player.Count; j++)
            {
                if (m_Player[j] == objMine || i == j)
                    continue;

                if (distance[i] > distance[j])
                    count++;
            }

            /*Debug.Log("player = " + count);

            if (isMobile == false)
            {
                m_Player[i].GetComponent<AD.CharacterControl>().nameTMP.gameObject.SetActive(count < MAX_PLAYER);
                m_Player[i].GetComponent<AD.CharacterControl>().ObjPlayerArmature.SetActive(count < MAX_PLAYER);
            }

            if (isMobile == true)
            {
                m_Player[i].GetComponent<AD.CharacterControl>().nameTMP.gameObject.SetActive(count < MAX_PLAYER_MOBILE);
                m_Player[i].GetComponent<AD.CharacterControl>().ObjPlayerArmature.SetActive(count < MAX_PLAYER_MOBILE);
            }*/
        }
    }

    public void vAddPlayer(GameObject player)
    {
        m_Player.Add(player);
    }

    public void vRemovePlayer(GameObject player)
    {
        m_Player.Remove(player);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMoveFloor : MonoBehaviour
{
    public int SumTimeMove = 20;

    int m_NumSecond;

    // Start is called before the first frame update
    void Start()
    {
        m_NumSecond = -1;
        GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_NumSecond != System.DateTime.Now.Second)
        {
            if (System.DateTime.Now.Second % SumTimeMove == 0)
            {
                if(!GetComponent<Animator>().enabled)
                    GetComponent<Animator>().enabled = true;

                GetComponent<Animator>().Play("Base Layer.MoveFloor", 0, 0f);
                //Debug.Log(" = " + System.DateTime.Now.Second);
            }
        }

        m_NumSecond = System.DateTime.Now.Second;
    }
}

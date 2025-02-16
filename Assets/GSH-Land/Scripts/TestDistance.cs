using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using AD;
public class TestDistance : MonoBehaviourPunCallbacks
{
    public int num;  
    public GameObject[] target;
    public LineRenderer lineRenderer;
    public Color colorPlayer;
    public Color colorOther;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectsWithTag("Activities");

        if (photonView.IsMine)
        {
            spriteRenderer.color = colorPlayer;
            DrawLine();
       
          
        }
        else
        {
            spriteRenderer.color = colorOther;
        }
    }
  
    private void DrawLine()
    {
        // set width of the renderer
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // set the position
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target[num].transform.position);

        lineRenderer.useWorldSpace = true;
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMap : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
            Debug.Log("FindTarget");
        }
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }
}

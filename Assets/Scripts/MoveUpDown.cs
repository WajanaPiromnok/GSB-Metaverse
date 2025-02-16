using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField] float altitude;
    [SerializeField] float freq;

    [SerializeField] Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(pos.x, Mathf.Sin(Time.time * freq) * altitude + pos.y, pos.z);
    }
}

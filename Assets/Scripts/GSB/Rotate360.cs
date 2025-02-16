using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Rotate360 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public List<Sprite> sprites;
    public Image img;
    public float fps;

    public int index = 0;

    void Start()
    {

    }

    void Update()
    {
        
    }

    void ShowFrame(int index)
    {
        img.sprite = sprites[index];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetAxis("Mouse X") > 0)
        {
            if (index == 0) index = sprites.Count - 1;
            ShowFrame(index);
            index--;
        }
        else if (Input.GetAxis("Mouse X") < 0)
        {
            if (index >= sprites.Count) index = 0;
            ShowFrame(index);
            index++;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //ShowFrame(0);
    }
}

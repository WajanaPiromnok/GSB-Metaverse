using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDetail : MonoBehaviour
{
    public string shop_code;
    GameObject canvas;
  
    void Start()
    {
        if(shop_code == ""){
            shop_code = "streeshop";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        //Debug.Log("Click at:"+this.gameObject.name);
        /*canvas =  GameObject.Find("Canvas");
        var obj = (GameObject)Instantiate(Resources.Load("ui/"+shop_code, typeof(GameObject)));
        obj.transform.parent = canvas.transform;*/
        Instantiate(Resources.Load("ui/"+shop_code, typeof(GameObject)));
    }
}

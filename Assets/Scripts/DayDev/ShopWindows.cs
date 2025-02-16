using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindows : MonoBehaviour
{
    public string payment_url = "https://link.omise.co/63F5C35F";
    public void CloseWindows(){
        Destroy(this.gameObject);
    }

    public void CheckOut(){
        Application.OpenURL(payment_url);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLink : MonoBehaviour
{
    public OpenLinkOnIOS linkOpener;

    public void OpenWebsite()
    {
        linkOpener.OpenLink("https://www.google.com");
    }
}

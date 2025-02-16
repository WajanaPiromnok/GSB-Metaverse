using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckUIZonePhase2 : MonoBehaviour
{
    [Header("UI Popup Controller")]
    [SerializeField] TextMeshProUGUI Headline;
    [Space(10)]
    [SerializeField] string[] HeadlineString;
    [Space(10)]
    [SerializeField] Image[] imageNPCZone;
    [Space(10)]
    [SerializeField] Sprite[] spriteNPCZone;
    [Space(10)]
    [SerializeField] string[] websiteString;
    

    [Space(20)]
    public int checkNPC;

    [Space(10)]
    [Header("Check NPC Phase 2")]
    public CheckNPCPhase2 checkNPCPhase2;

    // Start is called before the first frame update
    void Start()
    {
        checkNPC = checkNPCPhase2.npc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickPopup(int a)
    {
        Headline.text = HeadlineString[a];
        if (checkNPC == 0)
        {
            imageNPCZone[0].gameObject.SetActive(true);
            imageNPCZone[1].gameObject.SetActive(false);
            imageNPCZone[0].sprite = spriteNPCZone[0];
        }
        else
        {
            imageNPCZone[0].gameObject.SetActive(false);
            imageNPCZone[1].gameObject.SetActive(true);
            imageNPCZone[1].sprite = spriteNPCZone[a];
        }

    }

    public void OpenLinkZone()
    {
        Application.OpenURL(websiteString[checkNPC]);
    }
}

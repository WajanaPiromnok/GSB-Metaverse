using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckUITutorial : MonoBehaviour
{
    public TextMeshProUGUI[] zone;
    public string[] stringZone;

    public Image[] zoneDescription;
    public Sprite[] zoneImageDescription;

    public int zoneCount;

    public TextMeshProUGUI zoneNumCount;

    public Button buttonNext;
    //public Button[] buttonBack;

    //public Image[] Background;
    //public Image[] Close;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (zoneCount == 0)
        {
            zone[0].gameObject.SetActive(true);
            zone[1].gameObject.SetActive(true);
            zone[2].gameObject.SetActive(true);

            zone[0].text = stringZone[0];
            zone[1].text = stringZone[1];
            zone[2].text = stringZone[2];

            zoneDescription[0].gameObject.SetActive(true);
            zoneDescription[1].gameObject.SetActive(true);
            zoneDescription[2].gameObject.SetActive(true);

            zoneDescription[0].sprite = zoneImageDescription[0];
            zoneDescription[1].sprite = zoneImageDescription[1];
            zoneDescription[2].sprite = zoneImageDescription[2];

            /*for (int i = 0; i < buttonBack.Length; i++)
            {
                buttonBack[i].gameObject.SetActive(false);
            }*/
            
            zoneNumCount.text = "1 / 3";

            /*Background[0].gameObject.SetActive(true);
            Background[1].gameObject.SetActive(false);*/

            /*Close[0].gameObject.SetActive(true);
            Close[1].gameObject.SetActive(false);*/

        }

        if (zoneCount == 1)
        {
            zone[0].gameObject.SetActive(true);
            zone[1].gameObject.SetActive(true);
            zone[2].gameObject.SetActive(true);

            zone[0].text = stringZone[3];
            zone[1].text = stringZone[4];
            zone[2].text = stringZone[5];

            zoneDescription[0].gameObject.SetActive(true);
            zoneDescription[1].gameObject.SetActive(true);
            zoneDescription[2].gameObject.SetActive(true);

            zoneDescription[0].sprite = zoneImageDescription[3];
            zoneDescription[1].sprite = zoneImageDescription[4];
            zoneDescription[2].sprite = zoneImageDescription[5];

            buttonNext.gameObject.SetActive(true);
            //buttonBack[0].gameObject.SetActive(true);
            //buttonBack[1].gameObject.SetActive(false);

            zoneNumCount.text = "2 / 3";

            /*Background[0].gameObject.SetActive(true);
            Background[1].gameObject.SetActive(false);*/

            /*Close[0].gameObject.SetActive(true);
            Close[1].gameObject.SetActive(false);*/
        }

        if (zoneCount == 2)
        {
            zone[0].gameObject.SetActive(false);
            zone[1].gameObject.SetActive(true);
            zone[2].gameObject.SetActive(false);

            zone[1].text = stringZone[6];

            zoneDescription[0].gameObject.SetActive(false);
            zoneDescription[1].gameObject.SetActive(true);
            zoneDescription[2].gameObject.SetActive(false);

            zoneDescription[1].sprite = zoneImageDescription[6];

            buttonNext.gameObject.SetActive(false);
            //buttonBack[0].gameObject.SetActive(false);
            //buttonBack[1].gameObject.SetActive(true);

            zoneNumCount.text = "3 / 3";

            /*Background[0].gameObject.SetActive(false);
            Background[1].gameObject.SetActive(true);*/

            /*Close[0].gameObject.SetActive(false);
            Close[1].gameObject.SetActive(true);*/
        }
    }

    public void onClickNext()
    {
        zoneCount += 1;

        if (zoneCount > 2)
        {
            zoneCount = 2;           
        }
    }

    public void onClickBack()
    {
        zoneCount -= 1;

        if (zoneCount < 0)
        {
            zoneCount = 0;
        }
    }
}

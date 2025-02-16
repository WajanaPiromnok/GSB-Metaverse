using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountNumberNickname : MonoBehaviour
{
    public static int countValue = 0;
    public TextMeshProUGUI countdown;

    Color lerpedColor = Color.black;

    [SerializeField] [Range(0f, 1f)] float lerpTime;
    // Start is called before the first frame update
    void Start()
    {
        countdown.color = lerpedColor;
    }

    // Update is called once per frame
    void Update()
    {
        countdown.text = countValue + " / 12";

        lerpedColor = Color.Lerp(Color.black, Color.red, lerpTime);
        countdown.color = lerpedColor;
        ChangeColorText();

    }

    void ChangeColorText()
    {
        if (countValue == 12)
        {
            lerpTime = 1f;
        }
        else if (countValue == 10)
        {
            lerpTime = 0.8f;
        }
        else if (countValue <= 8)
        {
            lerpTime = 0f;
        }
    }
}

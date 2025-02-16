using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_CharacterCustom : MonoBehaviour
{
    public TextMeshProUGUI PlayerName;

    [SerializeField] private string str_nickname;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetString("nickname");
        str_nickname = PlayerPrefs.GetString("nickname");

        PlayerName.text = str_nickname;
        //PlayerName.text = input_nickname.text;
        Debug.Log(str_nickname);
        //PlayerPrefs.
    }

    // Update is called once per frame
    void Update()
    {
        PlayerName.text = str_nickname;
    }
}

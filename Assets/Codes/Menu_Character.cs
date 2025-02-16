using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json.Serialization;

public class Menu_Character : MonoBehaviour
{
    public Toggle genders;

    public int gender_value = 1;

    public GameObject window_alert;

    /*public Text player_nickname, player_nicknamebio, player_fullname;*/
    //public InputField player_bio;

    public string /*nickname,*/ gender;

    public TextMeshProUGUI PlayerName;

    [SerializeField] private string str_nickname;

    public TMP_InputField input_nickname;

    public Button btnNext;

    public GameObject go;
    public GameObject goDisable;

    /*    PlayerPrefs.SetString("authen_code", jsonNode["authen_code"]);
                    PlayerPrefs.SetString("email", jsonNode["email"]);
                    PlayerPrefs.SetString("nickname", jsonNode["nickname"]);
                    PlayerPrefs.SetString("gender", jsonNode["gender"]);
                    PlayerPrefs.SetString("fullname", jsonNode["fullname"]);*/


    // Start is called before the first frame update
    void Start()
    {
        /*window_name.SetActive(true);
        window_character.SetActive(false);*/
        genders.onValueChanged.AddListener((value) =>
        {
            HandleGenders(value);
        });
        if (SceneManager.GetActiveScene().name == "ChooseRoom")
        {
            if (PlayerPrefs.GetString("gender") == "1")
            {
                go = GameObject.Find("PlayerMP");
                goDisable = GameObject.Find("PlayerFP");
                goDisable.SetActive(false);
            }

            if (PlayerPrefs.GetString("gender") == "2")
            {
                go = GameObject.Find("PlayerFP");
                goDisable = GameObject.Find("PlayerMP");
                goDisable.SetActive(false);
            }
        }

        Debug.Log("s_StrAuthenCode = "+ GameController.s_StrAuthenCode);
    }

    public void HandleGenders(bool isOn)
    {
        if (isOn) gender_value = 1;
        else gender_value = 2;
    }

    public void RegisterPlayer()
    {
        PlayerPrefs.SetString("gender", gender_value.ToString());
        Debug.Log(PlayerPrefs.GetString("gender"));
        PlayerPrefs.SetString("nickname", input_nickname.text);
        Debug.Log(PlayerPrefs.GetString("nickname"));
        //str_nickname = input_nickname.text;
    }

    public void LoadPlayerData()
    {
        //nickname = PlayerPrefs.GetString("nickname");
        //fullname = PlayerPrefs.GetString("fullname");
        gender = PlayerPrefs.GetString("gender");
        //bio = PlayerPrefs.GetString("bio");
        //player_nickname.text = nickname;
        //player_nicknamebio.text = nickname;
        //player_fullname.text = fullname;
        //player_bio.text = bio;
        //PlayerName.text = input_nickname.text;
    }

    /*public void RegisterName()
    {
        str_nickname = input_nickname.text;
    }*/

    public void SaveBio()
    {
        //PlayerPrefs.SetString("bio", player_bio.text);
    }

    // Update is called once per frame
    void Update()
    {
        str_nickname = PlayerName.text;

        if (PlayerName.text == "Enter your name")
        {
            CountNumberNickname.countValue = 0;
        }

        else
        {
            CountNumberNickname.countValue = str_nickname.Length;
        }
    }

    public void ToggleMenu(bool toggle)
    {
        print(PlayerPrefs.GetString("gender"));

        /*if (toggle)
        {
            window_name.SetActive(false);
            window_character.SetActive(true);
        }
        else 
        {
            window_name.SetActive(true);
            window_character.SetActive(false);
        }*/
    }

    public void CharacterChoose()
    {
        str_nickname = input_nickname.text;
        print(PlayerPrefs.GetString("gender"));
        if (PlayerPrefs.GetString("gender") == "1") SceneManager.LoadScene("Character_Aomsin_M");
        if (PlayerPrefs.GetString("gender") == "2") SceneManager.LoadScene("Character_Aomsin_F");
    }

    public void onSelectedSceneMode(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BackToMain()
    {
        StartCoroutine(LoadSceneObject("Character_Aomsin"));
        Destroy(GameObject.Find("Network"));
    }

    public void ShowAlert()
    {
        window_alert.SetActive(true);
    }

    public IEnumerator LoadSceneObject(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            if (progress == 1f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void UpdateTextField()
    {
        PlayerName.text = input_nickname.text;

        if (input_nickname.text == "")
        {
            PlayerName.text = "Enter your name";
            btnNext.interactable = false;
        }
        else
        {
            btnNext.interactable = true;
        }

        if (input_nickname.text.Length >= 12)
        {
            input_nickname.characterLimit = 12;
        }
    }

    public void onSelectInputField()
    {
        Debug.Log("Select");
    }

    public void onDeselectInputField()
    {
        Debug.Log("DeSelect");
    }
}

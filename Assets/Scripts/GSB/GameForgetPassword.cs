using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameForgetPassword : MonoBehaviour
{
    public TMP_InputField InputEmail;

    public GameController ObjGameController;

    public ForgetClass ObjForgetClass;

    public ResRequestResetPassword ObjResRequestResetPassword;

    public ReqResetPassword ObjReqResetPassword;

    public ResResetPassword ObjResResetPassword;

    public AniImage AniSetPassword;

    public Button BtSubmit;

    public TMP_InputField InputPassword;
    public TMP_InputField InputConfirmPassword;

    public Button BtResetPasswordSubmit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BtSubmit.interactable = InputEmail.text.Length > 0;

        BtResetPasswordSubmit.interactable = InputPassword.text.Length > 0 && InputConfirmPassword.text.Length > 0 && InputPassword.text.Equals(InputConfirmPassword.text);
    }

    public void vReset()
    {
        InputEmail.text = "";
        GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutin);
    }

    public void vPressSubmit()
    {
        string path = ObjGameController.base_url+""+ GameController.USER_REQUEST_RESET_PASSWORD+"/" + InputEmail.text;
        Debug.Log("vPressSubmit = " + path);

        WWWForm form = new WWWForm();

        StartCoroutine(SetPost(path, form));
    }

    public void vPressResetPassword()
    {
        string path = ObjGameController.base_url + "" + GameController.USER_RESET_PASSWORD;
        Debug.Log("vPressResetPassword = " + path);

        WWWForm form = new WWWForm();

        ObjReqResetPassword = new ReqResetPassword();
        ObjReqResetPassword.user = new ReqUserResetPassword();

        ObjReqResetPassword.user.id = ObjResRequestResetPassword.id;
        ObjReqResetPassword.user.password = InputPassword.text;

        string json = JsonUtility.ToJson(ObjReqResetPassword);

        Debug.Log("json = "+ json);

        form.AddField("id", ObjReqResetPassword.user.id);
        form.AddField("password", ObjReqResetPassword.user.password);

        string test = System.Convert.ToBase64String(form.data);

        Debug.Log("form = "+ test);

        StartCoroutine(SetPost(path, form));
    }

    IEnumerator SetPost(string URL, WWWForm form)
    {
        //TxtCheck = check;//URL;
        //Debug.Log(URL);

        //Debug.Log(URL.Length+","+check.Length);

        ObjGameController.AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutin);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        ObjGameController.AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            ObjGameController.ShowAlert("" + www.error);
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(" = "+ www.downloadHandler.text);
            string jsonData = www.downloadHandler.text;
            
            Debug.Log(" = " + jsonData+","+ www.error);

            if (URL.Contains(GameController.USER_REQUEST_RESET_PASSWORD))
            {
                ObjResRequestResetPassword = ResRequestResetPassword.CreateFromJSON(jsonData);

                if (ObjResRequestResetPassword.status_code == 200)
                {
                    //ObjGameController.window_alert.vSetAlert("SEND SUCCESS!", false);
                    AniSetPassword.vSetCutSceneAutoCheck(CutInOut.Cutin);
                }
                else
                {
                    ObjGameController.window_alert.vSetAlert("INVALID EMAIL ADDRESS", false);
                }
            }
            else if (URL.Contains(GameController.USER_RESET_PASSWORD))
            {
                ObjResResetPassword = ResResetPassword.CreateFromJSON(jsonData);

                if (ObjResResetPassword.status_code == 200)
                {
                    ObjGameController.window_alert.vSetAlert("RESET PASSWORD SUCCESS!", false);
                    AniSetPassword.vSetCutSceneAutoCheck(CutInOut.Cutout);
                    GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutout);
                }
                else
                {
                    ObjGameController.window_alert.vSetAlert(ObjResResetPassword.message, false);
                }
            }
            /*
            ObjLoginClass = LoginClass.CreateFromJSON(jsonData);

            //JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
            //string status_code;
            //status_code = jsonNode["status_code"];
            if (ObjLoginClass.status_code == 200)
            {
                s_StrAuthenCode = ObjLoginClass.authen_code;

                PlayerPrefs.SetString("authen_code", ObjLoginClass.authen_code);// jsonNode["authen_code"]);
                PlayerPrefs.SetString("email", ObjLoginClass.email);// jsonNode["email"]);
                PlayerPrefs.Save();

                AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutin);
                StartCoroutine(LoadSceneObject("Character_Aomsin"));
            }
            else
            {
                Debug.Log("Failed");
                ShowAlert("EMAIL & PASSWORD");
                AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
            }
            */
        }
    }
}

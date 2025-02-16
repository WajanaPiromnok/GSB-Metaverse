using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public const string API_KEY = "gsb08834696200181647256";

    public const string USER_REGISTER = "user/register";
    public const string USER_LOGIN = "user/login";
    public const string USER_REQUEST_RESET_PASSWORD = "user/request-reset-password";
    public const string USER_RESET_PASSWORD = "user/reset-password";

    public string base_url;


    public GameObject window_register, window_login;
    public GameAlert window_alert;
    //public int gender_value = 1;
    //public Toggle genders;
    public TMP_InputField input_email_login, input_pass_login;
    public TMP_InputField input_email, input_password/*, input_nickname*/;
    public TMP_InputField input_ComfirmPassword;
    public TMP_InputField input_mobile;
    public string jsonData;
    private string str_email, str_password, str_mobile/*, str_nickname*/;
    public string authen_code;

    public RegisterClass ObjRegisterClass;
    public ResLoginClass ObjResLoginClass;

    public ResRequestResetPassword ObjResRequestResetPassword;

    public ResResetPassword ObjResResetPassword;

    public static string s_StrAuthenCode;
    public static bool s_IsGuest = false;

    [SerializeField] string s_AuthenCode;

    public AniImage AniLoading;

    public AniImage AniFadeInOut;

    string m_GoogleEmail;
    string m_GoogleUid;
    string m_GooglePhone;

    bool m_IsGoogleLogin;
    bool m_IsGoogleRegister;

    private void Awake()
    {
#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
        AniFadeInOut.gameObject.SetActive(true);

        m_IsGoogleLogin = false;
        m_IsGoogleRegister = false;
        m_GoogleEmail = "";
        m_GoogleUid = "";
        m_GooglePhone = "";

    }

    void Start()
    {
        window_register.SetActive(false);
        window_login.SetActive(true);
        window_alert.gameObject.SetActive(false);
        authen_code = PlayerPrefs.GetString("authen_code");
        string param_authen, api_url_authen;
        param_authen = "authen_code=" + authen_code;
        api_url_authen = base_url + "user.php" + "?" + param_authen;
        /*genders.onValueChanged.AddListener((value) =>
        {
            HandleGenders(value);
        });*/
        //UnityWebRequest www = new UnityWebRequest(api_url_authen);
        //StartCoroutine(FetchAuthentication(api_url_authen));

        GetComponent<OtpLogin>().IsOtpComplete = false;//true;//

        AniFadeInOut.vSetCutSceneAutoCheck(CutInOut.Cutout);

#if UNITY_WEBGL && !UNITY_EDITOR
        FirebaseAuth.OnAuthStateChanged(gameObject.name, "DisplayUserInfo", "DisplayInfo");

        //FacebookBridge.fbAsyncInit();
#endif

        Dictionary<string, object> actionButtonPressedParameters = new Dictionary<string, object>
            {
                    { "buttonName", "login" },
                    { "sceneName", "login" },
            };

        //AnalyticsService.Instance
        AnalyticsResult analyticsResult = Analytics.CustomEvent("ActionButtonPressed", actionButtonPressedParameters);

        Debug.Log("analyticsResult: = " + analyticsResult);
        //StartCoroutine(LoadSceneObject("MainMenu"));

    }

    private void Update()
    {
        //input_mobile.readOnly = GetComponent<OtpLogin>().IsOtpComplete;

        str_mobile = input_mobile.text;

        if (Input.GetKeyDown(KeyCode.P))
        {
            //StartCoroutine(FetchResponseUser(TxtCheck));
            //string test = "test copy";
            //GUIUtility.systemCopyBuffer = test;
            //test.CopyToClipboard();
        }


    }

    public void DisplayInfo(string info)
    {
        //Debug.Log("DisplayInfo = "+ info+","+ m_IsGoogleLogin);

        if (info.Contains("Success") && m_IsGoogleLogin)
        {
            if (m_GoogleEmail.Length > 0 && m_GoogleUid.Length > 0)
            {
                m_IsGoogleLogin = false;
                //Debug.Log("DisplayInfo = " + m_GoogleEmail + "," + m_GoogleUid);

                //LoginUser(m_GoogleEmail, m_GoogleUid);
            }
            FirebaseAuth.OnAuthStateChanged(gameObject.name, "DisplayUserInfo", "DisplayInfo");
            //StartCoroutine(LoadSceneObject("Character_Aomsin"));
        }
        else
            AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
    }

    public void DisplayUserInfo(string user)
    {
        Debug.Log("user = " + user);

        var parsedUser = StringSerializationAPI.Deserialize(typeof(FirebaseUser), user) as FirebaseUser;

        DisplayData($"Email: {parsedUser.email}, UserId: {parsedUser.uid}, EmailVerified: {parsedUser.isEmailVerified}");

        if (m_IsGoogleLogin)
        {
            m_GoogleEmail = parsedUser.email;// "mankjon024";//
            m_GoogleUid = parsedUser.uid;//"mankjon024";//
            m_GooglePhone = (parsedUser.phoneNumber.Length > 4) ? parsedUser.phoneNumber : "00000000";
        }

        Debug.Log("DisplayUserInfo =" + parsedUser.email + "," + parsedUser.uid + "," + m_IsGoogleLogin + "," + str_mobile);

        if (m_IsGoogleLogin && parsedUser.email.Length > 5)
        {
            m_IsGoogleLogin = false;
            m_IsGoogleRegister = true;

            //google login
            //string api_url = base_url + "" + USER_REGISTER;

            //WWWForm form = new WWWForm();
            //ReqRegister regis = new ReqRegister();
            //regis.user = new ReqRegisterUser();

            //regis.user.email = str_email;
            //regis.user.password = str_password;
            //regis.user.phone = parsedUser.phoneNumber;

            //string json = JsonUtility.ToJson(regis);

            //Debug.Log(" = " + json);

            //form.AddField("token", JWT.GetJwtUserClass(json));

            //StartCoroutine(FetchData(api_url, form));

            Debug.Log("DisplayUserInfo = " + m_GoogleEmail + "," + m_GoogleUid);

            LoginUserPost(m_GoogleEmail, m_GoogleUid);
        }
        else
        {
            if (m_IsGoogleLogin)
                LoginUserPost(m_GoogleEmail, m_GoogleUid);
            //LoginUser(m_GoogleEmail, m_GoogleUid);

            AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
        }
    }

    public void DisplayData(string data)
    {
        //outputText.color = outputText.color == Color.green ? Color.blue : Color.green;
        //outputText.text = data;
        Debug.Log("DisplayData = " + data);
    }

    public void RegisterUser()
    {
        Debug.Log("RegisterUser");
        str_email = input_email.text;
        str_password = input_password.text;
        str_mobile = input_mobile.text;
        //str_nickname = input_nickname.text;
        if (str_email == "" || str_password == "" || str_mobile == "" || !input_ComfirmPassword.text.Equals(str_password) || !GetComponent<OtpLogin>().IsOtpComplete)
        {
            if (str_email == "" || str_password == "")
                ShowAlert("EMAIL & PASSWORD");
            else if (str_mobile == "" && str_mobile.Length < 10 && str_mobile.Length > 10 || !GetComponent<OtpLogin>().IsOtpComplete)
                ShowAlert("PHONE NUMBER");
            if (!input_ComfirmPassword.text.Equals(str_password))
                ShowAlert("PASSWORD MISMATCH");
        }
        else
        {
            //string api_url = base_url + "" + USER_REGISTER;

            //WWWForm form = new WWWForm();
            //ReqRegister regis = new ReqRegister();
            //regis.user = new ReqRegisterUser();

            //regis.user.email = str_email;
            //regis.user.password = str_password;
            //regis.user.phone = str_mobile;

            //string json = JsonUtility.ToJson(regis);
            //Debug.Log(" = "+ json);

            //form.AddField("token", JWT.GetJwtUserClass(json));

            //StartCoroutine(FetchData(api_url, form));

            vRegisterPost(str_email, str_password, str_mobile);
            //StartCoroutine(FetchResponseUser(api_url));
        }
    }

    public void vRegisterPost(string email, string password, string mobile)
    {
        string api_url = base_url + "" + USER_REGISTER;

        WWWForm form = new WWWForm();
        ReqRegister regis = new ReqRegister();
        regis.user = new ReqRegisterUser();

        regis.user.email = email;
        regis.user.password = password;
        regis.user.phone = mobile;

        string json = JsonUtility.ToJson(regis);
        Debug.Log(" = " + json);

        form.AddField("token", JWT.GetJwtUserClass(json));

        StartCoroutine(FetchData(api_url, form));
    }

    public void GuestUser()
    {
        s_IsGuest = true;
        AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutin);
        StartCoroutine(LoadSceneObject("Character_Aomsin"));
    }

    public void GoogleLogin()
    {
        AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutin);

        if (m_GoogleEmail.Length > 0 && m_GoogleUid.Length > 0)
        {
            //Debug.Log("1 GoogleLogin = " + m_GoogleEmail + "," + m_GoogleUid);
            //LoginUser(m_GoogleEmail, m_GoogleUid);
            /*string param, api_url;
            param = "phone=" + "&email=" + m_GoogleEmail + "&password=" + m_GoogleUid + "&api_key=" + API_KEY;
            api_url = base_url + "loginget.php" + "?" + param;
            StartCoroutine(FetchResponseLogin(api_url));*/

            LoginUserPost(m_GoogleEmail, m_GoogleUid);
        }
        else
        {
            //Debug.Log("2 GoogleLogin = " + m_GoogleEmail + "," + m_GoogleUid);
            m_IsGoogleLogin = true;
        }

        FirebaseAuth.SignInWithGooglePopup(gameObject.name, "DisplayInfo", "DisplayErrorObject");
    }

    public void vTestFacebook()
    {
        //FacebookBridge.PostFBIm();//PostFB();
    }

    /*public void HandleGenders(bool isOn) 
    {
        if (isOn) gender_value = 1;
        else gender_value = 2;
    }*/

    public void DoLogin()
    {
        str_email = input_email_login.text;
        str_password = input_pass_login.text;
        if (str_email == "" || str_password == "")
        {
            ShowAlert("EMAIL & PASSWORD");
        }
        else
        {
            LoginUser(str_email, str_password);
        }
    }

    public void LoginUser(string _email, string _password)
    {
        print(_email);
        print(_password);
        string api_url = base_url + "login.php";
        WWWForm form = new WWWForm();
        form.AddField("email", _email);
        form.AddField("password", _password);
        form.AddField("api_key", API_KEY);

        //Debug.Log("LoginUser = " + api_url+","+ _email+","+ _password);

        StartCoroutine(FetchData(api_url, form));
    }

    public void LoginUserGet()
    {
        str_email = input_email_login.text;
        str_password = input_pass_login.text;
        if (str_email == "" || str_password == "")
        {
            ShowAlert("EMAIL & PASSWORD");
        }
        else
        {
            string param, api_url;
            param = "email=" + str_email + "&password=" + str_password + "&api_key=" + API_KEY;
            api_url = base_url + "loginget.php" + "?" + param;
            //UnityWebRequest www = new UnityWebRequest(api_url);
            StartCoroutine(FetchResponseLogin(api_url));
            AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutin);
            //StartCoroutine(LoadSceneObject("Character_Aomsin"));
        }
    }

    public void vPressLoginUser()
    {
        LoginUserPost(input_email_login.text, input_pass_login.text);
    }

    public void LoginUserPost(string email, string password)
    {
        str_email = email;
        str_password = password;

        if (str_email == "" || str_password == "")
        {
            ShowAlert("EMAIL & PASSWORD");
        }
        else
        {
            string api_url = base_url + USER_LOGIN;

            WWWForm form = new WWWForm();
            ReqLogin login = new ReqLogin();
            login.user = new ReqLoginUser();

            login.user.email = str_email;
            login.user.password = str_password;

            form.AddField("token", JWT.GetJwtUserClass(JsonUtility.ToJson(login)));

            StartCoroutine(FetchData(api_url, form));

            AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutin);
        }
    }

    public void LoginUserGetPassingParam(string str_email, string str_password)
    {
        if (str_email == "" || str_password == "")
        {
            ShowAlert("EMAIL & PASSWORD");
        }
        else
        {
            string param, api_url;
            //param = "phone=" + "&email=" + str_email + "&password=" + str_password + "&api_key=" + API_KEY;
            api_url = base_url + USER_LOGIN;// + "?" + param;
            //UnityWebRequest www = new UnityWebRequest(api_url);
            //StartCoroutine(FetchResponseLogin(api_url));

            vPressLoginUser();
        }
    }

    IEnumerator FetchData(string URL, WWWForm form)
    {
        Debug.Log("URL = " + URL);

        AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutin);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            if (!m_IsGoogleRegister)
            {
                Debug.Log(www.error + "," + www.downloadHandler.text);

                jsonData = www.downloadHandler.text;

                ResError objResError = ResError.CreateFromJSON(jsonData);

                AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);

                ShowAlert(objResError.message);
            }
            else if (URL.Contains(USER_LOGIN))
            {
                Debug.Log("vRegisterPost = " + m_GoogleEmail + "," + m_GoogleUid + "," + str_mobile);
                vRegisterPost(m_GoogleEmail, m_GoogleUid, m_GooglePhone);
            }
            else if (URL.Contains(USER_REGISTER))
            {
                Debug.Log("vSubmitGoogleLogin");
                vSubmitGoogleLogin();
            }
        }
        else
        {
            Debug.Log("Success!");
            jsonData = www.downloadHandler.text;
            JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
            Debug.Log(jsonData);
            if (URL.Contains(USER_REGISTER))
            {
                ObjRegisterClass = RegisterClass.CreateFromJSON(jsonData);

                s_StrAuthenCode = ObjRegisterClass.authen_code;

                PlayerPrefs.SetString("authen_code", jsonNode["authen_code"]);
                PlayerPrefs.SetString("email", jsonNode["email"]);
                PlayerPrefs.Save();

                AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);

                /*if (!m_IsGoogleRegister)
                    LoginUserGetPassingParam(input_email.text, input_password.text);
                else
                    LoginUserGetPassingParam(m_GoogleEmail, m_GoogleUid);
                */

                LoginUserPost(!m_IsGoogleRegister ? input_email.text : m_GoogleEmail, !m_IsGoogleRegister ? input_password.text : m_GoogleUid);
            }
            else if (URL.Contains(USER_LOGIN))
            {
                ObjResLoginClass = ResLoginClass.CreateFromJSON(jsonData);
                if (ObjResLoginClass.status_code == 200)
                {
                    s_StrAuthenCode = ObjResLoginClass.id;
                    StartCoroutine(LoadSceneObject("Character_Aomsin"));
                }
            }
            else if (URL.Contains(USER_REQUEST_RESET_PASSWORD))
            {
                ObjResRequestResetPassword = ResRequestResetPassword.CreateFromJSON(jsonData);

                if (ObjResRequestResetPassword.status_code == 200)
                {
                    vResetPasswordGoogleLogin();
                }
                else
                {
                    AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
                    ShowAlert(ObjResRequestResetPassword.message);
                }
            }
            else if (URL.Contains(GameController.USER_RESET_PASSWORD))
            {
                ObjResResetPassword = ResResetPassword.CreateFromJSON(jsonData);

                if (ObjResResetPassword.status_code == 200)
                {
                    LoginUserPost(m_GoogleEmail, m_GoogleUid);
                }
                else
                {
                    AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
                    ShowAlert(ObjResResetPassword.message);
                }
            }
        }
    }

    //public string TxtCheck = "";

    IEnumerator FetchResponseLogin(string URL)
    {
        //TxtCheck = check;//URL;
        //Debug.Log(URL);

        //Debug.Log(URL.Length+","+check.Length);

        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);

            jsonData = www.downloadHandler.text;

            ResError objResError = ResError.CreateFromJSON(jsonData);

            ShowAlert(objResError.message);

            Debug.Log(www.error + "," + www.downloadHandler.text);
        }
        else
        {
            //Debug.Log(" = "+ www.downloadHandler.text);
            jsonData = www.downloadHandler.text;
            Debug.Log(" = " + jsonData);
            ObjResLoginClass = ResLoginClass.CreateFromJSON(jsonData);

            //JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
            //string status_code;
            //status_code = jsonNode["status_code"];
            if (ObjResLoginClass.status_code == 200)
            {
                s_StrAuthenCode = ObjResLoginClass.id;

                PlayerPrefs.SetString("authen_code", ObjResLoginClass.id);// jsonNode["authen_code"]);
                //PlayerPrefs.SetString("email", ObjResLoginClass.email);// jsonNode["email"]);
                //PlayerPrefs.SetString("nickname", jsonNode["nickname"]);
                //PlayerPrefs.SetString("gender", jsonNode["gender"]);
                PlayerPrefs.Save();
                //Bypass
                //print(PlayerPrefs.GetString("gender"));
                /*if (PlayerPrefs.GetString("gender") == "1") StartCoroutine(LoadSceneObject("CharacterM_Aomsin"));
                else StartCoroutine(LoadSceneObject("CharacterF_Aomsin"));*/
                AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutin);
                StartCoroutine(LoadSceneObject("Character_Aomsin"));
            }
            else
            {
                Debug.Log("Failed");
                ShowAlert("EMAIL & PASSWORD");
                AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
            }
        }
    }

    IEnumerator FetchResponseUser(string URL)
    {
        //Debug.Log("URL = "+URL);
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            jsonData = www.downloadHandler.text;

            ResError objResError = ResError.CreateFromJSON(jsonData);

            ShowAlert(objResError.message);

            Debug.Log(URL + "\n = " + www.error + "," + www.downloadHandler.text);

            AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
        }
        else
        {
            Debug.Log("jsonData = " + www.downloadHandler.text);
            jsonData = www.downloadHandler.text;
            JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
            string status_code;
            status_code = jsonNode["status_code"];
            if (status_code == "200")
            {
                ObjRegisterClass = RegisterClass.CreateFromJSON(jsonData);

                s_StrAuthenCode = ObjRegisterClass.authen_code;

                PlayerPrefs.SetString("authen_code", jsonNode["authen_code"]);
                PlayerPrefs.SetString("email", jsonNode["email"]);
                //PlayerPrefs.SetString("nickname", jsonNode["nickname"]);
                //PlayerPrefs.SetString("gender", jsonNode["gender"]);
                PlayerPrefs.Save();
                if (!m_IsGoogleRegister)
                    LoginUserGetPassingParam(input_email.text, input_password.text);
                else
                    LoginUserGetPassingParam(m_GoogleEmail, m_GoogleUid);
            }
            else
            {
                Debug.Log("Failed");
                if (!m_IsGoogleRegister)
                    ShowAlert("EMAIL & PASSWORD");
                else
                {
                    m_IsGoogleRegister = false;
                    ShowAlert("GOOGLE LOGIN");
                }
                AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
            }
        }
    }

    public void vSubmitGoogleLogin()
    {
        string path = base_url + "" + USER_REQUEST_RESET_PASSWORD + "/" + m_GoogleEmail;

        Debug.Log("vSubmitGoogleLogin = " + path);

        WWWForm form = new WWWForm();

        StartCoroutine(FetchData(path, form));
    }

    public void vResetPasswordGoogleLogin()
    {
        string path = base_url + "" + USER_RESET_PASSWORD;

        WWWForm form = new WWWForm();

        form.AddField("id", ObjResRequestResetPassword.id);
        form.AddField("password", m_GoogleUid);

        Debug.Log("vResetPasswordGoogleLogin = " + m_GoogleEmail + "," + m_GoogleUid);

        StartCoroutine(FetchData(path, form));
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

    IEnumerator FetchAuthentication(string URL)
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);

            jsonData = www.downloadHandler.text;

            ResError objResError = ResError.CreateFromJSON(jsonData);

            ShowAlert(objResError.message);
        }
        else
        {
            jsonData = www.downloadHandler.text;
            JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
            string status_code;
            status_code = jsonNode["status_code"];
            if (status_code == "200")
            {
                //StartCoroutine(LoadSceneObject("Character_Aomsin"));
            }
            else
            {
                Debug.Log("Failed");
                ShowAlert("EMAIL & PASSWORD");
            }
        }
    }

    public void ToggleLogin()
    {
        window_register.SetActive(false);
        window_login.SetActive(true);
    }
    public void ToggleRegister()
    {
        window_register.SetActive(true);
        window_login.SetActive(false);
    }

    public void ShowAlert(string strAlert)
    {
        window_alert.vSetAlert(strAlert);//GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutin);//SetActive(true);
    }

    public void closeAlertWindows()
    {
        window_alert.GetComponent<AniImage>().vSetCutSceneAutoCheck(CutInOut.Cutout);//.SetActive(false);
    }

    public void OpenWeb(string website)
    {
        Application.OpenURL(website);
    }
}

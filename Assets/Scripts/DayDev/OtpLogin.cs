using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class OtpLogin : MonoBehaviour
{

    private const string URL_OTP_SEND = "https://portal-otp.smsmkt.com/api/otp-send";
    private const string URL_OTP_VALIDATE = "https://portal-otp.smsmkt.com/api/otp-validate";

    public string Api_key = "ffdead5fb38523bde5d9be7d86055e09";
    public string Secret_key = "0ckzcrkvOzRTri5e";
    public string Project_key = "338964479f";

    public string TokenId = "";
    public string PhoneNumber = "";

    public string Otp_code = "";

    public TMP_InputField InputPhoneNumber;
    public TMP_InputField InputOtp;

    public InputField[] InputOtpGroup;

    public Button BtPhoneNumber;
    public Button BtOtp;

    public AniImage AniPopupOtp;

    public AniImage AniLoading;

    public GameAlert window_alert;

    public TextMeshProUGUI TxtTimeCound;

    public DataResponseLogin ObjDataResponseLogin;
    public DataResponseCheckOtp ObjDataResponseCheckOtp;


    public bool IsOtpComplete = false;

    float m_TimeCount;

    // Start is called before the first frame update
    void Start()
    {
        AniPopupOtp.gameObject.SetActive(false);

        m_TimeCount = 600f;
    }

    public void vStart()
    {
        for (int i = 0; i < InputOtpGroup.Length; i++)
            InputOtpGroup[i].text = "";
        Otp_code = "";
        BtOtp.interactable = false;
        m_TimeCount = 600f;
    }

    public void vRequest()
    {
        if (PhoneNumber.Length < 10)
        {
            window_alert.vSetAlert("PHONE NUMBER");
            return;
        }

        AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutin);

        StartCoroutine(PostRequest(URL_OTP_SEND));

        /*
        try
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL_OTP_SEND);

            request.Method = "POST";
            request.Headers["api_key"] = Api_key;
            request.Headers["secret_key"] = Secret_key;
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{ \"project_key\":\"" + Project_key + "\",\"phone\":\"" + PhoneNumber + "\" }";
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            WebResponse webResponse = request.GetResponse();

            using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
            {
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    ObjDataResponseLogin = DataResponseLogin.CreateFromJSON(response);

                    if (ObjDataResponseLogin.detail.Equals("OK."))
                    {
                        TokenId = ObjDataResponseLogin.result.token;
                        AniPopupOtp.vSetCutSceneAutoCheck(CutInOut.Cutin);
                        AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
                    }
                    else
                    {
                        window_alert.vSetAlert("INVALID MOBILE");
                        Debug.Log("error = " + ObjDataResponseLogin.detail);
                    }


                    Debug.Log(response);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("-----------------");
            Debug.Log(e.Message);
        }*/
    }

    IEnumerator PostRequest(string uri)
    {
        UnityWebRequest www = UnityWebRequest.Post(uri, new WWWForm());

        string toJson = "";

        if (uri.Contains(URL_OTP_SEND))
            toJson =  "{ \"project_key\":\"" + Project_key + "\",\"phone\":\"" + PhoneNumber + "\" }";
        else
            toJson = "{ \"token\":\"" + TokenId + "\",\"otp_code\":\"" + Otp_code + "\" }";

        www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(toJson));

        www.method = "POST";
        www.SetRequestHeader("api_key", Api_key);
        www.SetRequestHeader("secret_key", Secret_key);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            window_alert.vSetAlert("ERROR :" + www.error.ToUpper());
            Debug.Log("error, " + uri + " = " + www.error + "," + www.downloadHandler.text);
        }
        else
        {
            string jsonData = www.downloadHandler.text;

            if (www.responseCode == 200)
            {
                if (uri.Contains(URL_OTP_SEND))
                {
                    ObjDataResponseLogin = DataResponseLogin.CreateFromJSON(jsonData);

                    if (ObjDataResponseLogin.detail.Equals("OK."))
                    {
                        TokenId = ObjDataResponseLogin.result.token;
                        AniPopupOtp.vSetCutSceneAutoCheck(CutInOut.Cutin);
                        AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
                        vStart();
                    }
                    else
                    {
                        window_alert.vSetAlert("PHONE NUMBER");
                        Debug.Log("error = " + ObjDataResponseLogin.detail);
                    }
                }
                else
                {
                    ObjDataResponseCheckOtp = DataResponseCheckOtp.CreateFromJSON(jsonData);
                    if (ObjDataResponseCheckOtp.detail.Equals("OK."))
                    {
                        if (ObjDataResponseCheckOtp.result.status)
                        {
                            IsOtpComplete = true;
                            AniPopupOtp.vSetCutSceneAutoCheck(CutInOut.Cutout);
                        }
                        else
                            window_alert.vSetAlert("OTP NUMBER");
                    }
                    else
                    {
                        window_alert.vSetAlert("OTP NUMBER");
                        Debug.Log("cannot Login " + ObjDataResponseCheckOtp.detail);
                    }
                }
            }

            Debug.Log("done, " + uri + " = " + www.downloadHandler.text);
        }

        AniLoading.vSetCutSceneAutoCheck(CutInOut.Cutout);
    }

    public void vCheckOtp()
    {
        if (TokenId.Length <= 10)
        {
            window_alert.vSetAlert("TOKEN");
            return;
        }

        if (Otp_code.Length < 6)
        {
            window_alert.vSetAlert("OTP NUMBER");
            return;
        }

        StartCoroutine(PostRequest(URL_OTP_VALIDATE));
        /*
        try
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL_OTP_VALIDATE);
            request.Method = "POST";
            request.Headers["api_key"] = Api_key;
            request.Headers["secret_key"] = Secret_key;
            request.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{ \"token\":\"" + TokenId + "\",\"otp_code\":\"" + Otp_code + "\" }";
                streamWriter.Write(json);
                streamWriter.Flush();
            }
            WebResponse webResponse = request.GetResponse();
            using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
            using (StreamReader responseReader = new StreamReader(webStream))
            {
                string response = responseReader.ReadToEnd();
                ObjDataResponseCheckOtp = DataResponseCheckOtp.CreateFromJSON(response);
                if (ObjDataResponseCheckOtp.detail.Equals("OK."))
                {

                    if (ObjDataResponseCheckOtp.result.status)
                    {
                        IsOtpComplete = true;
                        AniPopupOtp.vSetCutSceneAutoCheck(CutInOut.Cutout);
                    }
                    else
                        window_alert.vSetAlert("INVALID OTP");
                }
                else
                {
                    window_alert.vSetAlert("INVALID OTP");
                    Debug.Log("cannot Login "+ ObjDataResponseCheckOtp.detail);
                }

                Debug.Log(response);
            }
        }
        catch (Exception e)
        {
            Debug.Log("-----------------");
            Debug.Log(e.Message);
        }*/
    }

    public void vUpdatePhoneNumber()
    {
        PhoneNumber = InputPhoneNumber.text;
        InputPhoneNumber.characterLimit = 10;
    }

    public void vUpdateOtp()
    {
        Otp_code = "";
        for (int i = 0; i < InputOtpGroup.Length; i++)
            Otp_code += InputOtpGroup[i].text;

        BtOtp.interactable = Otp_code.Length >= 6;
    }

    public void vUpdateTime()
    {
        if (m_TimeCount > 0f)
        {
            m_TimeCount -= Time.deltaTime;

            if (m_TimeCount <= 0f)
            {
                m_TimeCount = 0f;
            }

            string txt = ((int)m_TimeCount / 60)+":"+((int)m_TimeCount % 60);
            TxtTimeCound.text = txt;
        }
    }

    public void vUpdateOtpGroup(int num)
    {
        //Debug.Log("num = "+ num+";"+ InputOtpGroup.Length);

        if(num + 1 < InputOtpGroup.Length && InputOtpGroup[num].text.Length >= 1)
            InputOtpGroup[num + 1].Select();
        else if (num - 1 >= 0 && InputOtpGroup[num].text.Length == 0)
            InputOtpGroup[num - 1].Select();
    }

    public void vCheckBackSpace()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            for (int i = 0; i < InputOtpGroup.Length; i++)
            {
                if (InputOtpGroup[i].isFocused)
                {
                    if (i - 1 >= 0 && InputOtpGroup[i].text.Length == 0)
                    {
                        InputOtpGroup[i - 1].Select();
                        InputOtpGroup[i - 1].text = "";
                    }
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        vCheckBackSpace();
        vUpdateOtp();
        vUpdateTime();
    }

}

[System.Serializable]
public class DataResponseLogin
{
    public string code;
    public string detail;
    public ResultLogin result;

    public static DataResponseLogin CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<DataResponseLogin>(jsonString);
    }
}

[System.Serializable]
public class ResultLogin
{
    public string ref_code;
    public string token;
    public bool status;
}

[System.Serializable]
public class ResultCheckOtp
{
    public bool status;
}

[System.Serializable]
public class DataResponseCheckOtp
{
    public string code;
    public string detail;
    public ResultLogin result;

    public static DataResponseCheckOtp CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<DataResponseCheckOtp>(jsonString);
    }
}

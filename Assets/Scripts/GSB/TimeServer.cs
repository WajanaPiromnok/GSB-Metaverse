using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TimeServer : MonoBehaviour
{
    DateTime timeNow;
    DateTime modifiedDatetime;
    string time;

    string timeAsString;

    /*private string _url = "https://meta.tr1.co.th/api/Time.php";
    private string _url2 = "https://meta.tr1.co.th/api/Date.php";*/

    private string _timeData;
    private string _currentTime;

    private string _dateData;
    private string _currentDate;
    //private string _currentDate;

    public static TimeServer sharedInstance = null;

    public string words;
    public string wordDate;
    void Awake()
    {
        //Local Time
        time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
        print(time);

        modifiedDatetime = DateTime.UtcNow.ToLocalTime().AddMinutes(3).AddSeconds(23);
        print(modifiedDatetime.ToString("HH:mm"));

        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //System Time
        /*timeNow = DateTime.Now;
        DateTime modifiedDatetime = timeNow.AddMinutes(3);
        modifiedDatetime.ToString("HH:mm");
        print(modifiedDatetime.ToString("HH:mm"));*/
    }

    void Start()
    {
        //Debug.Log(StartCoroutine(GetServerTime()));
        //Debug.LogStartCoroutine(GetDate());

    }

    private void Update()
    {
        //StartCoroutine(GetServerTime());
        //StartCoroutine(GetDate());
    }
    /*public IEnumerator GetServerTime()
    {
        //Debug.Log("connecting to php");
        WWW www = new WWW(_url);
        yield return www;
        if (www.error != null)
        {
            //Debug.Log("Error");
        }
        else
        {
            //Debug.Log("got the php information");
        }
        _timeData = www.text;
        words = _timeData;
        //timerTestLabel.text = www.text;
        //Debug.Log("The date is : " + words[0]);
        //Debug.Log("The time is : " + words);

        //setting current time
        //_currentDate = words[0];
        _currentTime = words;
    }

    public IEnumerator GetDate()
    {
        WWW date = new WWW(_url2);
        yield return date;
        if (date.error != null)
        {
            //Debug.Log("Error");
        }
        else
        {
            //Debug.Log("got the information");
        }
        _dateData = date.text;
        wordDate = _dateData;
        //Debug.Log("The date is : " + wordDate);
        _currentDate = wordDate;
    }

    //get the current Time
    public string getCurrentTimeNow()
    {
        return _currentTime;
    }

    /*public int getCurrentDateNow()
    {
        string[] words = _currentDate.Split('-');
        int x = int.Parse(words[0] + words[1] + words[2]);
        return x;
    }*/
}
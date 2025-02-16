using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FortuneSystem : MonoBehaviour
{
    public static Root data = new Root();
    private string json;
    private string filePath;
    private int today;
    private int daySelect;
    private int valueBack;
    [SerializeField] private GameObject dayPanel;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private GameObject btnFortune;
    [SerializeField] private GameObject textFortune;
    [SerializeField] private GameObject btnBack;
    [SerializeField] private Text txtValue;
    
    void Start()
    {
        filePath = Application.dataPath + "/Resources/DataFortune.json";
        StartCoroutine(FetchDataWithFilepath(filePath));
        today = DateTime.Today.Day;
    }
    
    public void Clicked_Fortune()
    {
        dayPanel.SetActive(true);
        btnFortune.SetActive(false);
        btnBack.SetActive(true);
        valueBack = 0;
    }
    public void Clicked_Back()
    {
        switch (valueBack)
        {
            case 0:
                btnBack.SetActive(false);
                dayPanel.SetActive(false);
                btnFortune.SetActive(true);
                break;
            case 1:
                valueBack = 0;
                dayPanel.SetActive(true);
                textPanel.SetActive(false);
                break;
            case 2:
                valueBack = 1;
                textFortune.SetActive(false);
                textPanel.SetActive(true);
                break;
        }
    }
    public void Clicked_Days(int value)
    {
        daySelect = value;
        dayPanel.SetActive(false);
        textPanel.SetActive(true);
        valueBack = 1;
    }
    public void Clicked_ShowText(int value)
    {
        textPanel.SetActive(false);
        if (today >= 1 || today <= 13)
        {

        }
        if (today >= 14 || today <= 20)
        {
            txtValue.text = data.Feb[2].day[daySelect].data[value].value;
        }
        if (today >= 21 || today <= 27)
        {
            txtValue.text = data.Feb[3].day[daySelect].data[value].value;
        }
        textFortune.SetActive(true);
        valueBack = 2;
    }

    IEnumerator FetchDataWithFilepath(string filePath)
    {
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            json = www.text;
            //JSONNode jsonNode = JSON.Parse(json);
            JsonUtility.FromJsonOverwrite(json, data);
            //JsonConvert.SerializeObject(data, Formatting.Indented);
            Debug.Log(data.Feb[2].day[0]);
        }
        else
        {
            json = File.ReadAllText(filePath);
            Debug.Log(json);
            JsonUtility.FromJsonOverwrite(json, data);
            Debug.Log(data.Feb[2].day[0].data[0].value);
        }
    }

}
//DATA JSON
[Serializable]
public class Data
{
    public string value;
}
[Serializable]
public class Day
{
    public List<Data> data;
}
[Serializable]
public class Feb
{
    public List<Day> day;
}
[Serializable]
public class Root
{
    public List<Feb> Feb;
}

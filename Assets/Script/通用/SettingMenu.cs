using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("物件設定")]
    public GameObject[] uiPages;

    public TMP_Dropdown resolutionDP;
    public Toggle fullToggle;
    public bool isPause;
    public bool isLevel;

    Resolution[] resolutions;
    List<Resolution> fliterResolution;
    void Start()
    {
        SetResolutionInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevel && !isPause && Input.GetKeyDown(KeyCode.Escape))
        {

            uiPages[0].SetActive(true);
            isPause = true;
            Time.timeScale = 0;

        }
    }
    void CloseAllPage()
    {
        foreach (GameObject i in uiPages)
        {
            i.SetActive(false);
        }
    }
    public void OpenPage(int i)
    {
        CloseAllPage();
        uiPages[i].SetActive(true);
    }
    //關閉設定選單(關卡用)
    public void CloseSettingUI()
    {
        CloseAllPage();
        Time.timeScale = 1;
        isPause = false;
    }


    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (!isFullScreen)
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
    }
    //設定解析度，會用在解析度的下拉選單
    public void SetResolution(int i)
    {
        Resolution resolution = fliterResolution[i];
        bool isFull;
        if (PlayerPrefs.GetInt("FullScreen") == 1) isFull = true; else isFull = false;
        Screen.SetResolution(resolution.width, resolution.height, isFull);

        //儲存解析度
        PlayerPrefs.SetInt("ScreenResolutionX", resolution.width);
        PlayerPrefs.SetInt("ScreenResolutionY", resolution.height);
    }
    // 設定解析度內容
    void SetResolutionInfo()
    {
        //取得所有解析度,並過濾
        fliterResolution = new List<Resolution>();
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            float value1 = Mathf.Floor((float)resolutions[i].refreshRateRatio.value);
            float value2 = Mathf.Floor((float)Screen.currentResolution.refreshRateRatio.value);
            if (value1 == value2)
            {
                fliterResolution.Add(resolutions[i]);
            }
        }

        //接解析度轉換成string格式
        int currnetIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < fliterResolution.Count; i++)
        {
            string option = $"{fliterResolution[i].width}x{fliterResolution[i].height}";
            options.Add(option);

            if (fliterResolution[i].width == PlayerPrefs.GetInt("ScreenResolutionX")
                 && fliterResolution[i].height == PlayerPrefs.GetInt("ScreenResolutionY"))
            {
                currnetIndex = i;
            }
        }
        resolutionDP.ClearOptions();
        resolutionDP.AddOptions(options);
        resolutionDP.onValueChanged.RemoveListener(SetResolution);
        resolutionDP.value = currnetIndex;
        resolutionDP.onValueChanged.AddListener(SetResolution);
        resolutionDP.RefreshShownValue();

        //設定isFull 的Toggle
        if (PlayerPrefs.GetInt("FullScreen") == 1) fullToggle.isOn = true; else fullToggle.isOn = false;
    }

    public void StartGame()
    {
        Load load = GameObject.Find("LevelLoader").GetComponent<Load>();
        if (load != null) load.LoadLevel(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}

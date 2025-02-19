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
    void Start()
    {
        // SetResolutionInfo();
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


    // public void SetFullScreen(bool isFullScreen)
    // {
    //     Screen.fullScreen = isFullScreen;
    //     if (!isFullScreen)
    //     {
    //         PlayerPrefs.SetInt("FullScreen", 0);
    //     }
    //     else
    //     {
    //         PlayerPrefs.SetInt("FullScreen", 1);
    //     }
    // }
    //設定解析度，會用在解析度的下拉選單
    // public void SetResolution(int i)
    // {
    //     Resolution resolution = resolutions[i];
    //     bool isFull;
    //     if (PlayerPrefs.GetInt("FullScreen") == 0) { isFull = false; } else { isFull = true; }
    //     Screen.SetResolution(resolution.width, resolution.height, isFull);

    //     //儲存解析度
    //     PlayerPrefs.SetInt("ScreenResolutionX", resolution.width);
    //     PlayerPrefs.SetInt("ScreenResolutionY", resolution.height);
    // }
    //設定解析度內容
    // void SetResolutionInfo()
    // {
    //     //讀取原本儲存的解析度並設定
    //     bool isFull;
    //     if (PlayerPrefs.GetInt("FullScreen") == 0) { isFull = false; } else { isFull = true; }
    //     Screen.SetResolution(PlayerPrefs.GetInt("ScreenResolutionX"), PlayerPrefs.GetInt("ScreenResolutionY"), isFull);




    //     //取得所有解析度
    //     resolutions = Screen.resolutions;
    //     List<string> resolutionList = new List<string>();
    //     resolutionDP.ClearOptions();

    //     int currnetIndex = 0;
    //     //接解析度轉換成string格式
    //     for (int i = 0; i < resolutions.Length; i++)
    //     {
    //         string options = $"{resolutions[i].width}x{resolutions[i].height}";

    //         if (!resolutionList.Contains(options))
    //         {
    //             resolutionList.Add(options);
    //             if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
    //             {
    //                 currnetIndex = i;
    //             }
    //         }
    //     }
    //     resolutionDP.AddOptions(resolutionList);
    //     resolutionDP.onValueChanged.RemoveListener(SetResolution);
    //     resolutionDP.value = currnetIndex;
    //     resolutionDP.onValueChanged.AddListener(SetResolution);
    //     resolutionDP.RefreshShownValue();


    // }

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

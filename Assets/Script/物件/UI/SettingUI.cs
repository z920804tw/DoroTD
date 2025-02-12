using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] uiObj;

    public bool isPause;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                uiObj[0].SetActive(true);
                isPause = true;
                Time.timeScale = 0;
            }
        }
    }
    public void CloseAllUI()
    {
        foreach (GameObject i in uiObj)
        {
            i.SetActive(false);
        }
    }

    public void CloseUI()
    {
        CloseAllUI();
        Time.timeScale = 1;
        isPause = false;
    }
    public void OpenPage(int i)
    {
        CloseAllUI();
        uiObj[i].SetActive(true);
    }

}

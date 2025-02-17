using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] uiPages;

    public void StartGame()
    {
        Load load = GameObject.Find("LevelLoader").GetComponent<Load>();
        if (load != null) load.LoadLevel(1);
    }

    public void SelectPage(int i)
    {
        CloseAllPage();
        uiPages[i].SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    void CloseAllPage()
    {
        foreach (GameObject i in uiPages)
        {
            i.SetActive(false);
        }
    }
}

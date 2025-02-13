using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] uiPages;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
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

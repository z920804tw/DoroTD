using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playUI;
    public GunSelectUI gunSelectUI;
    public HpBar hpBar;
    public PlayerMoney playerMoney;
    public LevelInfo levelInfo;
    public GameOverUI gameOverUI;
    public SettingUI settingUI;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackToLobby()
    {
        Time.timeScale=1;
        SceneManager.LoadScene(0);
    }
}

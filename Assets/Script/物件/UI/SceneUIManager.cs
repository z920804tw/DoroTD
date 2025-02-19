using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI元素設定")]
    public GameObject playUI;
    public GunSelectUI gunSelectUI;
    public HpBar hpBar;
    public PlayerMoney playerMoney;
    public LevelInfo levelInfo;



    public void BackToLobby()
    {
        Time.timeScale = 1;
        Load load = GameObject.Find("LevelLoader").GetComponent<Load>();
        if (load != null)
        {
            load.LoadLevel(0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum GameStatus
{
    Break,
    Start,
    End,
}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("遊戲物件設定")]
    public GameStatus gameStatus;
    public GameObject startRoundObj;
    public GameObject levelInfoUI;
    public TMP_Text timerText;
    public TMP_Text roundText;
    [Header("關卡參數設定")]
    public float increaseHp;
    [Header("Debug")]
    [SerializeField] float roundTime;
    float timer;
    [SerializeField] int currnetRound;

    [SerializeField] GameObject[] enemyPortals;
    void Start()
    {
        gameStatus = GameStatus.Break;
        enemyPortals = GameObject.FindGameObjectsWithTag("EnemyPortal");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatus == GameStatus.Start)
        {
            timer -= Time.deltaTime;
            int s = (int)timer;
            timerText.text = $"剩餘時間:{s / 60}分{s % 3600 % 60}秒";
            roundText.text = $"當前回合:{currnetRound}";
            if (timer <= 0)
            {
                EndRound();
            }
        }
    }

    //回合開始
    public void StartRound()
    {
        currnetRound++;
        timer = roundTime;
        levelInfoUI.SetActive(true);
        gameStatus = GameStatus.Start;


        foreach (GameObject i in enemyPortals)
        {
            i.GetComponent<EnemySpawner>().CanSpawn = true;
        }
    }
    //回合結束
    void EndRound()
    {
        gameStatus = GameStatus.Break;
        levelInfoUI.SetActive(false);
        startRoundObj.SetActive(true);
        foreach (GameObject i in enemyPortals)
        {
            i.GetComponent<EnemySpawner>().CanSpawn = false;
            i.GetComponent<EnemySpawner>().increaseEnemyHp += increaseHp;
            i.GetComponent<EnemySpawner>().SpawnLimit++;
        }

        //將場上剩餘的敵人清空
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in enemys)
        {
            i.GetComponent<EnemyHealth>().DestoryEnemy();
        }
    }

    //遊戲結束
    public void GameOver()
    {
        gameStatus = GameStatus.End;
        levelInfoUI.SetActive(false);
        //顯示失敗的UI畫面
    }

    // 遊戲勝利
    public void WinGame()
    {
        gameStatus = GameStatus.End;
        levelInfoUI.SetActive(false);
        //顯示勝利的UI畫面
    }
}

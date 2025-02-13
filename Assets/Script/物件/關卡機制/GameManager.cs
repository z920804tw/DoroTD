using System;
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
    public SceneUIManager sceneUIManager;
    public GameObject startRoundObj;

    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    LevelInfo levelInfo;
    GameOverUI gameOverUI;


    [Header("關卡參數設定")]
    public float increaseHp;
    [SerializeField] float portalSpawnTime;
    [SerializeField] float transitionDuration;
    [Header("Debug")]
    [SerializeField] float roundTime;
    float timer;
    [SerializeField] int currnetRound;
    float suvivalTime;
    int killCount;

    [SerializeField] GameObject[] enemyPortals;



    void Start()
    {
        levelInfo = sceneUIManager.levelInfo;
        gameOverUI = sceneUIManager.gameOverUI;
        gameStatus = GameStatus.Break;
        enemyPortals = GameObject.FindGameObjectsWithTag("EnemyPortal");
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(TransitionMusic(0, 0, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatus == GameStatus.Start)
        {
            suvivalTime += Time.deltaTime;
            timer -= Time.deltaTime;
            int s = (int)timer;
            levelInfo.timerText.text = $"剩餘時間:{s / 60}分{s % 3600 % 60}秒";
            levelInfo.roundText.text = $"當前回合:{currnetRound}";
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
        levelInfo.gameObject.SetActive(true);
        gameStatus = GameStatus.Start;

        //切換音效
        StartCoroutine(TransitionMusic(1, 0, 0.5f));

        foreach (GameObject i in enemyPortals)
        {
            i.GetComponent<EnemySpawner>().CanSpawn = true;
            i.GetComponent<EnemySpawner>().SpawnTime = portalSpawnTime;
            i.GetComponent<EnemySpawner>().Timer = 100;
        }
    }
    //回合結束
    void EndRound()
    {
        gameStatus = GameStatus.Break;
        levelInfo.gameObject.SetActive(false);
        startRoundObj.SetActive(true);
        //切換音效
        StartCoroutine(TransitionMusic(0, 0, 0.5f));

        //增加血量
        foreach (GameObject i in enemyPortals)
        {
            i.GetComponent<EnemySpawner>().CanSpawn = false;
            i.GetComponent<EnemySpawner>().increaseEnemyHp = increaseHp * currnetRound;
        }
        //判斷回合，設定每3回合會增加生成速度
        if (currnetRound % 3 == 0 && portalSpawnTime > 2)
        {
            portalSpawnTime--;
            foreach (GameObject i in enemyPortals)
            {
                i.GetComponent<EnemySpawner>().SpawnTime = portalSpawnTime;
            }
        }

        //將場上剩餘的敵人清空
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in enemys)
        {
            i.GetComponent<EnemyHealth>().DestoryEnemy();
        }

        //回復玩家血量
        PlayerStatus playerStatus = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        playerStatus.currentHp = playerStatus.maxHp;
        sceneUIManager.hpBar.UpdateHpInfo();
    }

    //遊戲結束
    public void GameOver()
    {
        //停止生成敵人
        foreach (GameObject i in enemyPortals)
        {
            i.GetComponent<EnemySpawner>().CanSpawn = false;
        }

        //顯示結束的UI畫面  
        gameStatus = GameStatus.End;
        levelInfo.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);

        //設定內容
        gameOverUI.suvivalTimeText.text = $"存活時間:{(int)suvivalTime / 60}分{(int)suvivalTime % 3600 % 60}秒";
        gameOverUI.roundText.text = $"回合數:{currnetRound}";
        gameOverUI.killCountText.text = $"擊殺數:{killCount}";
        gameOverUI.moneyCountText.text = $"金錢獲得數:{sceneUIManager.playerMoney.TotalMoney}";
    }

    public int KillCount
    {
        get { return killCount; }
        set { killCount = value; }
    }


    IEnumerator TransitionMusic(int i, float start, float end)
    {
        float timer = 0;
        //前次音樂退場
        if (audioSource.clip != audioClips[i])
        {
            while (timer < transitionDuration)
            {
                timer += Time.deltaTime;
                float t = timer / transitionDuration;
                audioSource.volume = Mathf.Lerp(end, start, t);
                yield return null;
            }
            audioSource.clip = audioClips[i];
            audioSource.Play();
            Debug.Log("切換完成");
        }

        //目標音樂的進場
        timer = 0;
        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;
            audioSource.volume = Mathf.Lerp(start, end, t);
            yield return null;
        }

    }
}

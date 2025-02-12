using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyPrefab;
    public Transform spawnPos;
    [SerializeField] float spawnTime;
    [SerializeField] bool canSpawn;

    public float increaseEnemyHp;
    float timer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0;
                int rnd = Random.Range(0, 100);
                int id;
                if (rnd % 12 == 0)
                {
                    id = 2;
                }
                else if (rnd % 5 == 0)
                {
                    id = 1;
                }
                else
                {
                    id = 0;
                }

                GameObject enemy = Instantiate(enemyPrefab[id], spawnPos.position, Quaternion.identity);
                enemy.GetComponent<EnemyHealth>().maxHp += increaseEnemyHp; //敵人血量會隨著回合而增加
                // enemy.GetComponent<EnemyHealth>().DeadEvevnt += ReduceCount;
                Debug.Log("生成敵人");

            }
        }
    }


    public bool CanSpawn
    {
        get { return canSpawn; }
        set { canSpawn = value; }
    }
    public float SpawnTime
    {
        get { return spawnTime; }
        set { spawnTime = value; }
    }
    public float Timer
    {
        set{timer=value;}
    }
}

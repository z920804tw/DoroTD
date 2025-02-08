using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyPrefab;
    public Transform spawnPos;
    [SerializeField] float spawnTime;
    [SerializeField] int spawnLimit;
    [SerializeField] bool canSpawn;
    [SerializeField] int spawnCount;

    public float increaseEnemyHp;
    float timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && spawnCount < spawnLimit)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0;
                int rnd = Random.Range(0, enemyPrefab.Length);
                GameObject enemy = Instantiate(enemyPrefab[rnd], spawnPos.position, Quaternion.identity);
                enemy.GetComponent<EnemyHealth>().maxHp += increaseEnemyHp; //敵人血量會隨著回合而增加
                enemy.GetComponent<EnemyHealth>().DeadEvevnt += ReduceCount;
                spawnCount++;

            }
        }
    }

    void ReduceCount()
    {
        spawnCount--;
    }

    public bool CanSpawn
    {
        get { return canSpawn; }
        set { canSpawn = value; }
    }
    public int SpawnLimit
    {
        get { return spawnLimit; }
        set { spawnLimit = value; }
    }
}

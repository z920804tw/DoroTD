using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySetting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("敵人設定")]
    public float maxHp;
    public float enemyHp;
    EnemyHealth enemyHealth;
    [Header("敵人AI相關設定")]
    public NavMeshAgent nav;
    public float moveSpeed;
    Transform target;

    [Header("敵人UI設定")]
    public GameObject enemyBody;
    public GameObject enemyHpBar;
    public Image hpImg;
    public Transform dmgPos;
    public GameObject dmgText;


    void Start()
    {
        enemyHealth=GetComponent<EnemyHealth>();
        enemyHp = maxHp;

        target = GameObject.Find("Player").transform;
        nav.speed = moveSpeed;
        nav.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemyHpBar.transform.LookAt(Camera.main.transform.position);
        enemyBody.transform.LookAt(Camera.main.transform.position);
        nav.SetDestination(target.position);
        
    }

    public void TakeDmg(float dmg)
    {
        enemyHp -= dmg;
        enemyHealth.ChangeDmgColor();
        
        InstantiateDmgText(dmg);
        UpdateHpBar();
        if (enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHpBar()
    {
        hpImg.fillAmount = enemyHp / maxHp;
    }

    void InstantiateDmgText(float dmg)
    {
        Vector3 rndPos = dmgPos.position + Random.insideUnitSphere * 1f;


        GameObject dmgT = Instantiate(dmgText, rndPos, Quaternion.identity);
        dmgT.GetComponent<DmgText>().dmgText.text = $"-{dmg}";
    }
}

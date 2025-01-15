using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySetting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("敵人設定")]
    public Animator anim;
    public float maxHp;
    public float enemyHp;
    EnemyHealth enemyHealth;
    Vector3 lookVector;
    Vector3 turnPos;
    [Header("敵人AI相關設定")]
    public NavMeshAgent nav;
    public LayerMask targetLayer;
    public float attackRange;
    public float attackDelay;
    public float moveSpeed;
    public float attackDmg;
    bool inAttackRange;
    Transform target;
    float attackTime;


    [Header("敵人UI設定")]
    public GameObject enemyBody;
    public GameObject enemyHpBar;
    public Image hpImg;
    public Transform dmgPos;
    public GameObject dmgText;


    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHp = maxHp;

        target = GameObject.Find("Player").transform;
        nav.speed = moveSpeed;
        nav.stoppingDistance = attackRange;
        nav.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target.GetComponent<PlayerComponet>().playerStatus.isDead)
        {
            //角色、UI寫條面向攝影機
            lookVector = Camera.main.transform.position - transform.position;
            lookVector.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lookVector);
            enemyBody.transform.localRotation = targetRotation;
            enemyHpBar.transform.rotation = targetRotation;

            //敵人角色方向，如果玩家在敵人左邊就朝向左，反之如果再右邊就朝向右
            // turnPos = target.position - transform.position;
            // Debug.Log(turnPos.x);
            // // if (turnPos.x >= 0) //玩家在右側
            // // {
            // //     enemyBody.transform.localScale = new Vector3(-0.01f,
            // //     enemyBody.transform.localScale.y, enemyBody.transform.localScale.z);
            // //     Debug.Log("在右側");
            // // }
            // // else if (turnPos.x < 0) //玩家在左側
            // // {
            // //     enemyBody.transform.localScale = new Vector3(0.01f,
            // //     enemyBody.transform.localScale.y, enemyBody.transform.localScale.z);
            // //     Debug.Log("在左側");
            // // }

            inAttackRange = Physics.CheckSphere(transform.position, attackRange, targetLayer);
            if (!inAttackRange)
            {
                TrackTarget();
                Debug.Log("追擊");
            }
            else
            {
                Attack();

            }
        }
        else
        {
            nav.isStopped = true;
            Debug.Log("玩家死亡，暫停導航");
        }
    }
    void TrackTarget()
    {
        nav.isStopped = false;
        nav.SetDestination(target.position);
        anim.SetBool("Run", true);
        anim.SetBool("Attack", false);
    }
    void Attack()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Attack", true);
        nav.isStopped = true;
        attackTime += Time.deltaTime;
        if (attackTime >= attackDelay)
        {
            attackTime = 0;
            target.GetComponent<PlayerComponet>().playerStatus.TakeDmg(attackDmg);
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

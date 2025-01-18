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
    public bool canTrack;
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
        
        if (canTrack)
        {
            //角色、UI寫條面向攝影機
            lookVector = Camera.main.transform.position - transform.position;
            lookVector.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lookVector);
            transform.rotation = targetRotation;

            // enemyBody.transform.localRotation = targetRotation;
            // enemyHpBar.transform.localRotation = targetRotation;

            FlipBodyX();



            inAttackRange = Physics.CheckSphere(transform.position, attackRange, targetLayer);
            if (!inAttackRange)
            {
                TrackTarget();
            }
            else
            {
                Attack();

            }
        }
        else
        {
            nav.isStopped = true;

            Debug.Log("暫停導航");
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
    void FlipBodyX()
    {
        turnPos = target.position - transform.position;
        Debug.DrawRay(transform.position, turnPos, Color.red);
        Debug.DrawRay(transform.position, transform.forward * 20f, Color.red);
        // 計算敵敵人向前和敵人到玩家的兩個向量角度，如果再左邊，角度為正1~180，在右邊為負-1~-180
        float angle = Vector3.SignedAngle(transform.forward, turnPos, Vector3.up);
        // Debug.Log(angle);
        if (angle > 0) //玩家在左邊
        {
            enemyBody.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }
        else        //玩家在右邊
        {
            enemyBody.transform.localScale = new Vector3(-0.35f, 0.35f, 0.35f);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

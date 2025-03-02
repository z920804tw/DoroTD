using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySetting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("敵人設定")]
    public EnemyInfoSO enemyInfoSO;
    public EnemyAttack enemyAttack;
    public bool canTrack;
    Vector3 lookVector;
    Vector3 turnPos;

    [Header("敵人AI相關設定")]
    public LayerMask targetLayer;
    public float attackRange;
    public float attackDelay;
    public float attackDmg;
    float attackTime;
    public float moveSpeed;
    bool inAttackRange;
    NavMeshAgent nav;
    Transform target;

    [SerializeField] EnemyHealth enemyHealth;


    [Header("敵人UI設定")]
    public GameObject enemyBody;
    public GameObject enemyHpBar;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();

        InitializationInfo();
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
        }
    }

    public bool InAttackRange
    {
        get { return inAttackRange; }
    }

    //敵人追蹤用
    void TrackTarget()
    {
        nav.isStopped = false;
        nav.SetDestination(target.position);
    }
    //敵人攻擊用
    void Attack()
    {
        nav.isStopped = true;
        attackTime += Time.deltaTime;
        if (attackTime >= attackDelay)
        {
            attackTime = 0;
            enemyAttack.AttackAction(this.gameObject, target.gameObject);
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
            // enemyBody.transform.localScale = new Vector3(scale, scale, scale);
            enemyBody.GetComponent<SpriteRenderer>().flipX = false;

        }
        else        //玩家在右邊
        {
            // enemyBody.transform.localScale = new Vector3(-scale, scale, scale);
            enemyBody.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void InitializationInfo()
    {
        attackRange = enemyInfoSO.attackRange;
        attackDelay = enemyInfoSO.attackDelay;
        attackDmg = enemyInfoSO.attackDmg;
        moveSpeed = enemyInfoSO.moveSpeed;
        enemyHealth.maxHp = enemyInfoSO.maxHp;

        nav.speed = moveSpeed;
        nav.stoppingDistance = attackRange;
        nav.updateRotation = false;
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

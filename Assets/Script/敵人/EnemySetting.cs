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
    Vector3 lookVector;
    [Header("敵人AI相關設定")]
    public NavMeshAgent nav;
    public LayerMask targetLayer;
    public float attackRange;
    public float attackDelay;
    public float moveSpeed;
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
        nav.stoppingDistance=attackRange;
        nav.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    { 
        //角色、UI寫條旋轉
        lookVector = Camera.main.transform.position-transform.position;
        lookVector.y=0;
        Quaternion targetRotation = Quaternion.LookRotation(lookVector);
        enemyBody.transform.localRotation=targetRotation;
        enemyHpBar.transform.rotation=targetRotation;

        


        inAttackRange= Physics.CheckSphere(transform.position,attackRange,targetLayer);
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
    void TrackTarget()
    {
        nav.isStopped = false;
        nav.SetDestination(target.position);
    }
    void Attack()
    {
        nav.isStopped = true;
        attackTime += Time.deltaTime;
        if (attackTime >= attackDelay)
        {
            attackTime = 0;
            Debug.Log("攻擊");
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
        Gizmos.DrawWireSphere(transform.position,attackRange);    
    }
}

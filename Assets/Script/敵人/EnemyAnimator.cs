using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("敵人動畫控制")]
    public Animator anim;
    [SerializeField]EnemySetting enemySetting;
    bool inAttackRange;

    void Start()
    {
        enemySetting=GetComponent<EnemySetting>();
    }

    // Update is called once per frame
    void Update()
    {
      
        inAttackRange = enemySetting.InAttackRange();
        EnemyAnim();
    }

    void EnemyAnim()
    {
        if (!inAttackRange)
        {
            anim.SetBool("Run", true);
            anim.SetBool("Attack", false);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Attack", true);
        }
    }
}

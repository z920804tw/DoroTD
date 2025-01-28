using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("敵人動畫控制")]
    public Animator anim;
    EnemySetting enemySetting;

    void Start()
    {
        enemySetting = GetComponent<EnemySetting>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemySetting.canTrack)
        {
            EnemyAnim();
        }
    }

    void EnemyAnim()
    {
        if (!enemySetting.InAttackRange)
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

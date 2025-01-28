using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyMeleeAttack", menuName = "Enemy/MeleeAttack")]
public class MeleeAttack : EnemyAttack
{
    float dmg;
    //進佔攻擊
    public override void AttackAction(GameObject gameObject, GameObject target)
    {
        dmg=gameObject.GetComponent<EnemySetting>().attackDmg;
        PlayerStatus playerStatus = target.GetComponent<PlayerStatus>();
        if (playerStatus != null)
        {
            playerStatus.TakeDmg(dmg);
        }
    }
}

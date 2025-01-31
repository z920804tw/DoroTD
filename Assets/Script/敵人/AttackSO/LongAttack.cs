using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyLongAttack", menuName = "Enemy/LongAttack")]
//遠距離攻擊(只會生成一顆子彈)
public class LongAttack : EnemyAttack
{
    public GameObject bullet;
    public float bulletSpeed;

    [SerializeField]float dmg;
    public override void AttackAction(GameObject gameObject, GameObject target)
    {
        dmg=gameObject.GetComponent<EnemySetting>().attackDmg;

        GameObject bb= Instantiate(bullet,gameObject.transform.position,Quaternion.identity);
        Vector3 dir=target.transform.position-gameObject.transform.position;
        dir.y=0;
        dir= dir.normalized;

        bb.GetComponent<Rigidbody>().AddForce(dir*bulletSpeed*15,ForceMode.Force);
        bb.GetComponent<EnemyBullet>().dmg=dmg;
    }
}

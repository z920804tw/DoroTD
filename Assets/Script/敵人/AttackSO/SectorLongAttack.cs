using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemySectorLongAttack", menuName = "Enemy/SectorLongAttack")]
public class SectorLongAttack : EnemyAttack
{
    public GameObject bullet;
    public float minAngle;
    public float maxAngle;
    public float bulletCount; //會發射幾顆
    public float bulletSpeed;
    float dmg;
    public override void AttackAction(GameObject gameObject, GameObject target)
    {
        dmg = gameObject.GetComponent<EnemySetting>().attackDmg;

        //扇型攻擊
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bb = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
            float offsetX = Mathf.Lerp(minAngle, maxAngle, i / (bulletCount - 1)); //使用Lerp來計算在minAngle和maxAngle之間的角度，角度的值會根據子彈數而改變

            Vector3 dir = target.transform.position - gameObject.transform.position;
            dir = new Vector3(dir.x + offsetX, dir.y, dir.z);
            dir.y = 0;
            dir = dir.normalized;

            bb.GetComponent<Rigidbody>().AddForce(dir * bulletSpeed * 15, ForceMode.Force);
            bb.GetComponent<EnemyBullet>().dmg = dmg;
        }

    }

    private void Update()
    {
        Debug.Log("123");
    }

}

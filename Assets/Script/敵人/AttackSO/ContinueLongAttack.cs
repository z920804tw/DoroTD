using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyContinueLongAttack", menuName = "Enemy/ContinueLongAttack")]
//遠距連續攻擊(會一次生成多顆子彈，每顆子彈會有一些間隔)
public class ContinueLongAttack : EnemyAttack
{
    public GameObject bullet;
    public float bulletSpeed;
    public int bulletCount;
    public float delay;
    float dmg;
    public override void AttackAction(GameObject gameObject, GameObject target)
    {
        dmg = gameObject.GetComponent<EnemySetting>().attackDmg;

        gameObject.GetComponent<MonoBehaviour>().StartCoroutine(spawnDelay(gameObject, target));

    }

    IEnumerator spawnDelay(GameObject gameObject, GameObject target)
    {
        int i = 0;
        while (i < bulletCount)
        {
            GameObject bb = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
            Vector3 dir = target.transform.position - gameObject.transform.position;
            dir=dir.normalized;

            bb.GetComponent<Rigidbody>().AddForce(dir * bulletSpeed * 10, ForceMode.Force);
            bb.GetComponent<EnemyBullet>().dmg = dmg;
            i++;
            yield return new WaitForSeconds(delay);
        }
        Debug.Log("延遲完成");
    }

}

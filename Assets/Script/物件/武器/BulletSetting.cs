using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSetting : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletDmg;
    public float count;
    public bool canPenetrate;
    void Start()
    {
        Destroy(gameObject, 3f);


    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (canPenetrate) //判斷子彈能不能穿牆
            {
                count--; //每擊中一個目標就-1
                damageable.TakeDmg(bulletDmg);
                if (count <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                damageable.TakeDmg(bulletDmg);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("Hit");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    [SerializeField]bool isDead;



    // Start is called before the first frame update

    void Start()
    {
        currentHp = maxHp;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDmg(float dmg)
    {
        currentHp -= dmg;
        if (currentHp <= 0)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy"); //玩家死亡會找場上帶有Enemy標籤的敵人
            foreach (GameObject i in enemys)
            {
                i.GetComponent<EnemySetting>().canTrack = false;
            }
            isDead=true;
            Debug.Log("死亡");
        }
    }

    public bool IsDead
    {
        get { return isDead; }
    }
}

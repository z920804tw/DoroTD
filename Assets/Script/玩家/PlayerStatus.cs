using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    [SerializeField] GameObject dmgPrefab;
    [SerializeField] bool isDead;


    // Start is called before the first frame update

    void Start()
    {
        currentHp = maxHp;
        isDead = false;
        if (GameObject.FindWithTag("SceneUI") != null) GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().hpBar.UpdateHpInfo(currentHp, maxHp);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDmg(float dmg)
    {
        currentHp -= dmg;
        InstantiateDmgText(dmg);
        if (GameObject.FindWithTag("SceneUI") != null) GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().hpBar.UpdateHpInfo(currentHp, maxHp);

        if (currentHp <= 0)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy"); //玩家死亡會找場上帶有Enemy標籤的敵人
            foreach (GameObject i in enemys)
            {
                i.GetComponent<EnemySetting>().canTrack = false;
            }
            isDead = true;
            Debug.Log("死亡");
        }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    void InstantiateDmgText(float dmg)
    {
        Vector3 offset=new Vector3(0,3,0);
        Vector3 rndPos = transform.position+offset + Random.insideUnitSphere * 1f;
        GameObject dmgT = Instantiate(dmgPrefab, rndPos, Quaternion.identity);
        dmgT.GetComponent<DmgText>().dmgText.text = $"-{dmg}";
        dmgT.GetComponent<DmgText>().dmgText.color=new Color32(255,166,0,255);
    }
}

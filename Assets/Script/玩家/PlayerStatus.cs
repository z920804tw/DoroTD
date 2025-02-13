using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    [SerializeField] GameObject dmgPrefab;
    [SerializeField] bool isDead;

    HpBar hpBar;
    // Start is called before the first frame update
    void Awake()
    {
        currentHp = maxHp;
    }
    void Start()
    {
        hpBar = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().hpBar;

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDmg(float dmg)
    {
        currentHp -= dmg;
        InstantiateDmgText(dmg);
        if (hpBar != null) hpBar.UpdateHpInfo();

        if (currentHp <= 0)
        {
            //當玩家死亡後，會停止所有敵人的AI
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject i in enemys)
            {
                i.GetComponent<EnemySetting>().canTrack = false;
            }
            isDead = true;

            //玩家死亡後，會去嘗試找GameManager，如果有就觸發他的GameOver
            if (GameObject.Find("GameManager") != null) GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
            Debug.Log("死亡");
        }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    void InstantiateDmgText(float dmg)
    {
        Vector3 offset = new Vector3(0, 3, 0);
        Vector3 rndPos = transform.position + offset + Random.insideUnitSphere * 1f;
        GameObject dmgT = Instantiate(dmgPrefab, rndPos, Quaternion.identity);
        dmgT.GetComponent<DmgText>().dmgText.text = $"-{dmg}";
        dmgT.GetComponent<DmgText>().dmgText.color = new Color32(255, 166, 0, 255);
    }
}

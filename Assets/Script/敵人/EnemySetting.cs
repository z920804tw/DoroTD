using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySetting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("敵人設定")]
    public float maxHp;
    [SerializeField] float enemyHp;

    [Header("敵人UI設定")]
    public GameObject enemyHpBar;
    public Image hpImg;
    public Transform dmgPos;
    public GameObject dmgText;


    void Start()
    {
        enemyHp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        enemyHpBar.transform.LookAt(Camera.main.transform.position);
    }

    public void TakeDmg(float dmg)
    {
        enemyHp -= dmg;

        InstantiateDmgText(dmg);
        UpdateHpBar();
        if (enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHpBar()
    {
        hpImg.fillAmount = enemyHp / maxHp;
    }

    void InstantiateDmgText(float dmg)
    {
        Vector3 rndPos = dmgPos.position + Random.insideUnitSphere * 1f;


        GameObject dmgT = Instantiate(dmgText, rndPos, Quaternion.identity);
        dmgT.GetComponent<DmgText>().dmgText.text = $"-{dmg}";
    }
}

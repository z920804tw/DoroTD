using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("敵人設定")]
    public float maxHp;
    public float currentHp;
    public GameObject deadSmoke;
    [SerializeField] float money;

    [Header("敵人物件設定")]
    public SpriteRenderer bodyImg;
    public Image hpImg;

    public GameObject dmgText;
    public float transformTime;
    Material bodyMat;
    bool isChange;
    void Start()
    {
        bodyMat = bodyImg.material;
        currentHp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {

    }
    //造成傷害
    public void TakeDmg(float dmg)
    {
        if (currentHp > 0)
        {
            currentHp -= dmg;
            ChangeDmgColor();

            InstantiateDmgText(dmg);
            UpdateHpBar();
            if (currentHp <= 0)
            {
                if(GameObject.FindWithTag("SceneUI")!=null)GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().playerMoney.AddMoney(money);

                GameObject smoke = Instantiate(deadSmoke, transform.position, Quaternion.identity);
                Destroy(smoke, 1.2f);

                Destroy(gameObject);
            }
        }
    }
    //更新Hp
    void UpdateHpBar()
    {
        hpImg.fillAmount = currentHp / maxHp;
    }
    //生成傷害文字
    void InstantiateDmgText(float dmg)
    {
        Vector3 offset = new Vector3(0, 2, 0);
        Vector3 rndPos = transform.position + offset + Random.insideUnitSphere * 1f;
        GameObject dmgT = Instantiate(dmgText, rndPos, Quaternion.identity);
        dmgT.GetComponent<DmgText>().dmgText.text = $"-{dmg}";
    }

    //受傷時會有短暫的受傷顏色
    public void ChangeDmgColor()
    {
        if (!isChange)
        {

            StartCoroutine(changeColor(Color.white, new Color32(255, 110, 110, 255)));
        }

    }
    IEnumerator changeColor(Color star, Color end)
    {
        float timer = 0;
        isChange = true;
        while (timer < transformTime)
        {
            bodyMat.color = Color.Lerp(star, end, timer / transformTime);
            timer += Time.deltaTime;
            yield return null;
        }
        bodyMat.color = Color.white;
        isChange = false;
    }

}

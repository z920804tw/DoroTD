using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image hpBarImg;
    [SerializeField] TMP_Text hpText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHpInfo(float hp,float maxHp)
    {
        hpBarImg.fillAmount=hp/maxHp;
        hpText.text=$"Hp:{hp}/{maxHp}";
    }
}

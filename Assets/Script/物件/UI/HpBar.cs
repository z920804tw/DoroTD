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
    PlayerStatus playerStatus;
    void Start()
    {
        playerStatus = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        UpdateHpInfo();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHpInfo()
    {

        hpBarImg.fillAmount = playerStatus.currentHp / playerStatus.maxHp;
        hpText.text = $"Hp:{playerStatus.currentHp}/{playerStatus.maxHp}";
    }
}

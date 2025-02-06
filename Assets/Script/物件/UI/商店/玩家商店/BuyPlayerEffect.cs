using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyPlayerEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("物件設定")]
    public PlayerStoreEffect playerStoreEffect;
    public Image effectImage;
    public TMP_Text effectLvText;
    public TMP_Text effectInfoText;
    public TMP_Text effectPriceText;
    [Header("參數設定")]
    public float upgradeLV;
    public float upgradeValue;
    public float upgradePrice;
    [SerializeField] float increasePrice;
    float currentValue;
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        InitializationInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyEffect()
    {
        PlayerMoney playerMoney = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().playerMoney;
        if (playerMoney.money >= upgradePrice)
        {
            //購買效果
            playerStoreEffect.BuyEffect(player, upgradeValue);

            //扣錢並增加數值
            playerMoney.AddMoney(-upgradePrice);
            upgradeLV++;
            upgradePrice += increasePrice;
            currentValue += upgradeValue;

            //更新UI
            effectLvText.text = $"{playerStoreEffect.titleName}:{upgradeLV}->{upgradeLV + 1}";
            effectInfoText.text = $"{playerStoreEffect.infoName}:{currentValue}->{currentValue + upgradeValue}";
            effectPriceText.text = $"價格:{upgradePrice}";
        }
    }
    void InitializationInfo()
    {
        currentValue = playerStoreEffect.GetTargetValue(player);
        effectImage.sprite = playerStoreEffect.effectImage;
        effectLvText.text = $"{playerStoreEffect.titleName}:{upgradeLV}->{upgradeLV + 1}";
        effectInfoText.text = $"{playerStoreEffect.infoName}:{currentValue}->{currentValue + upgradeValue}";
        effectPriceText.text = $"價格:{upgradePrice}";

    }
}

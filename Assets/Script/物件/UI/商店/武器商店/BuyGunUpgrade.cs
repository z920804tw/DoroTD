using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyGunUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("物件設定")]
    public GunInfoSO gunInfoSO;
    public WeaponStoreSO weaponStoreSO;
    [SerializeField] Image upgradeImg;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text contentText;
    [SerializeField] TMP_Text gunUpgradePrice;
    [Header("參數設定")]
    public float upgradeLv;
    public int upgradeValue;
    public float upgradePrice;
    [SerializeField] float increasePrice;
    int currentValue;
    void Start()
    {
        InitializationInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BuyUpgrade()
    {
        PlayerMoney playerMoney = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().playerMoney;
        GameObject weapon = FindGun();
        if (weapon != null && playerMoney.money >= upgradePrice)
        {
            //增加傷害
            weaponStoreSO.BuyUpgrade(weapon,upgradeValue);
            currentValue +=upgradeValue;

            //扣錢和加等級、價格
            playerMoney.DeductMoey(upgradePrice);
            upgradeLv++;
            upgradePrice += increasePrice;

            //更新UI內容
            titleText.text = $"{weaponStoreSO.titleName}:{upgradeLv}->{upgradeLv + 1}";
            contentText.text = $"{weaponStoreSO.infoName}:{currentValue}->{currentValue + upgradeValue}";
            gunUpgradePrice.text = $"價錢:{upgradePrice}";
        }

    }
    GameObject FindGun()
    {
        List<GameObject> weapons = GameObject.FindWithTag("Player").GetComponent<PlayerWeapons>().weapons;
        foreach (GameObject i in weapons)
        {
            if (i.GetComponent<GunSetting>().weaponType == gunInfoSO.weaponType)
            {
                return i;
            }
        }
        return null;
    }
    void InitializationInfo()
    {
        currentValue = weaponStoreSO.GetTargetValue(gunInfoSO);
        upgradeImg.sprite = gunInfoSO.gunImg;
        titleText.text = $"{weaponStoreSO.titleName}:{upgradeLv}->{upgradeLv + 1}";
        contentText.text = $"{weaponStoreSO.infoName}:{currentValue}->{currentValue + upgradeValue}";
        gunUpgradePrice.text = $"價錢:{upgradePrice}";
    }
}

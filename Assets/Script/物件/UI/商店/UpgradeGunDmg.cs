using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeGunDmg : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("物件設定")]
    public GunInfoSO gunInfoSO;
    [SerializeField] Image gunImg;
    [SerializeField] TMP_Text gunName;
    [SerializeField] TMP_Text gunDmgText;
    [SerializeField] TMP_Text gunUpgradePrice;
    [Header("參數設定")]
    public float upgradeLv;
    public float upgradeDmg;
    public float upgradePrice;

    float currentDmg;

    void Start()
    {
        InitializationGunInfo();
    }

    // Update is called once per frame


    public void BuyUpgrade()
    {
        PlayerMoney playerMoney = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().playerMoney;
        GameObject weapon = FindGun();
        if (weapon != null && playerMoney.money >= upgradePrice)
        {
            //增加傷害
            weapon.GetComponent<GunSetting>().weaponDmg += upgradeDmg;
            currentDmg = weapon.GetComponent<GunSetting>().weaponDmg;

            //扣錢和加等級、價格
            playerMoney.AddMoney(-upgradePrice);
            upgradeLv++;
            upgradePrice += 100;

            //更新UI內容
            gunName.text = $"傷害升級:{upgradeLv}->{upgradeLv + 1}";
            gunDmgText.text = $"傷害:{currentDmg}->{currentDmg + upgradeDmg}";
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
    void InitializationGunInfo()
    {
        currentDmg = gunInfoSO.weaponDmg;
        gunImg.sprite = gunInfoSO.gunImg;
        gunName.text = $"傷害升級:{upgradeLv}->{upgradeLv + 1}";
        gunDmgText.text = $"傷害:{currentDmg}->{currentDmg + upgradeDmg}";
        gunUpgradePrice.text = $"價錢:{upgradePrice}";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeGunAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("物件設定")]
    public GunInfoSO gunInfoSO;
    [SerializeField] Image gunImg;
    [SerializeField] TMP_Text gunName;
    [SerializeField] TMP_Text gunAmmoText;
    [SerializeField] TMP_Text gunUpgradePrice;
    [Header("參數設定")]
    public float upgradeLv;
    public int upgradeAmmo;
    public float upgradePrice;

    int currentAmmo;
    void Start()
    {
        InitializationGunInfo();
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
            //增加子彈
            weapon.GetComponent<GunSetting>().maxAmmo += upgradeAmmo;
            currentAmmo = weapon.GetComponent<GunSetting>().maxAmmo;

            //扣錢和加等級、價格
            playerMoney.AddMoney(-upgradePrice);
            upgradeLv++;
            upgradePrice += 150;

            //更新UI內容
            gunName.text = $"子彈數升級:{upgradeLv}->{upgradeLv + 1}";
            gunAmmoText.text = $"子彈數:{currentAmmo}->{currentAmmo + upgradeAmmo}";
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
        currentAmmo = gunInfoSO.maxAmmo;
        gunImg.sprite = gunInfoSO.gunImg;
        gunName.text = $"子彈數升級:{upgradeLv}->{upgradeLv + 1}";
        gunAmmoText.text = $"子彈數:{currentAmmo}->{currentAmmo + upgradeAmmo}";
        gunUpgradePrice.text = $"價錢:{upgradePrice}";
    }
}

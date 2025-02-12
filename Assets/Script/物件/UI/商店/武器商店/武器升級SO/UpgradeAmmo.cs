using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BuyGunMaxAmmo", menuName = "Store/Weapon/BuyGunMaxAmmo")]
public class UpgradeAmmo : WeaponStoreSO
{
    public override void BuyUpgrade(GameObject target, int value)
    {
        target.GetComponent<GunSetting>().maxAmmo += value;
        target.GetComponent<GunSetting>().currentAmmo=target.GetComponent<GunSetting>().maxAmmo;
        
    }
    public override int GetTargetValue(GunInfoSO target)
    {
        return target.maxAmmo;
    }
}

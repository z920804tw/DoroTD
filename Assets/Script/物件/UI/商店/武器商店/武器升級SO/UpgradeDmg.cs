using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BuyGunDmg", menuName = "Store/Weapon/BuyGunDmg")]
public class UpgradeDmg : WeaponStoreSO
{
    public override void BuyUpgrade(GameObject target, int value)
    {
        target.GetComponent<GunSetting>().weaponDmg += value;
    }

    //在槍枝中只會用在初始化時候
    public override int GetTargetValue(GunInfoSO target)
    {
        return target.weaponDmg;
    }

}

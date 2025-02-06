using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponStoreSO : ScriptableObject
{
    public string titleName;
    public string infoName;
    

    public abstract void BuyUpgrade(GameObject target,int value);

    public abstract int GetTargetValue(GunInfoSO target);

}

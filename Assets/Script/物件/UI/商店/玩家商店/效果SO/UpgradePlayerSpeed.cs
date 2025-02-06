using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuyPlayerMoveSpeed", menuName = "Store/Player/BuyPlayerMoveSpeed")]
public class UpgradePlayerSpeed : PlayerStoreEffect
{
    public override void BuyEffect(GameObject target, float value)
    {
        //升級效果
        target.GetComponent<PlayerController>().MoveSpeed += value;
        target.GetComponent<PlayerController>().PreSpeed += value;
    }

    public override float GetTargetValue(GameObject target)
    {
        return target.GetComponent<PlayerController>().MoveSpeed;
    }
}

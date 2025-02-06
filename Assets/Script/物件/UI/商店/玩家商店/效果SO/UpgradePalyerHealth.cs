using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BuyPlayerHealth", menuName = "Store/Player/BuyPlayerHealth")]
public class UpgradePalyerHealth : PlayerStoreEffect
{
    public override void BuyEffect(GameObject target, float value)
    {
        //升級效果
        target.GetComponent<PlayerStatus>().maxHp += value;
        target.GetComponent<PlayerStatus>().currentHp =target.GetComponent<PlayerStatus>().maxHp;

        //更新玩家血量UI狀態
        GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().hpBar.UpdateHpInfo(target.GetComponent<PlayerStatus>().currentHp,target.GetComponent<PlayerStatus>().maxHp);
    }

    public override float GetTargetValue(GameObject target)
    {
        return target.GetComponent<PlayerStatus>().maxHp;
    }

}

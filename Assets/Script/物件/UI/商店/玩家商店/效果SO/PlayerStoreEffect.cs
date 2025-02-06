using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerStoreEffect : ScriptableObject
{
    public Sprite effectImage;
    public string titleName;
    public string infoName;

    public abstract void BuyEffect(GameObject target,float value);

    public abstract float GetTargetValue(GameObject target);
}

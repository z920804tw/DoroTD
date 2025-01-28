using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : ScriptableObject
{
    public abstract void AttackAction(GameObject gameObject,GameObject target);

}

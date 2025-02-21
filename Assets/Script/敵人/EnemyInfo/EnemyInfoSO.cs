using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Enemy/EnemyInfo")]
public class EnemyInfoSO : ScriptableObject
{
    [Header("敵人基本資訊")]
    public string enemyName;
    public Sprite enemyImg;

    [Header("敵人數值設定")]
    public float attackRange;
    public float attackDelay;
    public float attackDmg;
    public float moveSpeed;

    public float maxHp;


}

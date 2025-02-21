using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyInfoSO enemyInfoSO;
    public Image enemyImage;
    public TMP_Text enemyName;
    public TMP_Text enemyattackRange;
    public TMP_Text attackDelay;
    public TMP_Text attackDmg;
    public TMP_Text moveSpeed;
    public TMP_Text maxHp;
    void Start()
    {
        if (enemyInfoSO != null)
        {
            InitializationInfo();
        }
    }

    void InitializationInfo()
    {
        enemyImage.sprite = enemyInfoSO.enemyImg;
        enemyName.text = $"名稱:{enemyInfoSO.enemyName}";
        enemyattackRange.text = $"攻擊範圍:{enemyInfoSO.attackRange}";
        attackDelay.text = $"攻擊速度:{enemyInfoSO.attackDelay}";
        attackDmg.text = $"攻擊傷害:{enemyInfoSO.attackDmg}";
        moveSpeed.text = $"移動速度:{enemyInfoSO.moveSpeed}";
        maxHp.text = $"基礎血量{enemyInfoSO.maxHp}";

    }
}

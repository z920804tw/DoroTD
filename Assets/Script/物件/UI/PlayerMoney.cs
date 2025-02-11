using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text moneyText;
    public float money;

    float totalMoney;
    void Start()
    {
        UpdateMoneyText();
    }


    public void AddMoney(float money1)
    {
        money += money1;
        totalMoney += money1;
        UpdateMoneyText();
    }
    public void DeductMoey(float money1)
    {
        money -= money1;
        UpdateMoneyText();
    }
    void UpdateMoneyText()
    {
        moneyText.text = $"Coin:{money}";
    }

    public float TotalMoney
    {
        get{return totalMoney;}
    }
}

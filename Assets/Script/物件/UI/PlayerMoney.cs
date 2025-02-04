using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text moneyText;
    public float money;
    void Start()
    {
        UpdateMoneyText();  
    }


    public void AddMoney(float money1)
    {
        money += money1;
        UpdateMoneyText();
    }
    void UpdateMoneyText()
    {
        moneyText.text = $"Coin:{money}";
    }
}

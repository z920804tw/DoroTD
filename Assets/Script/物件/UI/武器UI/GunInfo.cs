using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GunInfoSO gunInfoSO;
    [SerializeField] Image gunImg;
    [SerializeField] TMP_Text gunName;
    [SerializeField] TMP_Text gunDmgText;
    [SerializeField] TMP_Text gunMaxBullet;
    [SerializeField] TMP_Text gunPrice;
    void Start()
    {
        InitializationGunInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializationGunInfo()
    {
        if (gunInfoSO != null)
        {
            gunImg.sprite=gunInfoSO.gunImg;
            gunName.text=gunInfoSO.gunName;
            gunDmgText.text=$"基本傷害:{gunInfoSO.weaponDmg}";
            gunMaxBullet.text=$"子彈數:{gunInfoSO.maxAmmo}";
            gunPrice.text=$"價格:{gunInfoSO.price}";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weaponsUI;
    GameObject currentWeapon;
    public GunObjectUI gObj;
    PlayerWeapons playerWeapons;
    void Start()
    {
        playerWeapons = GameObject.Find("Player").GetComponent<PlayerComponet>().playerWeapons;
        SelectWeapon(playerWeapons.currnetIndex);
    }


    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != null && gObj != null)
        {
            gObj.ammoText.text = $"AMMO:\n{currentWeapon.GetComponent<GunSetting>().currentAmmo}/{currentWeapon.GetComponent<GunSetting>().maxAmmo}";
        }
    }
    public void SelectWeapon(int index) //當玩家武器那邊去切換時會被呼叫。
    {

        foreach (GameObject i in weaponsUI)   //先設定全部的背景便灰色(代表沒被啟用)
        {
            RectTransform r = i.GetComponent<RectTransform>(); //取的該UI物件的座標組件
            r.anchoredPosition=new Vector2(r.anchoredPosition.x,-250);
            Debug.Log("切換位置");

            GunObjectUI j = i.GetComponent<GunObjectUI>(); //取的該UI物件身上的GunObjectUI.cs
            j.backgroundImg.color = new Color32(190, 190, 190, 255); //設定他的背景圖片的顏色
        }

        gObj = weaponsUI[index].GetComponent<GunObjectUI>(); //取得當前武器UI上的GunObjectUI組件，並給gObj
        gObj.backgroundImg.color = Color.white;  //之後設定被啟用的那個顏色為白色(代表啟用)

        RectTransform r1 = weaponsUI[index].GetComponent<RectTransform>();
        r1.anchoredPosition=new Vector2(r1.anchoredPosition.x,-100);


        currentWeapon = playerWeapons.weapons[index];                //設定當前的武器為玩家手上拿的武器
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelectUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject listParent;
    public List<GameObject> weaponUIList;
    public GunObjectUI gObj;
    GameObject currentWeapon;
    PlayerWeapons playerWeapons;

    void Start()
    {
        playerWeapons = GameObject.FindWithTag("Player").GetComponent<PlayerComponet>().playerWeapons;
        SelectWeaponUI(playerWeapons.currnetIndex);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != null && gObj != null)
        {
            gObj.ammoText.text =
            $"AMMO:\n{currentWeapon.GetComponent<GunSetting>().currentAmmo}/{currentWeapon.GetComponent<GunSetting>().maxAmmo}";

        }


    }

    public void SelectWeaponUI(int index)
    {
        //先將當前的weaponUIList所有物件都設定沒有持有狀態(灰色且位置向下)
        foreach (GameObject i in weaponUIList)
        {
            GunObjectUI g = i.GetComponent<GunObjectUI>();
            if (g != null)
            {
                //設定位置
                g.backgroundImg.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -60);
                g.backgroundImg.color=new Color32(180,180,180,255);
            }

            //設定目前的武器狀態啟用
            gObj = weaponUIList[index].GetComponent<GunObjectUI>();
            gObj.backgroundImg.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 65);
            gObj.backgroundImg.color=Color.white;

            currentWeapon = playerWeapons.weapons[index];
        }
    }
}

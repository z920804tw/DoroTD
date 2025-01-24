using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelectUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject listParent;
    public List<GameObject> weaponUIList;
    public GunObjectUI currentgObj;
    [SerializeField] GameObject currentWeapon;
    PlayerWeapons playerWeapons;
    GunSetting gS;


    void Start()
    {
        playerWeapons = GameObject.FindWithTag("Player").GetComponent<PlayerComponet>().playerWeapons;
        SelectWeaponUI(playerWeapons.currnetIndex);

    }

    // Update is called once per frame
    void Update()
    {
        //更新當前的武器UI內容
        if (currentWeapon != null)
        {
            currentgObj.ammoText.text = $"Ammo:\n{gS.currentAmmo}/{gS.maxAmmo}";
        }
    }

    //切換武器UI
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
                g.backgroundImg.color = new Color32(180, 180, 180, 255);

            }
        }
        //設定當前啟用的武器UI
        currentgObj = weaponUIList[index].GetComponent<GunObjectUI>();
        currentgObj.backgroundImg.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 65);
        currentgObj.backgroundImg.color = Color.white;
        

        //玩家實際拿的武器
        currentWeapon = playerWeapons.weapons[index];
        gS = currentWeapon.GetComponent<GunSetting>();
    }
}

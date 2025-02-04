using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{    // 0=手槍 1=步槍 2= 霰彈槍
    public List<GameObject> allGunUIList;
    public List<GameObject> allGunList;
    SceneUIManager sceneUIManager;
    GunSelectUI gunSelectUI;
    PlayerMoney playerMoney;
    void Start()
    {
        if (GameObject.FindWithTag("SceneUI") != null)
        {
            sceneUIManager = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>();
            gunSelectUI = sceneUIManager.gunSelectUI;
            playerMoney = sceneUIManager.playerMoney;
        }
    }

    public void BuyPistol()
    {
        Button btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        btn.GetComponent<Button>().interactable = false;
        BuyGun(0);
    }

    public void BuyRifle()
    {
        Button btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        btn.GetComponent<Button>().interactable = false;
        BuyGun(1);
    }
    public void BuyShotGun()
    {
        Button btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        btn.GetComponent<Button>().interactable = false;
        BuyGun(2);
    }
    void BuyGun(int i)
    {

        if (playerMoney.money > allGunList[i].GetComponent<GunSetting>().gunInfoSO.price)
        {
            //UI部分 新增槍枝UI的位置跟list
            GameObject gunUIObj = Instantiate(allGunUIList[i]);
            gunUIObj.transform.SetParent(gunSelectUI.listParent.transform);
            gunSelectUI.weaponUIList.Add(gunUIObj);

            //物件部分 新增可以使用的槍枝進去
            GameObject.FindWithTag("Player").GetComponent<PlayerWeapons>().weapons.Add(allGunList[i]);

            playerMoney.AddMoney(-allGunList[i].GetComponent<GunSetting>().gunInfoSO.price);
        }
    }
}

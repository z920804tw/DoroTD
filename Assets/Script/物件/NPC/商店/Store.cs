using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] allGunUIList;
    public GameObject[] allGunList;
    GunSelectUI gunSelectUI;

    void Start()
    {
        gunSelectUI = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().gunSelectUI;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void BuyGun(int i)
    {
        // 0=步槍 1=霰彈槍
        Button btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        btn.interactable = false;

        //UI部分 新增槍枝UI的位置跟list
        GameObject gunUIObj = Instantiate(allGunUIList[i]);
        gunUIObj.transform.SetParent(gunSelectUI.listParent.transform);
        gunSelectUI.weaponUIList.Add(gunUIObj);

        //物件部分 新增可以使用的槍枝進去
        GameObject.FindWithTag("Player").GetComponent<PlayerComponet>().playerWeapons.weapons.Add(allGunList[i]);
    }


}

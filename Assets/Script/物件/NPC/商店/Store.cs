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
        btn.interactable=false;

        //UI部分
        GameObject gunUIObj = Instantiate(allGunUIList[i]);
        GunSelectUI gui = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().gunSelectUI;
        gunUIObj.transform.SetParent(gui.listParent.transform);
        gui.weaponUIList.Add(gunUIObj);

        //物件部分
        GameObject.FindWithTag("Player").GetComponent<PlayerComponet>().playerWeapons.weapons.Add(allGunList[i]);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyGun : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("物件設定")]
    public GunInfoSO gunInfoSO;
    [SerializeField] Image gunImg;
    [SerializeField] TMP_Text gunName;
    [SerializeField] TMP_Text gunDmgText;
    [SerializeField] TMP_Text gunMaxBullet;
    [SerializeField] TMP_Text gunPrice;
    public GameObject gunUI;
    public GameObject gunObj;
    void Start()
    {
        InitializationGunInfo();
    }
    void InitializationGunInfo()
    {
        if (gunInfoSO != null)
        {
            gunImg.sprite = gunInfoSO.gunImg;
            gunName.text = gunInfoSO.gunName;
            gunDmgText.text = $"基本傷害:{gunInfoSO.weaponDmg}";
            gunMaxBullet.text = $"子彈數:{gunInfoSO.maxAmmo}";
            gunPrice.text = $"價格:{gunInfoSO.price}";
        }
    }


    public void BuyThisGun()
    {
        PlayerMoney playerMoney = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().playerMoney;
        GunSelectUI gunSelectUI = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().gunSelectUI;
        PlayerWeapons playerWeapons = GameObject.FindWithTag("Player").GetComponent<PlayerWeapons>();
        if (playerMoney.money >= gunInfoSO.price)
        {
            //產生槍枝UI物件
            GameObject gunUi = Instantiate(gunUI, transform.position, Quaternion.identity);
            gunUi.transform.SetParent(gunSelectUI.listParent.transform);
            gunSelectUI.weaponUIList.Add(gunUi);

            //產生實體槍枝到玩家手上
            GameObject gunOBJ = Instantiate(gunObj, transform.position, Quaternion.identity);
            gunOBJ.transform.SetParent(playerWeapons.parent);
            gunOBJ.transform.localPosition = Vector3.zero;
            gunOBJ.transform.localEulerAngles = Vector3.zero;
            gunOBJ.SetActive(false);
            playerWeapons.weapons.Add(gunOBJ);

            playerMoney.AddMoney(-gunInfoSO.price);

            Button btn=EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            btn.interactable=false;
            btn.GetComponentInChildren<TMP_Text>().text="SOLD OUT!";
        }
        else
        {
            Debug.Log("購買失敗");
        }

    }
}

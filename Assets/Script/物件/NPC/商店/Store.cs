using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ShotGun;
    public GameObject Rifle;
    public GameObject ShotGunUI;
    public GameObject RifleUI;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyShotGun()
    {
        GameObject.Find("Player").GetComponent<PlayerComponet>().playerWeapons.weapons.Add(ShotGun);
        GameObject.Find("GunList").GetComponent<GunSwitch>().weaponsUI.Add(ShotGunUI);
        int i = GameObject.Find("GunList").GetComponent<GunSwitch>().weaponsUI.Count - 1;
        GameObject.Find("GunList").GetComponent<GunSwitch>().weaponsUI[i].SetActive(true);

    }
    public void BuyRifle()
    {
        GameObject.Find("Player").GetComponent<PlayerComponet>().playerWeapons.weapons.Add(Rifle);
        GameObject.Find("GunList").GetComponent<GunSwitch>().weaponsUI.Add(RifleUI);
        int i = GameObject.Find("GunList").GetComponent<GunSwitch>().weaponsUI.Count - 1;
        GameObject.Find("GunList").GetComponent<GunSwitch>().weaponsUI[i].SetActive(true);

    }
}

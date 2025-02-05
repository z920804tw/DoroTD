using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStoreUI : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weaponStorePage;
    void Start()
    {
        weaponStorePage[0].SetActive(true);
    }

    // Update is called once per frame

    public void OpenPage(int i)
    {
        CloseAllPage();
        weaponStorePage[i].SetActive(true);
    }
    void CloseAllPage()
    {
        foreach (GameObject i in weaponStorePage)
        {
            i.SetActive(false);
        }
    }
}

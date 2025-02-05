using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponStore : MonoBehaviour
{

    [SerializeField] GameObject storeUI;
    [SerializeField] GameObject hintUI;
    [SerializeField] bool canBuy;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canBuy)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                storeUI.SetActive(true);
                Debug.Log("打開商店UI");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hintUI.SetActive(true);
            hintUI.GetComponentInChildren<TMP_Text>().text = $"按下F鍵開啟商店";
            canBuy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        hintUI.SetActive(false);
        hintUI.GetComponentInChildren<TMP_Text>().text = string.Empty;
        canBuy = false;
    }

}

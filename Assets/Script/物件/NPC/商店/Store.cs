using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    [SerializeField] GameObject storeUI;
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
            canBuy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canBuy = false;
    }

}

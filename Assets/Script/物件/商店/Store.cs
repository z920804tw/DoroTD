using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
    // Start is called before the first frame update
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
                canBuy = false;
                hintUI.SetActive(false);
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

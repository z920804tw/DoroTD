using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject hintUI;
    [SerializeField] bool canStart;


    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().StartRound();
                canStart = false;
                hintUI.GetComponentInChildren<TMP_Text>().text = string.Empty;
                transform.parent.gameObject.SetActive(false);

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hintUI.GetComponentInChildren<TMP_Text>().text = $"按下F鍵開始遊戲";
            canStart = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        hintUI.GetComponentInChildren<TMP_Text>().text = string.Empty;
        canStart = false;
    }
}

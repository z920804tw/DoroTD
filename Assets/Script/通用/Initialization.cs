using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (PlayerPrefs.HasKey("initialization") == false)
        {
            PlayerPrefs.SetInt("initialization", 1);

            //音效初始化
            PlayerPrefs.SetFloat("MainVolume", 1);
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            PlayerPrefs.SetFloat("GunVolume", 1);

            Debug.Log("初始化完成");
        }
    }
}
